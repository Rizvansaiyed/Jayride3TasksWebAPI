using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using Jayride3TasksWebAPI.Model;

namespace Jayride3TasksWebAPI.Controllers
{
    [Route("/Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public LocationController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string ipAddress)
        {
            try
            {
                var apiKey = "dcad1ad188d544cb82b179d2f2e553c8";
                var apiUrl = $"http://api.ipstack.com/{ipAddress}?access_key={apiKey}";

                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var location = await JsonSerializer.DeserializeAsync<LocationResponse>(responseStream);

                    return Ok(location);
                }
                else
                {
                    return BadRequest("Error while retrieving location information.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}

