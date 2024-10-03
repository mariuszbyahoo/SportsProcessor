using SportsProcessor.Models.Input;
using SportsProcessor.Models.Output;

namespace SportsProcessor;

public class InternalProcessor
{

    internal ProcessedActivity Process(ActivitySummary activitySummary, List<LapData> laps, List<SampleData> sampleDatas)
    {
        var result = new ProcessedActivity
        {
            UserId = activitySummary.UserId,
            ActivityId = activitySummary.ActivityId,
            ActivityType = activitySummary.ActivityType,
            MaxHeartRate = activitySummary.MaxHeartRateInBeatsPerMinute,
            Laps = new List<ProcessedLap>()
        };
        var dataSamples = sampleDatas.Where(s => s.SampleType == 2).ToList();
        /*
            We're only interested in heart rate samples, type == 2
            Due to the lack of any timezone data within HeartRateSample, 
            to make it clear on which sample to assign to which lap, I 
            will use FIFO then. (first lap in the set has two first samples from sample data set)
        */
        foreach (var lap in laps)
        {
            int index = 0;
            var samples = new SampleData[] 
            { 
                dataSamples.FirstOrDefault(),
                dataSamples.FirstOrDefault() 
            };
            var samplesList = new List<HeartRateSample>();
            foreach (var sample in samples) 
            {
                foreach (var sampleData in sample.Data)
                {
                    samplesList.Add(new HeartRateSample(){ HeartRate = sampleData, SampleIndex = index });
                    index ++;
                }
            }
            // dwa DataSample na jedno okrązenie 
            result.Laps.Add(new ProcessedLap
            {
                Distance = lap.TotalDistanceInMeters,
                Duration = lap.TimerDurationInSeconds,
                StartTime = lap.StartTimeInSeconds,
                HeartRateSamples = samplesList
            });
        }

        return result;
    }
}
