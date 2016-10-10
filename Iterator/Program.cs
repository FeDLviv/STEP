using System;
using System.Collections.Generic;

namespace Iterator
{
    /// <summary>
    /// Тестування патерна ПОВЕДІНКИ - ІТЕРАТОР (BEHAVIORAL - ITERATOR).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MyLibrary library = new MyLibrary();
            library[0] = new MyBook("C# 6.0 and the .NET 4.6 Framework", "Andrew Troelsen");
            library[1] = new MyBook("Thinking in Java (4th Edition)", "Bruce Eckel");
            library[1] = new MyBook("The C Programming Language (2nd Edition)", "Dennis Ritchie");

            IIterator<MyBook> iterator = library.CreateIterator();
            Console.WriteLine("***ALL BOOKS***");
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.Next());
            }
        }
    }

    /// <summary>
    /// Інтерфейс узагальненого ітератора.
    /// </summary>
    /// <typeparam name="T">Параметр для узагальненого ітератора.</typeparam>
    interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    /// <summary>
    /// Інтерфейс узагальненого агрегата.
    /// </summary>
    /// <typeparam name="T">Параметр для узагальненого агрегата.</typeparam>
    interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    /// <summary>
    /// Клас описує книгу.
    /// </summary>
    class MyBook
    {
        public string Title { set; get; }
        public string Author { set; get; }

        public MyBook(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return String.Format("\"{0}\" - {1}", Title, Author);
        }
    }

    /// <summary>
    /// Клас, являє собою бібліотеку, яка може містити в собі книги (MyBook). Даний клас реалізує інтерфейс IAggregate<MyBook>.
    /// </summary>
    class MyLibrary : IAggregate<MyBook>
    {
        private List<MyBook> list = null;

        public MyLibrary()
        {
            list = new List<MyBook>();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public MyBook this[int index]
        {
            set { list.Insert(index, value); }
            get { return list[index]; }
        }

        public IIterator<MyBook> CreateIterator()
        {
            return new LibraryIterator(this);
        }
    }

    /// <summary>
    /// Клас-ітератор для бібліотеки (MyLibrary). Даний клас реалізує інтерфейс IIterator<MyBook>.
    /// </summary>
    class LibraryIterator : IIterator<MyBook>
    {
        private MyLibrary library = null;
        private int index = 0;

        public LibraryIterator(MyLibrary library)
        {
            this.library = library;
        }

        public bool HasNext()
        {
            return index < library.Count;
        }

        public MyBook Next()
        {
            return library[index++];
        }
    }
}