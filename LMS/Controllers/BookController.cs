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
                return RedirectToAction("BooksView", "View");
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
            return RedirectToAction("BooksView", "View");
        }
        
        public ActionResult RemoveBook(int id)
        {
            Books.RemoveByID(id);
            return RedirectToAction("BooksView", "View");

        }
        public ActionResult EditAndDelete()
        {

            return View(Books.GetAll());
        }
    }
}