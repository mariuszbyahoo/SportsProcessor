using System;
using System.Text.Json;
using SportsProcessor.Contracts;
using SportsProcessor.Models.Input;

namespace SportsProcessor;

public class DataLoader : IDataLoader
{
    public List<LapData> LoadLapData(string json)
    {
        return JsonSerializer.Deserialize<List<LapData>>(json);
    }

    public List<SampleData> LoadSamples(string json)
    {
        return JsonSerializer.Deserialize<List<SampleData>>(json);
    }

    public ActivitySummary LoadSummary(string json)
    {
        return JsonSerializer.Deserialize<ActivitySummary>(json);
    }
}
