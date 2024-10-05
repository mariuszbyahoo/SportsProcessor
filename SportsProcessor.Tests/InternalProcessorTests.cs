using FluentAssertions;
using SportsProcessor.Models.Input;
using SportsProcessor.Processors;

namespace SportsProcessor.Tests;

public class InternalProcessorTests
{
    private InternalProcessor _processor;
    private ActivitySummary _testActivitySummary;
    private List<LapData> _testLapDataList;
    private List<SampleData> _testSampleDataList;

    [SetUp]
    public void Setup()
    {
        _processor = new InternalProcessor();
        _testActivitySummary = new ActivitySummary()
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
        _testLapDataList = new List<LapData>()
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
        _testSampleDataList = new List<SampleData>()
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
    }

    [Test]
    public void Process_EvenWhenEmptyInputClassesWillBePassedIn_WillNotThrowAnyExceptionAndResultWithNotNullObject()
    {
        var emptyActivitySummary = new ActivitySummary();
        var emptyLapDataList = new List<LapData>();
        var emptySampleDataList = new List<SampleData>();

        var result = _processor.Process(emptyActivitySummary, emptyLapDataList, emptySampleDataList);

        result.Should().NotBeNull();
    }

    [Test]
    public void Process_WhenNullPassedInAsActivitySummary_WillNotThrowAnExceptionAndReturnNull()
    {
        ActivitySummary nullActivitySummary = null;

        var result = _processor.Process(nullActivitySummary, _testLapDataList, _testSampleDataList);

        result.Should().Be(null);
    }

    [Test]
    public void Process_WhenNullPassedInAsLapDataList_WillNotThrowAnExceptionAndReturnNull()
    {
        List<LapData> nullLapDataList = null;

        var result = _processor.Process(_testActivitySummary, nullLapDataList, _testSampleDataList);

        result.Should().Be(null);
    }

    [Test]
    public void Process_WhenNullPassedInAsSampleDataList_WillNotThrowAnyExceptionAndReturnNull()
    {
        List<SampleData> nullSampleDataList = null;

        var result = _processor.Process(_testActivitySummary, _testLapDataList, nullSampleDataList);

        result.Should().Be(null);
    }

    [Test]
    public void Process_WhenAppropriateDataPassedIn_ReturnsObjectContainingConsolidatedDataWithDesiredActivitySummary()
    {
        var result = _processor.Process(_testActivitySummary, _testLapDataList, _testSampleDataList);

        result.Should().NotBeNull();
        result.ActivityId.Should().Be(_testActivitySummary.ActivityId);
        result.ActivityType.Should().Be(_testActivitySummary.ActivityType);
        result.UserId.Should().Be(_testActivitySummary.UserId);
        result.MaxHeartRate.Should().Be(_testActivitySummary.MaxHeartRateInBeatsPerMinute);
        result.Laps.Count.Should().Be(_testLapDataList.Count);
    }

    [Test]
    public void Process_WhenAppropriateDataPassedIn_ReturnsConsolidatedDataWithLapsAtTheSameOrder()
    {
        var result = _processor.Process(_testActivitySummary, _testLapDataList, _testSampleDataList);

        result.Should().NotBeNull();
        result.Laps.Count.Should().Be(2);
        result.Laps[0].StartTime.Should().Be(_testLapDataList[0].StartTimeInSeconds);
        result.Laps[0].Duration.Should().Be(_testLapDataList[0].TimerDurationInSeconds);
        result.Laps[0].Distance.Should().Be(_testLapDataList[0].TotalDistanceInMeters);
        result.Laps[1].StartTime.Should().Be(_testLapDataList[1].StartTimeInSeconds);
        result.Laps[1].Duration.Should().Be(_testLapDataList[1].TimerDurationInSeconds);
        result.Laps[1].Distance.Should().Be(_testLapDataList[1].TotalDistanceInMeters);
    }

    [Test]
    public void Process_WithTwoLaps_ShouldAssignFirstTwoDataSamplesToFirstLapAndNextTwoToSecondLap()
    {
        var result = _processor.Process(_testActivitySummary, _testLapDataList, _testSampleDataList);

        result.Should().NotBeNull();
        result.Laps.Count.Should().Be(2);
        result.Laps[0].HeartRateSamples.Count.Should().Be(14);
        result.Laps[1].HeartRateSamples.Count.Should().Be(13); // one null value omitted

        result.Laps[0].HeartRateSamples[0].HeartRate.Should().Be(_testSampleDataList[1].DataList[0]); // 120
        result.Laps[0].HeartRateSamples[7].HeartRate.Should().Be(_testSampleDataList[2].DataList[0]); // 141 
        result.Laps[1].HeartRateSamples[0].HeartRate.Should().Be(_testSampleDataList[5].DataList[0]); // 143
        result.Laps[1].HeartRateSamples[3].HeartRate.Should().Be(_testSampleDataList[5].DataList[4]); // 173
        result.Laps[1].HeartRateSamples[12].HeartRate.Should().Be(_testSampleDataList[6].DataList[6]); // 158

    }
}