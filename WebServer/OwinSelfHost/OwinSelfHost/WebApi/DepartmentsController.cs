using System;
using System.Collections.Generic;
using System.Web.Http;
using OwinSelfHost.Domain;
using OwinSelfHost.Repository;

namespace OwinSelfHost.WebApi
{
    public class DepartmentsController : ApiController
    {
        private readonly IRepository repository;
        public DepartmentsController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET api/departments 
        public IEnumerable<Department> Get()
        {
            return repository.GetAllDepartments();
        }

        // GET api/departments/name 
        public Department Get(string name)
        {
            return repository.GetDepartment(name);
        }

        // POST api/departments 
        public void Post([FromBody]Department department)
        {
            department.CreatedAt = DateTime.Now;
            repository.AddDepartment(department);
        }

       // DELETE api/departments/5 
        public void Delete(int id)
        {
        }
    }
}