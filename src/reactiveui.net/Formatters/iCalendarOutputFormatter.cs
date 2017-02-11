// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using reactiveui.net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace reactiveui.net.Formatters
{
    public class iCalendarOutputFormatter : OutputFormatter
    {
        private static Task _done = Task.FromResult(0);

        private bool _noNextShow = false;

        public iCalendarOutputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/calendar"));
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(LiveShowDetails);
        }

        public override void WriteResponseHeaders(OutputFormatterWriteContext context)
        {
            var liveShowDetails = context.Object as LiveShowDetails;

            Debug.Assert(context.Object == null || liveShowDetails != null, $"Object to be formatted should be of type {nameof(LiveShowDetails)}");

            if (liveShowDetails == null || liveShowDetails.NextShowDateUtc == null)
            {
                _noNextShow = true;
                context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            }
            else
            {
                base.WriteResponseHeaders(context);
            }
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var liveShowDetails = context.Object as LiveShowDetails;

            Debug.Assert(context.Object == null || liveShowDetails != null, $"Object to be formatted should be of type {nameof(LiveShowDetails)}");

            if (_noNextShow)
            {
                return _done;
            }

            return WriteLiveShowDetailsToResponseBody(liveShowDetails.NextShowDateUtc, context.HttpContext.Response);
        }

        private static string _dateTimeFormat = "yyyyMMddTHHmmssZ";

        private static Task WriteLiveShowDetailsToResponseBody(DateTime? nextShowDateUtc, HttpResponse response)
        {
            // Internet Calendaring and Scheduling Core Object Specification (iCalendar): https://tools.ietf.org/html/rfc5545
            /* BEGIN:VCALENDAR
               VERSION:2.0
               BEGIN:VEVENT
               UID:hello@reactiveui.net
               DESCRIPTION:ReactiveUI Community Standup
               DTSTART:20150804T170000Z
               DTEND:20150804T035959Z
               SUMMARY:ReactiveUI Community Standup
               LOCATION:https://reactiveui.net.net/
               BEGIN:VALARM
               TRIGGER:-PT15M
               ACTION:DISPLAY
               END:VALARM
               END:VEVENT
               END:VCALENDAR */

            var calendarString = new StringBuilder()
                .AppendLine("BEGIN:VCALENDAR")
                .AppendLine("VERSION:2.0")
                .AppendLine("BEGIN:VEVENT")
                .AppendLine("UID:hello@reactiveui.net")
                .AppendLine("DESCRIPTION:ReactiveUI Community Standup")
                .AppendLine("DTSTART:" + nextShowDateUtc?.ToString(_dateTimeFormat))
                .AppendLine("DTEND:" + nextShowDateUtc?.AddMinutes(30).ToString(_dateTimeFormat))
                .AppendLine("SUMMARY:ReactiveUI Community Standup")
                .AppendLine("LOCATION:https://reactiveui.net/")
                .AppendLine("BEGIN:VALARM")
                .AppendLine("TRIGGER:-PT15M")
                .AppendLine("ACTION:DISPLAY")
                .AppendLine("END:VALARM")
                .AppendLine("END:VEVENT")
                .AppendLine("END:VCALENDAR")
                .ToString();

            return response.WriteAsync(calendarString);
        }
    }
}