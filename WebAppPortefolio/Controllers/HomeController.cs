using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using NToastNotify;

namespace WebAppPortefolio.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _accessor;

        private readonly IToastNotification _toastNotification;

        //Construtor
        public HomeController(IHttpContextAccessor accessor, IToastNotification toastNotification)
        {
            _accessor = accessor;
            _toastNotification = toastNotification;
        }

        //Página inicial
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Info
                _toastNotification.AddInfoToastMessage("Bem vindo!");

                //Está autenticado
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }       
    }
}
