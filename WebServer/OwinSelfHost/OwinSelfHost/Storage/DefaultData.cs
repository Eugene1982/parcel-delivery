using System;
using OwinSelfHost.Domain;
using OwinSelfHost.Helpers;
using OwinSelfHost.Repository;

namespace OwinSelfHost.Storage
{
    public class DefaultData
    {
        private readonly IRepository repository;
      
        public DefaultData(IRepository repository)
        {
            this.repository = repository;
        }

        public void Upload()
        {
            repository.AddDepartments(new Department
                {
                    Name = "Mail",
                    WeightMin = 0,
                    WeightMax = 1,
                    CreatedAt = DateTime.Now
                },
                new Department
                {
                    Name = "Regular",
                    WeightMin = 1,
                    WeightMax = 10,
                    CreatedAt = DateTime.Now
                },
                new Department
                {
                    Name = "Heavy",
                    WeightMin = 10,
                    WeightMax = int.MaxValue,
                    CreatedAt = DateTime.Now
                },
                new Department
                {
                    Name = "Insurance",
                    PriceStart = 1000,
                    CreatedAt = DateTime.Now
                }
            );

        }
    }
}