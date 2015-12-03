using System.Data.Entity;
using Common.EF.Repositories;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Repositories.Read.Contract;

namespace OlegTask.EF.Repositories.Read
{
    public class CarReadRepository : ReadRepository<Car, CarFilter>, ICarReadRepository
    {
        public CarReadRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}