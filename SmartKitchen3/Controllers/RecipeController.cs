using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartKitchen3.Models;
using SmartKitchen3.ViewModel;

namespace SmartKitchen3.Controllers
{
    public class RecipeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Recipe
        public ActionResult Index()
        {
            var recipes = db.Recipes.Include(r => r.Category);
            return View(recipes.ToList());
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            var model = new RecipeViewModel
            {
                Recipe = recipe,
                Categorys = db.Category.ToList()
        };
            return View(model);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            var category = db.Category.ToList();
            var model = new RecipeViewModel
            {
                Categorys = category
            };
            return View(model);
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeViewModel recipeVM)
        {
            var recipe = new Recipe
            {
                Title = recipeVM.Recipe.Title,
                CategoryId = recipeVM.Recipe.CategoryId,
                Instructions = recipeVM.Recipe.Instructions,
                ImageUrl = recipeVM.Recipe.ImageUrl,
                VideoUrl = recipeVM.Recipe.VideoUrl,
                Calories = recipeVM.Recipe.Calories,
                LastUpdate = recipeVM.Recipe.LastUpdate,
                Category = recipeVM.Recipe.Category
            };

            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            recipeVM.Categorys = db.Category.ToList();
            return View(recipeVM);
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            var model = new RecipeViewModel
            {
                Recipe = recipe,
                Categorys = db.Category.ToList()
            };
            return View(model);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(RecipeViewModel recipeVM)
        {
            var recipe = new Recipe
            {
                RecipeId=recipeVM.Recipe.RecipeId,
                Title = recipeVM.Recipe.Title,
                CategoryId = recipeVM.Recipe.CategoryId,
                Instructions = recipeVM.Recipe.Instructions,
                ImageUrl = recipeVM.Recipe.ImageUrl,
                VideoUrl = recipeVM.Recipe.VideoUrl,
                Calories = recipeVM.Recipe.Calories,
                LastUpdate = recipeVM.Recipe.LastUpdate,
                Category = recipeVM.Recipe.Category
            };
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            recipeVM.Categorys = db.Category.ToList();
            return View(recipeVM);
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            var model = new RecipeViewModel
            {
                Recipe = recipe,
                Categorys = db.Category.ToList()
            };
            return View(model);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
