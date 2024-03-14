using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BanHangThoiTrangMVC.Areas.Admin.Controllers
{
    public class AuthenticationManager
    {
        private static AuthenticationManager _instance;
        private readonly IAuthenticationManager _authenticationManager;

        private AuthenticationManager(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        public static AuthenticationManager Instance(IAuthenticationManager authenticationManager)
        {
            if (_instance == null)
            {
                _instance = new AuthenticationManager(authenticationManager);
            }
            return _instance;
        }
        public void SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
        public void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie);
                    }
                }
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
    }
}