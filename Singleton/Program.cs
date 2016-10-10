using System;

namespace Singleton
{
    /// <summary>
    /// Тестування ПОРОДЖУЮЧОГО патерна - ОДИНАК (CREATIONAL - SINGLETON).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConnectDB a = ConnectDB.GetInstance();
            a.Connection();

            //ThreadSafeConnectDB.GetInstance().Connection();
        }
    }

    /// <summary>
    /// Клас, який використовується, для підключення до БД.
    /// Для програми з єдиним потоком.
    /// </summary>
    class ConnectDB
    {
        // Єдиний екземрляр класа.
        private static ConnectDB instance = null;

        /// <summary>
        /// Приватний конструктор, який можна використовувати тільк з середини класа.
        /// </summary>
        private ConnectDB()
        {
        }

        /// <summary>
        /// Статичний метод, який надає доступ до єдиного екземпляра класа.
        /// </summary>
        /// <returns>Єдиний екземпляр класа.</returns>
        public static ConnectDB GetInstance()
        {
            if (instance == null)
            {
                instance = new ConnectDB();
            }
            return instance;
        }

        /// <summary>
        /// Один з методів, даного класа.
        /// </summary>
        public void Connection()
        {
            Console.WriteLine("Connect to DB.");
        }
    }

    /// <summary>
    /// Клас, який використовується, для підключення до БД.
    /// Для багатопоточної програми.
    /// </summary>
    class ThreadSafeConnectDB
    {
        // Єдиний екземрляр класа.
        private static ThreadSafeConnectDB instance = null;

        //Змінна призначена для синхронізації (блокування).
        private static readonly object locker = new object();

        /// <summary>
        /// Приватний конструктор, який можна використовувати тільк з середини класа.
        /// </summary>
        private ThreadSafeConnectDB()
        {
        }

        /// <summary>
        /// Статичний метод, який надає доступ до єдиного екземпляра класа.
        /// </summary>
        /// <returns>Єдиний екземпляр класа.</returns>
        public static ThreadSafeConnectDB GetInstance()
        {
            lock (locker)
            {
                if (instance == null)
                {
                    instance = new ThreadSafeConnectDB();
                }
            }
            return instance;
        }

        /// <summary>
        /// Один з методів, даного класа.
        /// </summary>
        public void Connection()
        {
            Console.WriteLine("Connect to DB.");
        }
    }
}