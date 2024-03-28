using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Manufacturing
{
    public class Amp
    {
        [JsonProperty(Order = 1)]
        public string UOM = "Percentage_of_load";
        [JsonProperty(Order = 2)]
        public Double Value { get; set; }
    }
}
