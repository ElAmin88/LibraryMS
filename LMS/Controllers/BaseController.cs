using LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        { 
            if(currentUser.lang!=null && currentUser!=null)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentUser.lang);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentUser.lang);
            }
            

        }
        public User currentUser
        {
            get
            {
                if (Session["User"] == null)
                    return new Models.User();
                return (User)Session["User"];
            }
            set
            {
                Session["User"] = value;
            }
        }
        public UserStore<User> us = new UserStore<User>(new LibraryContext());
        public UserManager<User> um => HttpContext.GetOwinContext().Get<UserManager<User>>();
        public SignInManager<User, string> sim => HttpContext.GetOwinContext().Get<SignInManager<User, string>>();

        public RoleStore<IdentityRole> rs;
        public RoleManager<IdentityRole> rm;
        public IdentityRole role;
        public ActionResult English()
        {
            currentUser.lang = "en-US";
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Arabic()
        {
            currentUser.lang = "ar-EG";
            return Redirect(Request.UrlReferrer.ToString());
        }
    }

   
}