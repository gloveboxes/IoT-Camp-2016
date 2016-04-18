using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GHIElectronics.UWP.Shields;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTWorkshop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FEZHAT hat;
        private DispatcherTimer timer;
        private bool next;

        public MainPage()
        {
            this.InitializeComponent();

            // Initialize FEZ HAT shield
            this.SetupHat();
        }

        private async void SetupHat()
        {
            this.hat = await FEZHAT.CreateAsync();

            this.timer = new DispatcherTimer();

            this.timer.Interval = TimeSpan.FromMilliseconds(500);
            this.timer.Tick += this.Timer_Tick;

            this.timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            var btn1 = this.hat.IsDIO18Pressed();
            var btn2 = this.hat.IsDIO22Pressed();

            if (btn1 || btn2)
            {
                this.hat.DIO24On = this.next;
                System.Diagnostics.Debug.WriteLine("LED turned " + (this.next ? "on" : "off"));
                this.next = !this.next;
            }
            else
            {
                this.hat.DIO24On = false;
            }
            
            // Light Sensor
            var light = this.hat.GetLightLevel();

            // Temperature Sensor
            var temp = this.hat.GetTemperature();

            // Display values
            this.LightTextBox.Text = light.ToString("P2");
            this.LightProgress.Value = light;
            this.TempTextBox.Text = temp.ToString("N2");
            this.TempProgress.Value = temp;

            System.Diagnostics.Debug.WriteLine("Temperature: {0} °C, Light {1}", temp.ToString("N2"), light.ToString("N2"));

            var lightIntensity = (byte)(light * 255);
            var tempIntensity = (byte)(temp * 255 / 60);

            this.hat.D2.Color = new FEZHAT.Color(tempIntensity, 0, 0);
            this.hat.D3.Color = new FEZHAT.Color(0, 0, lightIntensity);
        }
    }
}
