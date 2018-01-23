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
                var senderNode = node.SelectSingleNode("Sender");
                var recepientNode = node.SelectSingleNode("Receipient");

                var senderAdress = senderNode.SelectSingleNode("Address");
                var recepientrAdress = recepientNode.SelectSingleNode("Address");

                parcelList.Add(new Parcel
                {
                    Price = double.Parse(node.SelectSingleNode("Value")?.InnerText),
                    Weight = double.Parse(node.SelectSingleNode("Weight")?.InnerText),
                    From = new Contact
                    {
                        Name = senderNode.SelectSingleNode("Name").InnerText,
                        City = senderAdress.SelectSingleNode("City").InnerText,
                        HouseNumber = int.Parse(senderAdress.SelectSingleNode("HouseNumber").InnerText),
                        PostalCode = senderAdress.SelectSingleNode("PostalCode").InnerText,
                        Street = senderAdress.SelectSingleNode("Street").InnerText
                    },
                    To = new Contact
                    {
                        Name = recepientNode.SelectSingleNode("Name").InnerText,
                        City = recepientrAdress.SelectSingleNode("City").InnerText,
                        HouseNumber = int.Parse(recepientrAdress.SelectSingleNode("HouseNumber").InnerText),
                        PostalCode = recepientrAdress.SelectSingleNode("PostalCode").InnerText,
                        Street = recepientrAdress.SelectSingleNode("Street").InnerText
                    },
                });
            }
            return parcelList;
        }
    }
}
