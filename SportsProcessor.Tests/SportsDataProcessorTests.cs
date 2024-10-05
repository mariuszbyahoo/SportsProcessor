using System;
using FluentAssertions;
using SportsProcessor.Models.Input;
using SportsProcessor.Processors;

namespace SportsProcessor.Tests;

public class SportsDataProcessorTests
{
    private SportsDataProcessor _processor;
    private string _testActivitySummaryJson;
    private string _testLapDataListJson;
    private string _testSampleDataListJson;

    [SetUp]
    public void Setup()
    {
        _processor = new SportsDataProcessor();
        _testActivitySummaryJson = "{\"userId\": \"1234567890\",\"activityId\": 9480958402,\"activityName\": \"Indoor Cycling\",\"durationInSeconds\": 3667,\"startTimeInSeconds\": 1661158927,\"startTimeOffsetInSeconds\": 7200,\"activityType\": \"INDOOR_CYCLING\",\"averageHeartRateInBeatsPerMinute\": 150,\"activeKilocalories\": 561,\"deviceName\": \"instinct2\",\"maxHeartRateInBeatsPerMinute\": 190}";
        _testLapDataListJson = "[{\"startTimeInSeconds\": 1661158927,\"airTemperatureCelsius\": 28,\"heartRate\": 109,\"totalDistanceInMeters\": 15,\"timerDurationInSeconds\": 600},{\"startTimeInSeconds\": 1661158929,\"airTemperatureCelsius\": 28,\"heartRate\": 107,\"totalDistanceInMeters\": 30,\"timerDurationInSeconds\": 900}]";
        _testSampleDataListJson = "[{\"recording-rate\": 5,\"sample-type\": \"0\",\"data\": \"86,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"120,126,122,140,142,155,145\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"141,147,155,160,180,152,120\"},{\"recording-rate\": 5,\"sample-type\": \"0\",\"data\": \"86,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"1\",\"data\": \"143,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"143,151,164,null,173,181,180\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"182,170,188,181,174,172,158\"},{\"recording-rate\": 5,\"sample-type\": \"3\",\"data\": \"143,87,88,88,88,90,91\"}]";
    }

    [Test]
    public void ProcessData_WhenValidDataPassedIn_ReturnsResultBeingNotNull()
    {
        var result = _processor.ProcessData(_testActivitySummaryJson, _testLapDataListJson, _testSampleDataListJson);

        result.Should().NotBeNull();
    }

    [Test]
    public void ProcessData_WhenSummaryJsonWillBeEmptyString_ReturnsResultBeingNotNullAndDoesNotThrowingAnyException()
    {
        var summaryJson = string.Empty;
        var processCall = () => _processor.ProcessData(summaryJson, _testLapDataListJson, _testSampleDataListJson);

        processCall.Should().NotThrow<Exception>();
        
        var result = processCall.Invoke();

        result.Should().NotBeNull();
    }

    [Test]
    public void ProcessData_WhenLapDataListJsonWillBeEmptyString_ReturnsResultBeingNotNullAndDoesNotThrowingAnyException()
    {
        var testLapDataListJson = string.Empty;
        var processCall = () => _processor.ProcessData(_testActivitySummaryJson, testLapDataListJson, _testSampleDataListJson);

        processCall.Should().NotThrow<Exception>();
        
        var result = processCall.Invoke();

        result.Should().NotBeNull();
    }

    [Test]
    public void ProcessData_WhenTestLapDataListJsonWillBeEmptyString_ReturnsResultBeingNotNullAndDoesNotThrowingAnyException()
    {
        var testSampleDataListJson = string.Empty;
        var processCall = () => _processor.ProcessData(_testActivitySummaryJson, _testLapDataListJson, testSampleDataListJson);

        processCall.Should().NotThrow<Exception>();
        
        var result = processCall.Invoke();

        result.Should().NotBeNull();
    }
}