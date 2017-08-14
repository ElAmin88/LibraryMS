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
            app.CreatePerOwinContext(() => new LibraryContext());
            app.CreatePerOwinContext<UserStore<User>>((opt, cont) => new UserStore<User>(cont.Get<LibraryContext>()));
            app.CreatePerOwinContext<UserManager<User>>((opt, cont) => new UserManager<User>(new UserStore<User>(cont.Get<LibraryContext>())));

            app.CreatePerOwinContext<SignInManager<User, string>>((opt, cont) => new SignInManager<User, string>(cont.Get<UserManager<User>>(), cont.Authentication));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });

        }
    }
}
