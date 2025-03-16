using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Repository.IRepository
{
    public interface IFutureResultRepository
    {
        public Task<IEnumerable<FutureResults>> GetAllPredictions();
        public Task<FutureResults?> GetAPrediction(int id);
        public Task<double?> GetaSaftyIndexForaCountry(string country, int year, int male, int female,
                                                             int genderTotal, int ageUnder18, int ageOver18, int ageTotal);

        public Task<double?> GetaSaftyPercentageForaCountry(string country, int year, int male, int female,
                                                             int genderTotal, int ageUnder18, int ageOver18, int ageTotal);
    }
}
