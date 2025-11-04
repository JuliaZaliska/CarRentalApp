using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        [StringLength(17)]
        public string VIN { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }
        
        [Required]
        public bool IsRented { get; set; } = false;

        // Одне авто може бути орендоване кілька разів
        public List<Rental> Rentals { get; set; } = new();
    }
}