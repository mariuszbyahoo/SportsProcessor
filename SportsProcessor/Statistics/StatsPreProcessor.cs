using System.Data;

namespace SportsProcessor.Statistics;

public class StatsPreProcessor
{
    public List<double> ProcessData(List<string> rawInput)
    {
        var parsedData = ParseData(rawInput);
        var cleanedData = RemoveOutliersAndSort(parsedData);

        var interpolatedData = ReverseAggregationAndInterpolateData(cleanedData);

        // var normalizedData = NormalizeData(InterpolatedData);
        return interpolatedData;
    }

    private List<double> ParseData(List<string> rawInput)
    {
        var res = new List<double>(rawInput.Count);
        for (var i = 0; i < rawInput.Count; i++)
        {
            if(double.TryParse(rawInput[i], out var value))
            {
                res.Add(value);
            }
            else 
            {
                // Znaleziono niepoprawną wartość, stosujemy interpolację
                // Szukamy sąsiadujących wartości do interpolacji
                var interpolatedValue = InterpolateValue(rawInput, i);
                res.Add(interpolatedValue);            
            }
        }
        return res;
    }

    private List<double> RemoveOutliersAndSort(List<double> input)
    {
        // Z-Score
        var average = input.Average();
        var kwadratowaneRozniceZmiennychDoSredniejZbioru = input.Select(x => (x - average) * (x - average));
        // średnia kwadratów rónic między wartościami mojego zbioru i jego średnią, czyli Wariancja :
        var rozproszenieWartosciWStosunkuDoSredniej = kwadratowaneRozniceZmiennychDoSredniejZbioru.Average(); 
        // Pierwiastek ze średniego rozproszeniaWartosciWStosunkuDoSredniej, czyli Odchylenie Standardowe :
        var standardDeviation = Math.Sqrt(rozproszenieWartosciWStosunkuDoSredniej); 
        // Usuwamy liczby dla których ZScore jest większy bądź równy od 3
        return [.. input.Where(x => GetZScore(x, average, standardDeviation) <= 3).OrderBy(x => x)];
    }

    private double GetZScore(double input, double average, double standardDeviation)
    {
        var zScore = (input - average) / standardDeviation;
        var absoluteZScore = Math.Abs(zScore);
        return absoluteZScore;
    }
    
    private double InterpolateValue(List<string> rawInput, int currentIndex)
    {
        // Poprzednia poprawna wartość
        double? previousValue = null;
        for (int i = currentIndex - 1; i >= 0 ; i--)
        {
            if(double.TryParse(rawInput[i], out var validValue))
            {
                previousValue = validValue;
                break;
            }
        }

        // następna poprawna wartość
        double? nextValue = null;
        for(int i = currentIndex + 1; i < rawInput.Count; i++)
        {
            if(double.TryParse(rawInput[i], out var validValue))
            {
                nextValue = validValue;
            }
        }

        if (!previousValue.HasValue && !nextValue.HasValue)
        {
            throw new ArgumentException("Invalid input data source - no valid value in data source found");
        }

        // Jeśli jest tylko jeden sąsiad, zwracamy jego wartość
        if (!previousValue.HasValue && nextValue.HasValue) return nextValue.Value;
        if (!nextValue.HasValue && previousValue.HasValue) return previousValue.Value;

        if(nextValue.HasValue && previousValue.HasValue)
        {
            // Średnia z poprzedniej i następnej wartości
            return (previousValue.Value + nextValue.Value) / 2.0;
        }
        throw new InvalidOperationException("Coś przekombinowałem...");
    }

    private List<double> ReverseAggregationAndInterpolateData(List<double> medians)
    {
        var interpolatedValues = new List<double>();

        // n represents the number of total points, including the original 2 medians and the interpolated 3 points.
        int n = 5;

        for (int i = 0; i < medians.Count - 1; i++)
        {
            double startValue = medians[i];
            double endValue = medians[i + 1];

            // Interpolate `n-1` values between each pair of consecutive medians
            for (int j = 0; j < n; j++)
            {
                double interpolatedValue = startValue + (endValue - startValue) * (j / (double)(n - 1));
                interpolatedValues.Add(interpolatedValue);
            }
        }

        return interpolatedValues;
    }
}
