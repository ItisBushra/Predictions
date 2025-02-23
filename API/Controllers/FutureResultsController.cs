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

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FutureResultsController : ControllerBase
    {
        private readonly IFutureResultRepository _futureResult;
        public FutureResultsController(IFutureResultRepository futureResult)
        {
            _futureResult = futureResult;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FutureResults>))]
        public async Task<IEnumerable<FutureResults>> GetAllPredictions()
        {
            var predictions = await _futureResult.GetAllPredictions();
            
            return predictions;
        }

        [HttpGet("GetAll/{id}")]
        [ProducesResponseType(200, Type = typeof(FutureResults))]
        public async Task<IActionResult> GetPredictionDetailed(int id)
        {
            var prediction = await _futureResult.GetAPrediction(id);
            return Ok(prediction);
        }
    }
}
