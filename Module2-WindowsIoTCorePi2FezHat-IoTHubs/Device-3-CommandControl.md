>[Home](README.md) </br>
>Previous Lab [Analytics with Power Bi](AnalyticsWithPowerBi.md)

# Controlling a device from Azure IoT Hub

Azure IoT Hub is a service that enables reliable and secure bi-directional communications between millions of IoT devices and an application back end. In this section you will see how to send cloud-to-device messages to your device to command it to change the color of one of the FEZ HAT leds, using the Device Explorer app as the back end.

Azure IoT Hub supports a number of protocols including [AMQP](https://en.wikipedia.org/wiki/AMPQ), HTTPS and [MQTT](https://en.wikipedia.org/wiki/MQTT).

In this lab we are going to publish data to Azure IoT Hub over MQTT and subscript to command messages.

1. Open in Visual Studio the **IoTHubMqttClient.sln** solution located at **Source\Ex4\Begin** folder.

This solution takes advantage of 3 Nuget packages that have already been installed and referenced.


1. The [M2Mqtt Client Library](https://m2mqtt.wordpress.com/using-mqttclient) (Install-Package M2Mqtt)
2. The [Fez Hat Library](https://www.ghielectronics.com/docs/329/fez-hat-developers-guide) (Install-Package GHIElectronics.UWP.Shields.FEZHAT)
3. The [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) library (Install-Package Newtonsoft.Json)

Steps to complete

1. From **Device Explorer** -> **Management Tab** -> **Right Mouse click your device** -> **Select Copy Connection String for selected device**
2. Modify the _SecurityManager_ in the **StartupTask.cs** class, replace the placeholder value with the **device connection string** you just copied in the previous step (note that the curly braces { } 
are _NOT_ part of the connection string and should be _removed_ when you paste in your connection string).  

````C#
SecurityManager cm = new SecurityManager("{Azure IoT Hub Device Connection String}");
````


3. Add code to be execute when the device receives a message from Azure IoT Hub. The following code should be pasted in to the  **Client_MqttMsgPublishReceived** method in the **StartupTask.cs** class.

````C#
switch (message) {
    case "RED":
        hat.D2.Color = new FEZHAT.Color(255, 0, 0);
        break;
    case "GREEN":
        hat.D2.Color = new FEZHAT.Color(0, 255, 0);
        break;
    case "BLUE":
        hat.D2.Color = new FEZHAT.Color(0, 0, 255);
        break;
    case "OFF":
        hat.D2.TurnOff();
        break;
    default:
        System.Diagnostics.Debug.WriteLine("Unrecognized command: {0}", message);
        break;
}
````

Your completed code should look like the following


````C#

using GHIElectronics.UWP.Shields;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using Windows.ApplicationModel.Background;


namespace IoTHubMqttClient {
    public sealed class StartupTask : IBackgroundTask {
        BackgroundTaskDeferral deferral;

        private FEZHAT hat;

        Telemetry temperature = new Telemetry("41c2e437-6c3d-48d0-8e12-81eab2aa5013", "Temperature", "C");
        Telemetry light = new Telemetry("41c2e437-6c3d-48d0-8e12-81eab2aa5014", "Light", "L");

        SecurityManager cm = new SecurityManager("HostName=glovebox-iot-hub.azure-devices.net;DeviceId=RPiSC;SharedAccessKey=z5c+MtYY5zMy7wj3SDiRMpZC7W+UiOkaKTxh/5kP6+c=");

        string hubUser;
        string hubTopicPublish;
        string hubTopicSubscribe;

        private MqttClient client;

        public async void Run(IBackgroundTaskInstance taskInstance) {
            deferral = taskInstance.GetDeferral();

            hubUser = $"{cm.hubAddress}/{cm.hubName}";
            hubTopicPublish = $"devices/{cm.hubName}/messages/events/";
            hubTopicSubscribe = $"devices/{cm.hubName}/messages/devicebound/#";

            this.hat = await FEZHAT.CreateAsync();

            // https://m2mqtt.wordpress.com/m2mqtt_doc/
            client = new MqttClient(cm.hubAddress, 8883, true, MqttSslProtocols.TLSv1_2);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { hubTopicSubscribe }, new byte[] { 0 });

            var result = Task.Run(async () => {
                while (true) {
                    if (!client.IsConnected) { client.Connect(cm.hubName, hubUser, cm.hubPass); }
                    if (client.IsConnected) {
                        client.Publish(hubTopicPublish, temperature.ToJson(hat.GetTemperature()));
                        client.Publish(hubTopicPublish, light.ToJson(hat.GetLightLevel()));
                    }
                    await Task.Delay(30000); // don't leave this running for too long at this rate as you'll quickly consume your free daily Iot Hub Message limit
                }
            });
        }

        private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e) {
            string message = System.Text.Encoding.UTF8.GetString(e.Message).ToUpperInvariant();
            Debug.WriteLine($"Command Received: {message}");

            switch (message) {
                case "RED":
                    hat.D2.Color = new FEZHAT.Color(255, 0, 0);
                    break;
                case "GREEN":
                    hat.D2.Color = new FEZHAT.Color(0, 255, 0);
                    break;
                case "BLUE":
                    hat.D2.Color = new FEZHAT.Color(0, 0, 255);
                    break;
                case "OFF":
                    hat.D2.TurnOff();
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Unrecognized command: {0}", message);
                    break;
            }
        }
    }
}
 

````

###Deploy your app to the Raspberry Pi

1. Press **F5** to deploy and run the app on the device.
2. Once the app has started on the Raspberry Pi start the **Device Explorer** go to the **Messages To Device tab**, 
    - Select your device
    - Check the Monitor Feedback Endpoint option 
    - Write your command in the Message field (red, green or blue). Click on Send to send the command to your Raspberry Pi
3. Try putting a break point in the Client_MqttMsgPublishReceived method, the break point will be hit when the code receives a message

    ![Sending cloud-to-device message](Images/sending-cloud-to-device-message.png?raw=true)

7. After a few seconds the message will be processed by the device and the LED will turn on in the color you selected. The feedback will also be reflected in the Device Explorer screen after a few seconds.

	![cloud-to-device message received](Images/cloud-to-device-message-received.png?raw=true)

Congratulations, all done!


>[Home](README.md)