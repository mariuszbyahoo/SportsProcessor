using System.Text.Json;
using SportsProcessor.Contracts;
using SportsProcessor.Utils;

namespace SportsProcessor.Processors;

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
        
        // no risk of any input or ouptut data being null - 
        // DataLoader.Load() always will return T being not null 
        // ( whereas props of the result object can be null)

        var result = _internalProcessor.Process(activitySummary, lapsData, samplesData);

        return JsonSerializer.Serialize(result);
    }
}
