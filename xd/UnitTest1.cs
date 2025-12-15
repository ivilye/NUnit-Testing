namespace xd;

using CO2_Daten_Analyse;
using System.Text.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

public class Tests
{
    private List<Weatherdata> _serializedRecords;
    private List<Weatherdata> _deserializedRecords;

    [SetUp]
    public void Setup()
    {
        _serializedRecords = Program.Serialize();
        _deserializedRecords = Program.Deserialize();
    }

    [Test]
    public void Test_Serialize()
    {
        int count = 192; // Expected Count. Correcte count = 192

        Assert.That(_serializedRecords.Count, Is.EqualTo(count), "Serialized count does not match expected count.");
        Console.WriteLine("Serialized count machtes expected count.");
    }

    [Test]
    public void Test_Deserialize()
    {
        int count = 192; // Expected Count. Correct count = 192

        Assert.That(_deserializedRecords.Count, Is.EqualTo(count), "Deserialized count does not match expected count.");
        Console.WriteLine("Deserialized count matches expected count.");
    }

    [Test]
    public void Test_Roundtrip_Csv_To_Json_To_Csv()
    {
        //Introduce an intentional error for testing purposes
        // if (_serializedRecords.Count > 0)
        // {
        //     _serializedRecords[0].temperature_2m = "999.99"; // Will cause failure
        // }

        Assert.That(_deserializedRecords.Count, Is.EqualTo(_serializedRecords.Count), "Roundtrip was not successful.");

        for (int i = 0; i < _serializedRecords.Count; i++)
        {
            Assert.That(_deserializedRecords[i].Equals(_serializedRecords[i]), Is.True, $"Records {i} differs after Roundtrip.");
        }

        Console.WriteLine("Roundtrip successful.");
    }

    [Test]
    public void Test_Serialize_Returns_Data()
    {
        Assert.That(_serializedRecords, Is.Not.Null);
        Assert.That(_serializedRecords.Count, Is.GreaterThan(0));
        Console.WriteLine("Serialize returns data.");
    }

    [Test]
    public void Test_Serialize_Creates_Json_File()
    {
        Assert.That(File.Exists("weatherdata.json"), Is.True);
        Console.WriteLine("File weatherdata.json exists.");
    }

    [Test]
    public void Test_Deserialize_Returns_List()
    {
        Assert.That(_deserializedRecords, Is.Not.Null);
        Console.WriteLine("Deserialized records are not Null.");
    }
}