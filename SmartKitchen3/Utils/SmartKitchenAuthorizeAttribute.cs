using SmartKitchen3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartKitchen3.Utils
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SmartKitchenAuthorizeAttribute : AuthorizeAttribute
    {

        private ApplicationDbContext context = new ApplicationDbContext();

        //Custom named parameters for annotation
        public string AdminOnly { get; set; }

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (Cookies.Get("ActiveUser") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "Login" })
                );
            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "NotAuthorized" })
                );
            }
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string email = Cookies.Get("ActiveUser");
            var b = IsEmailExist(email);

            //Is user logged in?
            if (b)
                //If user is logged in and we need a custom check:
                if (AdminOnly != null && AdminOnly.ToLower().Equals("true"))
                    return context.User.Where(a => a.Email == email).First().Admin;
            //Returns true or false, meaning allow or deny. False will call HandleUnauthorizedRequest above
            return b;
        }

        private bool IsEmailExist(string email)
        {

            User v = null;

            try
            {
                v = context.User.Where(a => a.Email == email).First();
            }
            catch
            {
                return false;
            }
            return v != null;

        }


    }
}