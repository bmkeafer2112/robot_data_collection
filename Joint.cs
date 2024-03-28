using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Manufacturing
{
    public class Joint
    {
        [JsonProperty(Order = 1)]
        public string Name { get; set; }
        [JsonProperty(Order = 2)]
        //public Speed speed = new Speed();

        public Amp amps = new Amp();
        [JsonProperty(Order = 4)]
        public Torque Torque = new Torque();
        [JsonProperty(Order = 5)]
        public float Angle = 0;




    }
}
