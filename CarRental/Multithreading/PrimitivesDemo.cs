using CarRental.Data;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarRental.Multithreading
{
    public class PrimitivesDemo
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private static List<string> _generatedNames = new();
        private static int _idCounter = 0;
        private static readonly Mutex _mutexObj = new();
        private static readonly object _lockObj = new();
        public PrimitivesDemo(DbContextOptions<AppDbContext> options) => _options = options;
        public async Task SetupDatabaseAsync()
        {
            using var context = new AppDbContext(_options);
            await context.Database.EnsureCreatedAsync();
            await context.Clients.ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }
        public void ThreadWithMutex()
        {
            int id;
            string name;
            _mutexObj.WaitOne();
            try
            {
                id = ++_idCounter;
                name = $"Клієнт_{id}";
                _generatedNames.Add(name);
            }
            finally
            {
                _mutexObj.ReleaseMutex();
            }
            using var context = new AppDbContext(_options);
            context.Clients.Add(new Client { Name = name, Surname = "Mutex", Age = 18 + (id % 5), TaxNumber = $"{id:D10}" });
            context.SaveChanges();
            Console.WriteLine($"Створено(Mutex): {name}");
        }
        public void ThreadWithLock()
        {
            int id;
            string name;
            lock (_lockObj)
            {
                id = ++_idCounter;
                name = $"Клієнт_{id}";
                _generatedNames.Add(name);
            }
            using var context = new AppDbContext(_options);
            context.Clients.Add(new Client
            {
                Name = name,
                Surname = "Lock",
                Age = 18 + (id % 5),
                TaxNumber = $"{id:D10}"
            });
            context.SaveChanges();
            Console.WriteLine($"Створено(Lock): {name}");
        }
        public void ThreadWithoutPrim()
        {
            int id = ++_idCounter;
            string name = $"Клієнт_{id}";
            _generatedNames.Add(name);

            using var context = new AppDbContext(_options);
            context.Clients.Add(new Client { Name = name, Surname = "Без_Синхр", Age = 18 + (id % 5), TaxNumber = $"{id:D10}" });
            context.SaveChanges();
            Console.WriteLine($"Створено(без_синхр): {name}");
        }
        public void RunDemoThreads()
        {
            _generatedNames.Clear();
            _idCounter = 0;
            var timer = Stopwatch.StartNew();

            Thread[] threads = new Thread[10000];
            for (int i = 0; i < threads.Length; i++)
            {
                //threads[i] = new Thread(ThreadWithMutex); // 100 за 6042 мс; 1000 за 23797 мс; 10 000 - 130314 мс;
                //threads[i] = new Thread(ThreadWithLock); // 100 - 5671 мс; 1000 - 23611 мс; 10 000 - 137410 мс;
                threads[i] = new Thread(ThreadWithoutPrim); // 100 - 6470 мс; 1000 - 25399 мс; 10 000 - 138490 мс;
                threads[i].Start();
            }
            foreach (var t in threads) t.Join();

            timer.Stop();
            Console.WriteLine($"Створено {threads.Length} екземплярів за {timer.ElapsedMilliseconds} мс");
        }
    }
}