using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer
{
    public class Db
    {
        DataContext data;

        public void Add(Employee emp)
        {
            using (data = new DataContext())
            {
                data.AddEmp(emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                data.SaveChanges();
            }
        }
        public void Edit(Employee emp)
        {
            using (data = new DataContext())
            {
                data.EditEmp(emp.Id, emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                data.SaveChanges();
            }
        }
        public void Remove(Employee emp)
        {
            using (data = new DataContext)
            {
                data.RemoveEmp(emp.Id);
                data.SaveChanges();
            }
        }
    }
}
