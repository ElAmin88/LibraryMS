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

        public ActionResult FriendsView(string search)
        {
            User u = Users.GetByName(Session["UserName"].ToString());
            List<User> friends = Users.GetFriends(u);
            ViewBag.Friends = Users.GetFriends(u);
            
            return View(Users.Search(search,u));
                        
        }

        public ActionResult AddFriend(String name)
        {
            User user = Users.GetByName(Session["UserName"].ToString());
            Users.AddFriendByName(user, name);
            return RedirectToAction("FriendsView");
        }
    }
}