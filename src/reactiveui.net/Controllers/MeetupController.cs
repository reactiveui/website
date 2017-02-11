// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using reactiveui.net.Models;
using reactiveui.net.Services;
using reactiveui.net.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace reactiveui.net.Controllers
{
    public class MeetupController : Controller
    {
        private readonly ILiveShowDetailsService _liveShowDetails;
        private readonly IShowsService _showsService;

        public MeetupController(IShowsService showsService, ILiveShowDetailsService liveShowDetails)
        {
            _showsService = showsService;
            _liveShowDetails = liveShowDetails;
        }

        [Route("/meetup")]
        public async Task<IActionResult> Index(bool? disableCache)
        {
            var liveShowDetails = await _liveShowDetails.LoadAsync();
            var showList = await _showsService.GetRecordedShowsAsync(User, disableCache ?? false);

            return View(new MeetupViewModel
            {
                AdminMessage = liveShowDetails?.AdminMessage,
                NextShowDateUtc = liveShowDetails?.NextShowDateUtc,
                LiveShowEmbedUrl = liveShowDetails?.LiveShowEmbedUrl,
                LiveShowHtml = liveShowDetails?.LiveShowHtml,
                PreviousShows = showList.Shows,
                MoreShowsUrl = showList.MoreShowsUrl
            });
        }

        [HttpGet("/meetup/ical")]
        [Produces("text/calendar")]
        public async Task<LiveShowDetails> GetiCal()
        {
            var liveShowDetails = await _liveShowDetails.LoadAsync();

            return liveShowDetails;
        }
    }
}
