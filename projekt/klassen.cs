using System;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using System.Text.Json.Serialization;


namespace CO2_Daten_Analyse
{
    public class Weatherdata
    {
        [Name("time")]
        [JsonPropertyName("time")]
        public DateTime time { get; set; }

        [Name("temperature_2m (°C)")]
        [JsonPropertyName("temperature_2m")]
        public required string temperature_2m { get; set; } // String for formatting issues in CSV/JSON

        [Name("rain (mm)")]
        [JsonPropertyName("rain")]
        public required string rain { get; set; }

        [Name("windspeed_10m (km/h)")]
        [JsonPropertyName("windspeed_10m")]
        public required string windspeed_10m { get; set; }

        [Name("winddirection_10m (°)")]
        [JsonPropertyName("winddirection_10m")]
        public required string winddirection_10m { get; set; }

        // Override ToString for better debugging output
        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}", time, temperature_2m, rain, windspeed_10m, winddirection_10m);
        }

        // Override Equals for proper comparison in unit tests
        public override bool Equals(object? obj)
        {
            if (obj is Weatherdata other)
            {
                return time == other.time &&
                       temperature_2m == other.temperature_2m &&
                       rain == other.rain &&
                       windspeed_10m == other.windspeed_10m &&
                       winddirection_10m == other.winddirection_10m;
            }

            return false;
        }

        // Override GetHashCode when Equals is overridden (why? Warning CS0659 otherwise)
        public override int GetHashCode()
        {
            return HashCode.Combine(time, temperature_2m, rain, windspeed_10m, winddirection_10m);
        }
    }
}