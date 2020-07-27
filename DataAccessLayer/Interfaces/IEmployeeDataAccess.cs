using MyModels;
using System.Data.Entity.Infrastructure;

namespace DataAccessLayer
{
    public interface IEmployeeDataAccess
    {
        DbRawSqlQuery<Employee> SelectFilteredEmployees(SearchModel model);
        Employee Edit(Employee emp);
        bool Remove(Employee emp);
        Employee GetEmployeeById(int? id);
    }
}