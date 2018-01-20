using System.Linq;
using Raven.Client.Documents.Session;

namespace OwinSelfHost.Repository
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
    }
}
