using System.Data.Entity;
using System.Linq;
using Common.EF.Repositories;
using Common.Exceptions;
using OlegTask.Helpers;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;

namespace OlegTask.EF.Repositories.Edit
{
    public class CarEditRepository : EditRepository<Car>, ICarEditRepository
    {
        public CarEditRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override void Remove(Car car)
        {
            var driver = dbContext
                .Set<Driver>()
                .Include(p => p.Cars)
                .AsNoTracking()
                .FirstOrDefault(p => p.Cars.Any(b => b.Id == car.Id) && p.Cars.Count() == 1);

            if (driver != null)
                throw new ValidationServiceException($"Водитель {driver.Surname} {driver.Name} останется без машины");

            //каскадное удаление отключил специально, на случай чтобы ничего лишнего случайно не удалить
            var query = DriversCars.Detach(car.Id);
            dbContext.Database.ExecuteSqlCommand(query);

            base.Remove(car);
        }
    }
}