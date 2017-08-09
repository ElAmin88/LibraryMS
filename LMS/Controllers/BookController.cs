using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Core;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult BooksView(string search)
        {
            if (Session["UserName"] != null)
            {
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
            User u = Users.GetByName(Session["UserName"].ToString());
            
            return View(Books.ReservedBooks(u));
        }
        
        public ActionResult Reserve(int id)
        {
             User u = Users.GetByName(Session["UserName"].ToString());

             Books.Reserve(u, id);
             return RedirectToAction("BooksView");
        }

        public ActionResult Details(int id)
        {
            Book b = Books.GetByID(id);
            return View(b);
        }
    }
}