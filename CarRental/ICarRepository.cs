using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp
{
    public interface ICarRepository
    {
        void AddCar(Car car);
        Car GetCarByVin(string vin);
        List<Car> GetAvailableCars();
    }
}