using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels;

namespace DataAccessLayer
{
    static public class Db
    {
        static DataContext data;

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
        static public void Remove(Employee emp)
        {
            using (data = new DataContext())
            {
                data.RemoveEmp(emp.Id);
                data.SaveChanges();
            }
        }
    }
}
