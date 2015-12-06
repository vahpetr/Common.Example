using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using OlegTask.EF.Tests;
using OlegTask.Models;

namespace OlegTask.EF.Repositories.Edit.Tests
{
    [TestClass]
    public class DriverEditRepositoryTests : TestEnvironment
    {
        [TestMethod]
        public void VerifyCarsAnyTest()
        {
            var service = GetInstance<DriverEditRepository>();
            var driver = new Driver();
            ThrowsAssert.Throws(() => service.Verify(driver), "Необходимо указать хотя бы одну машину");
        }

        [TestMethod]
        public void VerifyIdTest()
        {
            var service = GetInstance<DriverEditRepository>();
            var currDriver = new Driver
            {
                Cars = new[]
                {
                    new Car()
                }
            };
            var prevDriver = new Driver
            {
                Id = 1
            };
            ThrowsAssert.Throws(() => service.Verify(currDriver, prevDriver), "Нельзя обновить один объект другим объектом");
        }
    }
}