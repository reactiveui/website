using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactiveui.net.Controllers
{
    public class ContributeController : Controller
    {
        [Route("/contribute")]
        public IActionResult Index()
        {
            return View();
        }

    }

}
