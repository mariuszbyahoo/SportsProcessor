using System;
using System.Text.Json;
using FluentAssertions;
using SportsProcessor.Models.Input;
using SportsProcessor.Utils;

namespace SportsProcessor.Tests;

public class DataLoaderTests
{
    private DataLoader _dataLoader;
    
    private string _testActivitySummaryJson;
    private string _testLapDataListJson;
    private string _testSampleDataListJson;
    [SetUp]
    public void Setup()
    {
        _dataLoader = new DataLoader();
        _testActivitySummaryJson = "{\"userId\": \"1234567890\",\"activityId\": 9480958402,\"activityName\": \"Indoor Cycling\",\"durationInSeconds\": 3667,\"startTimeInSeconds\": 1661158927,\"startTimeOffsetInSeconds\": 7200,\"activityType\": \"INDOOR_CYCLING\",\"averageHeartRateInBeatsPerMinute\": 150,\"activeKilocalories\": 561,\"deviceName\": \"instinct2\",\"maxHeartRateInBeatsPerMinute\": 190}";
        _testLapDataListJson = "[{\"startTimeInSeconds\": 1661158927,\"airTemperatureCelsius\": 28,\"heartRate\": 109,\"totalDistanceInMeters\": 15,\"timerDurationInSeconds\": 600},{\"startTimeInSeconds\": 1661158929,\"airTemperatureCelsius\": 28,\"heartRate\": 107,\"totalDistanceInMeters\": 30,\"timerDurationInSeconds\": 900}]";
        _testSampleDataListJson = "[{\"recording-rate\": 5,\"sample-type\": \"0\",\"data\": \"86,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"120,126,122,140,142,155,145\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"141,147,155,160,180,152,120\"},{\"recording-rate\": 5,\"sample-type\": \"0\",\"data\": \"86,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"1\",\"data\": \"143,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"143,151,164,null,173,181,180\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"182,170,188,181,174,172,158\"},{\"recording-rate\": 5,\"sample-type\": \"3\",\"data\": \"143,87,88,88,88,90,91\"}]";
    }

    [Test]
    public void LoadSummary_WhenValidJsonPassedIn_ReturnsActivitySummaryObjectWithExpectedPropertyValues()
    {
        var action = () => _dataLoader.LoadSummary(_testActivitySummaryJson);

        action.Should().NotThrow<Exception>();

        var result = action.Invoke();

        result.Should().BeOfType<ActivitySummary>();
        result.UserId.Should().Be("1234567890");
        result.ActivityId.Should().Be(9480958402);
        result.ActivityName.Should().Be("Indoor Cycling");
    }

    [Test]
    public void LoadSummary_WhenEmptyStringPassedIn_ReturnsActivitySummaryObjectWithUninitializedPropertiesAndDoesNotThrowingAnyException()
    {
        var action = () => _dataLoader.LoadSummary(string.Empty);

        action.Should().NotThrow<JsonException>();

        var result = action.Invoke();

        result.Should().BeOfType<ActivitySummary>();
        result.ActivityType.Should().Be(null);
    }   

    
    [Test]
    public void LoadSummary_WhenInvalidJsonStringPassedIn_ReturnsActivitySummaryObjectWithUninitializedPropertiesAndDoesNotThrowingAnyException()
    {
        var invalidJson = "{\"userId\":{}} \"1234567890\",\"activityId\": 9480958402,\"activityName\": \"Indoor Cycling\",\"durationInSeconds\": 3667,\"startTimeInSeconds\": 1661158927,\"startTimeOffsetInSeconds\": 7200,\"activityType\": \"INDOOR_CYCLING\",\"averageHeartRateInBeatsPerMinute\": 150,\"activeKilocalories\": 561,\"deviceName\": \"instinct2\",\"maxHeartRateInBeatsPerMinute\": 190}";
        var action = () => _dataLoader.LoadSummary(invalidJson);

        action.Should().NotThrow<JsonException>();

        var result = action.Invoke();

        result.Should().BeOfType<ActivitySummary>();
        result.ActivityType.Should().Be(null);
    }   

    [Test]
    public void LoadLapData_WhenValidJsonPassedIn_ReturnsListOfLapDataObjectsWithExpectedPropertyValues()
    {
        var action = () => _dataLoader.LoadLapData(_testLapDataListJson);

        action.Should().NotThrow<Exception>();

        var result = action.Invoke();

        result.Should().BeOfType<List<LapData>>();
        result.Count.Should().BeGreaterThan(0);
        result[0].TotalDistanceInMeters.Should().Be(15);
    }

    [Test]
    public void LoadLapData_WhenEmptyStringPassedIn_ReturnsEmptyListAsResultAndDoesNotThrowingAnyException()
    {
        var action = () => _dataLoader.LoadLapData(string.Empty);

        action.Should().NotThrow<JsonException>();

        var result = action.Invoke();

        result.Should().BeOfType<List<LapData>>();
        result.Count.Should().Be(0);
    }


    [Test]
    public void LoadLapData_WhenInvalidJsonPassedIn_ReturnsEmptyListAsResultAndDoesNotThrowingAnyException()
    {
        var invalidJson = "[{{[\"startTimeInSeconds\": 1661158927,\"airTemperatureCelsius\": 28,\"heartRate\": 109,\"totalDistanceInMeters\": 15,\"timerDurationInSeconds\": 600},{\"startTimeInSeconds\": 1661158929,\"airTemperatureCelsius\": 28,\"heartRate\": 107,\"totalDistanceInMeters\": 30,\"timerDurationInSeconds\": 900}]";
        var action = () => _dataLoader.LoadLapData(invalidJson);

        action.Should().NotThrow<JsonException>();

        var result = action.Invoke();

        result.Should().BeOfType<List<LapData>>();
        result.Count.Should().Be(0);
    }

    [Test]
    public void LoadSamples_WhenValidJsonPassedIn_ReturnsListOfSampleDatasWithExpectedPropertyValues()
    {
        var action = () => _dataLoader.LoadSamples(_testSampleDataListJson);

        action.Should().NotThrow<Exception>();

        var result = action.Invoke();

        result.Should().BeOfType<List<SampleData>>();
        result.Count.Should().BeGreaterThan(0);
        result[0].RecordingRate.Should().Be(5);
        result[0].DataList[0].Should().Be(86);
    }

    
    [Test]
    public void LoadSamples_WhenEmptyStringPassedIn_ReturnsEmptyListAsResultAndDoesNotThrowingAnyException()
    {
        var action = () => _dataLoader.LoadSamples(string.Empty);

        action.Should().NotThrow<JsonException>();

        var result = action.Invoke();

        result.Should().BeOfType<List<SampleData>>();
        result.Count.Should().Be(0);
    }

    
    [Test]
    public void LoadSamples_WhenInvalidJsonPassedIn_ReturnsEmptyListAsResultAndDoesNotThrowingAnyException()
    {
        var invalidJson = "[}{)({\"recording-rate\": 5,\"sample-type\": \"0\",\"data\": \"86,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"120,126,122,140,142,155,145\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"141,147,155,160,180,152,120\"},{\"recording-rate\": 5,\"sample-type\": \"0\",\"data\": \"86,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"1\",\"data\": \"143,87,88,88,88,90,91\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"143,151,164,null,173,181,180\"},{\"recording-rate\": 5,\"sample-type\": \"2\",\"data\": \"182,170,188,181,174,172,158\"},{\"recording-rate\": 5,\"sample-type\": \"3\",\"data\": \"143,87,88,88,88,90,91\"}]";
        var action = () => _dataLoader.LoadSamples(invalidJson);

        action.Should().NotThrow<JsonException>();

        var result = action.Invoke();

        result.Should().BeOfType<List<SampleData>>();
        result.Count.Should().Be(0);
    }
}
