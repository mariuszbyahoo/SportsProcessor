using System;
using Microsoft.ML.Data;

namespace SportsProcessor.Statistics.Models;

public class HeartRateData
{
    [LoadColumn(0)]
    public float[] PreviousTicks { get; set; }

    [LoadColumn(1)]
    public float MedianNextTicks { get; set; }
}
