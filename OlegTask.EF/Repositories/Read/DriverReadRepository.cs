using System.Data.Entity;
using System.Linq;
using Common.EF.Repositories;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Repositories.Read.Contract;

namespace OlegTask.EF.Repositories.Read
{
    public class DriverReadRepository : ReadRepository<Driver, DriverFilter>, IDriverReadRepository
    {
        public DriverReadRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Driver> ApplyInclude(IQueryable<Driver> query)
        {
            query = query.Include(p => p.Cars);
            return base.ApplyInclude(query);
        }
    }
}