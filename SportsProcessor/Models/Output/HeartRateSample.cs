using System;

namespace SportsProcessor.Models.Output;

public class HeartRateSample
{
    public int SampleIndex { get; set; } 
    public int? HeartRate { get; set; } 
}
