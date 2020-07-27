using Exceptions;
using MyModels;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        private readonly DataContext data;

        public DataAccess(DataContext data)
        {
            this.data = data;
        }

        public DbRawSqlQuery<Employee> SelectFilteredEmployees(SearchModel model)
        {
            string query = GetQueryString(model);

            DbRawSqlQuery<Employee> employees = data.Database.SqlQuery<Employee>(query);

            return employees;
        }

        private string GetQueryString(SearchModel model)
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

        public Employee Edit(Employee emp)
        {
            bool isExistEmail = data.Employees.Any(e => e.Email == emp.Email && e.Id != emp.Id);
            bool isExistPhone = data.Employees.Any(e => e.Phone == emp.Phone && e.Id != emp.Id);

            if (isExistEmail && isExistPhone)
                throw new ExistException("This Email Addres And Phone Number Already Exists");

            if (isExistEmail)
                throw new ExistException("This Email Address Already Exists");

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

            DbRawSqlQuery<Employee> employee = data.Database.SqlQuery<Employee>($"Select * From Employees WHERE Email = '{emp.Email}'");
            return employee.FirstOrDefault();
        }

        public bool Remove(Employee emp)
        {
            if (emp.Id < 1)
                throw new ArgumentOutOfRangeException();

            data.RemoveEmp(emp.Id);
            data.SaveChanges();
            return !data.Employees.Any(e => e.Email == emp.Email || e.Phone == emp.Phone);
        }

        public Employee GetEmployeeById(int? id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            DbRawSqlQuery<Employee> employee = data.Database.SqlQuery<Employee>($"SELECT * FROM Employees WHERE Id = '{id}'");
            return employee.FirstOrDefault();
        }
    }
}