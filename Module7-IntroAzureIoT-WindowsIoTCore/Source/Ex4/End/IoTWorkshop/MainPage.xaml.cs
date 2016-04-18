namespace IoTWorkshop
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;
    using GHIElectronics.UWP.Shields;
    using Microsoft.Azure.Devices.Client;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage : Page
    {
        private const bool UseMockedSensors = false;
        private Random rnd;
        private FEZHAT hat;
        private DispatcherTimer timer;
        private DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("{device connection string}");
        private DispatcherTimer commandsTimer;

        public MainPage()
        {
            this.InitializeComponent();

            // Initialize FEZ HAT shield
            this.SetupHat();
        }

        public async void SendMessage(string message)
        {
            // Send message to an IoT Hub using IoT Hub SDK
            try
            {
                var content = new Message(Encoding.UTF8.GetBytes(message));
                await deviceClient.SendEventAsync(content);

                Debug.WriteLine("Message Sent: {0}", message, null);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception when sending message:" + e.Message);
            }
        }

        public async Task<string> ReceiveMessage()
        {
            try
            {
                var receivedMessage = await this.deviceClient.ReceiveAsync();

                if (receivedMessage != null)
                {
                    var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    await this.deviceClient.CompleteAsync(receivedMessage);
                    return messageData;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception when receiving message:" + e.Message);
                return string.Empty;
            }
        }

        private async void SetupHat()
        {
            if (UseMockedSensors)
            {
                this.rnd = new Random();
            }
            else
            {
                this.hat = await FEZHAT.CreateAsync();
            }

            this.timer = new DispatcherTimer();

            this.timer.Interval = TimeSpan.FromMilliseconds(500);
            this.timer.Tick += this.Timer_Tick;

            this.timer.Start();

            //setup receive timer
            this.commandsTimer = new DispatcherTimer();
            this.commandsTimer.Interval = TimeSpan.FromSeconds(60);
            this.commandsTimer.Tick += this.CommandsTimer_Tick;
            this.commandsTimer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // Light Sensor
            var light = UseMockedSensors ? rnd.NextDouble() : this.hat.GetLightLevel();

            // Temperature Sensor
            var temp = UseMockedSensors ? rnd.NextDouble() * 50 - 10 : this.hat.GetTemperature();

            // Display values
            this.LightTextBox.Text = light.ToString("P2");
            this.LightProgress.Value = light;
            this.TempTextBox.Text = temp.ToString("N2");
            this.TempProgress.Value = temp;

            // send data to IoT Hub
            var jsonMessage = string.Format("{{ displayname:null, location:\"USA\", organization:\"Fabrikam\", guid: \"41c2e437-6c3d-48d0-8e12-81eab2aa5013\", timecreated: \"{0}\", measurename: \"Temperature\", unitofmeasure: \"C\", value:{1}}}",
                 DateTime.UtcNow.ToString("o"),
                 temp);

            this.SendMessage(jsonMessage);

            jsonMessage = string.Format("{{ displayname:null, location:\"USA\", organization:\"Fabrikam\", guid: \"41c2e437-6c3d-48d0-8e12-81eab2aa5013\", timecreated: \"{0}\", measurename: \"Light\", unitofmeasure: \"L\", value:{1}}}",
                 DateTime.UtcNow.ToString("o"),
                 light);

            this.SendMessage(jsonMessage);
        }

        private async void CommandsTimer_Tick(object sender, object e)
        {
            string message = await this.ReceiveMessage();

            if (message != string.Empty)
            {
                Debug.WriteLine("Command Received: {0}", message);
                switch (message.ToUpperInvariant())
                {
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
                        Debug.WriteLine("Unrecognized command: {0}", message);
                        break;
                }
            }
        }
    }
}
