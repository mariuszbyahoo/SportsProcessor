using System;

namespace SportsProcessor.Models.Output;


public class ProcessedLap
{
    public long StartTime { get; set; } 
    public int Duration { get; set; } 
    public double Distance { get; set; } 
    public List<HeartRateSample> HeartRateSamples { get; set; } 
}

