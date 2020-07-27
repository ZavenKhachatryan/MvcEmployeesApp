using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MvcEmployeesApp.Filters
{
    public class AuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.Session;
            if (user["UserName"] is null && user["UserPassword"] is null)
                filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.Session;
            if (user["UserName"] is null && user["UserPassword"] is null)
            {
                filterContext.Result = new RedirectToRouteResult
                    (
                        new System.Web.Routing.RouteValueDictionary
                        {
                            { "controller" , "User" } , { "action", "LogIn"}
                        }
                    );
            }
        }
    }
}