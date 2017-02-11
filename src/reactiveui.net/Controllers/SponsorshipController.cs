using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace reactiveui.net.Controllers
{
    public class SponsorshipController : Controller
    {
        [Route("/sponsorship")]
        public IActionResult Index()
        {
            return Redirect("mailto:hello@reactiveui.net?subject=" + WebUtility.UrlEncode("Howdy, let's talk about brand placement on ReactiveUI's website, GitHub & other such nice things"));
        }
    }

}
