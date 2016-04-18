>[Home](README.md) </br>
>Previous Lab [Provisioning an IoT Hub](AzureIoTHub.md)

# Connecting and configuring your device

The Raspberry Pi will be connected to the development PC through a wired Ethernet connection. 
This connection is used both for deployment and debugging as well as passing through internet requests from the Raspberry Pi when [Internet Connection Sharing](http://ms-iot.github.io/content/en-US/win10/ConnectToDevice.htm) is enabled on the PC.


### Task 1 - Identifying the device using IoT Core Dashboard ####

In this task, you'll connect to your device and explore the web management interface.


1. Launch the **Windows 10 IoT Core Dashboard**, go to **My devices** and click the **Open in Device Portal** icon of your device name. 

	![Windows 10 IoT Core Dashboard](Images/ex1task1-watcher.png?raw=true "Windows 10 IoT Core Dashboard")

	_Windows 10 IoT Core Dashboard_

> **Note:** You can also launch the _Device Portal_ by browsing the _IP address_ and adding **:8080**.


1. In the credentials dialog, use the default username and password. Username: _Administrator_ Password: _p@ssw0rd_

	![Device Portal credentials](Images/ex1task1-device-portal-credentials.png?raw=true "Device Portal credentials")

	_Device Portal credentials_

1. **Windows Device Portal** should launch and display the web management home screen!

	![Windows Device Portal](Images/ex1task1-device-portal.png?raw=true "Windows Device Portal")

	_Windows Device Portal_

1. For now, just explore and don't change any settings.


### Task 2 - Verify the Windows IoT Remote Client connection  ####

In this task, you will verify the "Windows IoT Core Remote Client" connection.

To start press the Windows key and type “Windows IoT Core Remote Client” and run the app.

It is  likley that you will need to enter the IP address of your Raspberry Pi. Get the address of the device from the **Windows 10 IoT Core Dashboard**.

This will take a moment to connect. When it does you will see the video output of the Raspberry Pi remoted to your desktop.

Minimise the remote client application when you have verified that it is working.

![Windows IoT Remote Client](Images/windows-iot-remote-client.png?raw=true "Windows IoT Remote Client")

>[Home](README.md) </br>
>Next Lab [Streaming telemetry from a device](Device-2-Streaming.md)