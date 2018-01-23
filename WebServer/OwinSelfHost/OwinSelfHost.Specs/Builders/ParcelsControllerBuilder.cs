using System.Collections.Generic;
using System.Net.Http;
using FakeItEasy;
using OwinSelfHost.Domain;
using OwinSelfHost.Helpers;
using OwinSelfHost.Repository;
using OwinSelfHost.WebApi;

namespace OwinSelfHost.Specs.Builders
{
    public class ParcelsControllerBuilder
    {
        private IRepository repostiory = A.Fake<IRepository>();
        private IParser parser = new Parser();
        private IDistributeParcels distributeparcels;
        private List<Department> departments = new List<Department>();

        public ParcelsController Build()
        {
            if (departments.Count > 0)
            {
                A.CallTo(() => repostiory.GetAllDepartments()).Returns(departments);

            }
            distributeparcels = new DistributeParcels(repostiory, parser);
            return new ParcelsController(distributeparcels) {Request = new HttpRequestMessage()};
        }

        public ParcelsControllerBuilder Using(IRepository repostiory)
        {
            this.repostiory = repostiory;
            return this;
        }
        public ParcelsControllerBuilder WithDepartments(List<Department> departments)
        {
            this.departments = departments;
            return this;
        }

    }
}