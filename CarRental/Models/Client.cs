using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Surname { get; set; }

        [Required, StringLength(10)]
        public string TaxNumber { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }

        // Один клієнт може мати кілька оренд
        public List<Rental> Rentals { get; set; } = new();
    }
}
