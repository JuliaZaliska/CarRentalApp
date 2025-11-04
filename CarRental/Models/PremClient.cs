using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class PremClient : Client
    {
        public double DiscountRate { get; set; }
        public DateTime MembershipDate { get; set; }
    }
}
