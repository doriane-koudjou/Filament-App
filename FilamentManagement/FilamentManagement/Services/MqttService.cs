using MQTTnet;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilamentManagement.Services
{
    public class MqttService
    {
        private static MqttFactory mqttFactory = new MqttFactory();

        public static async Task<bool> Publish(string server,string topic, string payload)
        {
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(server).Build();
                try
                {
                    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error on MQTT Client connect: {e}");
                    return false;
                }

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .Build();
                try
                {
                    await mqttClient.PublishAsync(message, CancellationToken.None);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error on MQTT Client publish: {e}");
                    return false;
                }
                
                var mqttClientDisconnectOptions = new MqttClientDisconnectOptions();
                try
                {
                    await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
                }catch(Exception e)
                {
                    Console.WriteLine($"Error on MQTT Client disconnect: {e}");
                    return false;
                }
                
                return true;
            }
        }
    }
}
