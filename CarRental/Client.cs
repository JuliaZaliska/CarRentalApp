using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp
{
    public class Client
    {
        public string Name { get; }
        public string Surname { get; }
        public string TaxNumber { get; }
        public int Age { get; }

        public Client(string name, string surname, string taxNumber, int age)
        {
            Name = name;
            Surname = surname;
            TaxNumber = taxNumber;
            Age = age;
        }

        public override string ToString() => $"{Name} {Surname}, вік {Age}, ІПН: {TaxNumber}";
    }
}
