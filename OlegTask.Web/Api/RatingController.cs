using System;
using System.Threading.Tasks;
using System.Web.Http;
using Common.MVC.ApiControllers.Service;
using Common.Services.Contract;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Services.Edit.Contract;
using OlegTask.Services.Read.Contract;

namespace OlegTask.Web.Api
{
    [RoutePrefix("api/ratings"), Route]
    public class RatingController :
        ServiceRESTfulApiController<Rating, RatingFilter, IRatingReadService, IRatingEditService>
    {
        public RatingController(Lazy<IRatingReadService> read, Lazy<IRatingEditService> edit,
            Lazy<ITransactionService> transaction)
            : base(read, edit, transaction)
        {
        }

        public override Task<IHttpActionResult> Put(string id, Rating entity)
        {
            throw new NotSupportedException();
        }

        public override Task<IHttpActionResult> Delete(string id)
        {
            throw new NotSupportedException();
        }
    }
}