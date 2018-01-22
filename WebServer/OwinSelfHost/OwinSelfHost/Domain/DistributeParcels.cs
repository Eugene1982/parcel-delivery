using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OwinSelfHost.Helpers;
using OwinSelfHost.Repository;

namespace OwinSelfHost.Domain
{
    public class DistributeParcels: IDistributeParcels
    {
        private readonly IRepository repository;
        private readonly IParser parser;
         public DistributeParcels(IRepository repository, IParser parser)
        {
            this.repository = repository;
            this.parser = parser;
        }

        public Parcel[] Distribute(string data)
        {
            var parcels = parser.Parse(data);

            var departments = repository.GetAllDepartments();
            foreach (Parcel parcel in parcels)
            {
                var department = departments.Where(x =>
                                         x.PriceStart.HasValue && parcel.Price > x.PriceStart)
                                     .OrderByDescending(x => x.CreatedAt)
                                     .FirstOrDefault() ?? departments.Where(x =>
                                         x.WeightMin.HasValue && x.WeightMax.HasValue &&
                                         parcel.Weight >= x.WeightMin && parcel.Weight < x.WeightMax)
                                     .OrderByDescending(x => x.CreatedAt)
                                     .FirstOrDefault();

                parcel.DepartmentName = department != null ? department.Name : String.Empty;
            }

            return parcels.ToArray();
        }
    }
}
