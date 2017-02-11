using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class ChangelogController : Controller
    {
        [Route("/changelog")]
        public IActionResult Index()
        {
            return Redirect("https://github.com/reactiveui/reactiveui/releases");
        }

    }
}
