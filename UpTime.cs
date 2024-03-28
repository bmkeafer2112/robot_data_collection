using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Manufacturing
{
    public class UpTime
    {
        [JsonProperty(Order = 1)]
        public string UOM = "Up time";
        [JsonProperty(Order = 2)]
        public Double Value { get; set; }
    }
}


