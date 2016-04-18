>[Home](README.md)

### Exercise 1: Connecting and configuring your device ###

The Raspberry Pi will be connected to the development PC through a wired Ethernet connection. 
This connection is used both for deployment and debugging as well as passing through internet requests from the Raspberry Pi when [Internet Connection Sharing](http://ms-iot.github.io/content/en-US/win10/ConnectToDevice.htm) is enabled on the PC.


#### Task 1 - Identifying the device using IoT Core Dashboard ####

In this task, you'll connect to your device and explore the web management interface.


1. Launch the **Windows 10 IoT Core Dashboard**, go to **My devices** and click the **Open in Device Portal** icon of your device name. 

	![Windows 10 IoT Core Dashboard](Images/ex1task1-watcher.png?raw=true "Windows 10 IoT Core Dashboard")

	_Windows 10 IoT Core Dashboard_

If your device does not show up in the list it is almost certainly because the network connection between your PC and the Raspberry Pi is public and Device Discovery is not enabled. 
See [How to change Windows 10 network location from Public to Private](https://tinkertry.com/how-to-change-windows-10-network-type-from-public-to-private).

**Note:** Alternatively, open a web browser and browse to the default device url **[http://minwinpc:8080](http://minwinpc:8080)**.


1. In the credentials dialog, use the default username and password. Username: _Administrator_ Password: _p@ssw0rd_

	![Device Portal credentials](Images/ex1task1-device-portal-credentials.png?raw=true "Device Portal credentials")

	_Device Portal credentials_

1. **Windows Device Portal** should launch and display the web management home screen!

	![Windows Device Portal](Images/ex1task1-device-portal.png?raw=true "Windows Device Portal")

	_Windows Device Portal_

1. From the **Home** Tab verify the Time Zone, date and time are correct. If you change the Time zone you will need to reboot.

1. From the **Remote** tab verify that **Windows IoT Remote Server** enabled. If it is not, then enable it.


#### Task 2 - Verify the Windows IoT Remote Client connection  ####

In this task, you will verify the "Windows IoT Core Remote Client" connection.

To start press the Windows key and type “Windows IoT Core Remote Client” and run the app.

It is  likley that you will need to enter the IP address of your Raspberry Pi. Get the address of the device from the **Windows 10 IoT Core Dashboard**.

This will take a moment to connect. When it does you will see the video output of the Raspberry Pi remoted to your desktop.

Minimise the remote client application when you have verified that it is working.

![Windows IoT Remote Client](Images/windows-iot-remote-client.png?raw=true "Windows IoT Remote Client")

See [Remote Display Experience](https://developer.microsoft.com/en-us/windows/iot/win10/remotedisplay) for more information and Troubleshooting.

>[Home](README.md) </br>
>[Next Lab -> Creating and deploying your first "Hello World" app](Device-2-HelloWorld.md)