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
                
                return View(Users.currentUser);
            }

            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "User")]
        public ActionResult FriendsView(string search)
        {
            ViewBag.Friends = Users.GetFriends(Users.currentUser);
            ViewBag.Requests = Users.GetFriendRequest(Users.currentUser);
            return View(Users.Search(search, Users.currentUser));
                        
        }

        [Authorize(Roles = "User")]
        public ActionResult SendFriendRequest(String name)
        {
            Users.SendFriendRequest(Users.currentUser, name);
            return RedirectToAction("FriendsView");
        }
        
        [Authorize(Roles = "User")]
        public ActionResult AddFriend(string id)
        {
            Users.AddFriend(Users.currentUser, id);
            return RedirectToAction("FriendsView");

        }

        public ActionResult SearchUsers(string search)
        {
            return View(Users.Search(search, Users.currentUser));
        }

        
    }
}