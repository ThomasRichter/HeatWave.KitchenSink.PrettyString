using HeatWave.KitchenSink.PrettyString;
using System;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintTimeSpan(FromMillisPrecise(0));
            PrintTimeSpan(FromMillisPrecise(0.1));
            PrintTimeSpan(FromMillisPrecise(0.994));
            PrintTimeSpan(FromMillisPrecise(0.995));
            PrintTimeSpan(FromMillisPrecise(1));
            PrintTimeSpan(FromMillisPrecise(1.004));
            PrintTimeSpan(FromMillisPrecise(1.005));

            PrintTimeSpan(FromMillisPrecise(-1.005));
            PrintTimeSpan(TimeSpan.FromSeconds(-1.85));
            PrintTimeSpan(TimeSpan.FromHours(-1.85));
        }

        private static void PrintTimeSpan(TimeSpan timeSpan)
        {
            Console.WriteLine($"{timeSpan.ToPrettyString().PadRight(14)} | {timeSpan.TotalMilliseconds}");
        }

        private static TimeSpan FromMillisPrecise(double millis)
        {
            return TimeSpan.FromTicks((long)Math.Round(TimeSpan.TicksPerMillisecond * millis));
        }
    }
}
