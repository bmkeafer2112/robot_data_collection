using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Smart_Manufacturing
{
    [Serializable]
    [XmlType(TypeName = "Device")]
    public class Device
    {
        //Allow the field to be an Xml Element 
        [XmlElement]
        public string Name { get; set; }

        //Allow the field to be an Xml Element
        [XmlElement]
        [JsonIgnore]

        //Allow the field to be an Xml Element
        public string Group { get; set; }
        [XmlElement]
        [JsonIgnore]

        
        public string Line { get; set; }
        [XmlElement]
        [JsonIgnore]

        //Allow the field to be an Xml Element
        public string Block { get; set; }

        //Allow the field to be an Xml Element
        [XmlElement]
        public string IPAddress { get; set; }

        //Allow the field to be an Xml Element
        [XmlElement]
        public string DeviceType { get; set; }

        //Allow the field to be an Xml Element
        [XmlElement]
        [JsonIgnore]
        public int Packet_Speed { get; set; }
    }



}
