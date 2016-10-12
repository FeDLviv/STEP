using System;
using System.IO;
using System.Text;

namespace IO
{
    /// Клас для тестування IO в C#
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***FILE STREAM***");//читає байти
            FileStream file1 = new FileStream(@"ASCII.txt", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < file1.Length; i++)
            {
                Console.Write((char)file1.ReadByte());
            }
            Console.WriteLine();
            FileStream file2 = new FileStream(@"UTF-8.txt", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < file2.Length; i++)
            {
                Console.Write((char)file2.ReadByte());
            }
            Console.WriteLine();
            FileStream file3 = new FileStream(@"Unicode.txt", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < file3.Length; i++)
            {
                Console.Write((char)file3.ReadByte());
            }
            Console.WriteLine("\n\n");

            Console.WriteLine("***ENCODING***");
            file1.Seek(0, SeekOrigin.Begin);
            Byte[] buff1 = new Byte[file1.Length];
            file1.Read(buff1, 0, (int)file1.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buff1));
            file2.Seek(0, SeekOrigin.Begin);
            Byte[] buff2 = new Byte[file2.Length];
            file2.Read(buff2, 0, (int)file2.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buff2));
            file3.Seek(0, SeekOrigin.Begin);
            Byte[] buff3 = new Byte[file2.Length];
            file3.Read(buff3, 0, (int)file2.Length);
            Console.WriteLine(Encoding.Unicode.GetString(buff3));
            Console.WriteLine("\n\n");

            Console.WriteLine("***STREAM READER***");//читає символи
            StreamReader reader1 = new StreamReader(@"ASCII.txt");
            Console.WriteLine(reader1.ReadToEnd());
            StreamReader reader2 = new StreamReader(@"UTF-8.txt");
            Console.WriteLine(reader2.ReadToEnd());
            StreamReader reader3 = new StreamReader(@"Unicode.txt");
            Console.WriteLine(reader3.ReadToEnd());
            Console.WriteLine("\n\n");

            Console.WriteLine("***FILE STREAM WRITER***");
            FileStream fs1 = new FileStream(@"D:\FWASCII.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.WriteLine("FW");
            sw1.WriteLine("ASCII");
            sw1.WriteLine("Кириллица");
            sw1.Close();
            fs1 = new FileStream(@"D:\FWASCII.txt", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < fs1.Length; i++)
            {
                Console.Write((char)fs1.ReadByte());
            }
            FileStream fs2 = new FileStream(@"D:\FWUTF-8.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw2 = new StreamWriter(fs2);
            sw2.WriteLine("FW");
            sw2.WriteLine("UTF-8");
            sw2.WriteLine("Кириллица");
            sw2.Close();
            fs2 = new FileStream(@"D:\FWUTF-8.txt", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < fs2.Length; i++)
            {
                Console.Write((char)fs2.ReadByte());
            }
            FileStream fs3 = new FileStream(@"D:\FWUnicode.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw3 = new StreamWriter(fs3);
            sw3.WriteLine("FW");
            sw3.WriteLine("Unicode");
            sw3.WriteLine("Кириллица");
            sw3.Close();
            fs3 = new FileStream(@"D:\FWUnicode.txt", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < fs3.Length; i++)
            {
                Console.Write((char)fs3.ReadByte());
            }
        }
    }
}