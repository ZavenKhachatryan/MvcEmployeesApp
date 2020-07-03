using System.Web.Mvc;
using MyModels;
using DataAccessLayer;
using System.Linq;
using MvcEmployeesApp.Models;
using System;

namespace MvcEmployeesApp.Controllers
{
    //[Authentication]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            var emp = Db.SelectEmp(model);
            IQueryable<Employee> employeesPerPages = emp.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = model.Page, PageSize = model.PageSize, TotalItems = emp.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Employees = employeesPerPages };
            return View(ivm);
        }

        [HttpPost]
        public ActionResult Index(SearchModel model , IndexViewModel ivm)
        {
            ViewBag.mod = model;
            var emp = Db.SelectEmp(model);
            IQueryable<Employee> employeesPerPages = emp.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = model.Page, PageSize = model.PageSize, TotalItems = emp.Count() };
            ivm = new IndexViewModel { PageInfo = pageInfo, Employees = employeesPerPages };
            return View(ivm);
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
            if (!ModelState.IsValid)
            {
                ViewBag.ErrMessage = "One Or More Fields Are Filled Incorrectly";
                return View(emp);
            }

            Employee editedEmployee = Db.Edit(emp);

            if (!emp.Contains(editedEmployee))
            {
                ViewBag.ErrMessage = "This Email Address And/Or Phone Number Already Exists";
                ViewBag.Btn = "Go Back";
            }

            else
            {
                if (emp.Id != null)
                    ViewBag.CompleteMessage = "Employee Data Is Successfully Edited";
                else
                    ViewBag.CompleteMessage = "Employee Data Is Successfully Added";
                ViewBag.Btn = "Ok";
            }

            return View(editedEmployee);
        }

        public ActionResult Remove(int? id)
        {
            Employee employee = Db.GetEmployeeById(id);

            if (employee != null)
                ViewBag.WasFoundMessage = "Employee Was Found";
            else
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
            else
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