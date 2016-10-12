using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassWork2016_10_12
{
    class Example
    {
        public static void StartExample()
        {
            List<Student> list = new List<Student>();
            list.Add(new Bachelor() { Name = "Fedir", Years = 16});
            list.Add(new Magister() { Name = "Danil", Years = 15 });
            list.Add(new Magister() { Name = "Maxim", Years = 18 });
            list.Add(new Bachelor() { Name = "Vladislav", Years = 17 });
            list.Add(new Bachelor() { Name = "Pavlo", Years = 16 });

            Type typeBase = typeof(Student);
            ScholarshipAttribute[] arr = (ScholarshipAttribute[])typeBase.GetCustomAttributes(typeof(ScholarshipAttribute), false);
            if (!arr[0].Exists)
            {
                Console.WriteLine("There are no students with scholarship");
            }
            else
            {
                //Type typeB = typeof(Bachelor);
                //foreach (ScholarshipAttribute x in (ScholarshipAttribute[])typeB.GetCustomAttributes(typeof(ScholarshipAttribute), true))
                //{
                //    //знизу до гори (від нащадка до батьківського)
                //    Console.WriteLine(x.Exists);
                //}
                Console.WriteLine("List of the students with scholarship:");
                foreach (Student x in list)
                {
                    Bachelor temp = x as Bachelor;
                    if (temp != null)
                    {
                        PrintToConsole(x);
                    }
                }
            }
        }

        public static void PrintToConsole(object obj)
        {
            Type type=obj.GetType();
            MethodInfo info = type.GetMethod("ToString", Type.EmptyTypes);
            UpperAttribute[] arr = (UpperAttribute[])info.GetCustomAttributes(typeof(UpperAttribute), false);
            if (arr.Length > 0 && arr[0].IsUpper)
            {
                Console.WriteLine(obj.ToString().ToUpper());
            }
            else
            {
                Console.WriteLine(obj);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
    class UpperAttribute : Attribute
    {
        private bool isUpper;

        public UpperAttribute(bool value)
        {
            isUpper = value;
        }

        public bool IsUpper
        {
            get { return isUpper; }
            set { isUpper = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    class ScholarshipAttribute : Attribute
    {
        public bool Exists { get; set; }
    }

    [Scholarship(Exists = true)]
    abstract class Student
    {
        public string Name { get; set; }
        public int Years { get; set; }

        [Upper(false)]
        public override string ToString()
        {
            return String.Format("Student: {0} - {1} years", Name, Years);
        }
    }

    [Scholarship(Exists = true)]
    class Bachelor : Student
    {
        public string Specialization { get; set; }

        [Upper(true)]
        public override string ToString()
        {
            return base.ToString()+" - bachelor";
        }
    }

    [Scholarship(Exists = false)]
    class Magister:Student
    {
        public string SuperSpecialization { get; set; }

        [Upper(false)]
        public override string ToString()
        {
            return base.ToString() + " - magister";
        }
    }
}