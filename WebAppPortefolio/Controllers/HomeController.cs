using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebAppPortefolio.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _accessor;

        //Construtor
        public HomeController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        //Página inicial
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
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
