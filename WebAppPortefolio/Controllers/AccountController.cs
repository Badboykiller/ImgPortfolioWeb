using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAppPortefolio.Data;
using WebAppPortefolio.Models;
using WebAppPortefolio.Utils;

namespace WebAppPortefolio.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //Autenticação
        private readonly UserManager<Utilizador> _userManager;
        private readonly SignInManager<Utilizador> _signInManager;
        private readonly ILogger _logger;

        private IHttpContextAccessor _accessor;

        public AccountController(IHttpContextAccessor accessor, UserManager<Utilizador> userManager,
                    SignInManager<Utilizador> signInManager, ILogger<AccountController> logger)
        {
            _accessor = accessor;

            //Autenticação
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(IFormCollection col)
        {
            DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
            var _context = new PortefolioContext(_options);

            string username = col["usernam"];
            string pass = col["passw"];

            //Buscar user
            Utilizador _u = _context.Utilizadores.Where(ux => ux.UserName == username && ux.IsActive).FirstOrDefault();

            //Verificar user e pass hash
            if (_u != null && _u.PasswordHash == Funcionalidades.GetUInt64Hash(MD5.Create(), pass))
            {                
                var result = await _signInManager.PasswordSignInAsync(_u.UserName, _u.PasswordHash, false, false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in...");
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    _logger.LogWarning("Login error...");
                    return View(col);
                }
                
            }

            //Senao existir, volta para a view Login
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult NewUser(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult NewUser(Utilizador _model)
        {
            DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
            var _context = new PortefolioContext(_options);

            if (ModelState.IsValid)
            {
                //Random number
                Random random = new Random();
                int randomNumber = random.Next(101, 999999);

                //Novo id
                _model.Id = "#User#" + randomNumber.ToString() + "#" + (_context.Utilizadores.Count() + 1).ToString();

                //Pass segura com Hash MD5
                _model.PasswordHash = Funcionalidades.GetUInt64Hash(MD5.Create(), _model.PasswordHash);

                //Não consegui gerar a hash
                if (_model.PasswordHash == null)
                {
                    _logger.LogInformation("Hash generation error");
                    return View(_model);
                }

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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

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