using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LibraryContext :DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LibraryContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public LibraryContext() : base() 
        { 
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}