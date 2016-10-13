using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ClassWork2016_10_13
{
    class Program
    {
        //серіалізація
        static void Main(string[] args)
        {
            const string PATH_2D = "Point2D.dat";
            const string PATH = "Point.dat";

            Console.WriteLine("***SAVE TO FILE POINT2D***");
            Point2D p1 = new Point2D(5, 3);
            BinaryWriter bw = new BinaryWriter(new FileStream(PATH_2D, FileMode.Create, FileAccess.Write, FileShare.None));
            p1.WritePoint(bw);

            Console.WriteLine("***LOAD FROM FILE POINT2D***");
            Point2D p2 = new Point2D();
            p2.ReadPoint(PATH_2D);
            Console.WriteLine("POINT2D={0}", p2);

            Console.WriteLine("***SAVE TO FILE POINT***");
            Point p3 = new Point(91);

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = new FileStream(PATH, FileMode.Create))
            {
                bf.Serialize(file, p3);
            }

            Console.WriteLine("***LOAD FROM FILE POINT***");
            Point p4;
            p4 = (Point)bf.Deserialize(new FileStream(PATH, FileMode.Open, FileAccess.Read));
            Console.WriteLine("POINT={0}",p4);
        }    
    }

    //серіалізація методами .NET
    //чи дозволяється серіалізація класа
    [Serializable]
    class Point:ISerializable
    {
        //[NonSerialized] //не серіалізує поле
        private int x;

        public Point(int x)
        {
            this.x=x;
        }

        public override string ToString()
        {
            return String.Format("X={0}", x);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //ім'я можна писати будь-яке, не обов'язково називати так, як називається змінна
            Console.WriteLine("SERIALIZATION");
            info.AddValue("x", this.x);
        }

        //метод викликається перед серіалізацією
        [OnSerializing]
        void A(StreamingContext context)
        {
            Console.WriteLine("START SERIALIZATION");
        }

        //метод викликається після серіалізації
        [OnSerialized]
        void B(StreamingContext context)
        {
            Console.WriteLine("SERIALIZATION END");
        }

        //метод викликається перед десеріалізацією
        [OnDeserializing]
        void С(StreamingContext context)
        {
            Console.WriteLine("START DESERIALIZATION");
        }

        //метод викликається після десеріалізації
        [OnDeserialized]
        void D(StreamingContext context)
        {
            Console.WriteLine("DESERIALIZATION END");
        }

        protected Point(SerializationInfo info, StreamingContext context)
        {
            //ім'я можна писати будь-яке, не обов'язково називати так, як називається змінна
            x = info.GetInt32("x");
            Console.WriteLine("DESERIALIZATION");
        }
    }

    //серіалізація власноруч
    class Point2D
    {
        private int x;
        private int y;

        public Point2D()
        {
        }

        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return String.Format("X={0}, Y={1}", x, y);
        }

        public void WritePoint(BinaryWriter bw)
        {
            bw.Write(x);
            bw.Write(y);
            bw.Close();
        }

        public void ReadPoint(string path)
        {
            BinaryReader br = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None));
            x=br.ReadInt32();
            y=br.ReadInt32();
            br.Close();
        }
    }
}