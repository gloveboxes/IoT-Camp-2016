#Introduction to Windows 10 IoT Core


Estimated time to complete this module: **60 minutes**

##Overview

_Did you know Windows 10 runs on the Raspberry Pi?_ In this module, you'll get hands-on experience building a UWP app which runs on tiny devices. Learn how to build, deploy, debug your app, and how to configure the device.

**Windows 10 IoT Core** brings the power of Windows to your device and makes it easy to integrate richer experiences with your devices such as natural user interfaces, searching, online storage and cloud based services

**Windows 10 IoT Core** is a version of **Windows 10** that is optimized for smaller devices with or without a display, and that runs on the **Raspberry Pi, Arrow DragonBoard & MinnowBoard MAX**. **Windows 10 IoT Core** utilizes the rich, extensible [Universal Windows Platform (UWP)](https://msdn.microsoft.com/library/windows/apps/dn726767.aspx) API for building great solutions.


###Objectives
In this module, you'll see how to:

- Learned how to connect and configure a **Raspberry Pi** device with **Windows 10 IoT Core**
- Created a **Windows Universal Platform** app and deployed to a remote Windows IoT device
- Toggled an on-board red LED using standard Windows.Devices.Gpio APIs
- Programmed the input (buttons) and output (LEDs) of a GHI's FEZ HAT topping connected to the Raspberry Pi
- Read the temperature and light sensors and set the RGB LEDs colors from the FEZ HAT


###Prerequisites

The following is required to complete this module:

- Windows 10 with [developer mode enabled][1]
- [Visual Studio Community 2015][2] with [Update 1][3] or greater
- [Windows IoT Core Project Templates][4]
- [Raspberry PI board with Windows IoT Core image][5]
- [GHI FEZ HAT][6] (for exercises 3 and 4)
- [Windows 10 IoT Core Dashboard][7]
- [Windows IoT Remote Client][8] see [Remote Display Experience](https://developer.microsoft.com/en-us/windows/iot/win10/remotedisplay)



**Note:** The source code for this lab is available on [GitHub](/Module1-IntroWindows10IoTCore/Source).

[1]: https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx
[2]: https://www.visualstudio.com/products/visual-studio-community-vs
[3]: http://go.microsoft.com/fwlink/?LinkID=691134
[4]: https://visualstudiogallery.msdn.microsoft.com/55b357e1-a533-43ad-82a5-a88ac4b01dec
[5]: https://ms-iot.github.io/content/en-US/win10/RPI.htm
[6]: https://www.ghielectronics.com/catalog/product/500
[7]: https://developer.microsoft.com/en-us/windows/iot/getstarted
[8]: https://www.microsoft.com/store/apps/9nblggh5mnxz



## Exercises ##
This module includes the following exercises:

1. [Connecting and configuring your device](Device-1-Config.md)
1. [Creating and deploying your first "Hello World" app](Device-2-HelloWorld.md)
1. [Introduction to GPIO aka General Purpose IO](Device-3-GPIO.md)
1. [Programming IO](Device-4-Programming-IO.md)
1. [Advanced GPIO](Device-5-Advanced-GPIO.md)








