using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using SmartKitchen3.Models;
using SmartKitchen3.Utils;

namespace SmartKitchen3.Controllers
{
    public class UserController : Controller
    {
        ReportEventController report = new ReportEventController();

        private ApplicationDbContext context = new ApplicationDbContext();
        private static readonly int COOKIE_EXPIRATION_TIME = 180;

        // Login management. If the user login is cached redirect to homepage else, display login page.
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {

            ViewBag.Error = "Please Register / Log In";

            if (IsEmailExist(Cookies.Get("ActiveUser")))
            {
                return RedirectToAction("RecipesSelection", "Home");
            }
            return View();
        }

        // If a user is trying to login, validate the account and redirect to homepage.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(User inputUser)
        {

            var user = new User
            {
                Email = inputUser.Email,
                Password = inputUser.Password
            };

            #region Email Already Exist 
            var isExist = IsEmailExist(user.Email);
            if (!isExist)
            {
                ViewBag.Error = "Could not find a Kitchen that is assosiate with " + user.Email + ". Please register a new Smart Kitchen.";
                return View();
            }
            #endregion

            #region  Password Match 
            var isValidUser = IsPasswordMatch(user.Email, Crypto.Hash(user.Password));

            if (isValidUser)
            {
                Cookies.Set("ActiveUser", user.Email, COOKIE_EXPIRATION_TIME);
                report.UserAction(0, GetUserByEmail(user.Email).UserId);
                return RedirectToAction("RecipesSelection", "Home");

            }
            else
            {
                ViewBag.Error = "Wrong password for " + user.Email + ".";
                return View();
            }
            #endregion

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult FacebookLogin()
        {

            string username = Request.Headers.Get("sk_username");

            #region Email Not Exist 
            var isExist = IsEmailExist(username);
            if (!isExist)
            {
                ViewBag.Error = "Could not find a Kitchen that is assosiate with " + username + ". Please register a new Smart Kitchen.";
                //var responseString = "Could not find a Kitchen that is assosiate with this username. Please register a new Smart Kitchen";
                //Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                //await Response.WriteAsync(responseString, Encoding.UTF8);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }
            #endregion
            else
            {
                Cookies.Set("ActiveUser", username, COOKIE_EXPIRATION_TIME);

                Response.StatusCode = (int)HttpStatusCode.OK;

                report.UserAction(0, GetUserByEmail(username).UserId);
                return Json(new { redirectToUrl = Url.Action("RecipesSelection", "Home") }, JsonRequestBehavior.AllowGet);
            }

            //return View();
        }

        // Register a new user after registering a new kitchen
        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserRegistration(string KitchenId)
        {
            var Calendars = context.Calendar.ToList();
            ViewBag.List = new SelectList(Calendars, "CalendarId", "Name", "0");

            ViewBag.KitchenId = KitchenId;

            return View();
        }

        // Kitchen registration
        [HttpGet]
        [AllowAnonymous]
        public ActionResult KitchenRegistration()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserSelectCalendars()
        {
            ViewBag.Calendars = context.Calendar.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult KitchenRegistration(Kitchen inputKitchen)
        {

            var kitchen = new Kitchen
            {
                KitchenId = inputKitchen.KitchenId,
                Name = inputKitchen.Name,
                LocationLatitudes = inputKitchen.LocationLatitudes,
                LocationLongitude = inputKitchen.LocationLongitude
            };

            // Model Validation 
            if (ModelState.IsValid)
            {
                context.Kitchens.Add(kitchen);
                context.SaveChanges();

                return RedirectToAction("UserRegistration", new { KitchenId = kitchen.KitchenId });
            }

            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult UserRegistration(User inputUser, Object KitchenId)
        {

            var user = new User
            {
                FirstName = inputUser.FirstName,
                LastName = inputUser.LastName,
                Email = inputUser.Email,
                Password = inputUser.Password,
                KitchenId = inputUser.KitchenId

            };

            // Model Validation 
            if (ModelState.IsValid)
            {

                #region Kitchen Not Exist 
                //string KitchenId = (string)this.RouteData.Values["KitchenId"];

                var kitchen = GetKitchenById(user.KitchenId);

                if (kitchen == null)
                {
                    ModelState.AddModelError("KitchenNotExists", "Kitchen does not exist, please create a new kitchen");
                    return View(user);
                }

                #endregion

                #region Email Already Exist 
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExists", "Email already exists");
                    return View(user);
                }
                #endregion

                #region  Password Hashing 
                user.Password = Crypto.Hash(user.Password);
                #endregion

                #region Save to Database

                user.Kitchen = kitchen;

                context.User.Add(user);
                context.SaveChanges();

                #endregion

                Cookies.Set("ActiveUser", user.Email, COOKIE_EXPIRATION_TIME);
                report.UserAction(1, GetUserByEmail(user.Email).UserId);
                report.UserAction(0, GetUserByEmail(user.Email).UserId);
                return RedirectToAction("RecipesSelection", "Home");

            }
            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Cookies.Remove("ActiveUser");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AddAdminBar()
        {

            if (IsAdmin(Cookies.Get("ActiveUser")))
                return Json(new { Admin = "1" }, JsonRequestBehavior.AllowGet);
            return Json(new { Admin = "0" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult NotAuthorized()
        {
            return View();
        }

        [NonAction]
        public bool IsEmailExist(string email)
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

        [NonAction]
        public bool IsPasswordMatch(string email, string pass)
        {
            var v = context.User.Where(a => a.Email == email).First();
            return v.Password == pass;

        }

        [NonAction]
        public bool IsAdmin(string email)
        {
            if (email == null) return false;
            User v = null;

            try
            {
                v = context.User.Where(a => a.Email == email).First();
            }
            catch
            {
                return false;
            }
            return v.Admin;

        }

        [NonAction]
        public Kitchen GetKitchenById(int kitchenId)
        {
            Kitchen v = null;

            try
            {
                v = context.Kitchens.Where(a => a.KitchenId == kitchenId).First();
            }
            catch
            {
            }
            return v;

        }

        public User GetUserByEmail(string email)
        {
            User v = null;

            try
            {
                v = context.User.Where(a => a.Email == email).First();
            }
            catch
            {
            }
            return v;

        }
    }

}