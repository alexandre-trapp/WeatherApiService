using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using WeatherApiService.Models;

namespace WeatherApiService.Services
{
    [JsonObject("response")]
    public class ResponseWeather
    {
        [JsonProperty("wheatersList")]
        public List<WeatherForecast> WeathersList { get; set; } = new List<WeatherForecast>();

        [JsonProperty("responseList")]
        public List<IRestResponse> ResponseList { get; set; } = new List<IRestResponse>();

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
