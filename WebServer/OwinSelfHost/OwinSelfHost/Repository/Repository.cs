using System.Collections.Generic;
using System.Linq;
using OwinSelfHost.Domain;
using OwinSelfHost.Helpers;
using Raven.Client.Documents.Session;

namespace OwinSelfHost.Repository
{
    public class Repository : IRepository
    {
        private readonly IDocumentSession session;

        public Repository(IDocumentSession session)
        {
            this.session = session;
        }

        public IList<Department> GetAllDepartments()
        {
            return session
                .Query<Department>()
                .ToList();
        }

        public Department GetDepartment(string name)
        {
            return session
                .Query<Department>().FirstOrDefault(d => d.Name == name);

        }

        public void AddDepartment(Department department)
        {
            session.Store(department);
            session.SaveChanges();
        }

        public void DeleteDepartment(string name)
        {
            Department department = GetDepartment(name);
            session.Delete(department);
            session.SaveChanges();
        }

        public void ClearDepartments()
        {
            session.ClearDocuments<Department>();
        }
    }
}