using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels;
using System.Data.Entity.Infrastructure;
namespace DataAccessLayer
{
    static public class Db
    {
        static DataContext data;

        static public IQueryable<Employee> SelectEmp(Employee emp)
        {
            data = new DataContext();
            IQueryable<Employee> employees = data.Employees;
            return employees;
        }

        static public void Add(Employee emp)
        {
            using (data = new DataContext())
            {
                data.AddEmp(emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                data.SaveChanges();
            }
        }
        static public void Edit(Employee emp)
        {
            using (data = new DataContext())
            {
                data.EditEmp(emp.Id, emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                data.SaveChanges();
            }
        }
        static public void Remove(int? id)
        {
            using (data = new DataContext())
            {
                data.RemoveEmp(id);
                data.SaveChanges();
            }
        }
    }
}
