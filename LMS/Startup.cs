using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using LMS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(LMS.Startup))]

namespace LMS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
