﻿using Exceptions;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcEmployeesApp
{
    public static class Pagination
    {
        public static PaginationModel GetPaginationModel(this IEnumerable<Employee> emps, int pageNumber)
        {
            //try
            //{
            IEnumerable<Employee> employeesPerPages = emps.Skip((pageNumber - 1) * 5).Take(5);
            PageInfo pageInfo = new PageInfo { PageNumber = pageNumber, TotalItems = emps.Count() };
            PaginationModel pm = new PaginationModel { PageInfo = pageInfo, Employees = employeesPerPages };
            return pm;
            //}
            //catch (Exception)
            //{
            //    throw new Exception("Sorry Server Is Not Found. Please Try Later");
            //}

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