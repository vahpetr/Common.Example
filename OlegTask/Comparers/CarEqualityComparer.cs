using System.Collections.Generic;
using OlegTask.Models;

namespace OlegTask.Comparers
{
    public class CarEqualityComparer : IEqualityComparer<Car>
    {
        public bool Equals(Car x, Car y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Car car)
        {
            return car.Id;
        }
    }
}