using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using OwinSelfHost.Domain;
using OwinSelfHost.Repository;
using OwinSelfHost.Specs.Builders;
using OwinSelfHost.WebApi;
using Xunit;

namespace OwinSelfHost.Specs
{
    public class DepartmentControllerSpec
    {
        public class When_requesting_list_of_departments
        {
            private DepartmentsController controller;
            private readonly IList<Department> result;

            public When_requesting_list_of_departments()
            {
                controller = new DepartmentsControllerBuilder()
                    .WithDepartments(new List<Department>
                    {
                        new Department
                        {
                            Name = "Name1"
                        },
                        new Department
                        {
                            Name = "Name2"
                        }
                    }).Build();
                result = controller.Get().ToList();
            }

            [Fact]
            public void Then_it_should_be_returned()
            {
                result.Count.Should().Be(2);
                result[0].Name.Should().Be("Name1");
                result[1].Name.Should().Be("Name2");
            }
        }

        public class When_requesting_department
        {
            private DepartmentsController controller;
            private readonly Department result;

            public When_requesting_department()
            {
                controller = new DepartmentsControllerBuilder()
                    .WithDepartment(
                        new Department
                        {
                            Name = "Name1"
                        }
                    ).Build();
                result = controller.Get("Name1");
            }

            [Fact]
            public void Then_it_should_be_returned()
            {
                result.Name.Should().Be("Name1");
            }
        }

        public class When_adding_department
        {
            private DepartmentsController controller;
            private readonly Department department;
            private IRepository repostiory = A.Fake<IRepository>();

            public When_adding_department()
            {
                controller = new DepartmentsControllerBuilder()
                    .Using(repostiory).Build();
                department = new Department
                {
                    Name = "Name1"
                };

                controller.Post(department);
            }

            [Fact]
            public void Then_it_should_be_called_once()
            {
                A.CallTo(() => repostiory.AddDepartment(department)).MustHaveHappened(Repeated.Exactly.Once);
            }
        }
    }
}