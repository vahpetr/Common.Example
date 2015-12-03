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
    [RoutePrefix("api/cars"), Route]
    public class CarController : ServiceRESTfulApiController<Car, CarFilter, ICarReadService, ICarEditService>
    {
        public CarController(Lazy<ICarReadService> read, Lazy<ICarEditService> edit,
            Lazy<ITransactionService> transaction)
            : base(read, edit, transaction)
        {
        }
    }
}