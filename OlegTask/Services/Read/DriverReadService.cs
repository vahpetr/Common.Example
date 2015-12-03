using System;
using Common.Services;
using Common.Services.Contract;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Repositories.Read.Contract;
using OlegTask.Services.Read.Contract;

namespace OlegTask.Services.Read
{
    public class DriverReadService : ReadService<Driver, DriverFilter, IDriverReadRepository>, IDriverReadService
    {
        private readonly Lazy<ISecurityService> security;

        public DriverReadService(Lazy<ISecurityService> security, Lazy<IDriverReadRepository> repository)
            : base(repository)
        {
            this.security = security;
        }
    }
}