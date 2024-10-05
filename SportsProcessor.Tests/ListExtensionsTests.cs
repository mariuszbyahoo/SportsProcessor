using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using SportsProcessor.Extensions;

namespace SportsProcessor.Tests;

public class ListExtensionsTests
{   
    [Test]
    public void Pop_WhenCalledOnAnEmptyList_ReturnsDefault()
    {
        var list = new List<string>();

        var res = list.Pop();
        
        res.Should().Be(default);
    }

    [Test]
    public void Pop_WhenCalledOnListContainingFiveConsecutiveNumbers_WillGetRidOfTheFirstInRow()
    {
        var list = new List<int>(){ 1, 2, 3, 4, 5, 6 };

        var res = list.Pop();

        res.Should().Be(1);
        list.Count.Should().Be(5);
    }

    [Test]
    public void Pop_WhenCalledOnListContainingFiveStrings_WillGetRidOfTheFirstInRow()
    {
        var list = new List<string>() { "raz", "dwa", "czy", "cztery" };

        var res = list.Pop();

        res.Should().Be("raz");

        list.Count.Should().Be(3);
    }
}
