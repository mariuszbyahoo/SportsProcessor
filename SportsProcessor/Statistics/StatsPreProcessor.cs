using System.Data;

namespace SportsProcessor.Statistics;

public class StatsPreProcessor
{
    public List<double> ProcessData(List<string> rawInput)
    {
        var parsedData = ParseData(rawInput);
        var cleanedData = RemoveOutliersAndSort(parsedData);

        var interpolatedData = ReverseAggregationAndInterpolate(cleanedData);

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

    private List<double> ReverseAggregationAndInterpolate(List<double> medians)
    {
        // HACK TODO: Te mediany muszą być posortowane od najnizszego do najwyzszego
        var interpolatedValues = new List<double>();

        // chcę aby spomiędzy median wejściowych (lista 5ciu liczb)
        // wydzielić jeszcze cztery liczby i je tam wsadzić pomiedzy
        // czyli dla 2 i 3 
        // zrobić 2 2,25 2,5 2,75 3
        // Do tego skorzystaj ze  wzoru:
        // y = x1 + i * (x2 - x1)/n
        // i -> iteracja
        // n -> liczba następujących po sobie kroków, tutaj - 5
        for(int i = 0; i < medians.Count; i ++)
        {
            if(i == 0 || i == medians.Count-1) 
            {
                interpolatedValues.Add(medians[i]); // wartości skrajne
                continue; // do następnej iteracji
            }   
            throw new NotImplementedException("Dodaj drugą pętlę wewnątrz. Tam dodasz trzy brakujące elementy spomiędzy     medians[i] oraz medians[i+1]");
            var res = medians[i] + (i) * ((medians[i+1] - medians[i]) / 5); 
            // no i teraz potrzebuję jeszcze trzech wartości spomiędzy dwóch skrajnych
            interpolatedValues.Add(res);
        }
        return interpolatedValues;
    }
}
