using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Rental
    {
        public int RentalId { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal PricePerDay { get; set; }
    }
}

