using System;

using System.Xml;
using System.Xml.Serialization;

namespace ClassWork2016_10_17
{
    class Program
    {
        //XML
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            //завантажуєм файл xml
            doc.Load("XMLFile1.xml");
            //отримуємо корінь (вершину) дерева
            XmlNode root = doc.DocumentElement;
            //якщо корінь має теги
            if (root.HasChildNodes)
            {
                //повертає теги "першого рівня"
                XmlNodeList list = root.ChildNodes;
                //пробігаємося по тегах "першого рівня"
                foreach (XmlNode std in list)
                {
                    //перевірка чи є атрибути в тезі
                    if (std.Attributes != null)
                    {
                        //пробігаємось по атрибутах тега
                        foreach (XmlAttribute atr in std.Attributes)
                        {
                            //якщо атрибут має назву id
                            if (atr.Name=="id")
                            {
                                //виводимо дані атрибута
                                Console.Write("ID={0} ", atr.Value);
                            }
                        }
                    }
                    //виводимо дані тегів
                    Console.Write("NAME={0} RATE={0} GROUP={0} ", std["name"].InnerText, std["rate"].InnerText, std["group"].InnerText);
                    XmlNode date = std["birthday"];
                    Console.WriteLine("DATE={0}.{1}.{2}", date["day"].InnerText, date["month"].InnerText, date["year"].InnerText);

                    //видалити
                    /*
                    std.RemoveChild(std["group"]);
                    std.RemoveAll();
                    */
                    
                    //змінити
                    /*
                    std.ReplaceChild();
                    */
                }           
            }
            AddStudent(doc);
        }

        //метод створює та додає нового студента в файл xml
        static void AddStudent(XmlDocument doc)
        {
            //створення тегів
            XmlNode std = doc.CreateElement("student");
            XmlNode stdName=doc.CreateElement("name");
            XmlNode stdRate = doc.CreateElement("rate");
            XmlNode stdGroup = doc.CreateElement("group");
            XmlNode stdBirthday = doc.CreateElement("birthday");
            XmlNode stdDay = doc.CreateElement("day");
            XmlNode stdMonth = doc.CreateElement("month");
            XmlNode stdYear = doc.CreateElement("year");
            //створення даних
            XmlNode stdNameText = doc.CreateTextNode("Loha");
            XmlNode stdRateText = doc.CreateTextNode("9");
            XmlNode stdGroupText = doc.CreateTextNode("other");
            XmlNode stdDayText = doc.CreateTextNode("24");
            XmlNode stdMonthText = doc.CreateTextNode("6");
            XmlNode stdYearText = doc.CreateTextNode("1989");
            //звязування "теги-дані"
            stdName.AppendChild(stdNameText);
            stdRate.AppendChild(stdRateText);
            stdGroup.AppendChild(stdGroupText);
            stdDay.AppendChild(stdDayText);
            stdMonth.AppendChild(stdMonthText);
            stdYear.AppendChild(stdYearText);
            //додавання нащадків
            std.AppendChild(stdName);
            std.AppendChild(stdRate);
            std.AppendChild(stdGroup);
            std.AppendChild(stdBirthday);
            stdBirthday.AppendChild(stdDay);
            stdBirthday.AppendChild(stdMonth);
            stdBirthday.AppendChild(stdYear);
            //створення атрибута
            XmlAttribute stdId = doc.CreateAttribute("id");
            //прив'язування даних до атрибуту
            stdId.Value = "3";
            //додавання атрибута
            std.Attributes.Append(stdId);
            //додавання створеного тега до корня
            doc.DocumentElement.AppendChild(std);
            //зберігання файла
            doc.Save("new.xml");
        }
    }
}