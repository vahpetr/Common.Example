using System.Data.Entity;
using Common.EF.Repositories;
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
            //каскадное удаление отключил специально, на случай чтобы ничего лишнего случайно не удалить
            var query = DriversCars.Detach(car.Id);
            dbContext.Database.ExecuteSqlCommand(query);

            base.Remove(car);
        }
    }
}