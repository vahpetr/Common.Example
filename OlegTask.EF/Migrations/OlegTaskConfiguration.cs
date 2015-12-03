using System;
using System.Linq;
using Common.EF.DataAccess;
using OlegTask.Models;

namespace OlegTask.EF.Migrations
{
    public sealed class OlegTaskConfiguration : BaseConfiguration<OlegTaskContext>
    {
        public OlegTaskConfiguration() : base()
        {

        }

        protected override bool DataExist(OlegTaskContext context)
        {
            return context.Set<Driver>().Any();
        }

        protected override void ChangeDatabaseStructure(OlegTaskContext context)
        {
            base.ChangeDatabaseStructure(context);

            var commonCar = new Car()
            {
                Brand = "Brand 1",
                Model = "Model 1",
                Color = "Color 1",
                Number = "Number 1"
            };
            var drivers = new []
            {
                new Driver
                {
                    Surname = "Surname 1",
                    Name = "Name 1",
                    PassportNumber = 123456,
                    BirthDay = new DateTime(1989, 5, 21),
                    Cars = new []
                    {
                        commonCar,
                        new Car
                        {
                            Brand = "Brand 2",
                            Model = "Model 2",
                            Color = "Color 2",
                            Number = "Number 2"
                        }
                    }
                },
                new Driver
                {
                    Surname = "Surname 2",
                    Name = "Name 2",
                    PassportNumber = 987654,
                    BirthDay = new DateTime(1972, 1, 2),
                    Cars = new []
                    {
                        commonCar,
                        new Car
                        {
                            Brand = "Brand 3",
                            Model = "Model 3",
                            Color = "Color 3",
                            Number = "Number 3"
                        },
                        new Car
                        {
                            Brand = "Brand 4",
                            Model = "Model 4",
                            Color = "Color 4",
                            Number = "Number 4"
                        }
                    }
                }
            };

            var ratings = new[]
            {
                new Rating
                {
                    Value = 7,
                    Driver = drivers[0]
                },
                new Rating
                {
                    Value = 8,
                    Driver = drivers[0]
                },
                new Rating
                {
                    Value = 9,
                    Driver = drivers[0]
                },
                new Rating
                {
                    Value = 4,
                    Driver = drivers[1]
                },
                new Rating
                {
                    Value = 3,
                    Driver = drivers[1]
                },
                new Rating
                {
                    Value = 2,
                    Driver = drivers[1]
                }
            };

            context.Ratings.AddRange(ratings);
        }
    }
}