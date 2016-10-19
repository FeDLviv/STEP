using System;
using System.Text;
using System.IO;

//C# System.IO
namespace ClassWork2016_10_10
{
    class Program
    {
        static void Main(string[] args)

        {
            //C# System
            //0. Objects, MarshalByRefObjects
            //C# System.IO
            //1. Stream (FileStream, MemoryStream, BufferedStream)
            //2. TextReader (StreamReader, StringReader), TextWriter (StreamWriter, StringWriter), BinaryReader, BinaryWriter
            //3. FileSystemInfo (FileInfo, DirectoryInfo), Driveinfo, Path, File, Directory

            const string PATH_TO_FILE = @"data.txt";
            const string PATH_TO_BIN = @"data.dat";

            //MemoryStream+FileStream - потоки пам'яті та файла
            Console.WriteLine("***MemoryStream+FileStream***");
            MemoryStream mStr = new MemoryStream();
            byte[] buff = Encoding.UTF8.GetBytes(Console.ReadLine());
            mStr.Write(buff, 0, buff.Length);
            FileStream fStr = new FileStream(PATH_TO_FILE, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            mStr.WriteTo(fStr);
            fStr.Close();

            //StreamReader - символьний потік читання
            Console.WriteLine("***StreamReader***");
            StreamReader strRed = new StreamReader(PATH_TO_FILE);
            string text = strRed.ReadToEnd();
            strRed.Close();

            //StringReader - читання символів з стрічки
            Console.WriteLine("***StringReader***");
            StringReader stringR = new StringReader(text);
            Console.WriteLine(stringR.ReadLine());

            //StreamWriter - символьний потік запису
            Console.WriteLine("***StreamWriter***");
            StreamWriter strWrt = new StreamWriter(PATH_TO_FILE);
            strWrt.Write(Console.ReadLine());
            strWrt.Close();

            //StringWriter - запис символів в стрічку
            Console.WriteLine("***StringWriter***");
            StringWriter stringW = new StringWriter();
            stringW.WriteLine(Console.ReadLine());
            Console.WriteLine(stringW);

            //BinaryWriter - бінарний потік запису
            Console.WriteLine("***BinaryWriter***");
            BinaryWriter binWrt = new BinaryWriter(new FileStream(PATH_TO_BIN, FileMode.Create, FileAccess.Write));
            Console.WriteLine("Input int:");
            binWrt.Write(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Input char:");
            binWrt.Write(Console.Read());
            binWrt.Close();

            //BinaryReader - бінарний потік читання
            Console.WriteLine("***BinaryReader***");
            BinaryReader binRed = new BinaryReader(new FileStream(PATH_TO_BIN, FileMode.Open, FileAccess.Read));
            Console.WriteLine(binRed.ReadInt32());
            Console.WriteLine(binRed.ReadChar());

            //DriverInfo - дає інформацію про жорсткі диски
            Console.WriteLine("***DriveInfo***");
            foreach (DriveInfo x in DriveInfo.GetDrives())
            {
                Console.WriteLine("{0} - {1} - {2}GB", x.Name, x.DriveFormat, ((x.TotalSize / 1024) / 1024) / 1024);
            }

            //Path+Directory+File - шлях до файла або директорії + директорії + файли
            Console.WriteLine("***static Path+Directory+File***");
            string curDir = Directory.GetCurrentDirectory();
            string path = Path.Combine(curDir, PATH_TO_FILE);
            Console.WriteLine("FILE: {0} EXISTS: {1}", path, File.Exists(path));

            //DirectoryInfo - директорії
            Console.WriteLine("***DirectoryInfo***");
            string newPath = Path.Combine(Directory.GetCurrentDirectory(), "newDir");
            DirectoryInfo dir = new DirectoryInfo(newPath);
            Console.WriteLine("DIR: {0} EXISTS: {1}", newPath, dir.Exists);
            if (!dir.Exists)
            {
                dir.Create();
            }

            //FileInfo - файли
            Console.WriteLine("***FileInfo***");
            FileInfo file = new FileInfo(Path.Combine(newPath, "new.txt"));
            if (!file.Exists)
            {
                StreamWriter stream = file.CreateText();
                Console.ReadLine();
                stream.WriteLine(Console.ReadLine());
                stream.Close();
            }
            Console.WriteLine("Text from file: {0}:\n{1}", file.Name, file.OpenText().ReadToEnd());
        }
    }
}