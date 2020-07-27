using MyModels;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEmployeesApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDataAccess _userDataAccess;
        public UserController(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            User usr = _userDataAccess.GetUser(user);
            if (usr is null)
                return View(user);

            Session["UserName"] = usr.UserName;
            Session["UserPassword"] = usr.UserPassword;

            return Redirect("Home/Index");
        }
    }
}