using SportsProcessor.Contracts;
using SportsProcessor.Models.Input;
using SportsProcessor.Models.Output;

namespace SportsProcessor;

public class InternalProcessor : IInternalProcessor
{

    public ProcessedActivity Process(ActivitySummary activitySummary, List<LapData> lapsData, List<SampleData> sampleDatas)
    {
        var result = new ProcessedActivity
        {
            UserId = activitySummary.UserId,
            ActivityId = activitySummary.ActivityId,
            ActivityType = activitySummary.ActivityType,
            MaxHeartRate = activitySummary.MaxHeartRateInBeatsPerMinute,
            Laps = new List<ProcessedLap>()
        };

        foreach (var lap in lapsData)
        {
            var dataSamples = sampleDatas.Where(s => s.SampleType == 2).ToList();

            result.Laps.Add(new ProcessedLap
            {
                Distance = lap.TotalDistanceInMeters,
                Duration = lap.TimerDurationInSeconds,
                StartTime = lap.StartTimeInSeconds,
                HeartRateSamples = dataSamples.Select((sample, index) => new HeartRateSample
                {
                    SampleIndex = index,
                    HeartRate = sample.Data.FirstOrDefault()
                }).ToList()
            });
        }

        return result;
    }
}
