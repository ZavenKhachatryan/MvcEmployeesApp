//using MvcEmployeesApp.Models;
using System.Text;
using System.Web.Mvc;
using MvcEmployeesApp.Models;

namespace MvcEmployeesApp
{
    public static class Pagination
    {
        public static MvcHtmlString PageLinks(PageInfo pageInfo)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("input");
                tag.MergeAttribute("type", "submit");
                tag.MergeAttribute("name", "page");
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
