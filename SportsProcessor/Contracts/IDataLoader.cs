using System;
using SportsProcessor.Models.Input;

namespace SportsProcessor.Contracts;

public interface IDataLoader
{
    ActivitySummary LoadSummary(string json);
    List<LapData> LoadLapData(string json);
    List<SampleData> LoadSamples(string json);
}
