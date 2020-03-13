using System;
using RestSharp;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApiService.Models;
using WeatherApiService.Services;

namespace WeatherApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        public IRestClient _restClient { get; set; } = new RestClient();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{idCity}")]
        public async Task<IActionResult> Get([FromRoute] string idCity)
        {
            var request = new string[1] { idCity };
            return await ProcessRequestWeather(request);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] string[] cities)
        {
            return await ProcessRequestWeather(cities);
        }

        private async Task<IActionResult> ProcessRequestWeather(string[] cities)
        {
            if (!ContainCitiesReques(cities))
            {
                var responseNotFound = new ResponseWeather
                {
                    Message = "não informado cidades válidas no request"
                };

                return NotFound(Utf8Json.JsonSerializer.ToJsonString(responseNotFound));
            };

            var response = await ConsumeOpenWeatherApi(cities);
            return Ok(Utf8Json.JsonSerializer.ToJsonString(response));
        }

        private async Task<ResponseWeather> ConsumeOpenWeatherApi(string[] cities)
        {
            const string token = "eb8b1a9405e659b2ffc78f0a520b1a46";
            var responseList = new ResponseWeather();

            var sbLog = new StringBuilder();

            foreach (var city in cities)
            {
                _restClient.BaseUrl = new Uri($"http://api.openweathermap.org/data/2.5/forecast?id={city}&APPID={token}");
                var request = new RestRequest(Method.GET);

                var resp = await _restClient.ExecuteGetAsync<IRestResponse>(request);

                if (resp == null)
                    continue;

                Console.WriteLine(resp.Content);
                if (string.IsNullOrEmpty(resp.Content))
                    sbLog.AppendLine($"Content is empty; Request idCity: {city}");

                responseList.ResponseList.Add(resp);

                var weather = JsonSerializer.Deserialize<WeatherForecast>(resp.Content);
                responseList.WeathersList.Add(weather);
            }

            responseList.Message = (string.IsNullOrEmpty(sbLog.ToString()) ? "Success" : sbLog.ToString());

            return responseList;
        }

        private bool ContainCitiesReques(string[] cities)
        {
            return cities != null && cities.Length > 0;
        }
    }
}
