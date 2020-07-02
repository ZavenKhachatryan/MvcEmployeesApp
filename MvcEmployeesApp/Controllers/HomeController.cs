using System.Web.Mvc;
using MyModels;
using DataAccessLayer;
using System.Linq;
using MvcEmployeesApp.Models;

namespace MvcEmployeesApp.Controllers
{
    //[Authentication]
    public class HomeController : Controller
    {
        public ActionResult Index(SearchModel model)
        {
            ViewBag.mod = model;
            var emp = Db.SelectEmp(model);
            int pageSize = 6;
            IQueryable<Employee> employeesPerPages = emp.Skip((model.Page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = model.Page, PageSize = pageSize, TotalItems = emp.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Employees = employeesPerPages };
            return View(ivm);
        }

        [HttpGet]
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
            if (!ModelState.IsValid)
            {
                ViewBag.ErrMessage = "Tabs Are Filled Incorrectly";
                return View(emp);
            }

            Employee editedEmployee = Db.Edit(emp);

            if (!emp.Contains(editedEmployee))
                ViewBag.ErrMessage = "Such Email Address Or/And Phone Number Already Exists";

            if (emp.Id != null)
                ViewBag.completeMessage = "Employee Data Successfully Edited";
            else
                ViewBag.completeMessage = "Employee Data Successfully Added";

            return View(emp);
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