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
    
        public async Task<double?> GetaSaftyIndexForaCountry(string Country, int Year, int Male, 
                                                                    int Female, int GenderTotal, int AgeUnder18, int AgeOver18, int AgeTotal)
        {
            try
            {
                var SpecificPrediction = await _context.Future_Results.FirstOrDefaultAsync(c => c.Year == Year && c.Country == Country && c.GenderFemale == Female
                && c.GenderMale == Male && c.GenderTotal == GenderTotal && c.AgeOver18 == AgeOver18 && c.AgeUnder18 == AgeUnder18 && c.AgeTotal == AgeTotal);
                var saftyindex = SpecificPrediction?.SafetyIndex;
                return saftyindex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<double?> GetaSaftyPercentageForaCountry(string Country, int Year, int Male,
                                                                   int Female, int GenderTotal, int AgeUnder18, int AgeOver18, int AgeTotal)
        {
            try
            {
                var SpecificPrediction = await _context.Future_Results.FirstOrDefaultAsync(c => c.Year == Year && c.Country == Country && c.GenderFemale == Female
                && c.GenderMale == Male && c.GenderTotal == GenderTotal && c.AgeOver18 == AgeOver18 && c.AgeUnder18 == AgeUnder18 && c.AgeTotal == AgeTotal);
                var saftyPercentage = SpecificPrediction.SaftyPercentage;
                return saftyPercentage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
