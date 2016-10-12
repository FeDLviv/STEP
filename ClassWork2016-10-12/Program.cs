using System;
using System.Reflection;

namespace ClassWork2016_10_12
{
    //АТРИБУТИ
    class Program
    {
        static void Main(string[] args)
        {
            Point point = new Point();
            Type type = typeof(Point);
            //ДЛЯ КЛАСА
            //true - враховуються атрибути батьківських класів
            //false - враховуються атрибути тільки даного класу
            object[] arrAttribute = type.GetCustomAttributes(false);
            //якщо знаєм, що атрибут один і знаєм його тип
            //MyFirstAttribute[] arrAttribute = (MyFirstAttribute[])type.GetCustomAttributes(false);
            foreach (object x in arrAttribute)
            {
                if (x is MyFirstAttribute)
                {
                    Console.WriteLine("Owner={0}, version={1}", ((MyFirstAttribute)x).Organization, ((MyFirstAttribute)x).Version);
                }
            }
            //ДЛЯ МЕТОДА
            MethodInfo info=type.GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance);
            foreach(MyFirstAttribute x in info.GetCustomAttributes(typeof(MyFirstAttribute), false))
            {
                Console.WriteLine("Owner={0}, version={1}", x.Organization, x.Version);
            }

            Example.StartExample();
        }
    }

    //перший атрибут
    //AttributeTargets.Class|AttributeTargets.Struct (декорувати можна класи та структури)
    //AllowMultiple = true (декількома екземплярами можна декорувати)
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple =true)]
    class MyFirstAttribute : Attribute
    {
        private string organization;
        private int version;

        public MyFirstAttribute(string organization)
        {
            Organization = organization;
        }

        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Organization
        {
            get { return organization; }
            private set { organization = value; }
        }
    }

    //клас з нашим атрибутом з параметрами
    [MyFirst("STEP CA", Version=1)]
    class Point
    {
        //компілятор не пропусте, оскільки наш атрибут може декорувати тільки класи і структури
        //[MyFirst("STEP CA", Version =2)]
        public override string ToString()
        {
            return "POINT";
        }
    }
}