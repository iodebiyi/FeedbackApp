using FeedbackForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedbackForm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register(accountUser U)
        {
            if (ModelState.IsValid)
            {
                using (UserEntities dc = new UserEntities())
                {
                    //you should check duplicate registration here 
                    dc.accountUsers.Add(U);
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = " Registration Successful";
                }
            }
            return View(U);
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult TestRun()
        {
            return View();
        }
    }
}