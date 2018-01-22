using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OwinSelfHost.Domain;

namespace OwinSelfHost.Helpers
{
    public class Parser: IParser
    {
        private readonly XmlDocument doc = new XmlDocument();

        public IList<Parcel> Parse(string xml)
        {
            var parcelList = new List<Parcel>();
            doc.LoadXml(xml);
            XmlNode parcels = doc.DocumentElement.SelectSingleNode("parcels");
            foreach (XmlNode node in parcels.ChildNodes)
            {
               
                parcelList.Add(new Parcel
                {
                    Price = double.Parse(node.SelectSingleNode("Value")?.InnerText),
                    Weight = double.Parse(node.SelectSingleNode("Weight")?.InnerText)
                });
            }


            return parcelList;
        }
    }
}
