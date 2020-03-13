using System;
using RestSharp;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApiService.Models;
using WeatherApiService.Services;

namespace WeatherApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        public IRestClient _restClient { get; set; } = new RestClient();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ResponseWeather Get(CitiesRequest cities)
        {
            if (!ContainCitiesReques(cities))
            {
                return new ResponseWeather
                {
                    Message = "não informado cidades válidas no request"
                };
            };

            return ConsumeOpenWeatherApi(cities);
        }

        private ResponseWeather ConsumeOpenWeatherApi(CitiesRequest cities)
        {
            const string token = "eb8b1a9405e659b2ffc78f0a520b1a46";
            var responseList = new ResponseWeather();

            foreach (var city in cities.CitiesCodes)
            {
                _restClient.BaseUrl = new Uri($"http://api.openweathermap.org/data/2.5/forecast?id={city}&APPID={token}");
                var request = new RestRequest(Method.GET);

                var resp = _restClient.Execute(request);

                if (resp == null)
                    continue;

                Console.WriteLine(resp.Content);
                responseList.ResponseList.Add(resp);
            }

            return responseList;
        }

        private bool ContainCitiesReques(CitiesRequest cities)
        {
            return cities != null && cities.CitiesCodes != null && cities.CitiesCodes.Any();
        }
    }
}
