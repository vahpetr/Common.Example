using Common.Services.Contract;
using OlegTask.Filters;
using OlegTask.Models;

namespace OlegTask.Services.Read.Contract
{
    public interface IDriverReadService : IReadService<Driver, DriverFilter>
    {
         
    }
}