using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Models;
using static System.Reflection.Metadata.BlobBuilder;
using API.Repository.IRepository;
using API.Repository;
using AutoMapper;
using API.DTOS;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FutureResultsController : ControllerBase
    {
        private readonly IFutureResultRepository _futureResult;
        private readonly IMapper _mapper;

        public FutureResultsController(IFutureResultRepository futureResult, IMapper mapper)
        {
            _mapper = mapper;
            _futureResult = futureResult;
        }

        [HttpGet("GetAll")]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FutureResults>))]
        public async Task<ActionResult<IEnumerable<FutureResults>>> GetAllPredictions()
        {
            var predictions = _mapper.Map<IEnumerable<FutureResultsDTO>>(await _futureResult.GetAllPredictions());
            if (predictions == null) return BadRequest();            
            return Ok(predictions);
        }

        [HttpGet("GetAll/{id}")]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200, Type = typeof(FutureResults))]
        public async Task<IActionResult> GetPredictionDetailed(int id)
        {
            var prediction = _mapper.Map<FutureResultsDTO>(await _futureResult.GetAPrediction(id));

            return Ok(prediction);
        }

        [HttpGet("GetSpecificPrediction")]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200, Type = typeof(FutureResults))]
        public async Task<IActionResult> GetSpecificPrediction(string Country, int Year, int Male,
                                                                    int Female, int GenderTotal, int AgeUnder18, int AgeOver18, int AgeTotal)
        {
            var prediction = await _futureResult.GetaSaftyIndexForaCountry(Country, Year, Male,
                                                                    Female, GenderTotal, AgeUnder18, AgeOver18, AgeTotal);
            return Ok(prediction);
        }

        [HttpGet("GetSpecificSaftyPercentage")]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(200, Type = typeof(FutureResults))]
        public async Task<IActionResult> GetSpecificSaftyPercentage(string Country, int Year, int Male,
                                                                 int Female, int GenderTotal, int AgeUnder18, int AgeOver18, int AgeTotal)
        {
            var safty = await _futureResult.GetaSaftyPercentageForaCountry(Country, Year, Male,
                                                                    Female, GenderTotal, AgeUnder18, AgeOver18, AgeTotal);
            return Ok(safty);
        }
    }
}
