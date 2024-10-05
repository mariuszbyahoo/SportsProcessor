using System;
using System.Text.Json.Serialization;
using SportsProcessor.Utils;

namespace SportsProcessor.Models.Input;

public class SampleData
{
    [JsonPropertyName("recording-rate")]
    public int RecordingRate { get; set; }

    [JsonPropertyName("sample-type")]
    public string SampleType { get; set; }

    [JsonPropertyName("data")]
    [JsonConverter(typeof(CommaSeparatedStringToListConverter))]
    public List<int?> DataList { get; set; }
}

