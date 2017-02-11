using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class SourceCodeController : Controller
    {
        [Route("/source-code")]
        public IActionResult Index()
        {
            return Redirect("https://github.com/reactiveui/ReactiveUI");
        }

    }
}
