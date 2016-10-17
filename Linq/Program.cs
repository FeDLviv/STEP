using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    //LINQ to Objects
    class Program
    {
        static List<Player> argentina, netherlands;

        static void Main(string[] args)
        {
            SimpleFilterObject();
            InitialLists();

            //термінове виконання запиту (ToList<T>(), ToArray<T>(), ToDictionary<K,V>())
            var res1 = (from x in argentina 
                      where x.Club=="Barcelona"
                      orderby x.Pos ascending
                      select x).ToList();

            //кількість повернутих записів з запиту
            Console.WriteLine(res1.Count<Player>());

            Console.WriteLine("Всі гравці аргентинської збірної, які грають в Барселоні, відсортовані");
            foreach (var i in res1)
            {
                Console.WriteLine(i.Name);
            }

            //спільні клуби для двох збірних
            var res2 = (from x in argentina
                        select x.Club).Intersect(
                        from x in netherlands 
                        select x.Club);

            Console.WriteLine("Спільні клуби для двох збірних:");
            foreach (var i in res2)
            {
                Console.WriteLine(i);
            }

            //клуби в яких грають гравці двох збірних
            var res3 = (from x in argentina
                        select x.Club).Union(
                        from x in netherlands
                        select x.Club);

            Console.WriteLine("Клуби в яких грають гравці двох збірних:");
            foreach (var i in res3)
            {
                Console.WriteLine(i);
            }

            //клуби в яких грають гравці збірної аргентини, але не грають гравці збірної голландії
            var res4 = (from x in argentina
                        select x.Club).Except(
                        from x in netherlands
                        select x.Club);

            Console.WriteLine("Клуби в яких грають гравці збірної аргентини, але не грають гравці збірної голландії:");
            foreach (var i in res4)
            {
                Console.WriteLine(i);
            }

            //клуби в яких грають гравці збірної аргентини
            var res5 = (from x in argentina
                        select x.Club).Distinct();

            Console.WriteLine("Клуби в яких грають гравці збірної аргентини:");
            foreach (var i in res5)
            {
                Console.WriteLine(i);
            }
        }

        static void InitialLists()
        {
            argentina = new List<Player> 
            {
                new Player {Name="Romero", Club="MU", Pos=Position.GK},
                new Player {Name="Lamela", Club="Tottenham", Pos=Position.MF},
                new Player {Name="Banega", Club="Internazionale", Pos=Position.MF},
                new Player {Name="Messi", Club="Barcelona", Pos=Position.FW},
                new Player {Name="Rojo", Club="MU", Pos=Position.DF},
                new Player {Name="Mascherano", Club="Barcelona", Pos=Position.MF}
            };

            netherlands = new List<Player> 
            {
                new Player {Name="Vorm", Club="Tottenham", Pos=Position.GK},
                new Player {Name="Cillessen", Club="Barcelona", Pos=Position.GK},
                new Player {Name="Blind", Club="MU", Pos=Position.DF},
                new Player {Name="Depay", Club="MU", Pos=Position.FW},
                new Player {Name="Promes", Club="Spartak", Pos=Position.FW},
                new Player {Name="Janssen", Club="Tottenham", Pos=Position.FW}
            };
        }

        static void SimpleFilterObject()
        {
            ArrayList list = new ArrayList { 1, 2, "hello", 3.9, 'a', 5 };
            //відфільтровує об'єкти по заданому типу, "перевертає" послідовність даних та терміново виконує запит
            int[] arr = list.OfType<int>().Reverse().ToArray();
            foreach (var x in arr)
            {
                Console.WriteLine(x);
            }
            //методи Sum(), Min(), Max(), Average()
            int res = (from x in arr
                      select x).Sum();
            Console.WriteLine("SUM={0}", res);
        }
    }

    class Player
    {
        public string Name { get; set; }
        public Position Pos { get; set; }
        public string Club { get; set; }
    }

    enum Position { GK, DF, MF, FW }
    
    //var A=new {X=...}
    //Concat()
}