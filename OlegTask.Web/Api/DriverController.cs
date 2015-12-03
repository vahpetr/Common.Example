using System;
using System.Web.Http;
using Common.MVC.ApiControllers.Service;
using Common.Services.Contract;
using OlegTask.Filters;
using OlegTask.Models;
using OlegTask.Services.Edit.Contract;
using OlegTask.Services.Read.Contract;

namespace OlegTask.Web.Api
{
    [RoutePrefix("api/drivers"), Route]
    public class DriverController :
        ServiceRESTfulApiController<Driver, DriverFilter, IDriverReadService, IDriverEditService>
    {
        public DriverController(Lazy<IDriverReadService> read, Lazy<IDriverEditService> edit,
            Lazy<ITransactionService> transaction)
            : base(read, edit, transaction)
        {
        }
    }
}