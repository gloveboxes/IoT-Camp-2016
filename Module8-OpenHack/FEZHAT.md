<a name="HOLTop" />
# Exploring the GHI FEZ HAT
![](http://www.ghielectronics.com/img/www/products/500-0_large.jpg)

The GHI FEZ HAT was developed by GHI Electronics specifically for the Azure IoT labs we run around the world, but they couldn't resist adding some extras :)

In addition to the sensors and LEDs used in the previous labs, the following on-board devices are available for your experimentation

  * Analog Input for analog sensors
  * PWM for dimming LEDs, controlling external devices
  * Red LED (and Multi-color LEDs as used in the other labs)
  * Temperatore, Light, and Accelerometer sensors
  * Two servo connections

The FEZ HAT driver code exposes all of those interfaces and devices in a C#/VB-friendly form. We've also provided other devices in the in-room kit which you may use with those interfaces.

More information about the FEZ HAT 

  * [GHI FEZ HAT Details](http://www.ghielectronics.com/catalog/product/500)
  * [GHI FEZ HAT Driver on NuGet](https://www.nuget.org/packages/GHIElectronics.UWP.Shields.FEZHAT/)
  * [GHI FEZ HAT Developers' Guide](https://www.ghielectronics.com/docs/329/fez-hat-developers-guide)

<a href="#HOLTop"> -- Back to Top -- </a>

## Using servos with the FEZ HAT

The micro servos in the room work on 4.5 to 6v. The FEZ HAT has two connectors for servos. The V+ power comes from the motor voltage connector on the 10 pin screw terminal. You may connect this to the positive connector on the battery pack (connect negative to ground). We do not recommend connecting directly to the +5v connection because these servos can draw almost an amp at full range, and the Pi power supplies are not designed for this use.


Warning: There is a possibility that the solder joint for the motor control terminal blocks could create a short by touching the HDMI connector on the Raspberry Pi. To prevent this, simply use a stand-off to support the FEZ HAT and to prevent any contact between the terminal block and the HDMI connector, please see image below.

In the Build lab, there is a standoff applied, but with sufficient pressure, you could still make contact between the terminal and HDMI connector. Please pay attention to this.

![](https://www.ghielectronics.com/downloads/marketing/hat_warning.jpg)

<a href="#HOLTop"> -- Back to Top -- </a>

# Important instructions for removing the FEZ HAT
If your exploration requires removing any HAT from the Pi, please carefully remove the HAT from the Raspberry Pi. Be careful to lift it straight off, so as not to bend the pins on the Raspberry Pi. Do not lift off the HAT in the way you might lift up a post-it note, as it will bend the pins and possibly break them. If you are unsure as to how to do this, please see one of the in-room proctors for assistance.

Set the FEZ HAT down at your workstation so that it is available for other lab participants.

<a href="#HOLTop"> -- Back to Top -- </a>