using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class GetHelpController : Controller
    {
        [Route("/get-help")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
