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

            IQueryable<Employee> employees = data.Employees.SortByModel(model);

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
                return data.Employees.FirstOrDefault(e => e.Email == emp.Email || e.Phone == emp.Phone);
            }
        }

        static public bool Remove(Employee emp)
        {
            using (data = new DataContext())
            {
                data.RemoveEmp(emp.Id);
                data.SaveChanges();
                return !data.Employees.Any(e => e.Email == emp.Email || e.Phone == emp.Phone);
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