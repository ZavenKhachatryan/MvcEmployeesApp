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
            var emp = Db.SelectEmp("FIrstName", "Zaven");
            return View(emp);
        }

        public ActionResult Edit(int? id)
        {
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            Db.Add(emp);
            return View(emp);
        }

        public ActionResult Remove(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Remove()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            return View();
        }
    }
}