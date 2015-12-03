using System;
using Common.Services;
using Common.Services.Contract;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;
using OlegTask.Services.Edit.Contract;

namespace OlegTask.Services.Edit
{
    public class RatingEditService : EditService<Rating, IRatingEditRepository>, IRatingEditService
    {
        private readonly Lazy<ISecurityService> security;

        public RatingEditService(Lazy<ISecurityService> security, Lazy<IRatingEditRepository> repository)
            : base(repository)
        {
            this.security = security;
        }
    }
}