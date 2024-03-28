using MQTTnet;

using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Manufacturing
{
    class MQTT
    {

        /// <summary>
        /// 
        /// <para></para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="a"></typeparam>
        /// <param name="b"></param>
        /// <param name="c"></param>

        public static async Task ConnectAsync()
        {
            string clientID = Guid.NewGuid().ToString();
            string mqttURI = "192.168.190.52";
            //string mqttURI = "192.168.1.101";
            int mqttPort = 1883;
            bool mqttSecure = false;

            var messegeBuilder = new MQTTnet.Client.MqttClientOptionsBuilder()
                .WithClientId(clientID)
                .WithTcpServer(mqttURI, mqttPort)
                .WithCleanSession();
            var options = mqttSecure
                ? messegeBuilder
                .WithTls()
                .Build()
                : messegeBuilder
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();

            var client = new MqttFactory().CreateManagedMqttClient();


            await client.StartAsync(managedOptions);


        }

       
           
    }
}


