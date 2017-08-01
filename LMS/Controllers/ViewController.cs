using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class ViewController : Controller
    {
        LibraryContext ctx = new LibraryContext();
        public ActionResult BooksView()
        {
            if (Session["UserName"] != null)
            {
                
                return View(ctx.Books.ToList());
                

            }
            return RedirectToAction("Login","Home");
        }
    }
}