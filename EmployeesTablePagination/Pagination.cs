using MvcEmployeesApp.Models;
using System.Text;
using System.Web.Mvc;
using MyModels;
namespace MvcEmployeesApp
{
    public static class Pagination
    {
        public static MvcHtmlString PageLinks(PageInfo pageInfo, SearchModel mod)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", $"/Home/Index/?searchBy={mod.SearchBy}&searchValue={mod.SearchValue}&orderBy={mod.OrderBy}&page={i.ToString()}");
                tag.InnerHtml = i.ToString();
                if (i == mod.Page)
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
