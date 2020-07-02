using System.Linq;
using MyModels;

namespace z
{
    public class IndexViewModel
        {
            public IQueryable<Employee> Employees { get; set; }
            public PageInfo PageInfo { get; set; }
        }
}