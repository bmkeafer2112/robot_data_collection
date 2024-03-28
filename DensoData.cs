using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Smart_Manufacturing
{
     public class DensoData
    {

        [JsonProperty(Order = 1)]
        public string Robot_Name  { get; set; }
        [JsonProperty(Order = 2)]
        public string Device_Type = "robot";
        [JsonProperty(Order = 3)]
        public string Time_Stamp { get; set; }
        [JsonProperty(Order = 5)]

        public Joint[] Joints = new Joint[7]    
        {

            new Joint(), new Joint(), new Joint(), new Joint(), new Joint(), new Joint(), new Joint()
        };
        [JsonProperty(Order = 4)]

        public RobotPosition Tool_Center_Point = new RobotPosition();
       

    }

}
