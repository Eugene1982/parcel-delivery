using System;
using Raven.Client.Documents;

namespace OwinSelfHost.Repository
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Company"
            }.Initialize();

            return store;
        }
    }
}
