using System;
using System.IO;

namespace ClassWork2016_10_11
{
    class Program
    {
        static void Main(string[] args)
        {
            ///FileSystemInfo
            //FileInfo ,File (static)
            //DirectoryInfo, Directory (static)
            //DriveInfo

            //директорії
            DirectoryInfo dir = new DirectoryInfo(@"C:\MyDir");
            if (dir.Exists)
            {
                Console.WriteLine("FULL NAME={0}\tNAME={1}\tROOT={2}",dir.FullName, dir.Name, dir.Root);
                foreach (DirectoryInfo x in dir.GetDirectories())
                {
                    Console.WriteLine("DIR - {0}", x);
                }
                //dir.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
                foreach (FileInfo x in dir.GetFiles())
                {
                    Console.WriteLine("FILE - {0}", x.Name);
                }
            }
            else
            {
                dir.Create();
                dir.CreateSubdirectory("MySubdir");
                //Directory.CreateDirectory(@"C:\MyDir");
                //Directory.CreateDirectory(@"C:\MyDir\MySubdir");
            }

            //файли
            FileInfo file = new FileInfo(@"C:\MyDir\my.txt");
            if (file.Exists)
            {
                StreamReader strRead=file.OpenText();
                Console.WriteLine(strRead.ReadLine());
                string[] arr=File.ReadAllLines(@"C:\MyDir\my.txt");
            }
            else
            {
                FileStream fStr = file.Open(FileMode.Create, FileAccess.Write, FileShare.None);
                fStr.Close();

                StreamWriter strWr=file.AppendText();
                strWr.Write(Console.ReadLine());
                strWr.Close();
            }

            //шлях
            string path=Path.Combine(@"C:\MyDir\", @"asd.txt");
            Console.WriteLine(path);

            //логічні диски
            DriveInfo[] arrDrive=DriveInfo.GetDrives();
            foreach (DriveInfo x in arrDrive)
            {
                Console.WriteLine("NAME - {0}\tFORMAT={1}\tTOTAL SIZE={2}GB", x.Name, x.DriveFormat, ((x.TotalSize / 1024) / 1024) / 1024);
            }
        }
    }
}