using System;
using System.Text;
using System.Text.Json;
using SportsProcessor.Utils;

namespace SportsProcessor.Tests;

public class CommaSeparatedStringToListConverterTests
{
    private CommaSeparatedStringToListConverter _converter;

    [SetUp]
    public void SetUp()
    {
        _converter = new CommaSeparatedStringToListConverter();
    }

    private Utf8JsonReader CreateJsonReaderFromString(string json)
    {
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(jsonBytes);
        reader.Read(); 
        return reader;
    }

    [Test]
    public void Write_ValidListOfIntegers_ReturnsCommaSeparatedString()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new Utf8JsonWriter(memoryStream))
            {
                var list = new List<int?> { 1, 2, 3, 4, 5 };

                _converter.Write(writer, list, new JsonSerializerOptions());

                writer.Flush();
            }

            var jsonOutput = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual("\"1,2,3,4,5\"", jsonOutput);
        }
    }

    [Test]
    public void Write_ListWithNulls_ReturnsCommaSeparatedStringWithNulls()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new Utf8JsonWriter(memoryStream))
            {
                var list = new List<int?> { 1, null, 3, null, 5 };

                _converter.Write(writer, list, new JsonSerializerOptions());

                writer.Flush();
            }

            var jsonOutput = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual("\"1,null,3,null,5\"", jsonOutput);
        }
    }

    [Test]
    public void Write_EmptyList_ReturnsEmptyString()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new Utf8JsonWriter(memoryStream))
            {
                var list = new List<int?>();

                _converter.Write(writer, list, new JsonSerializerOptions());

                writer.Flush();
            }

            var jsonOutput = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual("\"\"", jsonOutput);
        }
    }

    [Test]
    public void Write_ListWithSingleInteger_ReturnsSingleIntegerString()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new Utf8JsonWriter(memoryStream))
            {
                var list = new List<int?> { 1 };

                _converter.Write(writer, list, new JsonSerializerOptions());

                writer.Flush();
            }

            var jsonOutput = Encoding.UTF8.GetString(memoryStream.ToArray());
            Assert.AreEqual("\"1\"", jsonOutput);
        }
    }

    [Test]
    public void Read_ValidCommaSeparatedString_ReturnsListOfIntegers()
    {
        var json = "\"1,2,3,4,5\"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?> { 1, 2, 3, 4, 5 }, result);
    }

    [Test]
    public void Read_StringWithNonNumericValues_ReturnsListWithNullsForInvalidEntries()
    {
        var json = "\"1,abc,3,def,5\"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?> { 1, null, 3, null, 5 }, result);
    }

    [Test]
    public void Read_EmptyString_ReturnsListWithOneNullObject()
    {
        var json = "\"\"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?>() { null }, result);
    }

    [Test]
    public void Read_StringWithNullValues_ReturnsListWithNulls()
    {
        var json = "\"1,null,3,null,5\"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?> { 1, null, 3, null, 5 }, result);
    }

    [Test]
    public void Read_StringWithExtraSpaces_IgnoresSpacesAndParsesCorrectly()
    {
        var json = "\" 1, 2 , 3, 4, 5 \"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?> { 1, 2, 3, 4, 5 }, result);
    }

    [Test]
    public void Read_StringWithSpecialCharacters_ReturnsListWithNullsForInvalidEntries()
    {
        var json = "\"1, @#, 3, $, 5\"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?> { 1, null, 3, null, 5 }, result);
    }

    [Test]
    public void Read_SingleInteger_ReturnsListWithSingleInteger()
    {
        var json = "\"1\"";
        var reader = CreateJsonReaderFromString(json);

        var result = _converter.Read(ref reader, typeof(List<int?>), new JsonSerializerOptions());

        Assert.AreEqual(new List<int?> { 1 }, result);
    }
}