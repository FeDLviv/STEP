using System;
using System.Reflection;

namespace fed
{
    class Program
    {
        //тестування атрибутів
        static void Main(string[] args)
        {
            SimpleClass obj = new SimpleClass();
            Type type = obj.GetType();
            //Type type = typeof(SimpleClass);

            //обхід всіх атрибутів класу SimpleClass, не враховуються атрибути батьківського класа (false)
            Console.WriteLine("Class attributes:");
            foreach (object x in type.GetCustomAttributes(false))
            {
                Console.Write(x);
                if (x is VersionAttribute)
                {
                    Console.WriteLine(" Version={0}", ((VersionAttribute)x).Current);
                }
                else 
                {
                    Console.WriteLine();                
                }
            }

            //пошук метаданих для метода ToString, класа SimpleClass
            MethodInfo info=type.GetMethod("ToString");
            //MethodInfo info = type.GetMethod("ToString", Type.EmptyTypes);
            //шукаємо конкретний атрибут
            InfoAttribute[] arts= (InfoAttribute[]) info.GetCustomAttributes(typeof(InfoAttribute), false);
            if (arts.Length > 0)
            {
                Console.WriteLine("{0} for method ToString(): organization={1}, developer={2}", arts[0].GetType(), arts[0].Organization, arts[0].Developer);
            }
        }
    }

    //приклад простого атрибута
    class VersionAttribute:Attribute
    {
        private double current;

        public VersionAttribute(double current)
        {
            this.current = current;
        }

        public double Current
        {
            get { return current; }
        }
    }

    //приклад іншого атрибута, який застосовується тільки до методів
    [AttributeUsage(AttributeTargets.Method)]
    class InfoAttribute : Attribute
    {
        private string organization;
        private string developer;

        public InfoAttribute(string organization)
        {
            Organization = organization;
        }

        public string Organization 
        {
            get { return organization; }
            private set { organization = value; }
        }

        public string Developer 
        {
            get { return developer; }
            set { developer = value; }
        }
    }

    //приклад атрибута, який можна вказувати декілька разів для одного і того ж класа
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    class MultiAttribute:Attribute
    { 
    
    }

    [Version(1.0)]
    [Multi]
    [Multi]
    //[Version(1.0), Multi, Multi]
    //[VersionAttribute(1.0)] [MultiAttribute] [MultiAttribute]
    class SimpleClass
    {
        [Info("Lvivteploenergo", Developer="FeD")]
        public override string ToString()
        {
            return base.ToString();
        }
    }
}