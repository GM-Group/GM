using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GM.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult SignIn()
        {
            bool result = Request.IsAuthenticated;
            string loginName = HttpContext.User.Identity.Name;

            ViewBag.LoginName = loginName;
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            string loginName = "zhangsan";
            string data = "lisi";
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, loginName, DateTime.Now, DateTime.Now.AddDays(1), true, data);
            //string cookieValue = FormsAuthentication.Encrypt(ticket);
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);



            //cookie.HttpOnly = true;
            //cookie.Secure = FormsAuthentication.RequireSSL;
            //cookie.Domain = FormsAuthentication.CookieDomain;
            //cookie.Path = FormsAuthentication.FormsCookiePath;

            //HttpContext context = System.Web.HttpContext.Current;
            //if (context == null)
            //    throw new InvalidOperationException();


            //context.Response.Cookies.Remove(cookie.Name);
            //context.Response.Cookies.Add(cookie);
            
            FormsAuthentication.SetAuthCookie(loginName,true);
            
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}