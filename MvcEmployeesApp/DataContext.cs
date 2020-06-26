using MvcEmployeesApp.Models;
using System.Data.Entity;

namespace MvcEmployeesApp
{
    public class DataContext : DbContext
    {
        public DataContext() : base("employeedbEntities")
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}