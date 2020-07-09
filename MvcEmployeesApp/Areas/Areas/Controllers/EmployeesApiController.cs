using DataAccessLayer;
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

        public IHttpActionResult GetEmployees([FromUri]SearchModel model)
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
    }
}
