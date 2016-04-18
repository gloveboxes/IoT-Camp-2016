using GHIElectronics.UWP.Shields;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using Windows.ApplicationModel.Background;


namespace IoTHubMqttClient {
    public sealed class StartupTask : IBackgroundTask {

        SecurityManager cm = new SecurityManager("{Azure IoT Hub Device Connection String}");

        BackgroundTaskDeferral deferral;

        private FEZHAT hat;

        Telemetry temperature = new Telemetry("41c2e437-6c3d-48d0-8e12-81eab2aa5013", "Temperature", "C");
        Telemetry light = new Telemetry("41c2e437-6c3d-48d0-8e12-81eab2aa5014", "Light", "L");        

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
