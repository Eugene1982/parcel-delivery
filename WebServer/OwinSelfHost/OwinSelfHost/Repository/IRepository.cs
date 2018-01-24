using System.Collections.Generic;
using OwinSelfHost.Domain;

namespace OwinSelfHost.Repository
{
    public interface IRepository
    {
        IList<Department> GetAllDepartments();

        Department GetDepartment(string name);

        void AddDepartment(Department department);

        void DeleteDepartment(string name);

        void ClearDepartments();

    }
}