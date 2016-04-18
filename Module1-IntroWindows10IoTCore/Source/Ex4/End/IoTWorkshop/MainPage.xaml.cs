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
        }
    }
}
