>[Home](README.md) </br>
>Previous Lab [Programming IO](Device-4-Programming-IO.md)

###Exercise 6: Advanced GPIO

In addition to the red LED and buttons in the FEZ HAT, there are also temperature and light sensors.

In this exercise, you'll refactor the UI to display the room temperature and light level values from the FEZ HAT sensors.


#### Task 1 - Getting light and temperature information ####

The FEZ HAT driver includes methods to get the temperature and light levels. In this task, you'll refactor the UI to display those values.

1. Open the **IoTWorkshop.sln** solution file from the **Begin** folder of this exercise or you can continue working with your solution from the previous exercise.

1. In the XAML code (**MainPage.xaml** file), replace the content of the **StackPanel** with the following markup.

	````XML
	<StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
		 <StackPanel Orientation="Horizontal">
			  <TextBlock Text="Light:"></TextBlock>
			  <ProgressBar x:Name="LightProgress" Value="0" Minimum="0" Maximum="1" Width="150"></ProgressBar>
			  <TextBlock x:Name="LightTextBox"></TextBlock>
		 </StackPanel>
		 <StackPanel Orientation="Horizontal">
			  <TextBlock Text="Temp:"></TextBlock>
			  <ProgressBar x:Name="TempProgress" Value="0" Minimum="0" Maximum="60" Width="150"></ProgressBar>
			  <TextBlock x:Name="TempTextBox"></TextBlock>
		 </StackPanel>
	</StackPanel>
	````

    This will include TextBlocks and StackPanels to show the temperature and light levels.

1. Add code in the **Timer_Tick** method to read the light level and temperature from the FEZ HAT sensors.

	<!-- mark:5-9 -->
	````C#
	private void Timer_Tick(object sender, object e)
	{
		 //...

		 // Light Sensor
		 var light = this.hat.GetLightLevel();

		 // Temperature Sensor
		 var temp = this.hat.GetTemperature();
	}
	````

1. Display the sensor values in the UI controls.

	<!-- mark:5-11 -->
	````C#
	private void Timer_Tick(object sender, object e)
	{
	    //...

	    // Display values
	    this.LightTextBox.Text = light.ToString("P2");
	    this.LightProgress.Value = light;
	    this.TempTextBox.Text = temp.ToString("N2");
	    this.TempProgress.Value = temp;

	    System.Diagnostics.Debug.WriteLine("Temperature: {0} °C, Light {1}", temp.ToString("N2"), light.ToString("N2"));
	}
	````

1. Hit **F5** to re-deploy the application to the device. 

1. Restore the "Windows IoT Remote Client" and you should see the values shown in the display. You can see how the light percentage varies by approaching your hand to the light sensor.

	> **Note:** You should set up the device deployment if you didn't continue working with the previous solution.

	![Running application](Images/ex4task1-running-application.png?raw=true)

	_Running application_


#### Task 2 - Using the RGB LED  ####

In this task, you'll use the 2 RGB LEDs in the FEZ HAT to output light with different intensities, according to the temperature and light in the room.


1. In the **Timer_Tick** method, add the following code to calculate a value from 0 to 255 depending on the light and temperature intensities.

	````C#
	var lightIntensity = (byte)(light * 255);
	var tempIntensity = (byte)(temp * 255 / 60);
	````
    We need a 0-255 range value for one of the RGB components of color.

1. Set the color of the RGB values using the light and temperature intensities. Use the Red component for the temperature, and Blue for light level.

	````C#
	this.hat.D2.Color = new FEZHAT.Color(tempIntensity, 0, 0);
	this.hat.D3.Color = new FEZHAT.Color(0, 0, lightIntensity);
	````

    With this code we are lighting the first RGB LED in red color for the temperature. The more it shines, the more heat the sensor is reading. For the second RGB LED, we use the blue color to output the amount of light.

1. Re-deploy the application to the device. Try changing the temperature and light levels by approaching your hand to the device and notice the RGB LEDs variations.

	![Running application](Images/ex4task2-raspberry-light-temp.png?raw=true "Running application")

	_Running application_

>[Home](README.md) </br>