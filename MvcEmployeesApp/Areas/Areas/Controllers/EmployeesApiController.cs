using DataAccessLayer;
using Exceptions;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

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
            catch (DatabaseException ex)
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
                return BadRequest(ex.Message);
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
                return Ok(editedEmployee);
            }
            catch (DatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult Remove(int? id)
        {
            try
            {
                Employee employee = dataAccess.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(DatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Remove(Employee emp)
        {
            try
            {
                bool isRemovedEmployee = dataAccess.Remove(emp);
                return Ok(isRemovedEmployee);
            }
            catch (DatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
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
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}