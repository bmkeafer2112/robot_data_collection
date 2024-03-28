using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Smart_Manufacturing
{

    public class Devices
    {
        public Devices() {Items = new List<Device>(); }
        [XmlElement("device")]
        public List<Device> Items { get; set; }

        
    }
}
