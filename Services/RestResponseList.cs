using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using WeatherApiService.Models;

namespace WeatherApiService.Services
{
    public class ResponseWeather
    {
        [JsonProperty("wheatersList")]
        public List<WeatherForecast> WeathersList { get; set; }

        [JsonProperty("responseList")]
        public List<IRestResponse> ResponseList { get; set; } = new List<IRestResponse>();

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
