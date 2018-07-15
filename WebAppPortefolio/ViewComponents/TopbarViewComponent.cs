using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var db = new PortefolioContext();

            string Uid = _accessor.HttpContext.Session.GetString("UserID");

            Utilizador _u = db.Utilizadores.Where(ut => ut.ID.ToString() == Uid).FirstOrDefault();

            //Nome do utilizador
            ViewBag.NomeDele = _u.Nome;

            return View();
        }               
    }
}
