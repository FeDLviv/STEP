using System;
using System.Xml;
using System.Net;
using System.Xml.XPath;

namespace ClassWork2016_10_18
{
    class Program
    {
        //XPathLanguage
        static void Main(string[] args)
        {
            const string URL= "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5";
            string XML;
                        
            //завантажуєм дані з ресурса (URL)           
            using (WebClient web = new WebClient())
            {
                XML = web.DownloadString(URL);
            }
            
            //завантажуєм xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XML);

            //шукаємо всі теги - exchangerate
            XmlNodeList list = doc.SelectNodes("//exchangerate");
            Console.WriteLine("Курс валют на сьогоднi:");
            foreach (XmlNode node in list)
            {
                //пробігаємось по атрибутах
                foreach (XmlAttribute atr in node.Attributes)
                {
                    switch (atr.Name)
                    {
                        case "buy":
                            Console.Write("купiвля ");
                            break;
                        case "sale":
                            Console.Write("продаж ");
                            break;
                    }
                    Console.Write(atr.Value + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Фiльтрацiя, операцiї, тiльки з гривнею:");
            //пошук з фільтрацією по атрибуту - base_ccy
            foreach (XmlNode node in doc.SelectNodes("/child::exchangerates/child::row/child::exchangerate[attribute::base_ccy='UAH']"))
            {
                //пробігаємось по атрибутах
                foreach (XmlAttribute atr in node.Attributes)
                {
                    switch (atr.Name)
                    {
                        case "buy":
                            Console.Write("купiвля ");
                            break;
                        case "sale":
                            Console.Write("продаж ");
                            break;
                    }
                    Console.Write(atr.Value + " ");
                }
                Console.WriteLine();
            }

            //System.Xml.XPath
            XPathDocument xDoc = new XPathDocument(URL);
            XPathNavigator nav = xDoc.CreateNavigator();
            XPathNodeIterator itr = nav.Select("/exchangerates/row");
            while(itr.MoveNext())
            {
                XPathNavigator x = itr.Current;
                if (x.HasChildren)
                {
                    x.MoveToFirstChild();
                    if (x.HasAttributes)
                    {
                        x.GetAttribute("sd","");
                    }
                }
            }

            PB pb = new PB();
            foreach (string x in pb.GetAddress())
            {
               Console.WriteLine(x);
            }
        }
    }
}