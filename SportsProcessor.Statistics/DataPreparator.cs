using System;
using SportsProcessor.Statistics.Models;

namespace SportsProcessor.Statistics;

public class DataPreparator
{
    public List<HeartRateData> CreateTrainingData(List<double> heartRateData, int windowSize = 10)
    {
        var trainingData = new List<HeartRateData>();

        for (int i = 0; i < heartRateData.Count - windowSize - 5; i++)
        {
            // Input: previous 10 ticks
            var previousTicks = heartRateData.Skip(i).Take(windowSize).Select(x => (float)x).ToArray();

            // Label: median of the next 5 ticks
            var nextFiveTicks = heartRateData.Skip(i + windowSize).Take(5).Select(x => (float)x).ToArray();
            float medianNextTicks = GetMedian(nextFiveTicks);

            trainingData.Add(new HeartRateData
            {
                PreviousTicks = previousTicks,
                MedianNextTicks = medianNextTicks
            });
        }

        return trainingData;
    }

    public float GetMedian(float[] values)
    {
        Array.Sort(values);
        int mid = values.Length / 2;
        return (values.Length % 2 != 0) ? values[mid] : (values[mid - 1] + values[mid]) / 2f;
    }

}
