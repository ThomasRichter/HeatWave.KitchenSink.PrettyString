using HeatWave.KitchenSink.PrettyString;
using NUnit.Framework;
using System;

public class TimeSpanExtensionsTests
{
    [Test]
    public void When0Ms_Then_0Millis() => Assert.That(TimeSpan.Zero.ToPrettyString(), Is.EqualTo("0 millis"));

    [Test]
    public void When0_994Ms_Then_0_99Millis() => Assert.That(FromMillisPrecise(0.994).ToPrettyString(), Is.EqualTo("0.99 millis"));

    [Test]
    public void When_0_995Ms_Then_1Milli() => Assert.That(FromMillisPrecise(0.995).ToPrettyString(), Is.EqualTo("1 milli"));

    [Test]
    public void When_1Ms_Then_1Milli() => Assert.That(TimeSpan.FromMilliseconds(1).ToPrettyString(), Is.EqualTo("1 milli"));

    [Test]
    public void When_1_004Ms_Then_1Milli() => Assert.That(FromMillisPrecise(1.004).ToPrettyString(), Is.EqualTo("1 milli"));

    [Test]
    public void When_1_005Ms_Then_1_01Millis() => Assert.That(FromMillisPrecise(1.005).ToPrettyString(), Is.EqualTo("1.01 millis"));

    [Test]
    public void When_9_99Ms_Then_9_99Millis() => Assert.That(FromMillisPrecise(9.99).ToPrettyString(), Is.EqualTo("9.99 millis"));

    [Test]
    public void When_10Ms_Then_10Millis() => Assert.That(FromMillisPrecise(10).ToPrettyString(), Is.EqualTo("10 millis"));

    [Test]
    public void When_99_9Ms_Then_99_9Millis() => Assert.That(FromMillisPrecise(99.9).ToPrettyString(), Is.EqualTo("99.9 millis"));

    [Test]
    public void When_1S_Then_1Second() => Assert.That(TimeSpan.FromSeconds(1).ToPrettyString(), Is.EqualTo("1 second"));

    [Test]
    public void When_1_01S_Then_1_1Seconds() => Assert.That(FromMillisPrecise(1010).ToPrettyString(), Is.EqualTo("1.01 seconds"));

    [Test]
    public void When_9_994S_Then_9_99Seconds() => Assert.That(FromMillisPrecise(9994).ToPrettyString(), Is.EqualTo("9.99 seconds"));

    [Test]
    public void When_9_995S_Then_10Seconds() => Assert.That(FromMillisPrecise(9995).ToPrettyString(), Is.EqualTo("10 seconds"));

    [Test]
    public void When_10_04S_Then_10Seconds() => Assert.That(FromMillisPrecise(10040).ToPrettyString(), Is.EqualTo("10 seconds"));

    [Test]
    public void When_10_05S_Then_10Seconds() => Assert.That(FromMillisPrecise(10050).ToPrettyString(), Is.EqualTo("10.1 seconds"));

    [Test]
    public void When_59_9S_Then_59_9_Seconds() => Assert.That(TimeSpan.FromSeconds(59.9).ToPrettyString(), Is.EqualTo("59.9 seconds"));

    [Test]
    public void When_1M_Then_1Minute() => Assert.That(TimeSpan.FromMinutes(1).ToPrettyString(), Is.EqualTo("1 minute"));

    [Test]
    public void When_59M59S_Then_59M59S() => Assert.That((TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59)).ToPrettyString(), Is.EqualTo("59 minutes, 59 seconds"));

    [Test]
    public void When_1H_Then_1Hour() => Assert.That(TimeSpan.FromHours(1).ToPrettyString(), Is.EqualTo("1 hour"));

    [Test]
    public void When_1H1M_Then_1Hour1Minute() => Assert.That((TimeSpan.FromHours(1) + TimeSpan.FromMinutes(1)).ToPrettyString(), Is.EqualTo("1 hour, 1 minute"));

    [Test]
    public void When_1H5M_Then_1Hour5Minutes() => Assert.That((TimeSpan.FromHours(1) + TimeSpan.FromMinutes(5)).ToPrettyString(), Is.EqualTo("1 hour, 5 minutes"));

    [Test]
    public void When_Neg0_995Ms_Then_Neg1Milli() => Assert.That(FromMillisPrecise(-0.995).ToPrettyString(), Is.EqualTo("-1 milli"));

    [Test]
    public void When_Neg1Ms_Then_Neg1Milli() => Assert.That(TimeSpan.FromMilliseconds(-1).ToPrettyString(), Is.EqualTo("-1 milli"));

    [Test]
    public void When_Neg1_004Ms_Then_Neg1Milli() => Assert.That(FromMillisPrecise(-1.004).ToPrettyString(), Is.EqualTo("-1 milli"));

    [Test]
    public void When_Neg1_5S_Then_Neg1_5S() => Assert.That(TimeSpan.FromSeconds(-1.5).ToPrettyString(), Is.EqualTo("-1.5 seconds"));

    [Test]
    public void When_Neg9_99Ms_Then_Neg9_99Millis() => Assert.That(FromMillisPrecise(-9.99).ToPrettyString(), Is.EqualTo("-9.99 millis"));

    [Test]
    public void When_Neg10Ms_Then_Neg10Millis() => Assert.That(FromMillisPrecise(-10).ToPrettyString(), Is.EqualTo("-10 millis"));

    [Test]
    public void When_Neg99_9Ms_Then_Neg99_9Millis() => Assert.That(FromMillisPrecise(-99.9).ToPrettyString(), Is.EqualTo("-99.9 millis"));

    [Test]
    public void When_Neg1S_Then_Neg1Second() => Assert.That(TimeSpan.FromSeconds(-1).ToPrettyString(), Is.EqualTo("-1 second"));

    [Test]
    public void When_Neg1M_Then_Neg1Minute() => Assert.That(TimeSpan.FromMinutes(-1).ToPrettyString(), Is.EqualTo("-1 minute"));

    [Test]
    public void When_Neg1H_Then_Neg1Hour() => Assert.That(TimeSpan.FromHours(-1).ToPrettyString(), Is.EqualTo("-1 hour"));

    private static TimeSpan FromMillisPrecise(double millis)
    {
        return TimeSpan.FromTicks((long)Math.Round(TimeSpan.TicksPerMillisecond * millis));
    }
}
