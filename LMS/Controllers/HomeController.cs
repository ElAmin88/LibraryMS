using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LMS.Controllers
{

    public class HomeController : BaseController
    {
        

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]


        public ActionResult Login(string name, string password)
        {

            SignInStatus SIS = sim.PasswordSignIn(name, password, false, false);
            
            switch (SIS)
            {
                case SignInStatus.Failure:
                    return View();
                case SignInStatus.LockedOut:
                    return View();
                case SignInStatus.RequiresVerification:
                    return View();
                case SignInStatus.Success:
                    User user = Users.GetByName(name);
                    currentUser = user;
                    Session["UserName"] = user.UserName;
                    List<IdentityUserRole> role = user.Roles.ToList();
                    string type=null;
                    foreach(IdentityUserRole r in role)
                    {
                        type = Users.GetRole(r.RoleId);
                    }
                    Session["Type"] = type;
                    return RedirectToAction("BooksView", "Book");
                default:
                    return View();
            }

        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user,string password)
        {
            if (user != null)
            { 
                if (ModelState.IsValid)
                {
                    role = new IdentityRole();
                    role.Name = "User";
                    rs = new RoleStore<IdentityRole>(new LibraryContext());
                    rm = new RoleManager<IdentityRole>(rs);
                    rm.Create(role);
                    user.picture = "~/Content/ProfilePictures/icon-user-default.png";
                    um.Create(user,password);
                    um.AddToRole(user.Id, "User");
                    return RedirectToAction("Login","Home");
                }           
            }
          
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddAdmin(User user, string password)
        {
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    role = new IdentityRole();
                    role.Name = "Admin";
                    rs = new RoleStore<IdentityRole>(new LibraryContext());
                    rm = new RoleManager<IdentityRole>(rs);
                    rm.Create(role);
                    um.Create(user, password);
                    um.AddToRole(user.Id, "Admin");
                    return RedirectToAction("BooksView", "Book");
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
