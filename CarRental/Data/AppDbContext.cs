using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Car> Cars { get; set; }
        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.Rental> Rentals { get; set; }
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarRentalDB;Trusted_Connection=True;")
                    .UseLazyLoadingProxies();
                    //.LogTo(Console.WriteLine);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ключі
            modelBuilder.Entity<Car>()
                .HasKey(c => c.CarId);
            modelBuilder.Entity<Car>()
                .HasAlternateKey(c => c.VIN);

            // Один до багатьох
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Car)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Дефолтне знач
            modelBuilder.Entity<Rental>()
                .Property(r => r.RentDate)
                .HasDefaultValueSql("GETDATE()");

            // Обмеж довжини поля БД
            modelBuilder.Entity<Car>()
                .Property(c => c.Brand)
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(c => c.Model)
                .HasMaxLength(50);

            // Check
            modelBuilder.Entity<Client>()
                .HasCheckConstraint("CK_Client_Age", "Age >= 18");
            modelBuilder.Entity<Car>()
                .HasCheckConstraint("CK_Car_Year", "Year <= 2025");

            // Заповнення по замовч
            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, VIN = "7TDBE32K123456789", Brand = "Mazda", Model = "CX-5", Year = 2021 },
                new Car { CarId = 2, VIN = "8BAJU71030BL12345", Brand = "Audi", Model = "Q7", Year = 2017 }
            );
        
            // TPH
            modelBuilder.Entity<Client>()
                .HasDiscriminator<string>("ClientType")
                .HasValue<Client>("Standard")
                .HasValue<PremClient>("Premium");

            modelBuilder.Entity<PremClient>().HasData(new PremClient
            {
                ClientId = 100,
                Name = "Ірина",
                Surname = "Ковальук",
                Age = 29,
                TaxNumber = "1234567890",
                DiscountRate = 0.15,
                MembershipDate = new DateTime(2022, 5, 1)
            }
            );
        }
    }
}