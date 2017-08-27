using LMS.Model;
using LMS.Model.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LibraryContext : IdentityDbContext<User>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u=> u.Friends).WithRequired(u=> u.user1).WillCascadeOnDelete(false); //add this line code
            modelBuilder.Entity<User>().HasMany(u => u.Friends).WithRequired(u => u.user2).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.Reservations).WithRequired(u => u.user).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.Ratings).WithRequired(r => r.user).WillCascadeOnDelete(false);
            modelBuilder.Entity<Book>().HasMany(b => b.Reservations).WithRequired(b => b.book).WillCascadeOnDelete(false);
            modelBuilder.Entity<Book>().HasMany(b => b.Ratings).WithRequired(r=> r.book).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithRequired(u => u.user1).WillCascadeOnDelete(false); //add this line code
            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithRequired(u => u.user2).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.Msgs).WithRequired(u => u.user).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithRequired(u => u.user2).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.User_Group).WithRequired(u => u.user).WillCascadeOnDelete(false);
            modelBuilder.Entity<Group>().HasMany(u => u.User_Group).WithRequired(u => u.group).WillCascadeOnDelete(false);
            modelBuilder.Entity<Group>().HasMany(u => u.Messages).WithRequired(u => u.g).WillCascadeOnDelete(false);




            base.OnModelCreating(modelBuilder);


        }
        public LibraryContext() : base("LibraryContext") 
        { 
        
        }

        public static void InitModel()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraryContext, Configuration>());
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}