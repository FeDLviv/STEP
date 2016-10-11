using System;
using System.IO;

namespace ClassWork2016_10_11
{
    static class Exercise
    {
        public static void SearchMenu()
        {
            Console.WriteLine("Choose:");
            Console.WriteLine("\t1.Search by name");
            Console.WriteLine("\t2.Search by size");
            Console.WriteLine("\t3.Search by create date");
            Console.WriteLine("\t4.Search by access");

            switch ((char)Console.Read())
            {
                case '1':
                    Console.WriteLine("Enter file/directory name:");
                    Console.ReadLine();
                    findFilesAndDirectories(Console.ReadLine());
                    break;
                default:
                    break;
            }
        }

        private static void findFilesAndDirectories(string search)
        {
            foreach (DriveInfo x in DriveInfo.GetDrives())
            {
                SubDirectories(x.Name, search);
            }
        }

        private static void SubDirectories(string subDir, string search)
        {
            DirectoryInfo d = new DirectoryInfo(subDir);
            foreach (DirectoryInfo i in d.GetDirectories(search))
            {
                try
                {
                    SubDirectories(i.Name, search);
                }
                catch
                {
                    //Console.WriteLine("eX");
                }
                
                Console.WriteLine(i);
            }
        }
    }
}