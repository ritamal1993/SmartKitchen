using SmartKitchen3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartKitchen3.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db;
        public CategoryController()
        {
            db = new ApplicationDbContext();

        }

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }
        //get action
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
       
        public ActionResult Details(int? Id)
        {
            if (Id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(Id);
            if (category==null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
          
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {


            Category category = db.Category.Find(Id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
              
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}