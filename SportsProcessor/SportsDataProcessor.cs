using System;
using System.Security.Cryptography;
using System.Text.Json;
using SportsProcessor.Contracts;
using SportsProcessor.Models.Input;

namespace SportsProcessor;

public class SportsDataProcessor : ISportsDataProcessor
{
    private readonly IInternalProcessor _internalProcessor;

    public SportsDataProcessor(IInternalProcessor internalProcessor)
    {
        _internalProcessor = internalProcessor;
    }

    public string ProcessData(string summaryJson, string lapsJson, string samplesJson)
    {
        var activitySummary = JsonSerializer.Deserialize<ActivitySummary>(summaryJson);
        var lapsData = JsonSerializer.Deserialize<List<LapData>>(lapsJson);
        var samplesData = JsonSerializer.Deserialize<List<SampleData>>(samplesJson);

        var result = _internalProcessor.Process(activitySummary, lapsData, samplesData);

        return JsonSerializer.Serialize(result);
    }
}
