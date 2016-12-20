using DataGenerator.Objects;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = Credentials.IOT_HUB_URI;
        static string deviceKey = Credentials.DEVICE_KEY;

        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey), Microsoft.Azure.Devices.Client.TransportType.Mqtt);

            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            var delay = 1000;
            var geoList = GetGeoDataList();
            var signList = new List<string> { "STOP", "GO", "GO"};

            while (true)
            {
                var telemetryDataPoint = new
                {
                    DeviceID = "BlueBox1",
                    Latitude = GetRandomListElement(0, geoList, null),
                    Longitude = GetRandomListElement(1, geoList, null),
                    StreetSign = GetRandomListElement(2, null, signList)
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(delay).Wait();
            }
        }

        private static List<GeoObject> GetGeoDataList()
        {
            return new List<GeoObject>
            {
                new GeoObject{ lat = "36.129617", lon = "-115.154715" },
                new GeoObject{ lat = "36.129738", lon = "-115.136926" },
                new GeoObject{ lat = "36.140778", lon = "-115.136905" },
                new GeoObject{ lat = "36.131125", lon = "-115.165401" },
                new GeoObject{ lat = "36.143463", lon = "-115.153899" },
                new GeoObject{ lat = "36.130674", lon = "-115.180829" },
                new GeoObject{ lat = "36.144469", lon = "-115.170530" },
                new GeoObject{ lat = "36.136082", lon = "-115.090536" }, 
                new GeoObject{ lat = "36.115214", lon = "-115.082553" }
            };
        }

        private static string GetRandomListElement(int index, List<GeoObject> geolist, List<string> signList)
        {
            Random r = new Random();
            r.Next(0, 3);
            string value; 

            switch (index)
            {
                case 0:
                    value = geolist[r.Next(0, geolist.Count)].lat;
                    break;
                case 1:
                    value = geolist[r.Next(0, geolist.Count)].lon;
                    break;
                default:
                    value = signList[r.Next(signList.Count - 1)];
                    break;
            }

            return value;
        }
    }
}
