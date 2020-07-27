using DataAccessLayer;
using Exceptions;
using MvcEmployeesApp.Filters;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcEmployeesApp.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        private readonly IEmployeeDataAccess _employeeDataAccess;
        public HomeController(IEmployeeDataAccess dataAccess)
        {
            this._employeeDataAccess = dataAccess;
        }

        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            IEnumerable<Employee> emps = _employeeDataAccess.SelectFilteredEmployees(model);
            PaginationModel paginationModel = emps.GetPaginationModel(model.PageNumber);
            return View(paginationModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Btn = "Go Back";

            if (id != null)
            {
                Employee employee = _employeeDataAccess.GetEmployeeById(id);
                return View(employee);
            }

            return View(new Employee());
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrMessage = "One Or More Fields Are Filled Incorrectly";
                    ViewBag.Btn = "Go Back";
                    return View(emp);
                }

                Employee editedEmployee = _employeeDataAccess.Edit(emp);

                if (emp.Contains(editedEmployee))
                {
                    if (emp.Id != null)
                        ViewBag.CompleteMessage = "Employee Data Is Successfully Edited";

                    if (emp.Id == null)
                        ViewBag.CompleteMessage = "Employee Data Is Successfully Added";

                    ViewBag.Btn = "Ok";
                    return View(editedEmployee);
                }
            }
            catch (ExistException ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }

            ViewBag.Btn = "Go Back";

            return View(emp);
        }

        [HttpGet]
        public ActionResult Remove(int? id)
        {
            try
            {
                Employee employee = _employeeDataAccess.GetEmployeeById(id);

                ViewBag.Quetion = "Do you want to remove an employee from the database ?";
                ViewBag.Btn = "Go Back";

                return View(employee);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ViewBag.ErrMessage = ex.Message;
                ViewBag.Btn = "Ok";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Remove(Employee emp)
        {
            bool isRemovedEmployee = _employeeDataAccess.Remove(emp);

            if (isRemovedEmployee)
            {
                ViewBag.CompleteMessage = "Employee Data Is Successfully Deleted";
                ViewBag.Btn = "Ok";
            }

            if (!isRemovedEmployee)
            {
                ViewBag.ErrMessage = "Employee Data Was Not Deleted";
                ViewBag.Btn = "Go Back";
            }

            return View();
        }

        public ActionResult Details(int? id)
        {
            try
            {
                Employee employee = _employeeDataAccess.GetEmployeeById(id);
                return View(employee);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }

            return View();
        }
    }
}