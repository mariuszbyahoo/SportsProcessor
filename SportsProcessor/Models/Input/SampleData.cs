using System;

namespace SportsProcessor.Models.Input;

public class SampleData
{
    public int RecordingRate { get; set; } 
    public int SampleType { get; set; } 
    public List<int?> Data { get; set; } 
}
