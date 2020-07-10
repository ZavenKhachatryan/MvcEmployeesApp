using DataAccessLayer;
using Exceptions;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcEmployeesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataAccess dataAccess;
        public HomeController(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public ActionResult Index(SearchModel model)
        {
            try
            {
                ViewBag.mod = model;
                IEnumerable<Employee> emps = dataAccess.SelectFilteredEmployees(model);
                PaginationModel paginationModel = emps.GetPaginationModel(model.PageNumber);
                return View(paginationModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Btn = "Go Back";
            try
            {
                if (id != null)
                {
                    Employee employee = dataAccess.GetEmployeeById(id);
                    return View(employee);
                }
            }
            catch (DatabaseException ex)
            {
                ViewBag.dbErrMessage = ex.Message;
                ViewBag.Btn = "Ok";
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

                Employee editedEmployee = dataAccess.Edit(emp);

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
            catch (DatabaseException ex)
            {
                ViewBag.dbErrMessage = ex.Message;
                ViewBag.Btn = "Ok";
                return View();
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
            Employee employee = dataAccess.GetEmployeeById(id);

            if (employee == null)
            {
                ViewBag.ErrMessage = "Employee Was Not Found";
                ViewBag.Btn = "Ok";
                return View();
            }

            if (employee != null)
                ViewBag.WasFoundMessage = "Employee Was Found";

            ViewBag.Quetion = "Do you want to remove an employee from the database ?";
            ViewBag.Btn = "Go Back";

            return View(employee);
        }

        [HttpPost]
        public ActionResult Remove(Employee emp)
        {
            bool isRemovedEmployee = dataAccess.Remove(emp);

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
                Employee employee = dataAccess.GetEmployeeById(id);
                return View(employee);
            }
            catch (DatabaseException ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }

            return View();
        }
    }
}