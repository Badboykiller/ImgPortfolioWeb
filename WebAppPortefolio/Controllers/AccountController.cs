using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppPortefolio.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {       
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult NewUser()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgotCredentials()
        {
            return View();
        }
    }
}