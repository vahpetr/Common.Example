using System.Data.Entity;
using System.Linq;
using Common.EF.Repositories;
using OlegTask.Comparers;
using OlegTask.Helpers;
using OlegTask.Models;
using OlegTask.Repositories.Edit.Contract;

namespace OlegTask.EF.Repositories.Edit
{
    public class DriverEditRepository : EditRepository<Driver>, IDriverEditRepository
    {
        private readonly CarEditRepository carEditRepository;

        public DriverEditRepository(DbContext dbContext, CarEditRepository carEditRepository) : base(dbContext)
        {
            this.carEditRepository = carEditRepository;
        }

        public override void Update(Driver currDriver, Driver prevDriver)
        {
            var comparer = new CarEqualityComparer();

            var added = currDriver.Cars
                .Except(prevDriver.Cars, comparer)
                .ToList();
            added.ForEach(car =>
                {
                    if (car.Id == 0)
                    {
                        carEditRepository.Add(car);
                    }
                    else
                    {
                        var query = DriversCars.Attach(currDriver.Id, car.Id);
                        dbContext.Database.ExecuteSqlCommand(query);
                    }
                });

            var updated = currDriver.Cars
                .Intersect(prevDriver.Cars, comparer)
                .ToList();
            updated.ForEach(carEditRepository.Update);

            foreach (var car in prevDriver.Cars.Except(currDriver.Cars, comparer))
            {
                var query = DriversCars.Detach(currDriver.Id, car.Id);
                dbContext.Database.ExecuteSqlCommand(query);
            };

            base.Update(currDriver, prevDriver);
        }

        public override void Remove(Driver driver)
        {
            //запросом было бы лучше

            var ratings = dbContext.Set<Rating>()
                .AsNoTracking()
                .Where(p => p.DriverId == driver.Id)
                .ToArray();

            foreach (var rating in ratings)
            {
                dbContext.Entry(rating).State = EntityState.Deleted;
            }

            dbContext.Entry(driver).State = EntityState.Deleted;
        }
    }
}