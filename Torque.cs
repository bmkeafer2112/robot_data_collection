using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Manufacturing
{
    public class Torque
    {
        [JsonProperty(Order = 1)]
        public string UOM = "percentage_of_load";
        [JsonProperty(Order = 2)]
        public Double Value { get; set; }
    }
}
