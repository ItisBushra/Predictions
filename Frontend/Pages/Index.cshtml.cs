using API.DTOS;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using static System.Reflection.Metadata.BlobBuilder;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public FutureResultsDTO futureResultsDTO { get; set; }
        public SaftyViewModel Safty { get; set; }


        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnGet([FromQuery]FutureResultsDTO futureResultsDTO)
        {
            if(futureResultsDTO.Country != null)
            {
                var client = _clientFactory.CreateClient();
                Safty = new SaftyViewModel();
                var response = await client.GetAsync($"https://localhost:7000/api/FutureResults/GetSpecificPrediction?Country=" +
                    $"{futureResultsDTO.Country}&Year={futureResultsDTO.Year}&Male={futureResultsDTO.Gender_Male}&Female=" +
                    $"{futureResultsDTO.Gender_Female}&GenderTotal={futureResultsDTO.Gender_Total}&AgeUnder18={futureResultsDTO.Age_Under18}" +
                    $"&AgeOver18={futureResultsDTO.Age_Over18}&AgeTotal={futureResultsDTO.Age_Total}");

                if (!response.IsSuccessStatusCode) return NotFound();
                var predictionJson = await response.Content.ReadAsStringAsync();

                Safty.Percentage = predictionJson;
            }        
            return Page();
        }
    }
}
