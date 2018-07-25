using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppPortefolio.Data;
using WebAppPortefolio.Models;

namespace WebAppPortefolio.ViewComponents
{
    public class TopbarViewComponent : ViewComponent
    {
        private IHttpContextAccessor _accessor;

        public TopbarViewComponent(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IViewComponentResult Invoke(
        int maxPriority, bool isDone)
        {
            DbContextOptions<PortefolioContext> _options = new DbContextOptions<PortefolioContext>();
            var db = new PortefolioContext(_options);

            //Inicializar ome do utilizador
            ViewBag.NomeDele = "Sem Utilizador";

            if (User.Identity.IsAuthenticated)
            {
                Utilizador _u = db.Utilizadores.Where(ut => ut.Email == HttpContext.User.Identity.Name).FirstOrDefault();

                if(_u != null)
                    ViewBag.NomeDele = _u.Nome;
            }            

            return View();
        }               
    }
}
