using System.Data.Entity;
using Common.EF.Repositories;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Repositories.Read.Contract;

namespace OlegTask.EF.Repositories.Read
{
    public class RatingReadRepository : ReadRepository<Rating, RatingFilter>, IRatingReadRepository
    {
        public RatingReadRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}