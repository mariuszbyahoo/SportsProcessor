using System.Text.Json;
using SportsProcessor.Contracts;

namespace SportsProcessor;

public class SportsDataProcessor : ISportsDataProcessor
{
    private readonly InternalProcessor _internalProcessor;
    private readonly DataLoader _dataLoader;

    public SportsDataProcessor()
    {
        _internalProcessor = new InternalProcessor();
        _dataLoader = new DataLoader();
    }

    public string ProcessData(string summaryJson, string lapsJson, string samplesJson)
    {
        var activitySummary = _dataLoader.LoadSummary(summaryJson);
        var lapsData = _dataLoader.LoadLapData(lapsJson);
        var samplesData = _dataLoader.LoadSamples(samplesJson);

        var result = _internalProcessor.Process(activitySummary, lapsData, samplesData);

        if(result == null) return string.Empty;
        else return JsonSerializer.Serialize(result);
    }
}
