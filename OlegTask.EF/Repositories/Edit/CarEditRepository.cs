using System.Data.Entity;
using Common.EF.Repositories;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;

namespace OlegTask.EF.Repositories.Edit
{
    public class CarEditRepository : EditRepository<Car>, ICarEditRepository
    {
        public CarEditRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}