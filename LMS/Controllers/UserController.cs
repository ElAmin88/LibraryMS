using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Core;

namespace LMS.Controllers
{
    public class UserController : Controller
    {

        [Authorize(Roles = "User")]
        public ActionResult ProfileView()
        {
            if (Session["UserName"] != null)
            {
                
                return View(((User)Session["User"]));
            }

            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "User")]
        public ActionResult FriendsView(string search)
        {
            ViewBag.Friends = Users.GetFriends(((User)Session["User"]));
            ViewBag.Requests = Users.GetFriendRequest(((User)Session["User"]));
            return View(Users.Search(search, ((User)Session["User"])));
                        
        }

        [Authorize(Roles = "User")]
        public ActionResult SendFriendRequest(String name)
        {
            Users.SendFriendRequest(((User)Session["User"]), name);
            return RedirectToAction("FriendsView");
        }
        
        [Authorize(Roles = "User")]
        public ActionResult AddFriend(string id)
        {
            Users.AddFriend(((User)Session["User"]), id);
            return RedirectToAction("FriendsView");

        }

        public ActionResult SearchUsers(string search)
        {
            return View(Users.Search(search, ((User)Session["User"])));
        }

        
    }
}