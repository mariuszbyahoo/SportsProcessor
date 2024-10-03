using System;
using System.Diagnostics;
using SportsProcessor.Contracts;
using SportsProcessor.Models.Input;
using SportsProcessor.Models.Output;

namespace SportsProcessor;

public class InternalProcessor : IInternalProcessor
{

    public ProcessedActivity Process(ActivitySummary activitySummary, List<LapData> lapsData, List<SampleData> sampleDatas)
    {
        throw new NotImplementedException();
    }
}
