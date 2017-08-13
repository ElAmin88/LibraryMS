using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Core;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult BooksView(string search)
        {
            if (Session["UserName"] != null)
            {
                IdentityUser u = new IdentityUser();
               
                return View(Books.Search(search));
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            
            if (ModelState.IsValid)
            {
                Books.Add(book);
                return RedirectToAction("BooksView");
            }
            return View();
        }

        public ActionResult EditBook(int id)
        {

            return View(Books.GetByID(id));
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            Books.Update(book);
            return RedirectToAction("EditAndDelete");
        }
        
        public ActionResult RemoveBook(int id)
        {
            Books.RemoveByID(id);
            return RedirectToAction("EditAndDelete");

        }

        public ActionResult EditAndDelete()
        {

            return View(Books.GetAll());
        }

        public ActionResult ReservedBooks()
        {

            return View(Books.ReservedBooks(Users.currentUser));
        }
        
        public ActionResult Reserve(int id)
        {
            Books.Reserve(Users.currentUser, id);
             return RedirectToAction("BooksView");
        }

        public ActionResult Details(int id)
        {
            Book b = Books.GetByID(id);
            ViewBag.Ratings = Books.GetAllRatings(id);
            return View(b);
        }

        public ActionResult Rating (int id)
        {
            Rating r = new Rating
            {
                bookID = id
            };
            return View(r);
        }
        [HttpPost]
        public ActionResult AddRating(Rating r)
        {
            r.userID = Users.currentUser.ID;
            Books.Rate(r);

            return RedirectToAction("BooksView");
        }
    }
}