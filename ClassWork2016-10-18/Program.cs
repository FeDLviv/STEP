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
            const string PROXY_IP = "192.168.1.4";
            const int PROXY_PORT = 3128;
            const string PROXY_USER = "boss";
            const string PROXY_PASSWORD = "393735ws";
            const string URL= "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5";
            string XML;
                        
            //завантажуєм дані з ресурса (URL)           
            using (WebClient web = new WebClient())
            {
                
                WebProxy proxy = new WebProxy(PROXY_IP, PROXY_PORT);
                proxy.Credentials = new NetworkCredential(PROXY_USER, PROXY_PASSWORD);
                web.Proxy = proxy;           
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