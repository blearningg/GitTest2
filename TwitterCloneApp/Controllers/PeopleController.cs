using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitterCloneApp;


namespace TwitterCloneApp.Controllers
{
    public class PeopleController : Controller
    {
        private masterEntities db = new masterEntities();

        [HttpPost]
        public ActionResult Search(string searchUser)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            string currentuserid = Session["UserID"].ToString();
            var obj = (from p in db.People where p.user_id.Contains(searchUser) select p).ToList();

            return View(obj);

        }


        //[HttpPost]
        public ActionResult Follow(string id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Following objFollowing = new Following();
            objFollowing.user_id = id;
            objFollowing.following_id = Session["UserID"].ToString();

            db.Followings.Add(objFollowing);
            db.SaveChanges();
           
            return RedirectToAction("Index","Home");
        }

       
        public ActionResult Details()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login","Login");
            }
            PERSON pERSON = db.People.Find(Session["UserID"]);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        public ActionResult Edit(string id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,password,fullName,Email,JoinedDate,Active")] PERSON pERSON)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(pERSON).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Profile data updated successfully.";
                return RedirectToAction("Details");
            }
            return View(pERSON);
        }

        //// GET: People/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            PERSON pERSON = db.People.Find(id);
            db.People.Remove(pERSON);
            db.SaveChanges();
            return RedirectToAction("Login","Login");
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
