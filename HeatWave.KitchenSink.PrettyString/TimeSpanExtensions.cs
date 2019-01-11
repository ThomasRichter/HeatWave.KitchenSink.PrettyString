using System;

namespace HeatWave.KitchenSink.PrettyString
{
    public static class TimeSpanExtensions
    {
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan OneHundredMillis = TimeSpan.FromMilliseconds(100);
        private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan OneHour = TimeSpan.FromHours(1);

        public static string ToPrettyString(this TimeSpan timeSpan)
        {
            if (timeSpan > OneHour)
            {
                return $"{Math.Floor(timeSpan.TotalHours):0} hours, {timeSpan.Minutes} minutes";
            }

            if (timeSpan > OneMinute)
            {
                return $"{Math.Floor(timeSpan.TotalMinutes):0} minutes, {timeSpan.Seconds} seconds";
            }

            if (timeSpan > OneSecond)
            {
                return $"{timeSpan.TotalSeconds:0.0} seconds";
            }

            if (timeSpan > OneHundredMillis)
            {
                return $"{Math.Round(timeSpan.TotalMilliseconds):0} millis";
            }

            return $"{timeSpan.TotalMilliseconds:0.0} millis";
        }
    }
}
