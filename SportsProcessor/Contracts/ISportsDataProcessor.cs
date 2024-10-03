using System;

namespace SportsProcessor.Contracts;

public interface ISportsDataProcessor
{
    /// <summary>
    /// Entry point (facade) to process input JSON data and return processed data in unified JSON.
    /// </summary>
    /// <param name="summaryJson"></param>
    /// <param name="lapsJson"></param>
    /// <param name="samplesJson"></param>
    /// <returns></returns>
    string ProcessData(string summaryJson, string lapsJson, string samplesJson);
}
