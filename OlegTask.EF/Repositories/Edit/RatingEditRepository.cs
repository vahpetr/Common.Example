using System.Data.Entity;
using Common.EF.Repositories;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;

namespace OlegTask.EF.Repositories.Edit
{
    public class RatingEditRepository : EditRepository<Rating>, IRatingEditRepository
    {
        public RatingEditRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}