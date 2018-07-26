using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebAppPortefolio.Data;
using WebAppPortefolio.Models;
using WebAppPortefolio.Utils;

namespace WebAppPortefolio.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IHttpContextAccessor _accessor;

        //Construtor
        public AccountController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        //Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Está autenticado
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }

        //Login
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(IFormCollection col)
        {
            DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
            var _context = new PortefolioContext(_options);

            string username = col["usernam"];
            string pass = col["passw"];

            //Buscar user
            Utilizador _u = _context.Utilizadores.Where(ux => ux.Username == username && ux.IsActive).FirstOrDefault();

            //Verificar user e pass hash
            if (_u != null && Funcionalidades.GetUInt64Hash(MD5.Create(), pass) == _u.PasswordH)
            {
                try
                {
                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, _u.Email),
                                    new Claim(ClaimTypes.GivenName, _u.Nome)
                                };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity)
                        ).GetAwaiter().GetResult();

                    //return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception information: {0}", ex);
                    return View(col);
                }

            }
            else
            {
                return View(col);
            }

            //Se correr bem
            return RedirectToAction("Index", "Home");
        }

        //Logout
        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }

        //Registar novo Utilizador
        [AllowAnonymous]
        public IActionResult NewUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Está autenticado
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        //Registar novo Utilizador
        [HttpPost]
        [AllowAnonymous]
        public IActionResult NewUser(Utilizador _model)
        {
            DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
            var _context = new PortefolioContext(_options);

            if (ModelState.IsValid)
            {
                //Novo id
                _model.ID = (_context.Utilizadores.Count() + 1).ToString() + "#User";

                //Pass segura com Hash MD5
                _model.PasswordH = Funcionalidades.GetUInt64Hash(MD5.Create(), _model.PasswordH);

                //Senão conseguir gerar a hash
                if (_model.PasswordH == null)
                    return View(_model);

                _context.Utilizadores.Add(_model);

                _context.SaveChanges();

                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(_model);
            }
        }

        //Recuperar password
        [AllowAnonymous]
        public IActionResult ForgotCredentials()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Está autenticado
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        //Editar utilizador autenticado
        [HttpGet]
        public IActionResult Profile()
        {
            DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
            var _context = new PortefolioContext(_options);

            if (User.Identity.IsAuthenticated)
            {
                //Buscar user para mostrar
                Utilizador Xuser = _context.Utilizadores.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

                //Mostrar User
                return View(Xuser);

            }
            else
            {                
                return RedirectToAction("Login", "Account");
            }
        }

        //Editar utilizador autenticado
        [HttpPost]
        public IActionResult Profile(Utilizador _model, IFormCollection col)
        {

            if (User.Identity.IsAuthenticated)
            {
                DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
                var _context = new PortefolioContext(_options);

                //Buscar user
                Utilizador Xuser = _context.Utilizadores.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

                //Alterar dados
                Xuser.Nome = _model.Nome;
                Xuser.Username = _model.Username;
                Xuser.Email = _model.Email;

                //Buscar Chave
                string PalavraChave = col["Passwordd"];

                //Se for para mudar a pass
                if (!String.IsNullOrEmpty(PalavraChave))
                {
                    Xuser.PasswordH = Funcionalidades.GetUInt64Hash(MD5.Create(), PalavraChave);
                }

                //Guardar edição
                _context.Utilizadores.Update(Xuser);
                _context.SaveChanges();

                return View(_model);

            }
            else
            {
                //Não altera 
                return View(_model);
            }         
            
        }

        //Helpers
        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
        }
        #endregion

    }
}