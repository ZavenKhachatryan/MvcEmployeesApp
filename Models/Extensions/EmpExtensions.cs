using Exceptions;
using System;
using System.Linq;

namespace MyModels
{
    public static class EmpExtensions
    {
        public static IQueryable<Employee> SortByModel(this IQueryable<Employee> employees, SearchModel model)
        {
            if (model.OrderBy.Contains("ascId") || model.OrderBy == null)
                employees = employees.OrderBy(e => e.Id);
            else if (model.OrderBy.Contains("ascFirst"))
                employees = employees.OrderBy(e => e.FirstName);
            else if (model.OrderBy.Contains("ascLast"))
                employees = employees.OrderBy(e => e.LastName);
            else if (model.OrderBy.Contains("ascAge"))
                employees = employees.OrderBy(e => e.Age);

            else if (model.OrderBy.Contains("descId") || model.OrderBy == null)
                employees = employees.OrderByDescending(e => e.Id);
            else if (model.OrderBy.Contains("descFirst"))
                employees = employees.OrderByDescending(e => e.FirstName);
            else if (model.OrderBy.Contains("descLast"))
                employees = employees.OrderByDescending(e => e.LastName);
            else if (model.OrderBy.Contains("descAge"))
                employees = employees.OrderByDescending(e => e.Age);

            if (string.IsNullOrEmpty(model.SearchValue))
                return employees;

            if (model.SearchBy == "Id")
                employees = employees.Where(e => e.Id.ToString() == model.SearchValue);
            else if (model.SearchBy == "FirstName")
                employees = employees.Where(e => e.FirstName == model.SearchValue);
            else if (model.SearchBy == "LastName")
                employees = employees.Where(e => e.LastName == model.SearchValue);
            else if (model.SearchBy == "Age")
                employees = employees.Where(e => e.Age.ToString() == model.SearchValue);

            return employees;

        }

        public static bool Contains(this Employee thisEmp, Employee emp)
        {
            return thisEmp.FirstName == emp.FirstName && thisEmp.LastName == emp.LastName && thisEmp.Age == emp.Age && thisEmp.Salary == emp.Salary && thisEmp.Email == emp.Email && thisEmp.Phone == emp.Phone;
        }
    }
}
