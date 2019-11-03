using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using SmartKitchen3.ViewModel;
using SmartKitchen3.Models;

namespace SmartKitchen3.Controllers
{
    public class RecipeDetailController : Controller
    {

        private ApplicationDbContext db;

        public RecipeDetailController()
        {

            db = ApplicationDbContext.Create();
        }


        // GET: RecipeDetails

        public ActionResult Index(int id)
        {
            Console.WriteLine("test" + id);
            var recipeModel = db.Recipes.Include(b => b.Category).SingleOrDefault(b => b.RecipeId== id);
 
            Recipeviewmodel_1_ model = new Recipeviewmodel_1_
            {
                RecipeId = recipeModel.RecipeId,
                Category = db.Category.FirstOrDefault(g=>g.CategoryId.Equals(recipeModel.CategoryId)),
                Title = recipeModel.Title,
                CategoryId = recipeModel.CategoryId,
                Instructions = recipeModel.Instructions,
                ImageUrl = recipeModel.ImageUrl,
                VideoUrl = recipeModel.VideoUrl,
                Calories = recipeModel.Calories
               
                
            };
       
             
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
           if(disposing)
            {
                db.Dispose();
            }
        }
    }
}