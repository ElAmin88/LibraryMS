using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        LibraryContext ctx = new LibraryContext();
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
                ctx.Books.Add(book);
                ctx.SaveChanges();
                return RedirectToAction("BooksView", "View");
            }
            return View();
        }
        public void test()
        {

        }
    }
}