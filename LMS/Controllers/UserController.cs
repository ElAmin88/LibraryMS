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
        public ActionResult BooksView()
        {
            if (Session["UserName"] != null)
            {

                return View(Books.GetAll());


            }
            return RedirectToAction("Login", "Home");
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

        public ActionResult FriendsView(string search)
        {
            User u = Users.GetByName(Session["UserName"].ToString());
            if (u.friends == null)
            {
                ViewBag.Friends = new List<User>();
            }
            else
            {
                ViewBag.Friends = u.friends;
            }
            
            if(search == null)
            {
                return View(new List<User>());
            }
            else
            {
                return View(Users.Search(search));
            }
                        
        }

        public ActionResult AddFriend(String name)
        {
            User user = Users.GetByName(Session["UserName"].ToString());
            Users.AddFriendByName(user, name);
            return RedirectToAction("FriendsView");
        }
    }
}