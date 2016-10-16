using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace Serialization
{
    //бінарна та SOAP серіалізація/десеріалізація, та обмеження при них
    class Program
    {
        static void Main(string[] args)
        {
            //клас A, обмеження при серіалізації/десеріалізації прописуємо власноруч
            A a1 = new A(5,3);
            A a2, a3;
            BinaryFormatter binFor = new BinaryFormatter();
            SoapFormatter soapFor = new SoapFormatter();

            Console.WriteLine("a1={0}", a1);
            using(FileStream file=new FileStream("A.dat", FileMode.Create, FileAccess.Write))
            {
                binFor.Serialize(file, a1);
            }
            using (FileStream file = new FileStream("A.soap", FileMode.Create, FileAccess.Write))
            {
                soapFor.Serialize(file, a1);
            }

            using (FileStream file = new FileStream("A.dat", FileMode.Open, FileAccess.Read))
            {
                a2 = (A)binFor.Deserialize(file);
            }
            using (FileStream file = new FileStream("A.soap", FileMode.Open, FileAccess.Read))
            {
                a3 = (A)soapFor.Deserialize(file);
            }
            Console.WriteLine("a2={0}", a2);
            Console.WriteLine("a3={0}", a3);
            Console.WriteLine();

            //клас B, обмеження при серіалізації/десеріалізації прописуємо атрибутом
            B b1 = new B(2, 7);
            B b2, b3;
           
            Console.WriteLine("b1={0}", b1);
            using (FileStream file = new FileStream("B.dat", FileMode.Create, FileAccess.Write))
            {
                binFor.Serialize(file, b1);
            }
            using (FileStream file = new FileStream("B.soap", FileMode.Create, FileAccess.Write))
            {
                soapFor.Serialize(file, b1);
            }

            using (FileStream file = new FileStream("B.dat", FileMode.Open, FileAccess.Read))
            {
                b2 = (B)binFor.Deserialize(file);
            }
            using (FileStream file = new FileStream("B.soap", FileMode.Open, FileAccess.Read))
            {
                b3 = (B)soapFor.Deserialize(file);
            }
            Console.WriteLine("b2={0}", b2);
            Console.WriteLine("b3={0}", b3);
        }
    }

    //клас в якому обмеження при серіалізації/десеріалізації проводимо за допомогою інтерфейса ISerializable
    [Serializable]
    class A : ISerializable
    {
        private int x;
        private int y;

        public A(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //конструктор десеріалізації
        protected A(SerializationInfo info, StreamingContext context)
        {
            Console.WriteLine("Deserialization");
            //ім'я змінної вказувати, можна будь-яке головне, щоб імена збігалися при серіалізації/десеріалізації
            x=info.GetInt32("value_x");
        }

        //реалізація метода інтерфейса ISerializable
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Console.WriteLine("Serialization");
            //ім'я змінної вказувати, можна будь-яке головне, щоб імена збігалися при серіалізації/десеріалізації
            info.AddValue("value_x", x);
        }

        [OnSerializing]
        private void StartSerialization(StreamingContext con)
        {
            Console.WriteLine("Start Serialization");
        }

        [OnSerialized]
        private void EndSerialization(StreamingContext con)
        {
            Console.WriteLine("End Serialization");
        }

        [OnDeserializing]
        private void StartDeserialization(StreamingContext con)
        {
            Console.WriteLine("Start Deserialization");
        }

        [OnDeserialized]
        private void EndDeserialization(StreamingContext con)
        {
            Console.WriteLine("End Deserialization");
        }

        public override string ToString()
        {
            return String.Format("X={0}, Y={1}", x, y);
        }
    }

    //клас в якому обмеження при серіалізації/десеріалізації проводимо за допомогою атрибутів
    [Serializable]
    class B
    {
        private int x;
        [NonSerialized]
        private int y;

        public B(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        [OnSerializing]
        private void StartSerialization(StreamingContext con)
        {
            Console.WriteLine("Start Serialization");
        }

        [OnSerialized]
        private void EndSerialization(StreamingContext con)
        {
            Console.WriteLine("End Serialization");
        }

        [OnDeserializing]
        private void StartDeserialization(StreamingContext con)
        {
            Console.WriteLine("Start Deserialization");
        }

        [OnDeserialized]
        private void EndDeserialization(StreamingContext con)
        {
            Console.WriteLine("End Deserialization");
        }

        public override string ToString()
        {
            return String.Format("X={0}, Y={1}", x, y);
        }
    }
}