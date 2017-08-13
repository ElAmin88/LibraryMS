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
        public static User currentUser;
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
        public static List<User> Search(string search,User u)
        {
            if (search == null)
            {
                return new List<User>();
            }
            
            List<User> users = ctx.Users.Where(a => a.name.Contains(search)&& a.name!=u.name).ToList();
            List<User> friends = GetFriends(u);
            for (int i = users.Count - 1; i >= 0; i--)
            {
                foreach (User friend in friends)
                {
                    if (users[i].name == friend.name)
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
            List<int> friendsID = ctx.Friends.Where(a => a.user1ID == u.ID&& a.status==1).Select(b=>b.user2ID).ToList();

            List<User> friends = new List<User>();
            foreach (int id in friendsID)
            {
                friends.Add(ctx.Users.FirstOrDefault(a => a.ID == id));
            }
            return friends;
        }
        public static List<User> GetFriendRequest(User u)
        {
            List<int> friendsID = ctx.Friends.Where(a => a.user2ID == u.ID && a.status == 0).Select(b => b.user1ID).ToList();

            List<User> friends = new List<User>();
            foreach (int id in friendsID)
            {
                friends.Add(ctx.Users.FirstOrDefault(a => a.ID == id));
            }
            return friends;
        }

        public static void SendFriendRequest(User user1,string name)
        {
            User user2 = Users.GetByName(name);
            Friend f1 = new Friend 
            { 
                user1ID = user1.ID,
                user2ID=user2.ID,
                status=0
            };

            ctx.Friends.Add(f1);
            ctx.SaveChanges();
        }
        public static void AddFriend(User u,int id)
        {
            
            Friend f1 = ctx.Friends.FirstOrDefault(a => a.user1ID == id && a.user2ID == u.ID);
            f1.status = 1;
            Friend f2 = new Friend
            {
                user1ID = u.ID,
                user2ID = id,
                status = 1
            };
            ctx.Friends.Add(f2);
            ctx.SaveChanges();
        }
        
    }
}
