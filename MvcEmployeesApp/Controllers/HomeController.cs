using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using MyModels;
using DataAccessLayer;
using MvcEmployeesApp.Filters;
using System;

namespace MvcEmployeesApp.Controllers
{
    //[Authentication]
    public class HomeController : Controller
    {
        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            var emp = Db.SelectEmp(model);
            return View(emp);
        }

        public ActionResult Edit(int? id)
        {
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
            Dictionary<string, string> errorList = new Dictionary<string, string>();

            if (emp.Id == null)
            {
                Db.Add(emp, out errorList);
            }
            else
            {
                Db.Edit(emp, out errorList);
            }
            if (errorList != null)
            {
                ViewBag.ExEmail = errorList["Email"];
                ViewBag.ExPhone = errorList["Phone"];
                return View(emp);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int? id)
        {
            Employee employee = Db.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Remove(Employee employee)
        {
            Db.Remove(employee.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            Employee employee = Db.GetEmployeeById(id);
            return View(employee);
        }
    }
}