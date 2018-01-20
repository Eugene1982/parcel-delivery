using System.Collections.Generic;
using System.Linq;
using OwinSelfHost.Domain;
using Raven.Client.Documents.Session;

namespace OwinSelfHost.Repository
{
    public class Repository : IRepository
    {
        public List<Department> GetAllDepartments()
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                return session
                    .Query<Department>()
                    .ToList();
            }
        }
    }
}
