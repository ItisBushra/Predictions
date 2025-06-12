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
        public FutureResultsDTO futureResultsDTO { get; set; }
        public SaftyViewModel Safty { get; set; }


        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnGet([FromQuery]FutureResultsDTO futureResultsDTO)
        {
            if (futureResultsDTO.Country != null)
            {
                var client = _clientFactory.CreateClient();
                try
                {
                    var response = await client.GetAsync($"https://localhost:7000/api/FutureResults/GetSpecificPrediction?Country=" +
                   $"{futureResultsDTO.Country}&Year={futureResultsDTO.Year}&Male={futureResultsDTO.Gender_Male}&Female=" +
                   $"{futureResultsDTO.Gender_Female}&GenderTotal={futureResultsDTO.Gender_Total}&AgeUnder18={futureResultsDTO.Age_Under18}" +
                   $"&AgeOver18={futureResultsDTO.Age_Over18}&AgeTotal={futureResultsDTO.Age_Total}");
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["HasData"] = true;
                        var predictionJson = await response.Content.ReadAsStringAsync();
                        Safty = ResultInfo(futureResultsDTO, predictionJson);
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
        private SaftyViewModel ResultInfo(FutureResultsDTO futureResultsDTO, string Percentage)
        {
            Safty = new SaftyViewModel();
            string percentage_percent = $"{Percentage.Split('.')[0]}.{Percentage.Split('.')[1][0]}";

            double num = double.Parse(percentage_percent);
            string level = "";
            switch (num)
            {
                case var p when (p <= 25 && p >= 0):
                    level = "Low, Unsafe";
                    break;

                case var p when (p <= 50 && p > 25):
                    level = "Somewhat Unsafe";
                    break;

                case var p when (p <= 75 && p > 50):
                    level = "Somewhat Safe";
                    break;

                case var p when (p <= 100 && p > 75):
                    level = "Safe";
                    break;

            }
            Safty.Percentage = percentage_percent + "%";
            Safty.Level = level;
            Safty.Country = futureResultsDTO.Country;
            Safty.Year = futureResultsDTO.Year;

            if (futureResultsDTO.Gender_Female == 1) Safty.Gender = "female";
            if (futureResultsDTO.Gender_Male == 1) Safty.Gender = "male";
            if (futureResultsDTO.Gender_Total == 1) Safty.Gender = "total population";

            if (futureResultsDTO.Age_Over18 == 1) Safty.AgeRange = "adults (over 18)";
            if (futureResultsDTO.Age_Total == 1) Safty.AgeRange = "all ages";
            if (futureResultsDTO.Age_Under18 == 1) Safty.AgeRange = "minors (under 18)";
           return Safty;
        }
    }
}
