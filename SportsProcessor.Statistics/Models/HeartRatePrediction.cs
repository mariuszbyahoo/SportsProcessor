using System;
using Microsoft.ML.Data;

namespace SportsProcessor.Statistics.Models;

public class HeartRatePrediction
{
    [ColumnName("Score")]
    public float PredictedMedian { get; set; }

}
