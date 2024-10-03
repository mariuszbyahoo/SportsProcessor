using System;
using System.Text.Json.Serialization;

namespace SportsProcessor.Models.Input;

public class LapData
{
    [JsonPropertyName("startTimeInSeconds")]
    public long StartTimeInSeconds { get; set; }

    [JsonPropertyName("airTemperatureCelsius")]
    public int AirTemperatureCelsius { get; set; }

    [JsonPropertyName("heartRate")]
    public int HeartRate { get; set; }

    [JsonPropertyName("totalDistanceInMeters")]
    public double TotalDistanceInMeters { get; set; }

    [JsonPropertyName("timerDurationInSeconds")]
    public int TimerDurationInSeconds { get; set; }
}
