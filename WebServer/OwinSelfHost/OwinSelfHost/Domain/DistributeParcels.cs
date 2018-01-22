using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OwinSelfHost.Repository;

namespace OwinSelfHost.Domain
{
    public class DistributeParcels: IDistributeParcels
    {
        private readonly IRepository repository;
        private XmlDocument doc = new XmlDocument();
        public DistributeParcels(IRepository repository)
        {
            this.repository = repository;
        }

        public Parcel[] Distribute(string data)
        {
            doc.LoadXml(data);
            //Parse and assign correct department
            return new[] {new Parcel {DepartmentName = "Regular"}};
        }
    }
}
