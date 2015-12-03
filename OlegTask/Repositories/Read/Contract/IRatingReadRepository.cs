using Common.Repositories.Contract;
using OlegTask.Filters;
using OlegTask.Models;

namespace OlegTask.Repositories.Read.Contract
{
    public interface IRatingReadRepository : IReadRepository<Rating, RatingFilter>
    {
         
    }
}