using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppPortefolio.Data;

namespace WebAppPortefolio.ViewComponents
{
    public class TopbarViewComponent : ViewComponent
    {
        private readonly PortefolioContext db;

        public TopbarViewComponent(PortefolioContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke(
        int maxPriority, bool isDone)
        {
            return View();
        }               
    }
}
