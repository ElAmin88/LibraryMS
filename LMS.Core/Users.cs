using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LMS.Core
{
    
    public class Users
    {
        public static User currentUser;
        static LibraryContext ctx = new LibraryContext();

        

        public static User GetByName(string name)
        {
            User existUser = ctx.Users.FirstOrDefault(a => a.UserName == name);
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
        public static List<User> Search(string search,User u)
        {
            if (search == null)
            {
                return new List<User>();
            }
            
            List<User> users = ctx.Users.Where(a => a.UserName.Contains(search)&& a.UserName!=u.UserName).ToList();
            List<User> friends = GetFriends(u);
            for (int i = users.Count - 1; i >= 0; i--)
            {
                foreach (User friend in friends)
                {
                    if (users[i].UserName == friend.UserName)
                    {
                        users.RemoveAt(i);
                        break;
                    }
                }
            }
            
            return users;
        }
        public static List<User> GetFriends(User u)
        {
            List<string> friendsID = ctx.Friends.Where(a => a.user1ID == u.Id&& a.status==1).Select(b=>b.user2ID).ToList();

            List<User> friends = new List<User>();
            foreach (string id in friendsID)
            {
                friends.Add(ctx.Users.FirstOrDefault(a => a.Id == id));
            }
            return friends;
        }
        public static List<User> GetFriendRequest(User u)
        {
            List<string> friendsID = ctx.Friends.Where(a => a.user2ID == u.Id && a.status == 0).Select(b => b.user1ID).ToList();

            List<User> friends = new List<User>();
            foreach (string id in friendsID)
            {
                friends.Add(ctx.Users.FirstOrDefault(a => a.Id == id));
            }
            return friends;
        }

        public static void SendFriendRequest(User user1,string name)
        {
            User user2 = Users.GetByName(name);
            Friend f1 = new Friend 
            { 
                user1ID = user1.Id,
                user2ID=user2.Id,
                status=0
            };

            ctx.Friends.Add(f1);
            ctx.SaveChanges();
        }
        public static void AddFriend(User u,string id)
        {
            
            Friend f1 = ctx.Friends.FirstOrDefault(a => a.user1ID == id && a.user2ID == u.Id);
            f1.status = 1;
            Friend f2 = new Friend
            {
                user1ID = u.Id,
                user2ID = id,
                status = 1
            };
            ctx.Friends.Add(f2);
            ctx.SaveChanges();
        }
        
    }
}
