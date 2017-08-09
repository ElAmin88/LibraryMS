using LMS.Model;
using LMS.Model.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LibraryContext :DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u=> u.Friends).WithRequired(u=> u.user1).WillCascadeOnDelete(false); //add this line code
            modelBuilder.Entity<User>().HasMany(u => u.Friends).WithRequired(u => u.user2).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.Reservations).WithRequired(u => u.user).WillCascadeOnDelete(false);
            modelBuilder.Entity<Book>().HasMany(b => b.Reservations).WithRequired(b => b.book).WillCascadeOnDelete(false);


        }
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