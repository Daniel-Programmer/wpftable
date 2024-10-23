using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFXML.Models
{
    [XmlRoot(ElementName = "car")]
    public class Car
    {
        [XmlAttribute]
        public int CarId { get; set; }

        [XmlElement(ElementName = "model")]
        public string Model { get; set; }

        [XmlElement(ElementName = "saleDate")]
        public DateTime SaleDate { get; set; }

        [XmlElement(ElementName = "price")]
        public Price Price { get; set; }

        [XmlElement(ElementName = "vat")]
        public double Vat { get; set; }

        public double PriceWithVat
        {
            get
            {
                return Price.Amount + (Price.Amount / 100) * Vat;
            }
        }

        public override string ToString()
        {
            return $"Model: {Model}, price: {Price.Amount}, priceWithVat: {PriceWithVat}";
        }
    }
}
