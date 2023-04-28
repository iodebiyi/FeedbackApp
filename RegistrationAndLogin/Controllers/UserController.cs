using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationAndLogin.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace RegistrationAndLogin.Controllers
{
    public class UserController : Controller
    {


       

        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //Registration POST action 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {

                #region  
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return View(user);
                }
                #endregion

                #region  
                var isExistStudentID = IsStudentIDExist(user.StudentID);
                if (isExistStudentID)
                {
                    ModelState.AddModelError("StudentID", "StudentID already exist");
                    return View(user);
                }
                #endregion

                #region  Password Hashing 
                user.Password = PasswordCrypt.Hash(user.Password);
                user.ConfirmPassword = PasswordCrypt.Hash(user.ConfirmPassword); //
                user.UserRole = "student";
                #endregion
              

                #region Save to Database
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    try
                    {
                        dc.Users.Add(user);

                        dc.SaveChanges();
                        message = "Registration successfully done. ";
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;   
                    }
                     
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        //Verify Account  
        
        
        //Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl="")
        {
            string message = "";
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                 var v = dc.Users.Where(a => a.StudentID == login.StudentID).FirstOrDefault();
             
                if (v != null)
                {
                   
                    if (string.Compare(PasswordCrypt.Hash(login.Password),v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                                                                      // var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        var ticket = new FormsAuthenticationTicket(login.StudentID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Dashboard", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }


        public bool IsStudentIDExist(string studentID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.StudentID == studentID).FirstOrDefault();
                return v != null;
            }
        }

        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.Email == login.Email).FirstOrDefault() ;
               // var x = dc.Users.Where(b => b.UserRole == "admin");
                if (v != null && v.UserRole =="admin")
                {

                    if (string.Compare(PasswordCrypt.Hash(login.Password), v.Password) == 0 )
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                                                                      // var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ViewFeedbacks", "Home");
                        }
                    }
                    else
                    {
                        message = "Incorrect Credentials";
                    }
                }
                else
                {
                    message = "You are not Authorized to view or Login on this Channel";
                }
            }
            ViewBag.Message = message;
            return View();
        }
    }


}