using MvcEmployeesApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcEmployeesApp.Filters;

namespace MvcEmployeesApp.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        public ActionResult Index(SearchModel model)
        {

            return View();
        }

        public ActionResult Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            return View();
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