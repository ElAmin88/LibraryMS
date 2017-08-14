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

        public static void Reserve(User u,int id)
        {
            Reservation r = new Reservation
            {
                bookID = id,
                userID = u.Id
            };
            Book b = GetByID(id);
            b.available_copies -= 1;
            ctx.Reservations.Add(r);
            ctx.SaveChanges();
        }

        public static List<Book> ReservedBooks(User u)
        {
            List<int> BooksID = ctx.Reservations.Where(a => a.userID == u.Id).Select(b => b.bookID).ToList();

            List<Book> books = new List<Book>();
            foreach (int id in BooksID)
            {
                books.Add(ctx.Books.FirstOrDefault(a => a.ID == id));
            }
            return books;
        }

        public static void ReturnByID (int id)
        {
            Reservation r = ctx.Reservations.FirstOrDefault(a => a.bookID == id);
            ctx.Reservations.Remove(r);
            Book b = ctx.Books.FirstOrDefault(a => a.ID == id);
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
