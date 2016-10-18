using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace ClassWork2016_10_18
{
    public class PB
    {
        private readonly string URL= "https://api.privatbank.ua/p24api/pboffice?city=%D0%9B%D1%8C%D0%B2%D0%BE%D0%B2";
        private readonly string XML;

        public PB()
        {
            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            XML = web.DownloadString(URL);
            web.Dispose();
        }

        public SortedSet<string> GetAddress()
        {
            SortedSet<string> set = new SortedSet<string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XML);
            foreach(XmlNode tag in doc.SelectNodes("/pboffice/pboffice"))
            {
                foreach (XmlAttribute atr in tag.Attributes)
                {
                    if (atr.Name == "address")
                    {
                        set.Add(atr.Value);
                    }
                }
            }
            return set;
        }        
    }
}