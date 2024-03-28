﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Manufacturing
{
    public class Axis
    {
        [JsonProperty(Order = 1)]
        public string UOM = "mm";
        [JsonProperty(Order = 2)]
        public Single Value { get; set; }
    }
}
