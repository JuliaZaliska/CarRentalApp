using CarRental.Models;
using CarRentalApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services
{
    //public class RentalService : IRentalService
    //{
    //    private readonly ICarRepository carRepo;
    //    private readonly List<Rental> rentals = new List<Rental>();

    //    public event EventHandler<Car> RentalStarted;
    //    public event EventHandler<Car> RentalEnded;

    //    public RentalService(ICarRepository repo)
    //    {
    //        carRepo = repo;
    //    }

    //    public void RentCar(string vin, Client client)
    //    {
    //        var car = carRepo.GetCarByVin(vin);
    //        if (car == null || car.IsRented)
    //        {
    //            Console.WriteLine("Це авто недоступне.");
    //            return;
    //        }

    //        car.IsRented = true;
    //        var rental = new Rental(car, client);
    //        rentals.Add(rental);

    //        Console.WriteLine($"Процес орендування авто {car.Brand} {car.Model} розпочатий для {client.Name} {client.Surname}");
    //        RentalStarted?.Invoke(this, car);
    //    }

    //    public void ReturnCar(string vin)
    //    {
    //        var car = carRepo.GetCarByVin(vin);
    //        if (car == null || !car.IsRented)
    //        {
    //            Console.WriteLine("Це авто не орендоване.");
    //            return;
    //        }

    //        car.IsRented = false;
    //        Console.WriteLine($"Процес повернення авто {car.Brand} {car.Model} розпочато.");
    //        RentalEnded?.Invoke(this, car);
    //    }
    //}
}
