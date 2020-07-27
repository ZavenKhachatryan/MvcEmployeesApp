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
            IEnumerable<Employee> emps = dataAccess.SelectFilteredEmployees(model);
            PaginationModel paginationModel = emps.GetPaginationModel(model.PageNumber);
            return Ok(paginationModel);
        }

        [HttpGet]
        public IHttpActionResult Edit(int? id)
        {
            if (id != null)
            {
                Employee employee = dataAccess.GetEmployeeById(id);
                return Ok(employee);
            }

            return Ok(new Employee());
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
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}