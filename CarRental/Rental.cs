using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp
{
    public class Rental
    {
        public Car Car { get; }
        public Client Client { get; }
        public DateTime StartDate { get; }
        public DateTime? EndDate { get; set; }

        public Rental(Car car, Client client)
        {
            Car = car;
            Client = client;
            StartDate = DateTime.Now;
        }
    }
}

