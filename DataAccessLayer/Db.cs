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

        static public IQueryable<Employee> SelectEmp(SearchModel model)
        {
            data = new DataContext();

            IQueryable<Employee> employees = data.Employees;

            if (model.OrderBy == "ascId" || model.OrderBy == null)
                employees = employees.OrderBy(e => e.Id);
            else if (model.OrderBy == "ascFirst")
                employees = employees.OrderBy(e => e.FirstName);
            else if (model.OrderBy == "ascLast")
                employees = employees.OrderBy(e => e.LastName);
            else if (model.OrderBy == "ascAge")
                employees = employees.OrderBy(e => e.Age);

            else if (model.OrderBy == "descId" || model.OrderBy == null)
                employees = employees.OrderByDescending(e => e.Id);
            else if (model.OrderBy == "descFirst")
                employees = employees.OrderByDescending(e => e.FirstName);
            else if (model.OrderBy == "descLast")
                employees = employees.OrderByDescending(e => e.LastName);
            else if (model.OrderBy == "descAge")
                employees = employees.OrderByDescending(e => e.Age);

            if (string.IsNullOrEmpty(model.SearchValue))
            {
                return employees;
            }

            foreach (var emp in employees)
            {
                if (model.SearchBy == "Id")
                    employees = employees.Where(e => e.Id.ToString() == model.SearchValue);
                else if (model.SearchBy == "FirstName")
                    employees = employees.Where(e => e.FirstName == model.SearchValue);
                else if (model.SearchBy == "LastName")
                    employees = employees.Where(e => e.LastName == model.SearchValue);
                else if (model.SearchBy == "Age")
                    employees = employees.Where(e => e.Age.ToString() == model.SearchValue);
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
