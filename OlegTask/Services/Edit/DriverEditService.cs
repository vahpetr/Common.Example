using System;
using Common.Services;
using Common.Services.Contract;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;
using OlegTask.Services.Edit.Contract;

namespace OlegTask.Services.Edit
{
    public class DriverEditService : EditService<Driver, IDriverEditRepository>, IDriverEditService
    {
        private readonly Lazy<ISecurityService> security;

        public DriverEditService(Lazy<ISecurityService> security, Lazy<IDriverEditRepository> repository)
            : base(repository)
        {
            this.security = security;
        }
    }
}