using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFXML.Models
{
    public class CarDto
    {
        public string Model { get; set; }
        public Price Price { get; set; }
        public double PriceWithVat { get; set; }
    }
}
