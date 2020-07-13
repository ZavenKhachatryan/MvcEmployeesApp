using DataAccessLayer;
using Exceptions;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;

namespace MvcEmployeesApp.Areas.Areas.Controllers
{
    public class EmployeesApiController : ApiController
    {
        private IDataAccess dataAccess;
        public EmployeesApiController(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public IHttpActionResult GetEmployees([FromUri] SearchModel model)
        {
            try
            {
                IEnumerable<Employee> emps = dataAccess.SelectFilteredEmployees(model);
                PaginationModel paginationModel = emps.GetPaginationModel(model.PageNumber);
                return Ok(paginationModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    Employee employee = dataAccess.GetEmployeeById(id);
                    return Ok(employee);
                }

                return Ok(new Employee());
            }
            catch (DatabaseException ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Edit(Employee emp)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("One Or More Fields Are Filled Incorrectly");

                Employee editedEmployee = dataAccess.Edit(emp);

                if (emp.Contains(editedEmployee))
                    return Ok(editedEmployee);

                return BadRequest("Employee Data Wasn't Edited");
            }
            catch (DatabaseException ex)
            {
                return InternalServerError(ex);
            }
            catch (ExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult Remove(int? id)
        {
            Employee employee = dataAccess.GetEmployeeById(id);

            if (employee == null)
                return BadRequest("Employee Was Not Found");

            return Ok(employee);
        }

        [HttpPost]
        public IHttpActionResult Remove(Employee emp)
        {
            bool isRemovedEmployee = dataAccess.Remove(emp);

            return Ok(isRemovedEmployee);
        }

        public IHttpActionResult GetDetails(int? id)
        {
            try
            {
                Employee employee = dataAccess.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (DatabaseException ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
