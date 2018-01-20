using System.Collections.Generic;
using OwinSelfHost.Domain;

namespace OwinSelfHost.Repository
{
    public interface IRepository
    {
        List<Department> GetAllDepartments();
    }
}