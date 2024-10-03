using System;
using System.Security.Cryptography;
using System.Text.Json;
using SportsProcessor.Contracts;
using SportsProcessor.Models.Input;

namespace SportsProcessor;

public class SportsDataProcessor : ISportsDataProcessor
{
    private readonly IInternalProcessor _internalProcessor;
    private readonly IDataLoader _dataLoader;

    public SportsDataProcessor(IInternalProcessor internalProcessor, IDataLoader dataLoader)
    {
        _internalProcessor = internalProcessor;
        _dataLoader = dataLoader;
    }

    public string ProcessData(string summaryJson, string lapsJson, string samplesJson)
    {
        var activitySummary = _dataLoader.LoadSummary(summaryJson);
        var lapsData = _dataLoader.LoadLapData(lapsJson);
        var samplesData = _dataLoader.LoadSamples(samplesJson);

        var result = _internalProcessor.Process(activitySummary, lapsData, samplesData);

        return JsonSerializer.Serialize(result);
    }
}
