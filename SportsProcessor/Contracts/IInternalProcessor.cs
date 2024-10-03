using System;
using SportsProcessor.Models.Input;
using SportsProcessor.Models.Output;

namespace SportsProcessor.Contracts;

public interface IInternalProcessor
{
    ProcessedActivity Process(ActivitySummary activitySummary, List<LapData> lapsData, List<SampleData> sampleDatas);
}
