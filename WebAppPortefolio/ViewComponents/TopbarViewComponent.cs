using Microsoft.AspNetCore.Http;
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
        private IHttpContextAccessor _accessor;

        public TopbarViewComponent(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IViewComponentResult Invoke(
        int maxPriority, bool isDone)
        {
            return View();
        }               
    }
}
