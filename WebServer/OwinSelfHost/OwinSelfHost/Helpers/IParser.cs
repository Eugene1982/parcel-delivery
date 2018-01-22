using OwinSelfHost.Domain;
using System.Collections.Generic;

namespace OwinSelfHost.Helpers
{
    public interface IParser
    {
        IList<Parcel> Parse(string xml);
    }
}