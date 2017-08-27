using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Controllers
{
    public class ChatHub : Hub
    {
        public void Send(string name,string message)
        {
            Clients.All.BroadcastMessage(name,message);
        }
        public void SendRoom(string room,string name, string message)
        {
            Clients.Group(room).BroadcastMessage(name, message);
        }

        public void JoinChat(string name,string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Group(room).BroadcastMessage(name," has Joined The Room");
        }
    }
}