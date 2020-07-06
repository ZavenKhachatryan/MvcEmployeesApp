using DataAccessLayer;
using System;
using System.Linq;

namespace MvcEmployeesApp.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalItems { get; set; }/* = Db.SelectEmployees().Count();*/
        public int TotalPages 
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}