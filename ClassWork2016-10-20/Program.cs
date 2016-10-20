using System;
using System.Text;
using System.Reflection;
using NLog;

namespace ClassWork2016_10_20
{
    //РЕФЛЕКСІЯ
    class Program
    {
        static void Main(string[] args)
        {
            MyClass obj = new MyClass();

            //варіанти дізнавання типу об'єкта
            Type type1 = obj.GetType();
            Type type2 = typeof(ClassWork2016_10_20.MyClass);
            Type type3 = Type.GetType("ClassWork2016_10_20.MyClass");

            Console.WriteLine((type3.ToString()).ToUpper());

            //пробігаємося по методах об'єкта
            foreach (MemberInfo x in type3.GetMembers())
            {
                if (x.MemberType == MemberTypes.Method)
                {
                    MethodInfo met = x as MethodInfo;
                    if (met != null)
                    {
                        StringBuilder text = new StringBuilder("public ");
                        if (met.IsStatic)
                        {
                            text.Append("static ");
                        }

                        text.Append(met.ReturnType.Name + " " + met.Name + " (");

                        bool parametrs = false;
                        foreach (ParameterInfo i in met.GetParameters())
                        {
                            parametrs = true;
                            text.Append(i.ParameterType.Name + " ");
                        }
                        if (parametrs)
                        {
                            text.Remove(text.Length - 1, 1);
                        }
                        text.Append(")");

                        Console.WriteLine(text);
                    }
                }
            }

            //створюємо об'єкт конструктором по замовчуванню
            foreach (var con in type3.GetConstructors())
            {
                if (con.GetParameters().Length == 0)
                {
                    object o = con.Invoke(new object[] { }); //null
                    MethodInfo met = type3.GetMethod("GetText");
                    met.Invoke(o, new object[] { "Hello" });
                }
            }

            //підвантажуємо збірку і створюємо об'єкт класу, який знаходиться в збірці
            Assembly asem = Assembly.LoadFrom(@"D:\My.dll");
            Type t = asem.GetType("My.Asd");
            //ще один варіант для створення екземпляра класа
            object ob = Activator.CreateInstance(t);

            Logger log = LogManager.GetCurrentClassLogger();
            log.Debug("приклад");
        }
    }

    public class MyClass
    {
        public MyClass()
        {
            Console.WriteLine("CONSTRUCTOR");
        }

        public static void MyMethod(int x)
        {
        }

        public void GetText(string text)
        {
            Console.WriteLine(text);
        }
    }
}