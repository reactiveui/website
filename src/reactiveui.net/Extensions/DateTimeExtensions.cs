// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System
{
    public static class DateTimeExtensions
    {
        private const string AEST = "AUS Eastern Standard Time";
        private static readonly TimeZoneInfo _aestTimeZone = TimeZoneInfo.FindSystemTimeZoneById(AEST);

        public static DateTime ConvertToTimeZone(this DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone);
        }

        public static DateTime ConvertFromUtcToAest(this DateTime dateTime)
        {
            return dateTime.ConvertToTimeZone(TimeZoneInfo.Utc, _aestTimeZone);
        }

        public static DateTime ConvertFromPtcToUtc(this DateTime dateTime)
        {
            return dateTime.ConvertToTimeZone(_aestTimeZone, TimeZoneInfo.Utc);
        }
    }
}
