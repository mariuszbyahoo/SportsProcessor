using System.Text.Json;
using SportsProcessor.Models.Input;

namespace SportsProcessor.Utils;

/// <summary>
/// Class contains methods being a wrapper around JsonSerializer.Deserialize<T>(string).
/// We assume, that System.Text.Json assembly is reliable enough to omit adding any 
/// tests of such methods
/// </summary>
internal class DataLoader
{
    internal List<LapData> LoadLapData(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<List<LapData>>(json);
        }
        catch (JsonException ex)
        {
            return new List<LapData>();
        }
    }

    internal List<SampleData> LoadSamples(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<List<SampleData>>(json);
        }
        catch (JsonException ex)
        {
            return new List<SampleData>();
        }
    }

    internal ActivitySummary LoadSummary(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<ActivitySummary>(json);
        }
        catch (JsonException ex)
        {
            // in this case we can assume that json passed inside of the method was an empty string or had unappropriate tokens
            return new ActivitySummary();
        }
    }
}
