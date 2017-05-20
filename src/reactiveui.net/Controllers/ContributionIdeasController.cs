using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class ContributionIdeasController : Controller
    {
        [Route("/contribution-ideas")]
        public IActionResult Index()
        {
            return Redirect("https://reactiveui.net/dashboard/?showOpen=true&showClosed=false&showCommented=true&showUncommented=true&showIssues=true&showPullRequests=false&last24Hours=false&repos=null&labels=null&milestones=null&usernames=null#reactiveui");
        }
    }
}
