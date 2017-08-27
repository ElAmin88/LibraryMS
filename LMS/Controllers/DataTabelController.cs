using LMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class DataTabelController : Controller
    {
        // GET: DataTabel
        public String Index(string id)
        {
              
            DataTable tb= ResTest.getUsers(id);
     
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(tb);
            return JSONString;
        }

        public string All ()
        {
            List<AspNetUser> l = ResTest.GetAllUsers();
            string JSONString = string.Empty;
            string s = "amin";
            JSONString = JsonConvert.SerializeObject(l);
            return JSONString+s.AddHello();
        }
        
    }
}