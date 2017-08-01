using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Core;

namespace LMS.Controllers
{
    public class ViewController : Controller
    {
        public ActionResult BooksView()
        {
            if (Session["UserName"] != null)
            {
                
                return View(Books.GetAll());
                

            }
            return RedirectToAction("Login","Home");
        }

        public ActionResult ProfileView()
        {
            if (Session["UserName"] != null)
            {
                string name = Session["UserName"].ToString();

                User current = Users.GetByName(name);
                return View(current);
            }

            return RedirectToAction("Login", "Home");
        }
            
    }
}