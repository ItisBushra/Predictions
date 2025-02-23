using API.Models;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class FutureResultRepository : IFutureResultRepository
    {
        private readonly PredictionsDbContext _context;
        public FutureResultRepository(PredictionsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FutureResults>> GetAllPredictions()
        {
            var predictions = await _context.Future_Results.ToListAsync();
            return predictions;
        }

        public async Task<FutureResults?> GetAPrediction(int id)
        {
            try
            {
                var prediction = await _context.Future_Results.FirstOrDefaultAsync(x => x.Id == id);
                return prediction;

            }
            catch (Exception ex) {
               throw new Exception(ex.Message);
            }

        }
    }
}
