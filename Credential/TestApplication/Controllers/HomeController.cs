﻿using System;
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
            if (Request.Cookies["Github"] == null)
            {
                return Redirect("/github/requestcode");
            }

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
            if (Request.Cookies["Google"] != null)
            {
                var cookie = new HttpCookie("Google")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("Github")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                Response.Cookies.Add(cookie);
            }
            

            return Redirect("/");
        }
    }
}

