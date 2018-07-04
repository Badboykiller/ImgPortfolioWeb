using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppPortefolio.Data;
using WebAppPortefolio.Models;
using WebAppPortefolio.Utils;

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
        [HttpPost]
        public IActionResult NewUser(Utilizador _model)
        {
            var _context = new PortefolioContext();

            if (ModelState.IsValid)
            {
                //Novo id
                _model.ID = _context.Utilizadores.Count() + 1;

                //Pass segura com Hash MD5
                _model.PasswordH = Funcionalidades.CreateHash(_model.PasswordH);

                _context.Utilizadores.Add(_model);
                _context.SaveChanges();

                return RedirectToAction("Login", "Account");
            } 
            else
            {
                return View(_model);
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotCredentials()
        {
            return View();
        }
    }
}