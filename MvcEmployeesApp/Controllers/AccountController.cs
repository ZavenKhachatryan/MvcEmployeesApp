using MvcEmployeesApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcEmployeesApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginPass());
        }

        [HttpPost]
        public ActionResult Login(LoginPass model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Massage = "Incorrect Login or Password";
                return View(model);
            }

            using (DataContext data = new DataContext())
            {
                LoginPass login = data.LoginPasses.Where(l => l.Logn == model.Logn && l.Pass == model.Pass).FirstOrDefault();
                
                if (login == null)
                {
                    ViewBag.Massage = "Incorrect Login or Password";
                    return View(model);
                }
                Session["Logn"] = login.Logn;
            }
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new LoginPass());
        }

        [HttpPost]
        public ActionResult SignIn(LoginPass loginPass)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Not all tabs were filled or filed incorrect";
                return View(loginPass);
            }

            using (DataContext data = new DataContext())
            {
                    data.LoginPasses.Add(loginPass);
                    data.SaveChanges();
            }

            return RedirectToAction("Login");
        }

    }
}