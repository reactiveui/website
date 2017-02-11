using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class StackOverflowController : Controller
    {
        [Route("/stackoverflow")]
        public IActionResult Index()
        {
            return Redirect("https://stackoverflow.com/questions/tagged/reactiveui");
        }
    }
}
