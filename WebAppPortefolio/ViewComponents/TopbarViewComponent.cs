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

            //string Uid = _accessor.HttpContext.Session.GetString("UserID");

            //Utilizador _u = db.Utilizadores.Where(ut => ut.ID.ToString() == Uid).FirstOrDefault();

            //Nome do utilizador
            ViewBag.NomeDele = "André Silva";

            return View();
        }               
    }
}
