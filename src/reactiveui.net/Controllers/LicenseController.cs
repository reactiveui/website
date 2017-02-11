using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class LicenseController : Controller
    {
        [Route("/license")]
        public IActionResult Index()
        {
            return Redirect("https://github.com/reactiveui/ReactiveUI/blob/develop/LICENSE");
        }
    }
}
