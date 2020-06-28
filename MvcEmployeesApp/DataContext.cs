using MvcEmployeesApp.Models;
using System.Data.Entity;

namespace MvcEmployeesApp
{
    public class DataContext : DbContext
    {
        public DataContext() : base("employeedbEntities2")
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LoginPass> LoginPasses { get; set; }
    }
}