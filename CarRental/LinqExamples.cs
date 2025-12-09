using CarRental.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarRental
{
    public static class LinqExamples
    {
        public static void ShowQueries(AppDbContext db)
        {
            var oldCars = db.Cars.Where(c => c.Year < 2015);
            var newCars = db.Cars.Where(c => c.Year >= 2015);

            var allCars = oldCars.Union(newCars);
            Console.WriteLine($"Загалом авто: {allCars.Count()}");

            var onlyOld = oldCars.Except(newCars);
            Console.WriteLine($"Старі авто: {onlyOld.Count()}");

            var intersect = oldCars.Intersect(newCars);
            Console.WriteLine($"Спільних авто: {intersect.Count()}\n");

            var rentalsWithClients = from r in db.Rentals
                                     join c in db.Clients on r.ClientId equals c.ClientId
                                     join car in db.Cars on r.CarId equals car.CarId
                                     select new
                                     {
                                         Client = c.Name + " " + c.Surname,
                                         car.Brand,
                                         car.Model,
                                         r.RentDate
                                     };

            foreach (var item in rentalsWithClients)
            {
                Console.WriteLine($"{item.Client} орендував {item.Brand} {item.Model} ({item.RentDate:d})");
            }
            Console.WriteLine();

            var brands = db.Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToList();

            Console.WriteLine($"Унікальних брендів: {brands.Count}");
            Console.WriteLine(string.Join(", ", brands));

            var rentalsByClient = db.Rentals
                .GroupBy(r => r.ClientId)
                .Select(g => new
                {
                    ClientId = g.Key,
                    Count = g.Count(),
                    LastRent = g.Max(r => r.RentDate)
                })
                .ToList();

            foreach (var group in rentalsByClient)
            {
                Console.WriteLine($"Клієнт {group.ClientId} має {group.Count} оренд, остання оренда: {group.LastRent:d}");
            }
            Console.WriteLine();

            //loаding
            var eager = db.Rentals.Include(r => r.Client).Include(r => r.Car).ToList();
            Console.WriteLine($"{eager.Count} оренд завантажено.\n");

            var oneRental = db.Rentals.FirstOrDefault();
            if (oneRental != null)
            {
                db.Entry(oneRental).Reference(r => r.Client).Load();
                db.Entry(oneRental).Reference(r => r.Car).Load();
                Console.WriteLine($"{oneRental.Client?.Name} орендував {oneRental.Car?.Brand}\n");
            }

            var cars = db.Cars.AsNoTracking().ToList();
            Console.WriteLine($"Без відстеження завантажено {cars.Count} авто.");

            var carToUpdate = cars.FirstOrDefault();
            if (carToUpdate != null)
            {
                carToUpdate.Year = 2025;
                db.Cars.Update(carToUpdate);
                db.SaveChanges();
                Console.WriteLine($"Оновлено авто з ID={carToUpdate.CarId} (рік = {carToUpdate.Year})\n");
            }

            try
            {
                var clientsFromProc = db.Clients.FromSqlRaw("EXEC GetAllClients").ToList();
                Console.WriteLine($"Кількість клієнтів: {clientsFromProc.Count}\n");
            }
            catch
            {
                Console.WriteLine("GetAllClients не знайдена.\n");
            }

            var recentRentals = db.Rentals
                .FromSqlInterpolated($"SELECT * FROM Rentals WHERE RentDate > {DateTime.Now.AddMonths(-1)}")
                .ToList();
            Console.WriteLine($"Оренд за останній місяць: {recentRentals.Count}\n");
        }
    }
}