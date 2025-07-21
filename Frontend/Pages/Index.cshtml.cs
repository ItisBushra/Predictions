using API.DTOS;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Metrics;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public SafetyViewModel Safty { get; set; }


        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnGet([FromQuery]SafetyTrendsDTO safetyTrendsDTO)
        {
            if (safetyTrendsDTO.Country != null)
            {
                var client = _clientFactory.CreateClient();
                try
                {
                    var response = await client.GetAsync($"https://localhost:7000/api/SafetyTrends/GetSpecificTrend?Country=" +
                   $"{safetyTrendsDTO.Country}&Demographics={safetyTrendsDTO.Demographic}&Year={safetyTrendsDTO.Year}");
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["HasData"] = true;
                        var predictionJson = await response.Content.ReadAsStringAsync();
                        Safty = ResultInfo(predictionJson);
                    }                
                }
                catch (Exception ex)
                {
                    ViewData["HasData"] = false;
                    return BadRequest(ex.Message);
                }
            }
            return Page();
        }
        private SafetyViewModel ResultInfo(string predictionJson)
        {
            var prediction = JsonConvert.DeserializeObject<SafetyViewModel>(predictionJson);

            var parts = prediction.Demographic.Split('_');

            string gender = parts[0]
                .Replace("AllGenders", "All Genders");

            string ageRange = parts.Length > 1
                ? parts[1]
                    .Replace("Over", "Over ")
                    .Replace("Under", "Under ")
                    .Replace("AllAges", "All Ages")
                : "";

            var Safety = new SafetyViewModel
            {
                VALUE = prediction.VALUE,
                Rolling_Trend = prediction.Rolling_Trend,
                Country = prediction.Country,
                Year = prediction.Year,
                Gender = gender,
                AgeRange = ageRange,
                Probability = prediction.Probability,
                YoY_Pct_Change = prediction.YoY_Pct_Change,
                Baseline = prediction.Baseline,
                Rolling_Slope = prediction.Rolling_Slope,
                Yearly_Trend = prediction.Yearly_Trend,
                Cumulative_Growth = prediction.Cumulative_Growth
            };
            return Safety;
        }
    }
}
