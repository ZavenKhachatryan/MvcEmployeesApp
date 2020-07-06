using System.Collections.Generic;
using System.Linq;
using MyModels;

namespace MvcEmployeesApp.Models
{
    public class PaginationModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public PageInfo PageInfo { get; set; } = new PageInfo();
    }
}