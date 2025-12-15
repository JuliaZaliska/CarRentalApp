using CarRental.Data;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace CarRental.Multithreading
{
    public class MultithreadingDemo
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private const int TotalEntities = 100;
        private static int _globalCounter = 0;
        private static readonly SemaphoreSlim _sem = new(5, 5);
        private ConcurrentBag<int> _generatedClientIds = new();
        public MultithreadingDemo(DbContextOptions<AppDbContext> options) => _options = options;
        public async Task SetupDatabaseAsync()
        {
            using var context = new AppDbContext(_options);
            await context.Database.EnsureCreatedAsync();
            await context.Clients.ExecuteDeleteAsync();
            await context.Cars.ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }
        public void ThreadedWriteClient(object? state)
        {
            int id = Interlocked.Increment(ref _globalCounter);

            using var context = new AppDbContext(_options);
            var client = new Client{ Name = $"Клієнт_{id}", Surname = $"Прізвище_{id}", Age = 18 + (id %5), TaxNumber = $"{id:D10}" };
            
            context.Clients.Add(client);
            context.SaveChanges();
            _generatedClientIds.Add(client.ClientId);
            Console.WriteLine($"Створено: {client.Name}");
        }
        public void RunThreadApproach()
        {
            _globalCounter = 0;
            var stopwatch = Stopwatch.StartNew();

            Thread[] threads = new Thread[TotalEntities];
            for (int i = 0; i < TotalEntities; i++)
            {
                threads[i] = new Thread(ThreadedWriteClient);
                threads[i].Start();
            }
            foreach (var t in threads) t.Join();
            stopwatch.Stop();
            Console.WriteLine($"Створено через Thread {TotalEntities} екземплярів за {stopwatch.ElapsedMilliseconds} мс\n");
        }

        public async Task TPLWriteCarAsync()
        {
            int id;

            await _sem.WaitAsync();
            try
            {
                id = ++_globalCounter;
            }
            finally
            {
                _sem.Release();
            }
            using var context = new AppDbContext(_options);
            var car = new Car{ VIN = $"ABC{id:D11}XYZ", Brand = $"Машина_{id}", Model = $"Модель_{id}", Year = 1990 + (id%5), IsRented = false };
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
            Console.WriteLine($"Створено: {car.Brand}");
        }
        public async Task RunTPLApproachAsync()
        {
            _globalCounter = 0;
            var stopwatch = Stopwatch.StartNew();

            var tasks = new List<Task>();
            for (int i = 0; i < TotalEntities; i++) 
                tasks.Add(TPLWriteCarAsync());
            await Task.WhenAll(tasks);

            stopwatch.Stop();
            Console.WriteLine($"Створено через Task {TotalEntities} екземплярів за {stopwatch.ElapsedMilliseconds} мс\n");
        }

        public async Task<List<Client>> ReadClientsAsync(string filter)
        {
            using var context = new AppDbContext(_options);
            var clients = await context.Clients
                .Where(c => c.Name.Contains(filter))
                .ToListAsync();
            return clients;
        }
        public async Task<List<Car>> ReadCarsAsync(string filter)
        {
            using var context = new AppDbContext(_options);
            var cars = await context.Cars
                .Where(c => c.Brand.Contains(filter))
                .ToListAsync();
            return cars;
        }
        public async Task RunParallelReadAsync()
        {
            var clientsTask = ReadClientsAsync("Клієнт");
            var carsTask = ReadCarsAsync("Машина");
            await Task.WhenAll(clientsTask, carsTask);
            Console.WriteLine($"Паралельно було зчитано {clientsTask.Result.Count} клієнтів та {carsTask.Result.Count} машин");
        }
    }
}