using SportsProcessor.Extensions;
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
        var dataSamples = sampleDatas.Where(s => s.SampleType.Equals("2")).ToList();
        /*
            We're only interested in heart rate samples, type == "2"
            Due to the lack of any timezone data within HeartRateSample, 
            I will assign first lap in the set to the two first samples from sample data set

            Also, when the HeartRateSample is null, I am considering it as an readout anomaly 
            And I am ignoring those and omitting them in the end result
        */
        foreach (var lap in laps)
        {
            int index = 0;
            var samples = new SampleData[] 
            { 
                dataSamples.Pop(),
                dataSamples.Pop() 
            };
            var samplesList = new List<HeartRateSample>();
            foreach (var sample in samples) 
            {
                foreach (var sampleData in sample.DataList)
                {
                    if(sampleData.HasValue) 
                    {
                        samplesList.Add(new HeartRateSample()
                            { 
                                HeartRate = sampleData.Value, 
                                SampleIndex = index 
                            });
                        index ++;
                    }
                }
            }
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
