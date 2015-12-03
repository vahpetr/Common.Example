using System;
using Common.Services;
using Common.Services.Contract;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;
using OlegTask.Services.Edit.Contract;

namespace OlegTask.Services.Edit
{
    public class CarEditService : EditService<Car, ICarEditRepository>, ICarEditService
    {
        private readonly Lazy<ISecurityService> security;

        public CarEditService(Lazy<ISecurityService> security, Lazy<ICarEditRepository> repository)
            : base(repository)
        {
            this.security = security;
        }
    }
}