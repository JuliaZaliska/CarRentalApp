using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            ICarRepository carRepo = new CarRepository();
            IRentalService rentalService = new RentalService(carRepo);

            rentalService.RentalStarted += (sender, car) =>
                Console.WriteLine($"Автомобіль {car.Brand} {car.Model} успішно орендований!");
            rentalService.RentalEnded += (sender, car) =>
                Console.WriteLine($"Автомобіль {car.Brand} {car.Model} було успішно повернено!");

            carRepo.AddCar(new Car("JTDBE32K123456789", "Mazda", "CX-5", 2021));
            carRepo.AddCar(new Car("WBAJU71030BL12345", "Audi", "Q7", 2017));
            carRepo.AddCar(new Car("1HGCM82633A004352", "Toyota", "Camry", 2020));
            carRepo.AddCar(new Car("JHMCM56557C404453", "BMW", "X5", 2019));
            carRepo.AddCar(new Car("5N1AT2MV0FC123456", "Nissan", "Rogue", 2022));

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nМеню Автопарку");
                Console.WriteLine("1. Орендувати авто");
                Console.WriteLine("2. Повернути авто");
                Console.WriteLine("3. Вийти");
                Console.Write("Обери опцію: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть ваше ім'я: ");
                        string name = Console.ReadLine();

                        Console.Write("Введіть ваше прізвище: ");
                        string surname = Console.ReadLine();

                        Console.Write("Введіть ваш РНОКПП: ");
                        string taxNumber = Console.ReadLine();

                        Console.Write("Введіть ваш вік: ");
                        if (!int.TryParse(Console.ReadLine(), out int age))
                        {
                            Console.WriteLine("Некоректно введений вік!");
                            break;
                        }

                        Client client = new Client(name, surname, taxNumber, age);

                        var availableCars = carRepo.GetAvailableCars();
                        if (availableCars.Count == 0)
                        {
                            Console.WriteLine("На даний момент вільних авто немає.");
                            break;
                        }

                        Console.WriteLine("Вільні авто:");
                        foreach (var car in availableCars)
                        {
                            Console.WriteLine($"{car.VIN} - {car.Brand} {car.Model} ({car.Year})");
                        }

                        Console.Write("Введіть VIN авто для оренди: ");
                        string vinRent = Console.ReadLine();
                        rentalService.RentCar(vinRent, client);
                        break;

                    case "2":
                        Console.Write("Введіть VIN авто для повернення: ");
                        string vinReturn = Console.ReadLine();
                        rentalService.ReturnCar(vinReturn);
                        break;

                    case "3":
                        exit = true;
                        Console.WriteLine("Дякуємо за користування автопарком!");
                        break;

                    default:
                        Console.WriteLine("Такої опції не існує. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
