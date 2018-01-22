using System.Linq;
using OwinSelfHost.Domain;
using OwinSelfHost.Repository;
using Raven.Client.Documents.Session;

namespace OwinSelfHost.Helpers
{
    public static class Extension
    {
        public static void ClearDocuments<T>(this IDocumentSession session)
        {
            var objects = session.Query<T>().ToList();
            while (objects.Any())
            {
                foreach (var obj in objects)
                {
                    session.Delete(obj);
                }

                session.SaveChanges();
                objects = session.Query<T>().ToList();
            }
        }

        public static void AddDepartments(this IRepository repository, params Department[] departments)
        {
            repository.ClearDepartments();
           
            foreach (var department in departments)
            {
                repository.AddDepartment(department);
            }
        }
    }
}
