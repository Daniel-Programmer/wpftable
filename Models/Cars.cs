using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFXML.Models
{
    [XmlRoot(ElementName = "cars")]
    public class Cars
    {
        [XmlElement(ElementName = "car")]
        public List<Car> CarsList { get; set; }

    }
}
