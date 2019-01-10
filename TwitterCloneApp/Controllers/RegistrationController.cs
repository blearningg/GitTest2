using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterCloneApp.Controllers
{
    public class RegistrationController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: Registration
      

        // GET: Registration/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Registration/Create
        [HttpPost]
        public ActionResult Register(PERSON objPerson)
        {
            try
            {
                objPerson.JoinedDate = DateTime.Now;
                objPerson.Active = true;
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.People.Add(objPerson);
                    db.SaveChanges();
                   
                    return RedirectToAction("Register");
                }
                else
                {
                    ModelState.AddModelError("RegistrationFailed", "Invalid Data");
                    return View(objPerson);
                }


               
            }
            catch
            {
                return View();
            }
        }

    }
}
