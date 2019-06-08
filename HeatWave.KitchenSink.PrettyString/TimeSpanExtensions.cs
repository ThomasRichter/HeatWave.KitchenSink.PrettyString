using System;

namespace HeatWave.KitchenSink.PrettyString
{
    /// <summary>
    ///  This static class contains the extension method <see cref="ToPrettyString(TimeSpan)"/> for
    ///  displaying <see cref="TimeSpan"/> instances with an appropriate unit.
    /// </summary>
    public static class TimeSpanExtensions
    {
        private static readonly TimeSpan OneHour = TimeSpan.FromHours(1);
        private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1);

        private static readonly TimeSpan TenSeconds = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        private static readonly TimeSpan OneHundredMillis = TimeSpan.FromMilliseconds(100);
        private static readonly TimeSpan TenMillis = TimeSpan.FromMilliseconds(10);

        /// <summary>
        /// Returns a string using the appropriate unit to display 3 or 4 significant digits.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string ToPrettyString(this TimeSpan timeSpan)
        {
            var absTimeSpan = timeSpan >= TimeSpan.Zero
                ? timeSpan
                : timeSpan.Negate();

            // '0 millis' through '0.99 millis' (for up to 0.994) 
            // '1 milli' (for 0.995 through 1.004)
            // '1.01 millis' (for 1.005 and up)
            // '9.99 millis'
            if (absTimeSpan < TenMillis)
            {
                return $"{timeSpan.TotalMilliseconds:0.##} {(RoundsToSingleMilli(absTimeSpan) ? "milli" : "millis")}";                
            }

            // '10 millis' through '99.9 millis'
            if (absTimeSpan < OneHundredMillis)
            {
                return $"{timeSpan.TotalMilliseconds:0.#} millis";               
            }

            // '100 millis' through '999 millis'
            if (absTimeSpan < OneSecond)
            {
                return $"{timeSpan.TotalMilliseconds:0} millis";           
            }

            // '1 second' through '9.99 seconds'
            if (absTimeSpan < TenSeconds)
            {
                return $"{timeSpan.TotalSeconds:0.##} {(RoundsToSingleSecond(absTimeSpan) ? "second" : "seconds")}";
            }

            // '10 seconds' through '59.9 seconds'
            if (absTimeSpan < OneMinute)
            {
                return $"{timeSpan.TotalSeconds:0.#} {(RoundsToSingleSecond(absTimeSpan) ? "second" : "seconds")}";
            }

            // '1 minute' through '59 minutes, 59 seconds'
            if (absTimeSpan < OneHour)
            {
                return $"{timeSpan.Minutes} {Pluralize("minute", "minutes", timeSpan.Minutes)}{(timeSpan.Seconds == 0 ? "" : $", {timeSpan.Seconds} {Pluralize("second", "seconds", timeSpan.Seconds)}")}";
            }

            // '1 hour' ... 1 hour, 5 minutes
            return $"{timeSpan.Hours} {Pluralize("hour", "hours", timeSpan.Hours)}{(timeSpan.Minutes == 0 ? "" : $", {timeSpan.Minutes} {Pluralize("minute", "minutes", timeSpan.Minutes)}")}";
        }

        private static string Pluralize(string singular, string plural, int number) => Math.Abs(number) == 1 ? singular : plural;

        private static readonly long TicksPerHundrethMilli = TimeSpan.TicksPerMillisecond / 1000;

        private static readonly TimeSpan MillisLowerInclusiveRoundingBound = TimeSpan.FromTicks(TicksPerHundrethMilli * 995);
        private static readonly TimeSpan MillisUpperExclusiveRoundingBound = TimeSpan.FromTicks(TicksPerHundrethMilli * 1005);

        private static bool RoundsToSingleMilli(TimeSpan timeSpan) => timeSpan >= MillisLowerInclusiveRoundingBound && timeSpan < MillisUpperExclusiveRoundingBound;

        private static readonly TimeSpan SecondsUpperExclusiveRoundingBound = TimeSpan.FromMilliseconds(1004);

        private static bool RoundsToSingleSecond(TimeSpan timeSpan) => timeSpan < SecondsUpperExclusiveRoundingBound;
    }
}
