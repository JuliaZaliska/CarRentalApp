using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp
{
    public class CarRepository : ICarRepository
    {
        private readonly List<Car> cars = new List<Car>();

        public void AddCar(Car car) => cars.Add(car);

        public Car GetCarByVin(string vin) => cars.FirstOrDefault(c => c.VIN == vin);

        public List<Car> GetAvailableCars() => cars.Where(c => !c.IsRented).ToList();
    }
}


