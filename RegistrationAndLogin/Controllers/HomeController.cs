using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationAndLogin.Controllers
{
    public class HomeController : Controller
    {


        // GET: Home

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult SubmitForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(feedback fb)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {
                
                #region Save to Database
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    try
                    {
                        dc.feedbacks.Add(fb);

                        dc.SaveChanges();
                        message = "Feedback successfully Submitted. ";
                        Status = true;
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                    }
                     
                    
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(fb);
        }


        MyDatabaseEntities dc = new MyDatabaseEntities();

        [Authorize]
        public ActionResult ViewFeedbacks()
        {

            MyDatabaseEntities entities = new MyDatabaseEntities();
            return View(from fb in entities.feedbacks.Take(10)
                        select fb);
            //return View();
        }

    }
}