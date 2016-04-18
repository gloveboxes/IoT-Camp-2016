>[Home](README.md) </br>
>Previous Lab [Connecting and configuring your device](Device-1-Config.md)


### Exercise 2: Create and deploy "Hello World" UWP ###

A **Universal Windows Platform (UWP)** app has the potential to run on any Windows-powered device like a Raspberry Pi device with Windows 10 IoT core.

**Windows IoT Core** can be configured for either **headed** or **headless** mode. The difference between these two modes is the presence or absence of any form of UI. By default, Windows 10 IoT Core is in headed mode and runs the default startup app which displays system information like the computer name, connected devices, and IP addresses.

In this exercise, you'll create and deploy a UWP app for headed mode using a XAML view to display a TextBlock and a button that updates the TextBlock content.


#### Task 1 - Creating a Hello World UWP app ####

In this task, you'll use the Universal project template to create a Blank App. Then you'll add some UI elements and verify the app by debugging the application on your local PC.

1. Start **Visual Studio 2015** and create a new project **File > New Project...**

1. In the installed templates tree, navigate to **Visual C# > Windows > Universal** and select the template **Blank App (Windows Universal)**. Enter the name "IoTWorkshop".

	![New Universal Blank App](Images/ex2task1-new-blank-app.png?raw=true "New Universal Blank App")

	_New Universal Blank App_

1. Click **OK** to create the project.

1. Add a reference to the **Windows IoT extension SDK**. Right-click the **References** entry under the project, select **Add Reference** then navigate the resulting dialog to **Universal Windows > Extensions > Windows IoT Extensions for the UWP**, check the box, and click **OK**.

	![Add Windows IoT extension SDK](Images/ex2task1-IoT-SDK-ref.png?raw=true "Add Windows IoT extension SDK")

	_Add Windows IoT extension SDK_

1. From Solution Explorer, select the **MainPage.xaml** file.

1. Locate the **\<Grid\>** tag within the **XAML** section of the designer, and add the following markup inside the grid. This will add an input and a button that you'll see in the design surface.

	````XML
	<StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
		<TextBox x:Name="Message" Text="Hello, World!" Margin="10" IsReadOnly="True"/>
		<Button x:Name="ClickMe" Content="Click Me!"  Margin="10" HorizontalAlignment="Center"/>
	</StackPanel>
	````

1. Double click the **Click Me!** button in the design surface to generate the click method in the _MainPage.xaml.cs_ file.

1. Add the following line to the **ClickMe_Click** method:

	````C#
	private void ClickMe_Click(object sender, RoutedEventArgs e)
	{
	    this.Message.Text = "Hello, Windows IoT Core!";
	}
	````

1. Press **F5** to build and run the app locally (since this is a Universal Windows Platform (UWP) application, you can test the app on your Visual Studio machine as well).

	![Hello World app running locally](Images/ex2task1-hello-world-local.png?raw=true "Hello World app running locally")

	_Hello World app running locally_

1. Click the **Click Me!** button to verify that the input message changes and close the app after you're done verifying it.


#### Task 2 - Deploying your app to the device ####

Now that you validated the app in your local PC, you can deploy it to the Raspberry Pi device by using remote debugging.

In order to use remote debugging, your IoT Core device must first be connected to same local network as your development PC.

1. In Visual Studio, continue with the same solution that you created in the previous task and select the **ARM** architecture in the toolbar dropdown.

	![ARM Solution Platform](Images/ex2task2-arm.png?raw=true "ARM Solution Platform")

	_ARM Solution Platform_

1. Next, click the **Device** dropdown and select **Remote Machine**.

	![Run in remote machine](Images/ex2task2-run-remote-machine.png?raw=true "Run in remote machine")

	_Run in remote machine_

1. In  the **Remote Connections** dialog, click your device name within the **Auto Detected** list and then click **Select**. Not all devices can be auto detected, if you don't see it, enter the IP address using the **Manual Configuration**. After entering the device name/IP, select **Universal (Unencrypted Protocol)** Authentication Mode, then click **Select**.

	![Remote Connections dialog](Images/ex2task2-remote-connections-dialog.png?raw=true "Remote Connections dialog")

	_Run in Remote Machine_

    You can verify or modify these values by navigating to the project properties (select Properties in the Solution Explorer) and choosing the **Debug** tab on the left.

1. Build and deploy your app to your device by selecting **Build > Rebuild Solution and Build > Deploy Solution**.

    If there are any missing packages that you did not install during setup, Visual Studio may prompt you to acquire those now.

1. Hit **F5** to deploy and run the app. You can insert breakpoints and debug in the remote device.

1. Restore the "Windows IoT RemoTe Client" app and you should see the output of your app running on the Raspberry Pi.

>[Home](README.md) </br>
>Next Lab [Introduction to GPIO aka General Purpose IO](Device-3-GPIO.md)