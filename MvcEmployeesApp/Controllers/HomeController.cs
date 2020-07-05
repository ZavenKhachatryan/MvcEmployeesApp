using DataAccessLayer;
using Exceptions;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcEmployeesApp.Controllers
{
    //[Authentication]
    public class HomeController : Controller
    {
        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            IQueryable<Employee> emps = Db.SortedEmployees(model);
            PaginationModel paginationModel = emps.GetPaginationModel(model.PageNumber);
            return View(paginationModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Btn = "Go Back";

            if (id != null)
            {
                Employee employee = Db.GetEmployeeById(id);
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
                    throw new ValidationException("One Or More Fields Are Filled Incorrectly");

                Employee editedEmployee = Db.Edit(emp);

                if (emp.Contains(editedEmployee))
                {
                    if (emp.Id != null)
                        ViewBag.CompleteMessage = "Employee Data Is Successfully Edited";

                    if (emp.Id == null)
                        ViewBag.CompleteMessage = "Employee Data Is Successfully Added";

                    ViewBag.Btn = "Ok";
                }

                return View(editedEmployee);
            }
            catch (DatabaseException ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }
            catch (ExistException ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }
            catch(ValidationException ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }

            ViewBag.Btn = "Go Back";
            return View(emp);
        }

        [HttpGet]
        public ActionResult Remove(int? id)
        {
            Employee employee = Db.GetEmployeeById(id);

            if (employee != null)
                ViewBag.WasFoundMessage = "Employee Was Found";

            if (employee == null)
                ViewBag.ErrMessage = "Employee Was Not Found";

            ViewBag.Btn = "Go Back";
            ViewBag.Quetion = "Do you want to remove an employee from the database ?";

            return View(employee);
        }

        [HttpPost]
        public ActionResult Remove(Employee emp)
        {
            bool isRemovedEmployee = Db.Remove(emp);

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

            return View(new Employee());
        }

        public ActionResult Details(int? id)
        {
            Employee employee = Db.GetEmployeeById(id);
            return View(employee);
        }
    }
}