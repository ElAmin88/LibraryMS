using LMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core
{
    public class Books
    {
        static LibraryContext ctx = new LibraryContext();
      
        public static List<Book> GetAll()
        {
            return ctx.Books.ToList();
            
        }

        public static void Add(Book book)
        {
            ctx.Books.Add(book);
            ctx.SaveChanges();
        }

        public static void Remove(Book book)
        {
            ctx.Books.Remove(book);
            ctx.SaveChanges();
        }

        public static Book GetByID(int ID)
        {
            Book existBook = ctx.Books.FirstOrDefault(a => a.ID == ID);
            if (existBook != null)
            {
                return existBook;
            }
            return null;

        }

        public static void RemoveByID(int id)
        {
            Book book = Books.GetByID(id);
            ctx.Books.Remove(book);
            ctx.SaveChanges();
        }

        public static void Update(Book book )
        {
            Book b = GetByID(book.ID);
            b.ISBN = book.ISBN;
            b.title = book.title;
            b.details = book.details;
            ctx.SaveChanges();
            
        }

        public static List<Book> Search(string search)
        {
            if (search == null)
            {
                return Books.GetAll();
            }
            
            List<Book> books = ctx.Books.Where(a => a.title.Contains(search)).ToList();
            
            return books;
        }

        public static void ReserveRequest(User u,int id)
        {
            Reservation r = new Reservation
            {
                bookID = id,
                userID = u.Id,
                status=0
            };
            ctx.Reservations.Add(r);
            ctx.SaveChanges();
        }

        public static void Reserve (string userId , int bookId)
        {
            Reservation r = ctx.Reservations.FirstOrDefault(x=> x.userID ==userId &&x.bookID == bookId);
            r.status = 1;
            Book b = GetByID(bookId);
            b.available_copies -= 1;

            ctx.SaveChanges();
        }

        public static List<Book> ReservedBooksbyUser(User u)
        {
            List<int> BooksID = ctx.Reservations.Where(a => a.userID == u.Id && a.status==1).Select(b => b.bookID).ToList();

            List<Book> books = new List<Book>();
            foreach (int id in BooksID)
            {
                books.Add(ctx.Books.FirstOrDefault(a => a.ID == id));
            }
            return books;
        }

        public static List<Reservation> GetPendingReservations(string search)
        {
            List<Reservation> res=null;
            if (search==null)
            {
                res = ctx.Reservations.Include(x => x.user).Include(x => x.book).Where(r => r.status == 0).ToList();
            }
            else
            {
                res = ctx.Reservations.Include(x => x.user).Include(x => x.book).Where(r => r.status == 0 && r.user.UserName.Contains(search)).ToList();
            }
            return res;
        }

        public static List<Reservation> GetPendingRetrievals(string search)
        {
            List<Reservation> res = null;
            if (search == null)
            {
                res = ctx.Reservations.Include(x => x.user).Include(x => x.book).Where(r => r.status == 2).ToList();
            }
            else
            {
                res = ctx.Reservations.Include(x => x.user).Include(x => x.book).Where(r => r.status == 2 && r.user.UserName.Contains(search)).ToList();
            }
            return res;
        }

        public static void RequestReturnByID(string userId,int bookId)
        {
            Reservation r = ctx.Reservations.FirstOrDefault(x => x.userID == userId && x.bookID == bookId);
            r.status = 2;
            ctx.SaveChanges();
        }

        public static void ReturnById(string userId ,int bookId)
        {
            Reservation r = ctx.Reservations.FirstOrDefault(a => a.bookID == bookId && a.userID==userId);
            ctx.Reservations.Remove(r);
            Book b = ctx.Books.FirstOrDefault(a => a.ID == bookId);
            b.available_copies++;
            ctx.SaveChanges();
        }
        
        public static void Rate (Rating r)
        {
            Rating cr = GetRating(r.bookID);
            if(cr==null)
            {
                ctx.Ratings.Add(r);
            }
            else
            {
                cr = r;
            }
            ctx.SaveChanges();
        }

        public static Rating GetRating(int id)
        {
            return ctx.Ratings.FirstOrDefault(r => r.bookID == id&& r.userID== Users.currentUser.Id);
        }

        public static List<Rating> GetAllRatings(int id)
        {
            return ctx.Ratings.Include(x => x.user).Where(r => r.bookID == id).ToList();
        }
    }
}
