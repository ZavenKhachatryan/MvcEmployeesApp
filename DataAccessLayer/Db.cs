using System.ComponentModel;
using System.Data;
using System.Linq;
using MyModels;

namespace DataAccessLayer
{
    static public class Db
    {
        static DataContext data;

        static public IQueryable<Employee> SelectEmp(SearchModel model)
        {
            data = new DataContext();

            IQueryable<Employee> employees = data.Employees;

            if (model.OrderBy.Contains("ascId") || model.OrderBy == null)
                employees = employees.OrderBy(e => e.Id);
            else if (model.OrderBy.Contains("ascFirst"))
                employees = employees.OrderBy(e => e.FirstName);
            else if (model.OrderBy.Contains("ascLast"))
                employees = employees.OrderBy(e => e.LastName);
            else if (model.OrderBy.Contains("ascAge"))
                employees = employees.OrderBy(e => e.Age);

            else if (model.OrderBy.Contains("descId") || model.OrderBy == null)
                employees = employees.OrderByDescending(e => e.Id);
            else if (model.OrderBy.Contains("descFirst"))
                employees = employees.OrderByDescending(e => e.FirstName);
            else if (model.OrderBy.Contains("descLast"))
                employees = employees.OrderByDescending(e => e.LastName);
            else if (model.OrderBy.Contains("descAge"))
                employees = employees.OrderByDescending(e => e.Age);

            if (string.IsNullOrEmpty(model.SearchValue))
                return employees;

            if (model.SearchBy == "Id")
                employees = employees.Where(e => e.Id.ToString() == model.SearchValue);
            else if (model.SearchBy == "FirstName")
                employees = employees.Where(e => e.FirstName == model.SearchValue);
            else if (model.SearchBy == "LastName")
                employees = employees.Where(e => e.LastName == model.SearchValue);
            else if (model.SearchBy == "Age")
                employees = employees.Where(e => e.Age.ToString() == model.SearchValue);

            return employees;
        }

        static public Employee Edit(Employee emp)
        {
            using (data = new DataContext())
            {
                bool isExistEmail = data.Employees.Any(e => e.Email == emp.Email && e.Id != emp.Id);
                bool isExistPhone = data.Employees.Any(e => e.Phone == emp.Phone && e.Id != emp.Id);

                if (!isExistEmail && !isExistPhone)
                {
                    if (emp.Id != null)
                    {
                        data.EditEmp(emp.Id, emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                        data.SaveChanges();
                    }
                    else
                    {
                        data.AddEmp(emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                        data.SaveChanges();
                    }
                }
                return data.Employees.FirstOrDefault(e => e.Email == emp.Email && e.Phone == emp.Phone);
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
        static public bool Contains(this Employee thisEmp, Employee emp)
        {
            return thisEmp.FirstName == emp.FirstName && thisEmp.LastName == emp.LastName && thisEmp.Age == emp.Age && thisEmp.Salary == emp.Salary && thisEmp.Email == emp.Email && thisEmp.Phone == emp.Phone;
        }
    }
}