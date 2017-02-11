using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class DonateController : Controller
    {
        [Route("/donate")]
        public IActionResult Index()
        {
            return Redirect("https://opencollective.com/reactiveui");
        }
    }
}
