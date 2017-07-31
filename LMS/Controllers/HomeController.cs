using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (var cntx = new Models.LibraryContext())
            {
                cntx.Users.Remove(new Models.User { name = "ahmed", password = "ahmedbana" });

            }
            return View();
        }
    }
}