using System.Text.Json;
using SportsProcessor.Models.Input;

namespace SportsProcessor;

/// <summary>
/// Class contains methods being a wrapper around JsonSerializer.Deserialize<T>(string).
/// We assume, that System.Text.Json assembly is reliable enough to omit adding any 
/// tests of such methods
/// </summary>
internal class DataLoader
{
    internal List<LapData> LoadLapData(string json)
    {
        return JsonSerializer.Deserialize<List<LapData>>(json);
    }

    internal List<SampleData> LoadSamples(string json)
    {
        return JsonSerializer.Deserialize<List<SampleData>>(json);
    }

    internal ActivitySummary LoadSummary(string json)
    {
        return JsonSerializer.Deserialize<ActivitySummary>(json);
    }
}
