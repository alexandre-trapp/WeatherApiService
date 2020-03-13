using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApiService.Models
{
    [JsonObject("cities")]
    public class CitiesRequest
    {
        [JsonProperty("codes")]
        public List<string> CitiesCodes { get; set; }
    }
}
