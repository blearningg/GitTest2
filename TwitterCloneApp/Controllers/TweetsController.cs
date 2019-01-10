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
    public class TweetsController : Controller
    {
        private masterEntities db = new masterEntities();

        //// GET: Tweets
        //public ActionResult Index()
        //{
        //    var tweets = db.Tweets.Include(t => t.PERSON);
        //    return View(tweets.ToList());
        //}

        //// GET: Tweets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tweet tweet = db.Tweets.Find(id);
        //    if (tweet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tweet);
        //}

        //// GET: Tweets/Create
        //public ActionResult Create()
        //{
        //    ViewBag.user_id = new SelectList(db.People, "user_id", "password");
        //    return View();
        //}

        //// POST: Tweets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "tweet_id,user_id,message,created")] Tweet tweet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tweets.Add(tweet);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.user_id = new SelectList(db.People, "user_id", "password", tweet.user_id);
        //    return View(tweet);
        //}

        // GET: Tweets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Tweet tweet = db.Tweets.SingleOrDefault(t => t.tweet_id == id);

            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        // POST: Tweets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tweet_id,user_id,message,created")] Tweet tweet)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                tweet.created = DateTime.Now;
                db.Entry(tweet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ViewBag.user_id = Session["UserID"].ToString();
            return View(tweet);
        }

        // GET: Tweets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.SingleOrDefault(t => t.tweet_id == id);
            if (tweet == null)
            {
                return HttpNotFound();
            }

            db.Tweets.Remove(tweet);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
