using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Manufacturing

{
    public class RobotPosition
    {
        [JsonProperty(Order = 1)]
        public Axis x = new Axis();
        
        [JsonProperty(Order = 3)]
        public Axis y = new Axis();

        [JsonProperty(Order = 5)]

        public Axis z = new Axis();
        
        [JsonProperty(Order = 7)]
        public Axis rx = new Axis(); 
        
        [JsonProperty(Order = 9)]
        public Axis ry = new Axis();

        [JsonProperty(Order = 11)]
        public Axis rz = new Axis();


    }

    public class RobotPositionAngle
    {
        [JsonProperty(Order = 1)]
        public Axis one = new Axis();

        [JsonProperty(Order = 3)]
        public Axis two = new Axis();

        [JsonProperty(Order = 5)]

        public Axis three = new Axis();

        [JsonProperty(Order = 7)]
        public Axis four = new Axis();

        [JsonProperty(Order = 9)]
        public Axis five = new Axis();

        [JsonProperty(Order = 11)]
        public Axis six = new Axis();


    }
}
