using MvcEmployeesApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace MvcEmployeesApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            List<Employee> empList = new List<Employee>();
            using (DataContext data = new DataContext())
            {
                List<Employee> employees = data.Employees.ToList();
                if (model.OrderBy == "ascId" || model.OrderBy == null)
                    employees = data.Employees.OrderBy(e => e.Id).ToList();
                else if (model.OrderBy == "ascFirst")
                    employees = data.Employees.OrderBy(e => e.FirstName).ToList();
                else if (model.OrderBy == "ascLast")
                    employees = data.Employees.OrderBy(e => e.LastName).ToList();
                else if (model.OrderBy == "ascAge")
                    employees = data.Employees.OrderBy(e => e.Age).ToList();
                else if (model.OrderBy == "ascPosition")
                    employees = data.Employees.OrderBy(e => e.Position).ToList();
                
                else if (model.OrderBy == "descId" || model.OrderBy == null)
                    employees = data.Employees.OrderByDescending(e => e.Id).ToList();
                else if (model.OrderBy == "descFirst")
                    employees = data.Employees.OrderByDescending(e => e.FirstName).ToList();
                else if (model.OrderBy == "descLast")
                    employees = data.Employees.OrderByDescending(e => e.LastName).ToList();
                else if (model.OrderBy == "descAge")
                    employees = data.Employees.OrderByDescending(e => e.Age).ToList();
                else if (model.OrderBy == "descPosition")
                    employees = data.Employees.OrderByDescending(e => e.Position).ToList();

                if (string.IsNullOrEmpty(model.SearchValue))
                {
                    data.Dispose();
                    return View(employees);
                }

                foreach (var emp in employees)
                {
                    if (model.SearchBy == "FirstName" && model.SearchValue == emp.FirstName)
                        empList.Add(emp);
                    if (model.SearchBy == "Id" && model.SearchValue == emp.Id.ToString())
                        empList.Add(emp);
                    if (model.SearchBy == "LastName" && model.SearchValue == emp.LastName)
                        empList.Add(emp);
                    if (model.SearchBy == "Age" && model.SearchValue == emp.Age.ToString())
                        empList.Add(emp);
                    if (model.SearchBy == "Position" && model.SearchValue == emp.Position)
                        empList.Add(emp);
                }
            }

            return View(empList);
        }

        public ActionResult AddEdit(int? id)
        {
            if (id != null)
            {
                Employee employee = GetEmployeeWithId(id);
                return View(employee);
            }
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult AddEdit(Employee employee)
        {
            if (!ModelState.IsValidField("FirstName"))
                ViewBag.mfn = "Tab weren't filled";
            if (!ModelState.IsValidField("LastName"))
                ViewBag.mln = "Tab weren't filled";
            if (!ModelState.IsValidField("Age"))
                ViewBag.ma = "Tab weren't filled";
            if (!ModelState.IsValidField("Position"))
                ViewBag.mp = "Tab weren't filled";

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Not all tabs were filled";
                return View(employee);
            }
            using (DataContext data = new DataContext())
            {
                if (employee.Id != null)
                {
                    data.Entry(employee).State = EntityState.Modified;
                    data.SaveChanges();
                }
                else
                {
                    data.Employees.Add(employee);
                    data.SaveChanges();
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int? id)
        {
            Employee employee = GetEmployeeWithId(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Remove(Employee emp)
        {
            using (DataContext data = new DataContext())
            {
                Employee employee = data.Employees.FirstOrDefault(d => d.Id == emp.Id);
                if (employee != null)
                {
                    data.Entry(employee).State = EntityState.Deleted;
                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            Employee employee = GetEmployeeWithId(id);
            return View(employee);
        }
        private bool IsNull(Employee e)
        {
            return e.FirstName == null || e.LastName == null || e.Age == null || e.Position == null;
        }
        private Employee GetEmployeeWithId(int? id)
        {
            Employee employee = new Employee();
            using (DataContext data = new DataContext())
                employee = data.Employees.FirstOrDefault(x => x.Id == id);

            return employee;
        }
    }
}