using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace reactiveui.net.Controllers
{
    public class ContactController : Controller
    {
        [Route("/contact")]
        public IActionResult Index()
        {
            return Redirect("mailto:hello@reactiveui.net?subject=" + WebUtility.UrlEncode("Howdy, ......?"));
        }
    }

}
