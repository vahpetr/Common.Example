using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Threading;
using Common.Services;
using Common.Services.Contract;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlegTask.EF.Repositories.Edit;
using OlegTask.EF.Repositories.Read;
using OlegTask.Repositories.Edit.Contract;
using OlegTask.Repositories.Read.Contract;
using OlegTask.Services.Edit;
using OlegTask.Services.Edit.Contract;
using OlegTask.Services.Read;
using OlegTask.Services.Read.Contract;

namespace OlegTask.EF.Tests
{
    public class TestEnvironment
    {
        // ReSharper disable once InconsistentNaming
        protected Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            ConfigurateTestEnvirment(container);
            return container;
        });

        /// <summary>
        /// Настроить тестовое окружение
        /// </summary>
        /// <param name="container">Контейнер</param>
        private static void ConfigurateTestEnvirment(IUnityContainer container)
        {
            container.RegisterType<ITransactionService, TransactionService>(new PerThreadLifetimeManager());
            container.RegisterType<ISecurityService, SecurityService>(new PerThreadLifetimeManager());

            container.RegisterType<DbContext, OlegTaskContext>(new PerThreadLifetimeManager());

            container.RegisterType<ICarReadRepository, CarReadRepository>(new PerThreadLifetimeManager());
            container.RegisterType<IDriverReadRepository, DriverReadRepository>(new PerThreadLifetimeManager());
            container.RegisterType<IRatingReadRepository, RatingReadRepository>(new PerThreadLifetimeManager());
            container.RegisterType<ICarEditRepository, CarEditRepository>(new PerThreadLifetimeManager());
            container.RegisterType<IDriverEditRepository, DriverEditRepository>(new PerThreadLifetimeManager());
            container.RegisterType<IRatingEditRepository, RatingEditRepository>(new PerThreadLifetimeManager());

            container.RegisterType<ICarReadService, CarReadService>(new PerThreadLifetimeManager());
            container.RegisterType<IDriverReadService, DriverReadService>(new PerThreadLifetimeManager());
            container.RegisterType<IRatingReadService, RatingReadService>(new PerThreadLifetimeManager());

            container.RegisterType<ICarEditService, CarEditService>(new PerThreadLifetimeManager());
            container.RegisterType<IDriverEditService, DriverEditService>(new PerThreadLifetimeManager());
            container.RegisterType<IRatingEditService, RatingEditService>(new PerThreadLifetimeManager());
        }

        /// <summary>
        /// Получить экземпляр класса
        /// </summary>
        /// <typeparam name="T">Тип экземпляра класса</typeparam>
        /// <returns>Сервис</returns>
        protected T GetInstance<T>()
        {
            return container.Value.Resolve<T>();
        }

        [TestInitialize]
        public virtual void BeforeEachMethod()
        {
        }

        [TestCleanup]
        public virtual void AfterEachMethod()
        {
        }
    }
}