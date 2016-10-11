using System;
using System.Text;

namespace Object
{
    class Program
    {
        static void Main(string[] args)
        {
            Points p1 = new Points(3);
            p1[0] = 2;
            p1[1] = 1;
            p1[2] = 5;
            Points p2 = 5;
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine("p1==p2  {0}", p1==p2);
            p1.MyExtensionsMethod("new method");
            object obj = new object();
            obj.ObjectExtensionsMethod();
        }
    }

    //клас наслідується від класа System.Objects (дане наслідування при визначенні класа можна не прописувати) та реалізує інтерфейс System.IComparable<Points>
    class Points : object, IComparable<Points>
    {
        private int[] arr = null;

        //публічний конструктор, параметром задається кількість точок
        public Points(int count)
        {
            arr = new int[count];
        }

        //перевизначення метода System.Object.ToString()
        public override string ToString()
        {
            if (arr.Length == 0)
            {
                return String.Format("{0} []", GetType());
            }
            else
            {
                StringBuilder text = new StringBuilder();
                text.AppendFormat("{0} [", GetType());
                foreach (int x in arr)
                {
                    text.Append(x + ", ");
                }
                text.Remove(text.Length - 2, 2);
                text.AppendFormat("]");
                return text.ToString();
            }
        }

        //перевизначення метода System.Object.GetHashCode()
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        //перевизначення метода System.Object.Equals()
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Points temp = (Points)obj;
                if (GetCount() == temp.GetCount())
                {
                    for (int i = 0; i < GetCount(); i++)
                    {
                        if (this[i] != temp[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //метод-індексатор
        public int this[int index]
        {
            set { arr[index] = value; }
            get { return arr[index]; }
        }

        //перегрузка оператора приведення (неявне приведення)
        public static implicit operator Points(int x)
        {
            Points temp = new Points(x);
            for (int i = 0; i < x; i++)
            {
                temp[i] = x;
            }
            return temp;
        }

        //перегрузка оператора приведення (явне приведення)
        public static explicit operator Points(double x)
        {
            return (int)x;
        }

        //перегрузка оператора додавання
        public static Points operator +(Points p1, Points p2)
        {
            int minCount = p1.GetCount() < p2.GetCount() ? p1.GetCount() : p2.GetCount();
            Points temp = new Points(minCount);
            for (int i = 0; i < minCount; i++)
            {
                temp[i] = p1[i] + p2[i];
            }
            return temp;
        }

        //перегрузка оператора віднімання
        public static Points operator -(Points p1, Points p2)
        {
            int minCount = p1.GetCount() < p2.GetCount() ? p1.GetCount() : p2.GetCount();
            Points temp = new Points(minCount);
            for (int i = 0; i < minCount; i++)
            {
                temp[i] = p1[i] - p2[i];
            }
            return temp;
        }

        //перегрузка оператора множення
        public static Points operator *(Points p1, Points p2)
        {
            int minCount = p1.GetCount() < p2.GetCount() ? p1.GetCount() : p2.GetCount();
            Points temp = new Points(minCount);
            for (int i = 0; i < minCount; i++)
            {
                temp[i] = p1[i] * p2[i];
            }
            return temp;
        }

        //перегрузка оператора ділення
        public static Points operator /(Points p1, Points p2)
        {
            int minCount = p1.GetCount() < p2.GetCount() ? p1.GetCount() : p2.GetCount();
            Points temp = new Points(minCount);
            for (int i = 0; i < minCount; i++)
            {
                temp[i] = p1[i] / p2[i];
            }
            return temp;
        }

        //перегрузка оператора інкремента ОДНА ДЛЯ (префіксний інкремент (преінкремент) та постфіксний інкремент (постінкремент)
        public static Points operator ++(Points p)
        {
            Points temp=new Points(p.GetCount());
            for(int i=0; i<p.GetCount(); i++)
            {
                temp[i]++;
            }
            return temp;
        }

        //перегрузка оператора декремента ОДНА ДЛЯ (префіксний декремент (предекремент) та постфіксний декремент (постдекремент)
        public static Points operator --(Points p)
        {
            Points temp = new Points(p.GetCount());
            for (int i = 0; i < p.GetCount(); i++)
            {
                temp[i]--;
            }
            return temp;
        }

        //перегрузка операції == , виконується разом з перегрузкою операції !=
        public static bool operator == (Points p1, Points p2)
        {
            return p1.Equals(p2) ;
        }

        //перегрузка операції != , виконується разом з перегрузкою операції ==
        public static bool operator !=(Points p1, Points p2)
        {
            return !p1.Equals(p2);
        }

        //перегрузка операції < , виконується разом з перегрузкою операції >
        public static bool operator <(Points p1, Points p2)
        { 
            return (p1.CompareTo(p2) < 0); 
        }

        //перегрузка операції > , виконується разом з перегрузкою операції <
        public static bool operator >(Points p1, Points p2)
        {
            return (p1.CompareTo(p2) > 0);
        }

        //перегрузка операції <= , виконується разом з перегрузкою операції >=
        public static bool operator <=(Points p1, Points p2)
        { 
            return (p1.CompareTo(p2) <= 0); 
        }

        //перегрузка операції >= , виконується разом з перегрузкою операції <=
        public static bool operator >=(Points p1, Points p2)
        {
            return (p1.CompareTo(p2) >= 0);
        }

        //перегрузка значення TRUE, виконується разом з перегрузкою значення FALSE
        public static bool operator true(Points p)
        {
            return p.GetCount() > 0;
        }

        //перегрузка значення FALSE, виконується разом з перегрузкою значення TRUE
        public static bool operator false(Points p)
        {
            return p.GetCount() == 0; ;
        }

        //додатковий метод, повертає кількість точок
        public int GetCount()
        {
            return arr.Length;
        }

        //додатковий метод, повертає суму точок
        public int GetSum()
        {
            int sum = 0;
            foreach (int x in arr)
            {
                sum += x;
            }
            return sum;
        }

        //реалізація інтерфейса IComparable
        public int CompareTo(Points p)
        {
            if (this.GetSum() > p.GetSum())
            {
                return 1;
            }
            else if (this.GetSum() < p.GetSum())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    //клас створюється для додавання нових методів до класів (методи розширення), з'явилися в .NET 3.5
    static class ExtensionsPoints
    {
        //метод розширення для класа Points
        public static void MyExtensionsMethod(this Points p, string text)
        {
            Console.WriteLine("My extensions method. " + text);
        }

        //метод розширення для класа System.Objects
        public static void ObjectExtensionsMethod(this object obj)
        {
            Console.WriteLine("Object extensions method");
        }
    }
}