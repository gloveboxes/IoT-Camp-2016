<a name="HOLTop" />
# Using GPIO on Windows 10 IoT Core
![](http://ms-iot.github.io/content/images/Blinky/breadboard_assembled_rpi2_kit.jpg)

As part of the in-room kit, we've provided basic discrete components (LEDs, resistors, capacitors, switches, potentiometers, and more) as well as light and temperature sensors. We also have a number of other sensors and input devices mounted on PCBs.

If you're new to GPIO in Windows 10, we suggest you start with the one of the blinky examples.

Additional information:

  * [Pinout and more examples](http://ms-iot.github.io/content/en-US/win10/samples/PinMappingsRPi2.htm)
  * [Blinky](http://ms-iot.github.io/content/en-US/win10/samples/Blinky.htm)
  * [Blinky headless](http://ms-iot.github.io/content/en-US/win10/samples/BlinkyHeadless.htm)

<a href="#HOLTop"> -- Back to Top -- </a>

## Using discrete LEDs
When working with LEDs, keep in mind that the long lead of the LED is positive, and the short lead is negative. If you connect in the opposite way, you will likely burn out the LED.

Always use a resistor in series with the LED. Otherwise you risk damaging the LED, the processor, or both. Here's the resistor calculator. Use the next highest available value if the calculated value is not available.

  * [Choosing a Resistor for LEDs](http://www.instructables.com/id/Choosing-The-Resistor-To-Use-With-LEDs/)

<a href="#HOLTop"> -- Back to Top -- </a>

## LED Project Ideas
8 Bit Larson Scanner with Arduino (you can apply this to the GPIO on the Raspberry Pi as well). The Pi pins are 3v3, not 5v, so factor that into your resistor calculations.

  * [8 LED Larson Scanner with Arduino](http://www.instructables.com/id/8-LED-Larson-Scanner-with-Arduino/)
  
The most basic embedded/IoT "hello world" is blinking an LED. In Windows 10 IoT Core, you can do this from any number of languages. Examples in C# and Wiring follow.

### C# #
Here's how to blink an LED from C#. The Visual Basic code would be very similar.

	````C#
    using Windows.Devices.Gpio;

     GpioPin _pin;

    // Call from the page loaded event
    private async void InitGPIO()
    {
        // the default controller is configured per-device
        // There is also a lightning controller which provides much faster IO
        // at the expense of some of the security and related protection
        // provided by the default provider.
        var gpio = GpioController.GetDefault();

        if (gpio == null)
        {
            _pin = null;
            return;
        }

        _pin = gpio.OpenPin(LED_PIN);

        if (_pin == null)
        {
            return;
        }

        _pin.SetDriveMode(GpioPinDriveMode.Output);
        _pin.Write(GpioPinValue.High);
     }

     ...

     // turn the LED off, assuming we're driving by connecting positive to
     // to one of the IO pins. If you connect negative and use the Pi as
     // a current sink, the high/low values are inverted.
     // Use GpioPinValue.Low or GpioPinValue.High
     // Do this from in a button event handler, timer code, or other event

     _pin.Write(GpioPinValue.Low);
	````

<a href="#HOLTop"> -- Back to Top -- </a>

### Wiring in Windows
Wiring isn't just for Arduino and the Arduino IDE. In Windows 10, you can use Wiring and Visual Studio to code for the Raspberry Pi and Windows 10.

Here's the basic Blinky Wiring app. You may recognize it from Arduino samples.

	````C#
    void setup()
    {
        // put your setup code here, to run once
      
        pinMode(GPIO_5, OUTPUT); // Configure the pin for OUTPUT so you can turn on the LED.
    }

    void loop()
    {
        // put your main code here, to run repeatedly:

        digitalWrite(GPIO_5, LOW);    // turn the LED off by making the voltage LOW
        delay(500);                   // wait for a half second

        digitalWrite(GPIO_5, HIGH);   // turn the LED on by making the voltage HIGH
        delay(500);                   // wait for a half second
    }
	````

More information: 

  * [Hello Blinky (Wiring)](http://ms-iot.github.io/content/en-US/win10/samples/arduino-wiring/HelloBlinky.htm)
  * [Get started with Wiring](http://ms-iot.github.io/content/en-US/win10/ArduinoWiringProjectGuide.htm)

<a href="#HOLTop"> -- Back to Top -- </a>

### Node.js Web Server to blink an LED
On Windows 10 IoT Core, you may also use Node.js to create projects like a web server. You could use this to remotely turn on, from your PC, an LED attached to the Raspberry Pi.

	````JS
    var http = require('http');

    var uwp = require("uwp");
    uwp.projectNamespace("Windows");

    var gpioController = Windows.Devices.Gpio.GpioController.getDefault();
    var pin = gpioController.openPin(5);
    pin.setDriveMode(Windows.Devices.Gpio.GpioPinDriveMode.output)
    var currentValue = Windows.Devices.Gpio.GpioPinValue.high;
    pin.write(currentValue);

    http.createServer(function (req, res) {
        if (currentValue == Windows.Devices.Gpio.GpioPinValue.high){
            currentValue = Windows.Devices.Gpio.GpioPinValue.low;
        }else{
            currentValue = Windows.Devices.Gpio.GpioPinValue.high;
        }
        pin.write(currentValue);
        res.writeHead(200, { 'Content-Type': 'text/plain' });
        res.end('LED value: ' + currentValue + '\n');
    }).listen(1337);
    
    uwp.close();
	````

Additional information

  * [Node.js Server Sample](http://ms-iot.github.io/content/en-US/win10/samples/NodejsWUBlinky.htm)

<a href="#HOLTop"> -- Back to Top -- </a>

## Using the shift register
![](http://ms-iot.github.io/content/images/ShiftRegister/ShiftRegisterProjectPicture_480.png)

A common way to implement the Larson Scanner is by using a Shift Register. They are useful for many other tasks as well, such as controlling the individual values of 8 digital pins through a single connection to the board.

Additional information

  * [Shift Register Sample in C#](http://ms-iot.github.io/content/en-US/win10/samples/ShiftRegisterSample.htm)

<a href="#HOLTop"> -- Back to Top -- </a>

## Using the MCP3008 Analog to Digital Converter (ADC)
![](http://ms-iot.github.io/content/images/Potentiometer/OverallCon-3002.PNG)

The kit includes a SPI analog to digital converter (MCP3008). We've included this because you need to use it to handle any analog inputs like a potentiometer or analog sensor.

	````C#
    public void ReadADC()
    {
    	byte[] readBuffer = new byte[3]; /* Buffer to hold read data*/
    	byte[] writeBuffer = new byte[3] { 0x00, 0x00, 0x00 };

	    /* Setup the appropriate ADC configuration byte */
    	switch (ADC_DEVICE)
	    {
    		case AdcDevice.MCP3002:
    			writeBuffer[0] = MCP3002_CONFIG;
    			break;
    		case AdcDevice.MCP3208:
    			writeBuffer[0] = MCP3208_CONFIG;
    			break;
    	}
    
    	SpiADC.TransferFullDuplex(writeBuffer, readBuffer); /* Read data from the ADC                           */
    	adcValue = convertToInt(readBuffer);                /* Convert the returned bytes into an integer value */

    	/* UI updates must be invoked on the UI thread */
    	var task = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
    	{
    		textPlaceHolder.Text = adcValue.ToString();     /* Display the value on screen                      */
    	});
    }
	````

Additional information

  * [MCP3008 Product Information](http://www.mouser.com/ProductDetail/Microchip-Technology/MCP3008-I-P/?qs=sGAEpiMZZMsUzhEcHltCuWSXLB5uNgVo)
  * [MCP3008 Datasheet](http://www.mouser.com/ds/2/268/21295b-72710.pdf)
  * [MCP3008 Potentiometer Sample](http://ms-iot.github.io/content/en-US/win10/samples/Potentiometer.htm)

![](http://ms-iot.github.io/content/images/Potentiometer/MCP3208.PNG)

<a href="#HOLTop"> -- Back to Top -- </a>

## Additional Sensor and Peripheral Information

We don't have code samples for all the sensors in the room, but you can adapt existing SPI, I2C, and other GPIO code to use them from the Raspberry Pi.

  * [Other Official Samples (see Samples tab)](http://ms-iot.github.io/content/en-US/win10/StartCoding.htm)
  * [Robotlinking 37-in-1 Sensor kit](http://www.amazon.com/Robotlinking-Sensor-Module-Arduino-Black/dp/B009OVGKTQ/)
  * [Sunfounder Project Super Starter Kit](http://www.amazon.com/Sunfounder-Project-Starter-Arduino-Mega2560/dp/B00D9M4BQU/ )

<a href="#HOLTop"> -- Back to Top -- </a>

# Raspberry Pi GPIO Pinout
The Raspberry Pi 2 and 3 GPIO is the same, with the exception of the mechanism for accessing the on-board LEDs.

![](http://ms-iot.github.io/content/images/PinMappings/RP2_Pinout.png)

<a href="#HOLTop"> -- Back to Top -- </a>
