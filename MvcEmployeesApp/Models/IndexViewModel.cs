using System.Linq;
using MyModels;

namespace MvcEmployeesApp.Models
{
    public class IndexViewModel
    {
        public IQueryable<Employee> Employees { get; set; }
        public PageInfo PageInfo { get; set; } = new PageInfo();
    }
}