using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using TwitterCloneApp;
namespace TwitterCloneApp.Controllers
{
    public class HomeController : Controller
    {
        private masterEntities db = new masterEntities();

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string currentuserid = Session["UserID"].ToString();
            

            Session["TotalTweets"] = db.Tweets.Include(t => t.PERSON).Where(p => p.user_id == currentuserid).Count();
            Session["FollowingCount"] = db.Followings.Include(t => t.PERSON).Where(p => p.following_id == currentuserid).Count();
            Session["FollowersCount"] = db.Followings.Include(t => t.PERSON).Where(p => p.user_id == currentuserid).Count();
            TweetViewModel objTweetViewModel = new TweetViewModel();
            objTweetViewModel.Tweets = db.Tweets.ToList();

            return View(objTweetViewModel);
           
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;

            Session["UserName"] = string.Empty;

             return RedirectToAction("Login", "Login");
           
        }

        [HttpPost]
        public ActionResult Update(TweetViewModel objTweetViewModel)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            objTweetViewModel.tweet.user_id = Session["UserID"].ToString();
            objTweetViewModel.tweet.created = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Tweets.Add(objTweetViewModel.tweet);
                db.SaveChanges();
                ViewBag.Message = "User registered succesfully !!";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


    }
}