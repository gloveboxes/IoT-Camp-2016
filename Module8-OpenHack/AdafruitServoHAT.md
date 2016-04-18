<a name="HOLTop" />
# Adafruit Servo HAT
![](http://www.adafruit.com/images/1200x900/2327-13.jpg)

Note that we also have a dedicated servo HAT in the kit. It takes a high amp 5v power supply (provided in-room), but can run a number of servos. You'll find the code extremely simple to write.

In the room, we also have high amperage 5v power supplies for use with the HAT. You'll need to use one of those with the HAT to power the servos. This is separate from the 5v power supply which powers the Raspberry Pi.

Example code

	````C#
    //The servoMin/servoMax values are dependant on the hardware you are using.
    //The values below are for my SR-4303R continuous rotating servos.
    //If you are working with a non-continous rotatng server, it will have an explicit
    //minimum and maximum range; crossing that range can cause the servo to attempt to
    //spin beyond its capability, possibly damaging the gears.

    const int servoMin = 300;  // Min pulse length out of 4095
    const int servoMax = 480;      // Max pulse length out of 4095
    
    using (var hat = new Adafruit.Pwm.PwmController())
    {
        DateTime timeout = DateTime.Now.AddSeconds(10);
        hat.SetDesiredFrequency(60);
        while (timeout >= DateTime.Now)
        {
            hat.SetPulseParameters(0, servoMin, false);
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();

            hat.SetPulseParameters(0, servoMax, false);
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        }
    }
	````

Servo HAT Product Information and code

  * [Adafruit Servo HAT Details](https://www.adafruit.com/products/2327)
  * [Adafruit Servo HAT Code](https://github.com/golaat/Adafruit.Pwm)
  * [Adafruit Servo HAT Overview](https://learn.adafruit.com/adafruit-16-channel-pwm-servo-hat-for-raspberry-pi/overview)

# Important instructions for removing any HAT
If your exploration requires removing any HAT from the Pi, please carefully remove the HAT from the Raspberry Pi. Be careful to lift it straight off, with a gentle rocking motion, so as not to bend the pins on the Raspberry Pi. Do not lift off the HAT in the way you might lift up a post-it note, as it will bend the pins and possibly break them. If you are unsure as to how to do this, please see one of the in-room proctors for assistance.

Set the FEZ HAT down at your workstation so that it is available for other lab participants.

<a href="#HOLTop"> -- Back to Top -- </a>