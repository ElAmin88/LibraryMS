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

    public class HomeController : Controller
    {
        public UserStore<User> us = new UserStore<User>(new LibraryContext());
        public UserManager<User> um => HttpContext.GetOwinContext().Get<UserManager<User>>();
        public SignInManager<User, string> sim => HttpContext.GetOwinContext().Get<SignInManager<User, string>>();

        public RoleStore<IdentityRole> rs;
        public RoleManager<IdentityRole> rm;
        IdentityRole role;

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
                    Users.currentUser = user;
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
            Users.currentUser = null;
            Session.Abandon();
            return RedirectToAction("Login","Home");
        }

        

    }
}
