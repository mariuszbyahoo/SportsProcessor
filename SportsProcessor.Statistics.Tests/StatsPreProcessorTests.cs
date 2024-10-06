using System.Text.Json;
using SportsProcessor.Models.Output;
using SportsProcessor.Processors;

namespace SportsProcessor.Statistics.Tests;

public class StatsPreProcessorTests
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
/*

    *   THIS FILE IS MY "DEBUG NOTEPAD" FOR IMPLEMENTATION OF AN ML MODEL TRAINING

*/
    [Test]
    public void DebugMyPreProcessor()
    {
        var consolidatedProcessedActivity = JsonSerializer.Deserialize<ProcessedActivity>(
            _processor.ProcessData(_testActivitySummaryJson, _testLapDataListJson, _testSampleDataListJson));
        var heartRateSamples = $"{string.Join(',', consolidatedProcessedActivity.Laps[0].HeartRateSamples.Select(s => s.HeartRate))}," 
            + $"{string.Join(',', consolidatedProcessedActivity.Laps[1].HeartRateSamples.Select(s => s.HeartRate))}";

        var statsPreProcessor = new StatsPreProcessor();
        var prepareForMLModelTraining = statsPreProcessor.ProcessData(heartRateSamples.Split(',').ToList());
        var stringIntegers = string.Join(',', prepareForMLModelTraining);
    }
}
