// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using reactiveui.net.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace reactiveui.net.Services
{
    public class YouTubeShowsService : IShowsService
    {
        public const string CacheKey = nameof(YouTubeShowsService);

        private readonly IHostingEnvironment _env;
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _cache;
        private readonly TelemetryClient _telemetry;

        public YouTubeShowsService(
            IHostingEnvironment env,
            IOptions<AppSettings> appSettings,
            IMemoryCache memoryCache,
            TelemetryClient telemetry)
        {
            _env = env;
            _appSettings = appSettings.Value;
            _cache = memoryCache;
            _telemetry = telemetry;
        }

        public async Task<ShowList> GetRecordedShowsAsync(ClaimsPrincipal user, bool disableCache)
        {
            if (string.IsNullOrEmpty(_appSettings.YouTubeApiKey))
            {
                return new ShowList { Shows = DesignData.Shows };
            }

            if (user.Identity.IsAuthenticated && disableCache)
            {
                return await GetShowsList();
            }

            var result = _cache.Get<ShowList>(CacheKey);

            if (result == null)
            {
                result = await GetShowsList();

                _cache.Set(CacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });
            }

            return result;
        }

        private async Task<ShowList> GetShowsList()
        {
            using (var client = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = _appSettings.YouTubeApplicationName,
                ApiKey = _appSettings.YouTubeApiKey
            }))
            {
                var listRequest = client.PlaylistItems.List("snippet");
                listRequest.PlaylistId = _appSettings.YouTubePlaylistId;
                listRequest.MaxResults = 10 * 3; // 10 rows of 3 episodes

                PlaylistItemListResponse playlistItems = null;
                var started = Timing.GetTimestamp();
                try
                {
                    playlistItems = await listRequest.ExecuteAsync();
                }
                finally
                {
                    TrackDependency(client.BaseUri, listRequest, playlistItems, started);
                }

                var result = new ShowList();

                result.Shows = playlistItems.Items.Select(item => new Show
                {
                    Provider = "YouTube",
                    ProviderId = item.Snippet.ResourceId.VideoId,
                    Title = GetUsefulBitsFromTitle(item.Snippet.Title),
                    Description = item.Snippet.Description,
                    ShowDate = DateTimeOffset.Parse(item.Snippet.PublishedAtRaw, null, DateTimeStyles.RoundtripKind),
                    ThumbnailUrl = item.Snippet.Thumbnails.High.Url,
                    Url = GetVideoUrl(item.Snippet.ResourceId.VideoId, item.Snippet.PlaylistId, item.Snippet.Position ?? 0)
                }).ToList();

                // hacky: the dates here are "dates added to the playlist. Here we fudge the dates on some of the
                // older videos which don't belong to our channel so that they don't appear first.

                // monkey patch ReactiveUI with Xamarin - Michael Stonis & Geoffrey Huntley - Xamarin University Guest Lecture - https://www.youtube.com/watch?v=vydDJ9CaIug
                foreach (var video in result.Shows.Where(x => x.Url.Contains("vydDJ9CaIug")))
                {
                    video.ShowDate = DateTime.Parse("2017-03-17");
                }

                // monkey patch "FRP In Practice: Taking a look at Reactive[UI/Cocoa]" by Paul Betts - https://www.youtube.com/watch?v=1XNATGjqM6U
                foreach (var video in result.Shows.Where(x => x.Url.Contains("1XNATGjqM6U")))
                {
                    video.ShowDate = DateTime.Parse("2014-09-22");
                }


                // monkey patch "FRP In Practice: Taking a look at Reactive[UI/Cocoa]" by Paul Betts - https://www.youtube.com/watch?v=1XNATGjqM6U
                foreach (var video in result.Shows.Where(x => x.Url.Contains("1XNATGjqM6U")))
                {
                    video.ShowDate = DateTime.Parse("2014-09-22");
                }

                // monkey patch Xamarin Evolve 2014: Awaiting for Rx: A Play in Four Acts - Paul Betts, Github - https://www.youtube.com/watch?v=5DZ8nC0ENdg
                foreach (var video in result.Shows.Where(x => x.Url.Contains("5DZ8nC0ENdg")))
                {
                    video.ShowDate = DateTime.Parse("2014-10-24");
                }


                if (!string.IsNullOrEmpty(playlistItems.NextPageToken))
                {
                    result.MoreShowsUrl = GetPlaylistUrl(_appSettings.YouTubePlaylistId);
                }

            return result;
            }
        }

        private void TrackDependency(string url, PlaylistItemsResource.ListRequest listRequest, PlaylistItemListResponse playlistItems, long started)
        {
            if (_telemetry.IsEnabled())
            {
                Uri uri = null;
                Uri.TryCreate(url, UriKind.Absolute, out uri);
                var duration = Timing.GetDuration(started);
                var dependency = new DependencyTelemetry
                {
                    Type = "HTTP",
                    Target = uri?.Host ?? url,
                    Name = listRequest.RestPath,
                    Data = listRequest.CreateRequest().RequestUri.ToString(),
                    Timestamp = DateTimeOffset.UtcNow,
                    Duration = duration,
                    Success = playlistItems != null
                };
                dependency.Properties.Add("HTTP Method", listRequest.HttpMethod);
                dependency.Properties.Add("Event Id", playlistItems.EventId);
                dependency.Properties.Add("Total Results", (playlistItems.PageInfo.TotalResults ?? 0).ToString());
                _telemetry.TrackDependency(dependency);
            }
        }

        private static string GetUsefulBitsFromTitle(string title)
        {
            if (title == "Xamarin Evolve 2014: Awaiting for Rx: A Play in Four Acts - Paul Betts, Github")
            {
                return "Awaiting for Rx: A Play in Four Acts";
            }

            if (title == "\"FRP In Practice: Taking a look at Reactive[UI/Cocoa]\" by Paul Betts")
            {
                return @"Functional Reactive Programming in Practice";
            }

            if (title.Count(c => c == '-') < 2)
            {
                return string.Empty;
            }

            var lastHyphen = title.LastIndexOf('-');
            if (lastHyphen >= 0)
            {
                var result = title.Substring(lastHyphen + 1);
                return result;
            }

            return string.Empty;
        }

        private static string GetVideoUrl(string id, string playlistId, long itemIndex)
        {
            var encodedId = UrlEncoder.Default.Encode(id);
            var encodedPlaylistId = UrlEncoder.Default.Encode(playlistId);
            var encodedItemIndex = UrlEncoder.Default.Encode(itemIndex.ToString());

            return $"https://www.youtube.com/watch?v={encodedId}&list={encodedPlaylistId}&index={encodedItemIndex}";
        }

        private static string GetPlaylistUrl(string playlistId)
        {
            var encodedPlaylistId = UrlEncoder.Default.Encode(playlistId);

            return $"https://www.youtube.com/playlist?list={encodedPlaylistId}";
        }

        private static class DesignData
        {
            private static readonly TimeSpan _pstOffset = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time").BaseUtcOffset;

            public static readonly string LiveShow = null;

            public static readonly IList<Show> Shows = new List<Show>
            {
                new Show
                {
                    ShowDate = new DateTimeOffset(2015, 7, 21, 9, 30, 0, _pstOffset),
                    Title = "ASP.NET Community Standup - July 21st 2015",
                    Provider = "YouTube",
                    ProviderId = "7O81CAjmOXk",
                    ThumbnailUrl = "https://img.youtube.com/vi/7O81CAjmOXk/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=7O81CAjmOXk&index=1&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },
                new Show
                {
                    ShowDate = new DateTimeOffset(2015, 7, 14, 15, 30, 0, _pstOffset),
                    Title = "ASP.NET Community Standup - July 14th 2015",
                    Provider = "YouTube",
                    ProviderId = "bFXseBPGAyQ",
                    ThumbnailUrl = "https://img.youtube.com/vi/bFXseBPGAyQ/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=bFXseBPGAyQ&index=2&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },

                new Show
                {
                    ShowDate = new DateTimeOffset(2015, 7, 7, 15, 30, 0, _pstOffset),
                    Title = "ASP.NET Community Standup - July 7th 2015",
                    Provider = "YouTube",
                    ProviderId = "APagQ1CIVGA",
                    ThumbnailUrl = "https://img.youtube.com/vi/APagQ1CIVGA/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=APagQ1CIVGA&index=3&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },
                new Show
                {
                    ShowDate = DateTimeOffset.Now.AddDays(-28),
                    Title = "ASP.NET Community Standup - July 21st 2015",
                    Provider = "YouTube",
                    ProviderId = "7O81CAjmOXk",
                    ThumbnailUrl = "https://img.youtube.com/vi/7O81CAjmOXk/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=7O81CAjmOXk&index=1&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },
            };
        }
    }
}
