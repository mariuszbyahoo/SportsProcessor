using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace SportsProcessor.Utils;

public class CommaSeparatedStringToListConverter : JsonConverter<List<int?>>
{
    public override List<int?>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //throw new NotImplementedException("To wymaga przerobienia od nowa, bo najpierw powinno podzielić JSONa na tablicę obiektów jsonowych, potem kadego jsona na zbiór jego property, i na końcu listę cyferek na listę");
        var stringValue = reader.GetString();

        var result = stringValue
            .Split(',')
            .Select(d => int.TryParse(d, out var num) ? (int?)num : null)
            .ToList();    
        return result;
    }

    public override void Write(Utf8JsonWriter writer, List<int?> value, JsonSerializerOptions options)
    {
        // This handles serialization if needed (converting the list back to a comma-separated string)
        var stringValue = string.Join(",", value.Select(v => v.HasValue ? v.ToString() : "null"));
        writer.WriteStringValue(stringValue);    
    }
}
