using LMS.Model;
using LMS.Model.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LibraryContext :DbContext
    {
        public LibraryContext() : base() 
        { 
        
        }

        public static void InitModel()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraryContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}