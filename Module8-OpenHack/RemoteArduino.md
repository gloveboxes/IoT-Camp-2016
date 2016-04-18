<a name="HOLTop" />
# Windows Remote Arduino
![](http://www.arduino.cc/en/uploads/Main/ArduinoUno_R3_Front_450px.jpg)

Using an Arduino as an extension of your app on the Raspberry Pi or Windows PC is an exciting way to quickly add GPIO and other interfaces to a UWP app on a device which may not otherwise support them. For example, you can use an Arduino in concert with a Raspberry Pi to perform analog IO, something the Pi lacks in its native GPIO implementation.

Windows Remote Arduino is also an opportunity to play with "big brain / little brain" scenarios where an application on an application processor (PC, Pi, Phone) does all the user interface interaction and network IO, and the little brain (Arduino) does more real-time operations such as PWM (Pulse Width Modulation), pin toggling, and more.

Example using a Bluetooth-connected Arduino

	````C#
    private RemoteDevice arduino;
    private BluetoothSerial bluetooth;
 
    public MainPage()
    {
        this.InitializeComponent();
 
        bluetooth = new BluetoothSerial("RNBT-5A60");  
        arduino = new RemoteDevice(bluetooth); 
  
        arduino.DeviceReady += Arduino_DeviceReady;
        arduino.DeviceConnectionFailed += Arduino_DeviceConnectionFailed;
  
        bluetooth.begin();
    }
 
    private void Arduino_DeviceConnectionFailed(string message)
    {
        Debug.WriteLine(message);
    }
  
    private void Arduino_DeviceReady()
    {
        arduino.pinMode(13, PinMode.OUTPUT);
        loop();
    }
  
    private async void loop()
    {
        int DELAY_MILLIS = 1000;
  
        while( true )
        {
            // toggle pin 13 to a HIGH state and delay for 1 second
            arduino.digitalWrite(13, PinState.HIGH);
            await Task.Delay(DELAY_MILLIS);
  
            // toggle pin 13 to a LOW state and delay for 1 second
            arduino.digitalWrite(13, PinState.LOW);
            await Task.Delay(DELAY_MILLIS);
        }
    }
	````

For more information on how to use Windows Remote Arduino, please see these links:

  * [Windows Remote Arduino](http://ms-iot.github.io/content/en-US/win10/WRALanding.htm)
  * [Windows Remote Arduino Blog Post](http://blogs.windows.com/buildingapps/2016/02/04/what-is-windows-remote-arduino-and-what-can-it-do/)

<a href="#HOLTop"> -- Back to Top -- </a>
