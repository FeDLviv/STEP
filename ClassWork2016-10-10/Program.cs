using System;
using System.Text;
using System.IO;

namespace ClassWork2016_10_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***MEMORYSTREAM***");
            //потік в пам'яті
            MemoryStream memStr = new MemoryStream();
            Console.WriteLine("CAPACITY={0}", memStr.Capacity);

            //з консолі в потік MemoryStream
            byte[] buff = Encoding.Unicode.GetBytes(Console.ReadLine());
            memStr.Write(buff, 0, buff.Length);
            Console.WriteLine("CAPACITY={0}", memStr.Capacity);

            //записати в масив дані з потоку MemoryStream
            byte[] arr = memStr.ToArray();

            //записати в файл дані з MemoryStream
            FileStream file = new FileStream("text.txt", FileMode.Create);
            memStr.WriteTo(file);

            Console.WriteLine("***STRINGWRITER/STRINGREADER***");
            StringWriter sw = new StringWriter();
            sw.WriteLine("This text: {0}", 1);
            Console.WriteLine("Generate string: {0}", sw.ToString());

            StringReader sr = new StringReader("text");


            Console.WriteLine("***BINARYREADER/BINARYWRITER***");
            //бінарні потоки
            FileStream f = new FileStream("bin.dat", FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(f);
            //запис в файл bin.dat - int
            bw.Write(1985);
            //запис в файл bin.dat - string
            bw.Write("hello");
            bw.Close();

            BinaryReader bt = new BinaryReader(new FileStream("bin.dat", FileMode.Open, FileAccess.Read));
            Console.WriteLine(bt.ReadInt32());
            Console.WriteLine(bt.ReadString());

            Console.WriteLine("***EXAMPLE***");
            Example.StartExample();
        }
    }
}