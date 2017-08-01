using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        LibraryContext ctx = new LibraryContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objUser)
        {

            User existUser = ctx.Users.FirstOrDefault(a => a.name == objUser.name && a.password== objUser.password );
            if (existUser != null)
            {
                Session["UserName"] = objUser.name.ToString();
                return RedirectToAction("BooksView","View");
            }
                
            
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            if (user != null)
            { 
                if (ModelState.IsValid)
                {
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    return RedirectToAction("Login","Home");
                }           
            }
          
            return View();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login","Home");
        }

        

    }
}
