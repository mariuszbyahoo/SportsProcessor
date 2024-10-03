using FluentAssertions;
using SportsProcessor.Models.Input;

namespace SportsProcessor.Tests;

public class InternalProcessorTests
{
    private InternalProcessor _processor;

    [SetUp]
    public void Setup()
    {
        _processor = new InternalProcessor();
    }

    [Test]
    public void Process_EvenWhenEmptyInputClassesWillBePassedIn_WillNotThrowAnyExceptionAndResultWithNotNullObject()
    {
        var activitySummary = new ActivitySummary();
        var lapDataList = new List<LapData>();
        var sampleDataList = new List<SampleData>();

        var result = _processor.Process(activitySummary, lapDataList, sampleDataList);

        result.Should().NotBeNull();
    }
}