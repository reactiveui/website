using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class DocumentationController : Controller
    {
        [Route("/documentation")]
        public IActionResult Index()
        {
            return Redirect("https://docs.reactiveui.net/en");
        }
    }
}
