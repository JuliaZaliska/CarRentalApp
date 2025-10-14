using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp
{
    public class Car
    {
        public string VIN { get; }
        public string Brand { get; }
        public string Model { get; }
        public int Year { get; }
        public bool IsRented { get; set; }

        public Car(string vin, string brand, string model, int year)
        {
            VIN = vin;
            Brand = brand;
            Model = model;
            Year = year;
            IsRented = false;
        }

        public override string ToString() => $"{Brand} {Model} ({Year}) - VIN: {VIN}";
    }
}



