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

        public static void AddFriendByName(User user,string name)
        {
            User friend = Users.GetByName(name);
            user.friends.Add(friend);
        }
        
    }
}
