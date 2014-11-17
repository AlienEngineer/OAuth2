using System;
using System.Web;
using System.Web.Mvc;
using TestApplication.Filters;

namespace TestApplication.Controllers
{
    [OAuthCookieFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies["OAuth"] != null)
            {
                var cookie = new HttpCookie("OAuth")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                Response.Cookies.Add(cookie);
            }
            

            return Redirect("/");
        }
    }
}

