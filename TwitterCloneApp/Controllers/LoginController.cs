using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterCloneApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PERSON objUser)
        {

            using (masterEntities db = new masterEntities())
            {
                var obj = db.People.Where(a => a.user_id.Equals(objUser.user_id) && a.password.Equals(objUser.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.user_id.ToString();
                    Session["UserName"] = obj.fullName.ToString();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "Login Failed");
                    return View(objUser);
                }


            }


        }

        public ActionResult Home()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }



    }
}