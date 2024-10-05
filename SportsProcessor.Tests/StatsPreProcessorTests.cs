using System;
using SportsProcessor.Statistics;

namespace SportsProcessor.Tests;

public class StatsPreProcessorTests
{
    [Test]
    public void DebugMyPreProcessor()
    {

        var processor = new StatsPreProcessor();
        var res = processor.ProcessData("86,87,88,88,88,90,91".Split(',').ToList());
    }
}
