Windows 10 IoT Core Hands-on Lab
========================================

##Objective
In this lab, you will learned how to create a Universal app that reads from the sensors of a FEZ hat connected to a Raspberry Pi 2 running Windows 10 IoT Core, and stream these readings to an Azure IoT Hub. 
You also learned how to read and consume the information in the IoT Hub using Power BI to get near real-time analysis of the information gathered from the FEZ hat sensors. 
You will ge get some first hand experience with IoT Hub Command and Control.

###Exercises 

<a name="Prerequisites"></a>
###Prerequisites

The following is required to complete this module:


- Windows 10 with [developer mode enabled][1]
- [Visual Studio Community 2015][2] with [Update 1][3] or greater
- [Windows IoT Core Project Templates][4]
- [Raspberry PI board with Windows IoT Core image][5]
- [GHI FEZ HAT][6]
- [Windows 10 IoT Core Dashboard][7]
- [Windows IoT Remote Client][8] see [Remote Display Experience](https://developer.microsoft.com/en-us/windows/iot/win10/remotedisplay)
- [IoT Hub Device Explorer][9] (Scroll down for SetupDeviceExplorer.msi)
- [An Azure Account][10]



**Note:** The source code for this lab is available on [GitHub](/Module1-IntroWindows10IoTCore/Source).

[1]: https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx
[2]: https://www.visualstudio.com/products/visual-studio-community-vs
[3]: http://go.microsoft.com/fwlink/?LinkID=691134
[4]: https://visualstudiogallery.msdn.microsoft.com/55b357e1-a533-43ad-82a5-a88ac4b01dec
[5]: https://ms-iot.github.io/content/en-US/win10/RPI.htm
[6]: https://www.ghielectronics.com/catalog/product/500
[7]: https://developer.microsoft.com/en-us/windows/iot/getstarted
[8]: https://www.microsoft.com/store/apps/9nblggh5mnxz
[9]: https://github.com/Azure/azure-iot-sdks/releases
[10]: SettingUpAzure.md

Estimated time to complete Module: **60 minutes**

##Exercises
This module includes the following exercises:

1. [Provisioning an Azure Account](AzureProvisioning.md) 
1. [Provisioning an Azure IoT Hub](AzureIoTHub.md)
1. [Connecting and configuring your device](Device-1-Config.md)
1. [Streaming telemetry from a device](Device-2-Streaming.md)
1. [Analytics with Power Bi](AnalyticsWithPowerBi.md)
1. [Controlling a device from IoT Hub](Device-3-CommandControl.md)















