using System;
using System.IO;

namespace Test
{
    /// <summary>
    /// Тестовий
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***HARD:***");
            foreach (string x in Directory.GetLogicalDrives())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("***FILES IN DIR:***\n{0}", Directory.GetCurrentDirectory());
            foreach (string x in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                Console.WriteLine(Path.GetFileName(x));
            }
        }
    }
}