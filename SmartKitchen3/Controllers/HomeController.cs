using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartKitchen3.ViewModel;
using SmartKitchen3.Extensions;
using SmartKitchen3.Models;
namespace SmartKitchen3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult RecipesSelection(string search = null)
        {
            var thumbnails = new List<ThumbnailModel>().GetRecipeThumbnail(ApplicationDbContext.Create(), search);
            var count = thumbnails.Count() / 4;
            var model = new List<ThumbnailBoxViewModel>();

            for (int i = 0; i <= count; i++)

            {
                model.Add(new ThumbnailBoxViewModel

                {
                    Thumbnails = thumbnails.Skip(i * 4).Take(4)
                });
            }
            return View(model);




        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewData["Message"] = "A Web App developed as part of web course in the Collage of Management. This app will provide users cooking recommendations.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
       
    }
}