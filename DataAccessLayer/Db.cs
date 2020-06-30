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

        static public IQueryable<Employee> SelectEmp(string searchBy, string searchValue, string orderBy)
        {
            data = new DataContext();

            IQueryable<Employee> employees = data.Employees;

            if (orderBy == "ascId" || orderBy == null)
                employees = employees.OrderBy(e => e.Id);
            else if (orderBy == "ascFirst")
                employees = employees.OrderBy(e => e.FirstName);
            else if (orderBy == "ascLast")
                employees = employees.OrderBy(e => e.LastName);
            else if (orderBy == "ascAge")
                employees = employees.OrderBy(e => e.Age);

            else if (orderBy == "descId" || orderBy == null)
                employees = employees.OrderByDescending(e => e.Id);
            else if (orderBy == "descFirst")
                employees = employees.OrderByDescending(e => e.FirstName);
            else if (orderBy == "descLast")
                employees = employees.OrderByDescending(e => e.LastName);
            else if (orderBy == "descAge")
                employees = employees.OrderByDescending(e => e.Age);

            if (string.IsNullOrEmpty(searchValue))
            {
                return employees;
            }

            foreach (var emp in employees)
            {
                if (searchBy == "Id")
                    employees = employees.Where(e => e.Id.ToString() == searchValue);
                else if (searchBy == "FirstName")
                    employees = employees.Where(e => e.FirstName == searchValue);
                else if (searchBy == "LastName")
                    employees = employees.Where(e => e.LastName == searchValue);
                else if (searchBy == "Age")
                    employees = employees.Where(e => e.Age.ToString() == searchValue);
            }

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
        static public Employee GetEmployeeById(int? id)
        {
            Employee employee = new Employee();
            using (DataContext data = new DataContext())
                employee = data.Employees.FirstOrDefault(x => x.Id == id);

            return employee;
        }
    }
}
