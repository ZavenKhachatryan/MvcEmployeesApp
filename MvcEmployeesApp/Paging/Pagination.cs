using MvcEmployeesApp.Models;
using MyModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web.Mvc;
using Exceptions;

namespace MvcEmployeesApp
{
    public static class Pagination
    {
        public static PaginationModel GetPaginationModel(this IEnumerable<Employee> emps, int pageNumber)
        {
            IEnumerable<Employee> employeesPerPages = emps.Skip((pageNumber - 1) * 5).Take(5);
            PageInfo pageInfo = new PageInfo { PageNumber = pageNumber, TotalItems = emps.Count() };
            PaginationModel pm = new PaginationModel { PageInfo = pageInfo, Employees = employeesPerPages };
            return pm;
        }

        public static MvcHtmlString PageLinks(PageInfo pageInfo)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("input");
                tag.MergeAttribute("type", "submit");
                tag.MergeAttribute("name", "pageNumber");
                tag.MergeAttribute("value", i.ToString());

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
