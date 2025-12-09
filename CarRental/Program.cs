using CarRental;
using CarRental.Data;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;

using (var db = new AppDbContext(options))
    LinqExamples.ShowQueries(db);

/*var db = new AppDbContext();
//db.Database.EnsureDeleted();
//db.Database.EnsureCreated();

var car = new CarRental.Models.Car
{
    VIN = "4HGBH41JXMN109186",
    Brand = "BMW",
    Model = "X5",
    Year = 2015,
    IsRented = false
};

db.Cars.Add(car);
Console.WriteLine($"Adding car: {car.CarId}");
db.SaveChanges();
Console.WriteLine($"Adding car: {car.CarId}");*/

/*using (var db = new AppDbContext(options))
{
    var car = new Car
    {
        VIN = "3HGBH41JXMN109186",
        Brand = "BMW",
        Model = "x99",
        Year = 2020,
        IsRented = false
    };

    db.Cars.Add(car);
    db.SaveChanges();
}

using (var db = new AppDbContext(options))
{
    var c = db.Cars.FirstOrDefault(c => c.CarId == 2);
    if (c != null)
        c.Year = 2017;
    db.SaveChanges();
}

using (var db = new AppDbContext(options))
{
    var c = db.Cars.FirstOrDefault(c => c.CarId == 3);
    if (c != null)
        db.Cars.Remove(c);
    db.SaveChanges();
}*/


using (var db = new AppDbContext(options))
{
    var cars = db.Cars.ToList();
    foreach (var c in cars)
    {
        Console.WriteLine($"{c.CarId}. {c.Brand} {c.Model} ({c.Year}) - Rented: {c.IsRented}");
    }
}

using (var db = new AppDbContext(options))
{
    var cl = db.Clients.ToList();
    foreach (var c in cl)
    {
        Console.WriteLine($"{c.ClientId}. {c.Name} {c.Surname} {c.Age} {c.TaxNumber}");
    }
}

using (var db = new AppDbContext(options))
{
    var rentals = db.Rentals.ToList();
    foreach (var r in rentals)
    {
        Console.WriteLine($"{r.RentalId}. ClientId: {r.ClientId}, CarId: {r.CarId}, RentDate: {r.RentDate}, ReturnDate: {r.ReturnDate}");
    }
}