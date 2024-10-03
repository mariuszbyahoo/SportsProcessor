using System;

namespace SportsProcessor.Models.Input;

public class ActivitySummary
{
    public string UserId { get; set; }  
    public long ActivityId { get; set; } 
    public string ActivityName { get; set; } 
    public int DurationInSeconds { get; set; } 
    public long StartTimeInSeconds { get; set; } 
    public int StartTimeOffsetInSeconds { get; set; } 
    public string ActivityType { get; set; } 
    public int AverageHeartRateInBeatsPerMinute { get; set; } 
    public int ActiveKilocalories { get; set; } 
    public string DeviceName { get; set; } 
    public int MaxHeartRateInBeatsPerMinute { get; set; } 
}
