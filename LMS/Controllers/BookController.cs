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
    public class BookController : BaseController
    {
        [Authorize]
        public ActionResult BooksView(string search)
        {
            return View(Books.Search(currentUser,search));
        }

        public ActionResult SearchBooks(string search)
        {
            return View("~/Views/Partials/SearchBooks.cshtml",Books.Search(currentUser, search));
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

            return View(Books.GetAll(currentUser));
        }

        [Authorize(Roles = "User")]
        public ActionResult ReservedBooks()
        {

            return View(Books.ReservedBooksbyUser(currentUser));
        }
        [Authorize(Roles = "User")]
        public ActionResult Reserve(int id)
        {
            Books.ReserveRequest(currentUser, id);
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
            r.userID = currentUser.Id;
            Books.Rate(currentUser, r);

            return RedirectToAction("BooksView");
        }

        [Authorize(Roles = "User")]
        public ActionResult ReturnBook(int id )
        {
            Books.RequestReturnByID(currentUser.Id,id);
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

        public ActionResult RejectReserevations(String userId, int BookId)
        {
            Books.RejectReserve(userId, BookId);
            return RedirectToAction("Reservations");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AcceptRetrievals(String userId, int BookId)
        {
            Books.ReturnById(userId, BookId);
            return RedirectToAction("Reservations");
        }
        public ActionResult RejectRetrievals(String userId, int BookId)
        {
            Books.RejectReturnById(userId, BookId);
            return RedirectToAction("Reservations");
        }
    }

}