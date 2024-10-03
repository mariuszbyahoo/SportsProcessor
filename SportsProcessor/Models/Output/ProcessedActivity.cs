using System;

namespace SportsProcessor.Models.Output;

public class ProcessedActivity
{
    public string UserId { get; set; } 
    public long ActivityId { get; set; } 
    public string ActivityType { get; set; } 
    public int MaxHeartRate { get; set; } 
    public List<ProcessedLap> Laps { get; set; } 
}
