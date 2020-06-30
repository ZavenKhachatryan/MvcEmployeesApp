using MvcEmployeesApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using MyModels;
using DataAccessLayer;
using MvcEmployeesApp.Filters;

namespace MvcEmployeesApp.Controllers
{
    //[Authentication]
    public class HomeController : Controller
    {
        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            var emp = Db.SelectEmp(model.SearchBy, model.SearchValue, model.OrderBy);
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
            if (emp.Id == null)
                Db.Add(emp);
            else
                Db.Edit(emp);
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