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

        // GET api/values 
        public IEnumerable<Department> Get()
        {
            return repository.GetAllDepartments();
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}