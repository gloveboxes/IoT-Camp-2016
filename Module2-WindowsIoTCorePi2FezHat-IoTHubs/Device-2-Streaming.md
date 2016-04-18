>[Home](README.md) </br>
>Previous Lab [Connecting and configuring your device](Device-1-Config.md)

# Streaming telemetry data to the Azure IoT hub ####


In order to get the information out of the hat sensors, you will take advantage of the Developers' Guide that GHI Electronics published.

Now that the device is configured, you'll see how to make an application read the values of the FEZ HAT sensors, and then send those values to an Azure IoT Hub.

This task uses an existing Universal application that will be deployed to your Raspberry Pi device and use FEZ HAT sensors.

1. Open in Visual Studio the **IoTWorkshop.sln** solution located at **Source\Ex2\Begin** folder.

1. In **Solution Explorer**, right-click the **IoTWorkshop** project, and then click **Manage NuGet Packages**.

1. In the **NuGet Package Manager** window, click **Browse** and search for **Microsoft Azure Devices** and **PCL Crypto**, click **Install** to install the **Microsoft.Azure.Devices.Client** and **PCLCrypto** packages, and accept the terms of use.

    This downloads, installs, and adds a reference to the [Microsoft Azure IoT Service](https://www.nuget.org/packages/Microsoft.Azure.Devices/) SDK NuGet package.

1. Add the following _using_ statements at the top of the **MainPage.xaml.cs** file:

	````C#
	using Microsoft.Azure.Devices.Client;
	````

1. Add the following field to the **MainPage** class, replace the placeholder value with the **device connection string** you've created in the previous task (note that the curly braces { } are _NOT_ part of the connection string and should be _removed_ when you paste in your connection string):

	````C#
	private DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("{device connection string}");
	````

1. Add the following method to the **MainPage** class to create and send messages to the IoT hub. Resolve the missing using statements.

	````C#
	public async void SendMessage(string message)
	{
		 // Send message to an IoT Hub using IoT Hub SDK
		 try
		 {
			  var content = new Message(Encoding.UTF8.GetBytes(message));
			  await deviceClient.SendEventAsync(content);

			  Debug.WriteLine("Message Sent: {0}", message, null);
		 }
		 catch (Exception e)
		 {
			  Debug.WriteLine("Exception when sending message:" + e.Message);
		 }
	}
	````

1. Add the following code to the **Timer_Tick** method to send a message with the temperature and another with the light level:

	````C#
	// send data to IoT Hub
	var jsonMessage = string.Format("{{ displayname:null, location:\"USA\", organization:\"Fabrikam\", guid: \"41c2e437-6c3d-48d0-8e12-81eab2aa5013\", timecreated: \"{0}\", measurename: \"Temperature\", unitofmeasure: \"C\", value:{1}}}",
		 DateTime.UtcNow.ToString("o"),
		 temp);

	this.SendMessage(jsonMessage);

	jsonMessage = string.Format("{{ displayname:null, location:\"USA\", organization:\"Fabrikam\", guid: \"41c2e437-6c3d-48d0-8e12-81eab2aa5013\", timecreated: \"{0}\", measurename: \"Light\", unitofmeasure: \"L\", value:{1}}}",
		 DateTime.UtcNow.ToString("o"),
		 light);

	this.SendMessage(jsonMessage);
	````

	1. In order to deploy your app to the IoT device, select the **ARM** architecture in the Solution Platforms dropdown.

		![ARM Solution Platform](Images/arm-platform.png?raw=true "ARM Solution Platform")

		_ARM Solution Platform_

	1. Next, click the **Device** dropdown and select **Remote Machine**.

		![Run in remote machine](Images/run-remote-machine.png?raw=true "Run in remote machine")

		_Run in remote machine_

	1. In  the **Remote Connections** dialog, click your device name within the **Auto Detected** list and then click **Select**. Not all devices can be auto detected, if you don't see it, enter the IP address using the **Manual Configuration**. After entering the device name/IP, select **Universal (Unencrypted Protocol)** Authentication Mode, then click **Select**.

		![Remote Connections dialog](Images/remote-connections-dialog.png?raw=true "Remote Connections dialog")

		_Remote Connections dialog_

1. Press **F5** to run and deploy the app to the device.

	The information being sent can be monitored using the Device Explorer application. Run the application and go to the **Data** tab and select the name of the device you want to monitor (_myRaspberryDevice_ in your case), then click  **Monitor**.

	![Monitoring messages sent](Images/monitoring-messages-sent.png?raw=true "Monitoring messages sent")

	_Monitoring messages sent_

    **Note**: If you navigate back to your IoT Hub blade in the Azure Portal, it may take a couple minutes before the message count is updated to reflect the device activity under **Usage**.

>[Home](README.md) </br>
>Next Lab [Analytics with Power Bi](AnalyticsWithPowerBi.md)