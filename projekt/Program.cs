// See https://aka.ms/new-console-template for more information
using CO2_Daten_Analyse;
using System;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using System.Text.Json.Serialization;
using System.Reflection.Metadata.Ecma335;

public class Program
{
    private static void Main(string[] args)
    {
        Serialize();
        Deserialize();
    }

    // Serialize-Method: CSV → JSON
    public static List<Weatherdata> Serialize()
    {
        using (var reader = new StreamReader("C:\\Users\\lila\\Documents\\1Lehrjahr\\Berufsschule\\LF5\\One\\weather-data-analytics\\projekt\\daten.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Weatherdata>().ToList();

            // Serialize to JSON
            var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("weatherdata.json", json);

            Console.WriteLine("File weatherdata.json has been written!");
            
            return records;
        }

    }

     // Deserialize-Method: JSON → CSV
    public static List<Weatherdata> Deserialize()
    {
        // 1. read JSON
        var json = File.ReadAllText("weatherdata.json");

        // 2. JSON → Weatherdata-Objekte
        var records = JsonSerializer.Deserialize<List<Weatherdata>>(json);

        // 3. create CSV from Weatherdata-Objekte
        using (var writer = new StreamWriter("output.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            // Write records only if not null (Why?: Warning CS8604)
            if (records != null && records.Count > 0)
            {
                csv.WriteRecords(records);
            }
            else
            {
                Console.WriteLine("No records to write to CSV.");
            }

            Console.WriteLine("File output.csv has been written!");

            return records;
        }
    }
    
}