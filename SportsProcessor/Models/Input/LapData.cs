using System;

namespace SportsProcessor.Models.Input;

public class LapData
{
    public long StartTimeInSeconds { get; set; }  
    public int AirTemperatureCelsius { get; set; }  
    public int HeartRate { get; set; }  
    public double TotalDistanceInMeters { get; set; } 
    public int TimerDurationInSeconds { get; set; } 
}