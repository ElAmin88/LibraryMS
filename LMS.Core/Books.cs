﻿using LMS.Models;
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
            /*ctx.Books.Attach(book);
            var entry = ctx.Entry(book);
            entry.Property(e => e.title).IsModified = true;
            entry.Property(e => e.ISBN).IsModified = true;*/
            Book b = GetByID(book.ID);
            b.ISBN = book.ISBN;
            b.title = book.title;
            ctx.SaveChanges();
            
        }

    }
}