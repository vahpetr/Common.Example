using System.Data.Entity;
using Common.EF.Repositories;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;

namespace OlegTask.EF.Repositories.Edit
{
    public class DriverEditRepository : EditRepository<Driver>, IDriverEditRepository
    {
        public DriverEditRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}