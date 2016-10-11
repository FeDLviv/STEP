using System;
using System.Collections.Generic;
using System.IO;

namespace ClassWork2016_10_10
{
    static class Example
    {
        public static void StartExample()
        {
            SortedList<string, int> listCount=new SortedList<string, int>();
            try
            {
                StreamReader sr = new StreamReader(new FileStream("words.txt", FileMode.Open, FileAccess.Read));
                string line;
                List<string> list = new List<string>();

                //читання файла
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    list.AddRange(line.Split(new char[] {' ', ',', '.', ':', '!', '?'}, StringSplitOptions.RemoveEmptyEntries));
                }
                sr.Close();

                //підрахунок кількості повтореннь слів
                foreach (string x in list)
                {
                    if (listCount.ContainsKey(x))
                    {
                        listCount[x]++;
                    }
                    else
                    {
                        listCount.Add(x, 1);
                    }
                }

                //запис інформації в файл
                StreamWriter sw = new StreamWriter(new FileStream("countWords.txt", FileMode.Create, FileAccess.Write));
                foreach (var x in listCount)
                {
                    sw.WriteLine("{0}-{1}", x.Key, x.Value);
                }
                sw.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error file.");
            }
        }
    }
}