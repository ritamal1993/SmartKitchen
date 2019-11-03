using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartKitchen3.Models;

namespace SmartKitchen3.Controllers
{
    public class RecipeIngredientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecipeIngredients
        public ActionResult Index()
        {
            var recipeIngredient = db.RecipeIngredient.Include(r => r.Ingredient).Include(r => r.Recipe);
            return View(recipeIngredient.ToList());
        }

        // GET: RecipeIngredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeIngredient recipeIngredient = db.RecipeIngredient.Find(id);
            if (recipeIngredient == null)
            {
                return HttpNotFound();
            }
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Create
        public ActionResult Create()
        {
            ViewBag.IngredientId = new SelectList(db.Ingredients, "IngredientId", "Name");
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Title");
            return View();
        }

        // POST: RecipeIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeIngredientId,RecipeId,IngredientId,Quantity")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                db.RecipeIngredient.Add(recipeIngredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredientId = new SelectList(db.Ingredients, "IngredientId", "Name", recipeIngredient.IngredientId);
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Title", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeIngredient recipeIngredient = db.RecipeIngredient.Find(id);
            if (recipeIngredient == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientId = new SelectList(db.Ingredients, "IngredientId", "Name", recipeIngredient.IngredientId);
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Title", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeIngredientId,RecipeId,IngredientId,Quantity")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeIngredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientId = new SelectList(db.Ingredients, "IngredientId", "Name", recipeIngredient.IngredientId);
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Title", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeIngredient recipeIngredient = db.RecipeIngredient.Find(id);
            if (recipeIngredient == null)
            {
                return HttpNotFound();
            }
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecipeIngredient recipeIngredient = db.RecipeIngredient.Find(id);
            db.RecipeIngredient.Remove(recipeIngredient);
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
