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
        var activitySummary = new ActivitySummary()
        {
            UserId = "1234567890",
            ActivityId = 9480958402,
            ActivityName = "Indoor Cycling",
            DurationInSeconds = 3667,
            StartTimeInSeconds = 1661158927,
            StartTimeOffsetInSeconds = 7200,
            ActivityType = "INDOOR_CYCLING",
            AverageHeartRateInBeatsPerMinute = 150,
            ActiveKilocalories = 561,
            DeviceName = "instinct2",
            MaxHeartRateInBeatsPerMinute = 190
        };
        var lapDataList = new List<LapData>()
        {
            new LapData() 
            {
                StartTimeInSeconds = 1661158927,
                AirTemperatureCelsius = 28,
                HeartRate = 109,
                TotalDistanceInMeters = 15,
                TimerDurationInSeconds = 600
            },
            new LapData()
            {
                StartTimeInSeconds = 1661158929,
                AirTemperatureCelsius = 28,
                HeartRate = 107,
                TotalDistanceInMeters = 30,
                TimerDurationInSeconds = 900
            }
        };
        var sampleDataList = new List<SampleData>()
        {
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "0",
                DataList = new List<int?>
                {
                    86,87,88,88,88,90,91
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "2",
                DataList = new List<int?>
                {
                    120,126,122,140,142,155,145
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "2",
                DataList = new List<int?>
                {
                    141,147,155,160,180,152,120
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "0",
                DataList = new List<int?>
                {
                    86,87,88,88,88,90,91
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "1",
                DataList = new List<int?>
                {
                    143,87,88,88,88,90,91
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "2",
                DataList = new List<int?>
                {
                    143,151,164,null,173,181,180
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "2",
                DataList = new List<int?>
                {
                    182,170,188,181,174,172,158
                }
            },
            new SampleData()
            {
                RecordingRate = 5,
                SampleType = "3",
                DataList = new List<int?>
                {
                    143,87,88,88,88,90,91
                }
            }
        };

        var result = _processor.Process(activitySummary, lapDataList, sampleDataList);

        result.Should().NotBeNull();
    }
}