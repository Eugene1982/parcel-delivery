using System;
using System.Linq;
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
                Department department = departments.Where(x =>
                                         x.PriceStart.HasValue && parcel.Price > x.PriceStart)
                                     .OrderByDescending(x => x.CreatedAt)
                                     .FirstOrDefault() ?? departments.Where(x =>
                                         x.WeightMin.HasValue && x.WeightMax.HasValue &&
                                         parcel.Weight >= x.WeightMin && parcel.Weight < x.WeightMax)
                                     .OrderByDescending(x => x.CreatedAt)
                                     .FirstOrDefault();
                if (department == null)
                {
                    throw new Exception($"There is no department to procced the parcel with weight: {parcel.Weight} and price: {parcel.Price}");
                }
                parcel.DepartmentName = department.Name;
            }

            return parcels.ToArray();
        }
    }
}
