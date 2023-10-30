using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Jayride3TasksWebAPI.Model;

namespace Jayride3TasksWebAPI.Controllers
{

    [Route("/Listing")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ListingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetListings(int numberOfPassengers)
        {
            try
            {
                // Create an HttpClient instance using the factory
                var client = _httpClientFactory.CreateClient();

                // Make a request to the external search API
                var response = await client.GetAsync("https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserialize the response from the external API
                    var quoterequest = JsonConvert.DeserializeObject<QuoteRequest>(content);

                    if (quoterequest != null)
                    {
                        // Filter listings by the number of passengers
                        var filteredListings = quoterequest.Listings.Where(l => l.VehicleType.maxPassengers >= numberOfPassengers);
                        List<Result> result1 =  new List<Result>();
                        // Calculate the total price for each listing
                        foreach (var listing in filteredListings)
                        {
                            result1.Add(new Result {
                                Name = listing.Name,
                                PricePerPassenger = listing.PricePerPassenger,
                                Totalprice = numberOfPassengers * listing.PricePerPassenger,
                                VehicleType = listing.VehicleType,
                            });
                        }

                        // Sort the filtered listings by total price
                        var sortedListings = result1.OrderBy(l => l.Totalprice);

                        return Ok(sortedListings);
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
