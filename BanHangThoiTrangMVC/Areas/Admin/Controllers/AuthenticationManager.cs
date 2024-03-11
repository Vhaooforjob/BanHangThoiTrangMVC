﻿using System;
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

    }
}