using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Repository.IRepository
{
    public interface IFutureResultRepository
    {
        public Task<IEnumerable<FutureResults>> GetAllPredictions();
        public Task<FutureResults?> GetAPrediction(int id);
    }
}
