using HeatWave.KitchenSink.PrettyString;
using NUnit.Framework;
using System;
using System.Collections.Generic;

public class IEnumerableExtensionsTests
{
    [Test]
    public void WhenNull_Then_NullReferenceException()
    {
        IEnumerable<string> nullEnumerable = null;
        Assert.Throws<NullReferenceException>(() => nullEnumerable.ToPrettyString());
    }

    [Test]
    public void WhenEmptyList_Then_BracketsOnlyOutput() => Assert.That(new int[] { }.ToPrettyString(), Is.EqualTo("[]"));

    [Test]
    public void WhenSingleList_Then_SingleOutput() => Assert.That(new[] { 1 }.ToPrettyString(), Is.EqualTo("[ 1 ]"));

    [Test]
    public void WhenNormalList_Then_NormalOutput() => Assert.That(new[] { 1, 2, 3 }.ToPrettyString(), Is.EqualTo("[ 1, 2, 3 ]"));

    [Test]
    public void WhenSetNull_Then_NullReferenceException()
    {
        ISet<string> nullSet = null;
        Assert.Throws<NullReferenceException>(() => nullSet.ToPrettyString());
    }

    [Test]
    public void WhenEmptySet_Then_BracketsOnlyOutput() => Assert.That(new HashSet<int>().ToPrettyString(), Is.EqualTo("{}"));

    [Test]
    public void WhenSingleSet_Then_SingleOutput() => Assert.That(new HashSet<int>(new[] { 1 }).ToPrettyString(), Is.EqualTo("{ 1 }"));

    [Test]
    public void WhenNormalSet_Then_NormalOutput() => Assert.That(new HashSet<int>(new[] { 1, 2, 3 }).ToPrettyString(), Is.EqualTo("{ 1, 2, 3 }"));

    [Test]
    public void WhenDictNull_Then_NullReferenceException()
    {
        IDictionary<int, int> nullDict = null;
        Assert.Throws<NullReferenceException>(() => nullDict.ToPrettyString());
    }

    [Test]
    public void WhenEmptyDict_Then_BracketsOnlyOutput()
    {
        var dict = new Dictionary<int, string>();
        Assert.That(dict.ToPrettyString(), Is.EqualTo("{}"));
    }

    [Test]
    public void WhenSingleDict_Then_SingleOutput()
    {
        var dict = new Dictionary<int, string>
            {
                { 3, "three" }
            };

        Assert.That(dict.ToPrettyString(), Is.EqualTo("{ 3: three }"));
    }

    [Test]
    public void WhenNormalDict_Then_NormalOutput()
    {
        var dict = new Dictionary<int, string>
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" }
            };

        Assert.That(dict.ToPrettyString(), Is.EqualTo("{ 1: one, 2: two, 3: three }"));
    }

    [Test]
    public void WhenNormalSetAsEnumerable_Then_NormalOutput()
    {
        IEnumerable<int> setAsEnumerable = new HashSet<int>(new[] { 1, 2, 3 });
        Assert.That(setAsEnumerable.ToPrettyString(), Is.EqualTo("{ 1, 2, 3 }"));
    }

    [Test]
    public void WhenNormalDictAsEnumerable_Then_NormalOutput()
    {
        IEnumerable<KeyValuePair<int, string>> dictAsEnumerable = new Dictionary<int, string>
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" }
            };

        Assert.That(dictAsEnumerable.ToPrettyString(), Is.EqualTo("{ 1: one, 2: two, 3: three }"));
    }

    [Test]
    public void Throws_When_BeforeNull()
    {
        var x = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentNullException>(() => x.ToPrettyString(null, " - ", "]"));
    }

    [Test]
    public void Throws_When_SeparatorNull()
    {
        var x = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentNullException>(() => x.ToPrettyString("[", null, "]"));
    }

    [Test]
    public void Throws_When_AfterNull()
    {
        var x = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentNullException>(() => x.ToPrettyString("[", " - ", null));
    }
}
