using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApiService.Models
{
    public class Localizations
    {
        [JsonProperty("city")]
        public List<City> Cities { get; set; }

        public static Localizations FromJson(string json) => JsonConvert.DeserializeObject<Localizations>(json, Converter.Settings);
    }

    public class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public CoordinatesLocalization Coord { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        public long Timezone { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }

    public class CoordinatesLocalization
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }

    public static class Serialize
    {
        public static string ToJson(this Localizations self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}

