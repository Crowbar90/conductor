using System;
using System.Collections;
using System.Collections.Generic;
using Crowbar90.Common.Utilities.Generics;
using Shouldly;
using Xunit;

namespace Crowbar90.Common.Utilities.Tests.Generics;

public class TwoWayDictionaryTests
{
    private static TwoWayDictionary<int, string> InitializeSut() =>
        new()
        {
            {1, "one"},
            {"two", 2},
            new KeyValuePair<int, string>(3, "three"),
            new KeyValuePair<string, int>("four", 4)
        };

    [Fact]
    public void ObjectInitializer_Works_WithAllFormats()
    {
        var sut = InitializeSut();

        sut.Keys.ShouldContain(1);
        sut.Keys.ShouldContain(2);
        sut.Keys.ShouldContain(3);
        sut.Keys.ShouldContain(4);

        sut.Values.ShouldContain("one");
        sut.Values.ShouldContain("two");
        sut.Values.ShouldContain("three");
        sut.Values.ShouldContain("four");
    }

    [Fact]
    public void AddElements_FromBothSides_Succeeds()
    {
        var sut = InitializeSut();

        sut.Add(5, "five");
        sut.Add("six", 6);

        sut.Keys.ShouldContain(5);
        sut.Keys.ShouldContain(6);

        sut.Values.ShouldContain("five");
        sut.Values.ShouldContain("six");
    }

    [Fact]
    public void CanRetrieveElements_FromBothSides()
    {
        var sut = InitializeSut();

        sut[1].ShouldBe("one");
        sut[2].ShouldBe("two");
        sut[3].ShouldBe("three");
        sut[4].ShouldBe("four");

        sut["one"].ShouldBe(1);
        sut["two"].ShouldBe(2);
        sut["three"].ShouldBe(3);
        sut["four"].ShouldBe(4);
    }

    [Fact]
    public void CanUpdateElements_FromBothSides()
    {
        var sut = InitializeSut();

        sut[1] = "one-updated";
        sut["two"] = 22;

        sut[1].ShouldBe("one-updated");
        sut[22].ShouldBe("two");

        sut["one-updated"].ShouldBe(1);
        sut["two"].ShouldBe(22);
    }

    [Fact]
    public void RemoveElements_Works_WithAllFormats()
    {
        var sut = InitializeSut();

        var removeResult1 = sut.Remove(new KeyValuePair<int, string>(1, "one"));
        var removeResult2 = sut.Remove(new KeyValuePair<string, int>("two", 2));
        var removeResult3 = sut.Remove(3);
        var removeResult4 = sut.Remove("four");

        removeResult1.ShouldBeTrue();
        sut.Keys.ShouldNotContain(1);
        sut.Values.ShouldNotContain("one");
        removeResult2.ShouldBeTrue();
        sut.Keys.ShouldNotContain(2);
        sut.Values.ShouldNotContain("two");
        removeResult3.ShouldBeTrue();
        sut.Keys.ShouldNotContain(3);
        sut.Values.ShouldNotContain("three");
        removeResult4.ShouldBeTrue();
        sut.Keys.ShouldNotContain(4);
        sut.Values.ShouldNotContain("four");
    }

    [Fact]
    public void RemoveNonExistingElements_ReturnsFalse_WithAllFormats()
    {
        var sut = InitializeSut();

        var removeResult1 = sut.Remove(new KeyValuePair<int, string>(5, "five"));
        var removeResult2 = sut.Remove(new KeyValuePair<string, int>("five", 5));
        var removeResult3 = sut.Remove(5);
        var removeResult4 = sut.Remove("five");

        removeResult1.ShouldBeFalse();
        removeResult2.ShouldBeFalse();
        removeResult3.ShouldBeFalse();
        removeResult4.ShouldBeFalse();
    }

    [Fact]
    public void Accessing_MissingKey_ThrowsException()
    {
        var sut = InitializeSut();

        Should.Throw<KeyNotFoundException>(() => sut[5]);
        Should.Throw<KeyNotFoundException>(() => sut["five"]);
    }

    [Fact]
    public void IsReadonly_IsFalse()
    {
        var sut = InitializeSut();

        sut.IsReadOnly.ShouldBeFalse();
    }

    [Fact]
    public void Count_ReturnsCorrectCount()
    {
        var sut = InitializeSut();

        sut.Count.ShouldBe(4);
    }

    [Fact]
    public void Clear_RemovesAllEntries()
    {
        var sut = InitializeSut();

        sut.Clear();

        sut.Count.ShouldBe(0);
        sut.Keys.ShouldBeEmpty();
        sut.Values.ShouldBeEmpty();
    }

    [Fact]
    public void Contains_Works_WithAllFormats()
    {
        var sut = InitializeSut();

        sut.Contains(new KeyValuePair<int, string>(1, "one")).ShouldBeTrue();
        sut.Contains(new KeyValuePair<string, int>("two", 2)).ShouldBeTrue();

        sut.Contains(new KeyValuePair<int, string>(1, "two")).ShouldBeFalse();
        sut.Contains(new KeyValuePair<string, int>("two", 1)).ShouldBeFalse();
    }

    [Fact]
    public void ContainsKey_Works_FromBothSides()
    {
        var sut = InitializeSut();

        sut.ContainsKey(1).ShouldBeTrue();
        sut.ContainsKey("two").ShouldBeTrue();

        sut.ContainsKey(5).ShouldBeFalse();
        sut.ContainsKey("five").ShouldBeFalse();
    }

    [Fact]
    public void TryGetValue_Works_FromBothSides()
    {
        var sut = InitializeSut();

        var foundForward = sut.TryGetValue(1, out var valueFoundForward);
        var foundBackward = sut.TryGetValue("two", out var valueFoundBackward);
        var notFoundForward = sut.TryGetValue(5, out var valueNotFoundForward);
        var notFoundBackward = sut.TryGetValue("five", out var valueNotFoundBackward);

        foundForward.ShouldBeTrue();
        valueFoundForward.ShouldBe("one");
        foundBackward.ShouldBeTrue();
        valueFoundBackward.ShouldBe(2);
        notFoundForward.ShouldBeFalse();
        valueNotFoundForward.ShouldBe(null);
        notFoundBackward.ShouldBeFalse();
        valueNotFoundBackward.ShouldBe(default);
    }

    [Fact]
    public void Enumerator_IsReturned()
    {
        var sut = InitializeSut();

        using var enumerator = sut.GetEnumerator();

        enumerator.MoveNext().ShouldBeTrue();
    }

    [Fact]
    public void IEnumerableEnumerator_IsReturned()
    {
        var sut = InitializeSut();

        var enumerator = ((IEnumerable)sut).GetEnumerator();

        enumerator.MoveNext().ShouldBeTrue();
    }

    [Fact]
    public void CopyTo_Forward_CreatesCorrectArray()
    {
        var sut = InitializeSut();
        var target = new KeyValuePair<int, string>[4];

        sut.CopyTo(target, 0);

        target[0].Key.ShouldBe(1);
        target[0].Value.ShouldBe("one");
        target[1].Key.ShouldBe(2);
        target[1].Value.ShouldBe("two");
        target[2].Key.ShouldBe(3);
        target[2].Value.ShouldBe("three");
        target[3].Key.ShouldBe(4);
        target[3].Value.ShouldBe("four");
    }

    [Fact]
    public void CopyTo_Backward_CreatesCorrectArray()
    {
        var sut = InitializeSut();
        var target = new KeyValuePair<string, int>[4];

        sut.CopyTo(target, 0);

        target[0].Key.ShouldBe("one");
        target[0].Value.ShouldBe(1);
        target[1].Key.ShouldBe("two");
        target[1].Value.ShouldBe(2);
        target[2].Key.ShouldBe("three");
        target[2].Value.ShouldBe(3);
        target[3].Key.ShouldBe("four");
        target[3].Value.ShouldBe(4);
    }
    
    [Fact]
    public void CopyTo_ThrowsException_WhenTargetIsNull()
    {
        var sut = InitializeSut();

        Should.Throw<ArgumentNullException>(() => sut.CopyTo((KeyValuePair<int, string>[])null!, 0));
        Should.Throw<ArgumentNullException>(() => sut.CopyTo((KeyValuePair<string, int>[])null!, 0));
    }
    
    [Fact]
    public void CopyTo_ThrowsException_WhenTargetIsTooSmall()
    {
        var sut = InitializeSut();

        Should.Throw<ArgumentException>(() => sut.CopyTo(new KeyValuePair<int, string>[1], 0));
        Should.Throw<ArgumentException>(() => sut.CopyTo(new KeyValuePair<string, int>[1], 0));
    }
    
    [Fact]
    public void CopyTo_ThrowsException_WhenArrayIndexIsNegative()
    {
        var sut = InitializeSut();

        Should.Throw<ArgumentException>(() => sut.CopyTo(new KeyValuePair<int, string>[1], -1));
        Should.Throw<ArgumentException>(() => sut.CopyTo(new KeyValuePair<string, int>[1], -1));
    }
    
}