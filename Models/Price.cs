using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFXML.Models
{
    [XmlRoot(ElementName = "price")]
    public class Price
    {
        [XmlAttribute]
        public string Currency { get; set; }
        [XmlText]
        public string AmountText { get; set; }

        public double Amount => double.Parse(AmountText.Replace(".", ""));
    }
}
