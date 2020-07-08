using Exceptions;
using MyModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DataAccessLayer
{
    public static class Db
    {
        static DataContext data;

        public static DbRawSqlQuery<Employee> SelectFilteredEmployees(SearchModel model)
        {
            try
            {
                data = new DataContext();

                string query = GetQueryString(model);

                DbRawSqlQuery<Employee> employees = data.Database.SqlQuery<Employee>(query);

                return employees;
            }
            catch (Exception)
            {
                throw new Exception("Sorry Server Is Not Found. Please Try Later");
            }
        }

        private static string GetQueryString(SearchModel model)
        {
            string query = "select * from Employees";

            if (model.SearchValue != null)
                query += $" where {model.SearchBy} = '{model.SearchValue}'";

            if (model.OrderBy != null)
                query += $" order by {model.OrderBy}";

            if (model.AscDesc != null)
                query += $" {model.AscDesc}";

            return query;
        }

        public static Employee Edit(Employee emp)
        {
            using (data = new DataContext())
            {
                try
                {
                    bool isExistEmail = data.Employees.Any(e => e.Email == emp.Email && e.Id != emp.Id);
                    bool isExistPhone = data.Employees.Any(e => e.Phone == emp.Phone && e.Id != emp.Id);

                    if (isExistEmail && isExistPhone)
                        throw new ExistException("This Email Addres And Phone Number Already Exists");

                    if (isExistEmail)
                        throw new ExistException("This Email Address Already Exists ");

                    if (isExistPhone)
                        throw new ExistException("This Phone Number Already Exists");

                    if (emp.Id != null)
                    {
                        data.EditEmp(emp.Id, emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                        data.SaveChanges();
                    }

                    if (emp.Id == null)
                    {
                        data.AddEmp(emp.FirstName, emp.LastName, emp.Age, emp.Salary, emp.Email, emp.Phone);
                        data.SaveChanges();
                    }

                    return data.Employees.FirstOrDefault(e => e.Email == emp.Email || e.Phone == emp.Phone);
                }
                catch (Exception)
                {
                    throw new DatabaseException("Sorry Database Not Found. Please Try Later");
                }
            }
        }

        public static bool Remove(Employee emp)
        {
            using (data = new DataContext())
            {
                data.RemoveEmp(emp.Id);
                data.SaveChanges();
                return !data.Employees.Any(e => e.Email == emp.Email || e.Phone == emp.Phone);
            }
        }

        public static Employee GetEmployeeById(int? id)
        {
            Employee employee = new Employee();
            using (DataContext data = new DataContext())
            {
                try
                {
                    employee = data.Employees.FirstOrDefault(x => x.Id == id);
                    return employee;
                }
                catch
                {
                    throw new DatabaseException("Sorry Database Not Found. Please Try Later");
                }
            }
        }
    }
}