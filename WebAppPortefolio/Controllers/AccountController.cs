using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppPortefolio.Data;
using WebAppPortefolio.Models;
using WebAppPortefolio.Utils;

namespace WebAppPortefolio.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IHttpContextAccessor _accessor;

        public AccountController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(IFormCollection col)
        {
            var _context = new PortefolioContext();

            string username = col["usernam"];
            string pass = col["passw"];

            //Buscar user
            Utilizador _u = _context.Utilizadores.Where(ux => ux.Username == username && ux.IsActive).FirstOrDefault();

            if (_u != null)
            {
                //Verificar pass hash
                if (_u.PasswordH == Funcionalidades.CreateHash(pass))
                {
                    //Variavel de sessao
                    _accessor.HttpContext.Session.SetString("UserID", _u.ID.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Senao coincidirem, volta para a view Login
                    return View();
                }
            }
            else
            {
                //Senao existir, volta para a view Login
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //Limpar sessao
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
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