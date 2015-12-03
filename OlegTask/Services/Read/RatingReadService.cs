using System;
using Common.Services;
using Common.Services.Contract;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Repositories.Read.Contract;
using OlegTask.Services.Read.Contract;

namespace OlegTask.Services.Read
{
    public class RatingReadService : ReadService<Rating, RatingFilter, IRatingReadRepository>, IRatingReadService
    {
        private readonly Lazy<ISecurityService> security;

        public RatingReadService(Lazy<ISecurityService> security, Lazy<IRatingReadRepository> repository)
            : base(repository)
        {
            this.security = security;
        }
    }
}