using System.Collections.Generic;
using System.Net.Http;
using FakeItEasy;
using OwinSelfHost.Domain;
using OwinSelfHost.Repository;
using OwinSelfHost.WebApi;

namespace OwinSelfHost.Specs.Builders
{
    public class DepartmentsControllerBuilder
    {
        private IRepository repostiory = A.Fake<IRepository>();
        private List<Department> departments = new List<Department>();
        private Department department = null;
        
        public DepartmentsController Build()
        {
            if (departments.Count > 0)
            {
                A.CallTo(() => repostiory.GetAllDepartments()).Returns(departments);

            }
            if (department != null)
            {
                A.CallTo(() => repostiory.GetDepartment(department.Name)).Returns(department);
            }
            var controller = new DepartmentsController(repostiory) {Request = new HttpRequestMessage()};
            return controller;
        }

        public DepartmentsControllerBuilder Using(IRepository repostiory)
        {
            this.repostiory = repostiory;
            return this;
        }
        public DepartmentsControllerBuilder WithDepartments(List<Department> departments)
        {
            this.departments = departments;
            return this;
        }

        public DepartmentsControllerBuilder WithDepartment(Department department)
        {
            this.department = department;
            return this;
        }
    }
}