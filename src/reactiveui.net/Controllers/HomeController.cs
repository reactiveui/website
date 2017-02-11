using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reactiveui.net.Services;
using reactiveui.net.ViewModels;

namespace reactiveui.net.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILiveShowDetailsService _liveShowDetails;
        private readonly IShowsService _showsService;

        public HomeController(IShowsService showsService, ILiveShowDetailsService liveShowDetails)
        {
            _showsService = showsService;
            _liveShowDetails = liveShowDetails;
        }

        [Route("/")]
        public async Task<IActionResult> Index(bool? disableCache)
        {
            var meetupsToDisplay = 3;
            var liveShowDetails = await _liveShowDetails.LoadAsync();
            var showList = await _showsService.GetRecordedShowsAsync(User, disableCache ?? false);

            return View(new MeetupViewModel
            {
                AdminMessage = liveShowDetails?.AdminMessage,
                NextShowDateUtc = liveShowDetails?.NextShowDateUtc,
                LiveShowEmbedUrl = liveShowDetails?.LiveShowEmbedUrl,
                LiveShowHtml = liveShowDetails?.LiveShowHtml,
                PreviousShows = showList.Shows.Take(meetupsToDisplay).ToArray(),
                MoreShowsUrl = showList.MoreShowsUrl
            });
        }

        [Route("/error")]
        public IActionResult Error()
        {
            return View();
        }

    }
}
