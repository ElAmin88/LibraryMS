using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Core;
using System.IO;

namespace LMS.Controllers
{
    public class UserController : BaseController
    {

        [Authorize(Roles = "User")]
        public ActionResult ProfileView()
        {
            return View(currentUser);
        }

        [Authorize(Roles = "User")]
        public ActionResult FriendsView(string search)
        {
            ViewBag.Friends = Users.GetFriends(currentUser);
            ViewBag.Requests = Users.GetFriendRequest(currentUser);
            return View(Users.Search(search, currentUser));
                        
        }

        [Authorize(Roles = "User")]
        public ActionResult SendFriendRequest(String name)
        {
            Users.SendFriendRequest(currentUser, name);
            return RedirectToAction("FriendsView");
        }
        
        [Authorize(Roles = "User")]
        public ActionResult AddFriend(string id)
        {
            Users.AddFriend(currentUser, id);
            return RedirectToAction("FriendsView");

        }

        public ActionResult SearchUsers(string search)
        {
            return View("~/Views/Partials/SearchUsers.cshtml",Users.Search(search, currentUser));
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult UpdateProfile(User u)
        {
            Users.UpdateUserByID(currentUser.Id, u);
            return RedirectToAction("ProfileView");
        }
        public ActionResult UpdateImage(HttpPostedFileBase myFile)
        {
            string path = Server.MapPath("~/Content/ProfilePictures/") +currentUser.Id+".jpg";
            myFile.SaveAs(path);
            currentUser.picture = "~/Content/ProfilePictures/" + currentUser.Id + ".jpg";
            Users.UpdateUserByID(currentUser.Id, currentUser);
            return RedirectToAction("ProfileView");
        }

    }
}