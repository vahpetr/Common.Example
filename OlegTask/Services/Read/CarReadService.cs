using System;
using Common.Services;
using Common.Services.Contract;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Repositories.Read.Contract;
using OlegTask.Services.Read.Contract;

namespace OlegTask.Services.Read
{
    public class CarReadService : ReadService<Car, CarFilter, ICarReadRepository>, ICarReadService
    {
        private readonly Lazy<ISecurityService> security;

        public CarReadService(Lazy<ISecurityService> security, Lazy<ICarReadRepository> repository)
            : base(repository)
        {
            this.security = security;
        }
    }
}