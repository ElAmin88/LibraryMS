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
        
        public ActionResult BooksView(string search)
        {
            if (Session["UserName"] != null)
            {
                IdentityUser u = new IdentityUser();
               
                return View(Books.Search(search));
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult SearchBooks(string search)
        {
            return View(Books.Search(search));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult AddBook(Book book)
        {
            
            if (ModelState.IsValid)
            {
                Books.Add(book);
                return RedirectToAction("BooksView");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditBook(int id)
        {

            return View(Books.GetByID(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditBook(Book book)
        {
            Books.Update(book);
            return RedirectToAction("EditAndDelete");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveBook(int id)
        {
            Books.RemoveByID(id);
            return RedirectToAction("EditAndDelete");

        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditAndDelete()
        {

            return View(Books.GetAll());
        }

        [Authorize(Roles = "User")]
        public ActionResult ReservedBooks()
        {

            return View(Books.ReservedBooksbyUser((User)Session["User"]));
        }
        [Authorize(Roles = "User")]
        public ActionResult Reserve(int id)
        {
            Books.ReserveRequest(((User)Session["User"]), id);
             return RedirectToAction("BooksView");
        }

        public ActionResult Details(int id)
        {
            Book b = Books.GetByID(id);
            ViewBag.Ratings = Books.GetAllRatings(id);
            return View(b);
        }

        [Authorize(Roles = "User")]
        public ActionResult Rating (int id)
        {
            Rating r = new Rating
            {
                bookID = id
            };
            return View(r);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult AddRating(Rating r)
        {
            r.userID = ((User)Session["User"]).Id;
            Books.Rate(((User)Session["User"]),r);

            return RedirectToAction("BooksView");
        }

        [Authorize(Roles = "User")]
        public ActionResult ReturnBook(int id )
        {
            Books.RequestReturnByID(((User)Session["User"]).Id,id);
            return RedirectToAction("Rating", new { id = id });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Reservations(string searchReservation ,string searchRetrieval)
        {
            ViewBag.Reservations = Books.GetPendingReservations(searchReservation);
            ViewBag.Retrievals = Books.GetPendingRetrievals(searchRetrieval);
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SearchReservations(string searchReservation)
        {
            List<Reservation> r = Books.GetPendingReservations(searchReservation);
            ViewBag.Reservations = r;
            return View("~/Views/Partials/SearchReserve.cshtml");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SearchRetrievals (string searchRetrieval)
        {
            List<Reservation> r = Books.GetPendingRetrievals(searchRetrieval);
            ViewBag.Retrievals = r;
            return View("~/Views/Partials/SearchRetrievals.cshtml");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AcceptReserevations(String userId ,int BookId)
        {
            Books.Reserve(userId, BookId);
            return RedirectToAction("Reservations");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AcceptRetrievals(String userId, int BookId)
        {
            Books.ReturnById(userId, BookId);
            return RedirectToAction("Reservations");
        }
    }

}