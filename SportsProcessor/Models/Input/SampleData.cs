using System;
using System.Text.Json.Serialization;

namespace SportsProcessor.Models.Input;

public class SampleData
{
    [JsonPropertyName("recording-rate")]
    public int RecordingRate { get; set; }

    [JsonPropertyName("sample-type")]
    public int SampleType { get; set; }

    private string _data;

    [JsonPropertyName("data")]
    public string Data
    {
        get => _data;
        set
        {
            _data = value;
            DataList = _data.Split(',').Select(digit => int.TryParse(digit, out var num) ? (int?)num : null).ToList();
        }
    }

    [JsonIgnore]
    public List<int?> DataList { get; set; }
}

