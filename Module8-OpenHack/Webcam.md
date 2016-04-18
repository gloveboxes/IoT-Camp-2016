<a name="HOLTop" />
# The USB Webcam
![](http://hackster.imgix.net/uploads/cover_image/file/66861/SecurityCamera2.JPG)

In the room, we have a number of Microsoft Lifecam 3000 USB webcams. These are automatically detected by and compatible with Windows 10 IoT Core and Windows 10 in general.

## Security camera
In addition to the webcams in the kit, we've also included a number of PIR motion sensors. A great project to work on is a motion-triggered home security camera. You can find a great example of that at Hackster.io.

Additional information

  * [Motion Sensor in the room](http://www.amazon.com/Qunqi-HC-SR501-Pyroelectric-Infrared-Microcontrollers/dp/B0140WFNYQ/)
  * [Home Security Camera project and code](https://microsoft.hackster.io/en-US/windows-iot/security-camera-579b7f)
  * [Windows 10 basic camera example](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraStarterKit)
  * [Windows 10 face detection example](http://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFaceDetection)

<a href="#HOLTop"> -- Back to Top -- </a>

## Using Project Oxford face recognition
Project Oxford's public-facing API is based on REST calls. However, there are friendly API projections for a number of languages including C#.

![](http://www.projectoxford.ai/images/bright/face/FaceDetection.png) ![](http://www.projectoxford.ai/images/bright/face/FaceIdentification.png)

Tips:
Before you can use Oxford for face recognition, you need to train it under your key (the key you acquired when signing up). You'll first create a person group, then add people to the group, and then upload photos of those people. this is best done interactively in an application on the PC. We suggest using the PC webcam to take various photos of you (or the target person) with and without glasses if you wear them, at slightly different angles, etc. Submit each one individually. You'll likely want to do the training from a regular interactive desktop app just to make it easy. Here's an example of three people I added.

	````C#
    // This is called only one time
    await _faceServiceClient.CreatePersonGroupAsync(personGroupId, "Targets");

    // You call these only one time
    CreatePersonResult pete = await _faceServiceClient.CreatePersonAsync(PersonGroupId, "Pete");
    CreatePersonResult tony = await _faceServiceClient.CreatePersonAsync(PersonGroupId, "Tony");
    CreatePersonResult mat = await _faceServiceClient.CreatePersonAsync(PersonGroupId, "Mat");

    // You need to keep track of these IDs for training
    System.Diagnostics.Debug.WriteLine("Pete Id: " + pete.PersonId);
    System.Diagnostics.Debug.WriteLine("Tony Id: " + tony.PersonId);
    System.Diagnostics.Debug.WriteLine("Mat Id: " + mat.PersonId);
	````

<a href="#HOLTop"> -- Back to Top -- </a>

Keep in mind that Project Oxford free accounts are rate-limited, so you'll need to ensure you don't send too many calls in too short of a time. Additionally, you cannot train an additional face for the same person until the previous face has completed. If you use an interactive app to upload photos, you typically don't run into this limitation. However, if you script uploading many previously taken photos, you will get an exception if Oxford is still training for that face.

Here's the code to upload a file for training. The code to get the image from a camera is very similar once you have a stream of data. Note the .AsStreamForRead() call. This extension method is key for converting between UWP and .NET streams.

	````C#
    // SubscriptionKey is your own personal key
    private readonly IFaceServiceClient _faceServiceClient = new FaceServiceClient(SubscriptionKey);

    // Code to add a single face photo to an existing person
    private async void AddPersonFace(Guid personId, string fileName)
    {
        // image files are in the appx in this case, in the /Training folder 
        var uri = new Uri("ms-appx:///Training/" + fileName);

        // open the file
        StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
        
        using (IInputStream s = (IInputStream)await file.OpenReadAsync())
        {
            // AsStreamForRead is the key for translating here
            using (var stream = s.AsStreamForRead())
            {
                var result = await _faceServiceClient.AddPersonFaceAsync(PersonGroupId, personId, stream);

                if (result != null)
                {
                    System.Diagnostics.Debug.WriteLine("Face '{0}' added. PersistedFaceId: {1}", 
                                                                fileName, result.PersistedFaceId);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed to add face '{0}'", fileName);
                }
            }
        }
    }
	````

<a href="#HOLTop"> -- Back to Top -- </a>

Face recognition happens by sending the API an image stream, letting Project Oxford process it, and then return back a list of faces along with recognition confidence levels.

	````C#
    // "Enemy" because I was using this code to launch foam missiles when the face is recognized
    private Guid EnemyId = Guid.Parse(MatId.ToString());

    // Submit the image stream for evaluation. This image stream comes from a still frame capture from
    // the webcam on the Raspberry Pi
    public async Task<bool> RecognizeFacesAsync(Stream imageToEvaluate)
    {
        // Upload image and get back a face ID (or multiple)
        var faces = await _faceServiceClient.DetectAsync(imageToEvaluate);

        // get all the ids from the faces detected in the photo
        var faceIds = faces.Select(face => face.FaceId).ToArray();

        if (faceIds.Length != 0)
        {
            var identifyResults = 
                await _faceServiceClient.IdentifyAsync(PersonGroupId, faceIds);

             foreach (var result in identifyResults)
             {
                 foreach (Candidate candidate in result.Candidates)
                 {
                     System.Diagnostics.Debug.WriteLine("Recognized: confidence:{0}, id:{1}", 
                                             candidate.Confidence, candidate.PersonId);
                        
                   // if you find the enemy, return true
                   if (candidate.PersonId == EnemyId && candidate.Confidence > 0.5)
                   {
                       return true;
                   }
                }
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("No faces detected.");
        }

        return false;
    }
	````

More information

  * [Microsoft Project Oxford Site](https://www.projectoxford.ai/)
  * [Microsoft Project Oxford SDKs](https://www.projectoxford.ai/sdk)

<a href="#HOLTop"> -- Back to Top -- </a>