using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace reactiveui.net.Controllers
{
    public class SlackController : Controller
    {
        [Route("/slack")]
        public IActionResult Index()
        {
            return Redirect("mailto:hello@reactiveui.net?subject=" + Uri.EscapeDataString("Howdy, can you send me an invite to Slack?"));
        }
    }

}
