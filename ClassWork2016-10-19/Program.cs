using System.Text;
using System.Xml;

namespace ClassWork2016_10_19
{
    //XML
    class Program
    {
        static void Main(string[] args)
        {
            const string URL = "https://api.privatbank.ua/p24api/pboffice?city=%D0%9B%D1%8C%D0%B2%D0%BE%D0%B2";
            const string FILE_NEW = @"D:\Student.xml";

            //читання документа xml
            XmlTextReader read=new XmlTextReader(URL);
            while (read.Read())
            {
                if(read.NodeType==XmlNodeType.Element)
                {
                    if (read.AttributeCount >= 0)
                    {
                        read.MoveToAttribute("address");
                        System.Console.WriteLine(read.Value);
                    }
                }
            }
            read.Close();

            //створення документа xml
            XmlTextWriter write=new XmlTextWriter(FILE_NEW, Encoding.UTF8);
            write.Formatting= Formatting.Indented;
            write.WriteStartDocument(true);
            write.WriteStartElement("students");
            write.WriteStartElement("student");
            write.WriteAttributeString("id", "1");
            write.WriteElementString("name", "Fedir");
            write.WriteElementString("group", "s25-vp1");
            write.WriteEndElement();
            write.WriteEndElement();
            write.Close();

            //перевірка документа xml (XSD) //застаріла DTD
            XmlValidatingReader wr = new XmlValidatingReader(new XmlTextReader(FILE_NEW));
            //обробник
            wr.ValidationEventHandler += (sender, ev) => { System.Console.WriteLine("NOT VALIDATE!!!"); };
            //wr.ValidationType=ValidationType.None //ЗВИЧАЙНИЙ XmlTextReader
            wr.ValidationType = ValidationType.Auto;
            //задаєм схему (простір імен, ім'я файла)
            wr.Schemas.Add("", @".\XMLSchema1.xsd");
            while (wr.Read())
            {
            }
        }
    }
}