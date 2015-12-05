using System;
using System.Data.Entity;
using Common.Services;
using Common.Services.Contract;
using Microsoft.Practices.Unity;
using OlegTask.EF;
using OlegTask.EF.Repositories.Edit;
using OlegTask.EF.Repositories.Read;
using OlegTask.Repositories.Edit.Contract;
using OlegTask.Repositories.Read.Contract;
using OlegTask.Services.Edit;
using OlegTask.Services.Edit.Contract;
using OlegTask.Services.Read;
using OlegTask.Services.Read.Contract;

namespace OlegTask.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            container.RegisterType<ITransactionService, TransactionService>(new PerRequestLifetimeManager());
            container.RegisterType<ISecurityService, SecurityService>(new PerRequestLifetimeManager());

            container.RegisterType<DbContext, OlegTaskContext>(new PerRequestLifetimeManager());

            container.RegisterType<ICarReadRepository, CarReadRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IDriverReadRepository, DriverReadRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRatingReadRepository, RatingReadRepository>(new PerRequestLifetimeManager());

            container.RegisterType<ICarEditRepository, CarEditRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IDriverEditRepository, DriverEditRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IRatingEditRepository, RatingEditRepository>(new PerRequestLifetimeManager());

            container.RegisterType<ICarReadService, CarReadService>(new PerRequestLifetimeManager());
            container.RegisterType<IDriverReadService, DriverReadService>(new PerRequestLifetimeManager());
            container.RegisterType<IRatingReadService, RatingReadService>(new PerRequestLifetimeManager());

            container.RegisterType<ICarEditService, CarEditService>(new PerRequestLifetimeManager());
            container.RegisterType<IDriverEditService, DriverEditService>(new PerRequestLifetimeManager());
            container.RegisterType<IRatingEditService, RatingEditService>(new PerRequestLifetimeManager());
        }

        #region Unity container

        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() => new UnityContainer());

        public static IUnityContainer container => _container.Value;

        #endregion
    }
}