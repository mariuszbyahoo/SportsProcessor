using System;
using System.Text.Json.Serialization;

namespace SportsProcessor.Models.Input;
public class ActivitySummary
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("activityId")]
    public long ActivityId { get; set; }

    [JsonPropertyName("activityName")]
    public string ActivityName { get; set; }

    [JsonPropertyName("durationInSeconds")]
    public int DurationInSeconds { get; set; }

    [JsonPropertyName("startTimeInSeconds")]
    public long StartTimeInSeconds { get; set; }

    [JsonPropertyName("startTimeOffsetInSeconds")]
    public int StartTimeOffsetInSeconds { get; set; }

    [JsonPropertyName("activityType")]
    public string ActivityType { get; set; }

    [JsonPropertyName("averageHeartRateInBeatsPerMinute")]
    public int AverageHeartRateInBeatsPerMinute { get; set; }

    [JsonPropertyName("activeKilocalories")]
    public int ActiveKilocalories { get; set; }

    [JsonPropertyName("deviceName")]
    public string DeviceName { get; set; }

    [JsonPropertyName("maxHeartRateInBeatsPerMinute")]
    public int MaxHeartRateInBeatsPerMinute { get; set; }
}

