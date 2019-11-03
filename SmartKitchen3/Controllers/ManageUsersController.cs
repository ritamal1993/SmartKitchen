using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartKitchen3.Models;
using SmartKitchen3.Utils;

namespace SmartKitchen3.Controllers
{
    [SmartKitchenAuthorizeAttribute(AdminOnly = "true")]
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManageUsers
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.Kitchen);
            return View(user.ToList());
        }

        // GET: ManageUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: ManageUsers/Create
        public ActionResult Create()
        {
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "Name");
            return View();
        }

        // POST: ManageUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Email,Password,KitchenId,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Utils.Crypto.Hash(user.Password);
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "Name", user.KitchenId);
            return View(user);
        }

        // GET: ManageUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "Name", user.KitchenId);
            return View(user);
        }

        // POST: ManageUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Email,Password,KitchenId,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Utils.Crypto.Hash(user.Password);

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "Name", user.KitchenId);
            return View(user);
        }

        // GET: ManageUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: ManageUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
