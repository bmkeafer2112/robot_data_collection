using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Smart_Manufacturing
{
    public class MQTTSettings
    {
        [XmlElement]
        public string IPAddress { get; set; }

        [XmlElement]
        [JsonIgnore]
        public int Port_Number { get; set; }
        [XmlElement]
        [JsonIgnore]
        public int Send_Time { get; set; }
        
    }
}
