﻿using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS.Core;
namespace LMS.Controllers
{

    public class HomeController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]


        public ActionResult Login(User user)
        {

            if (Users.Check(user))
            {
                user = Users.GetByName(user.name);
                Users.currentUser = user;
                Session["UserName"] = user.name.ToString();
                Session["Type"] = user.type.ToString();
                return RedirectToAction("BooksView","Book");
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
                    Users.Add(user);
                    return RedirectToAction("Login","Home");
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
