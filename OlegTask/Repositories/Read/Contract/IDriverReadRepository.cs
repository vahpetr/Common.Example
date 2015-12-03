using Common.Filters;
using Common.Repositories.Contract;
using OlegTask.Filters;
using OlegTask.Models;

namespace OlegTask.Repositories.Read.Contract
{
    public interface IDriverReadRepository : IReadRepository<Driver, DriverFilter>
    {
         
    }
}