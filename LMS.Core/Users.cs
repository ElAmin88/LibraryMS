using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Models;

namespace LMS.Core
{
    
    public class Users
    {
        static LibraryContext ctx = new LibraryContext();
        public static void Add(User u)
        {
            ctx.Users.Add(u);
            ctx.SaveChanges();
        }

        public static bool Check(User u)
        {
            User existUser = ctx.Users.FirstOrDefault(a => a.name == u.name && a.password == u.password);
            if(existUser != null)
            {
                return true;
            }
            return false;
        }

        public static User GetByName(string name)
        {
            User existUser = ctx.Users.FirstOrDefault(a => a.name == name);
            if (existUser != null)
            {
                return existUser;
            }
            return null;

        }

        public static List<User> GetAll()
        {
            return ctx.Users.ToList();
        }

        public static void Delete(User u)
        {
            ctx.Users.Remove(u);
            ctx.SaveChanges();
        }
        public static List<User> Search(string search)
        {
            if (search == null)
            {
                return new List<User>();
            }
            List<User> users = ctx.Users.Where(a => a.name.Contains(search)).ToList();
            return users;
        }
        public static List<User> GetFriends(User u)
        {
            List<int> friendsID = ctx.Friends.Where(a => a.user1ID == u.ID).Select(b=>b.user2ID).ToList();

            List<User> friends = new List<User>();
            foreach (int id in friendsID)
            {
                friends.Add(ctx.Users.FirstOrDefault(a => a.ID == id));
            }
            return friends;
        }

        public static void AddFriendByName(User user1,string name)
        {
            User user2 = Users.GetByName(name);
            Friend f = new Friend 
            { 
                user1ID = user1.ID,
                user2ID=user2.ID 
            };

            ctx.Friends.Add(f);
            ctx.SaveChanges();
        }
        
    }
}
