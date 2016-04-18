using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTHelloBlinky
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeGpio();
        }

        private const int LED_PIN = 24;
        private GpioPin _pin;
        private void InitializeGpio()
        {
            var controller = GpioController.GetDefault();

            if (controller != null)
            {
                _pin = controller.OpenPin(LED_PIN);

                _pin.SetDriveMode(GpioPinDriveMode.Output);

                _pin.Write(GpioPinValue.Low);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Target device has no GPIO controller");
            }
        }

        private void ToggleLed_Checked(object sender, RoutedEventArgs e)
        {
            if (_pin != null)
                _pin.Write(GpioPinValue.High);
        }

        private void ToggleLed_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_pin != null)
                _pin.Write(GpioPinValue.Low);
        }
    }
}
