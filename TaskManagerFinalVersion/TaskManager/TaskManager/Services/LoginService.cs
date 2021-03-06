﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class LoginService: ILoginService
    {
        private readonly SignInManager<Users> _signInManager;

        public LoginService(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        }
    }
}
