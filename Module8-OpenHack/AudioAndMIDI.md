<a name="HOLTop" />
# MIDI and music/audio in Windows
![](Images/midi_keyboard.jpg)

Included in the equipment is a single MIDI controller keyboard. This provides an easy way to have a large number of inputs. You can use the built-in Windows 10 MIDI API to interface between the keyboard and Windows 10 on the PC or the Raspberry Pi.

Additionally, we have an IoT Core-compatible USB audio interface and a headset with mic. You can use the AudioGraph APIs to generate real-time audio, play back samples, record from the microphone, and much more.

As fun as it is, try not to create projects which are disruptive to others in the room. This includes anything which makes loud noises or super bright disco-light effects. :) If you want audio in your project, we've included a USB audio interface with separate mic/headphones connections, and a compatible headset.

Example MIDI message transmission

	````C#
    var message = new MidiControlChangeMessage(Channel, Controller, Value);

    _outputPort.SendMessage(message);
	````

Example of handling an incoming MIDI message

	````C#
    private void OnMidiMessageReceived(MidiInPort sender, MidiMessageReceivedEventArgs args)
    {
        switch (args.Message.Type)
        {
            case MidiMessageType.NoteOn:
                var noteOnMessage = args.Message as MidiNoteOnMessage;

                // a zero-velocity note-on message is equivalent to note-off
                if (noteOnMessage.Velocity == 0 && TranslateZeroVelocityNoteOnMessage)
                {
                    ...
                }
                else
                {
                       ...
                }
                break;

            case MidiMessageType.NoteOff:
                var noteOffMessage = args.Message as MidiNoteOffMessage;
                 ...
                break;

            case MidiMessageType.ControlChange:
                var ccMessage = args.Message as MidiControlChangeMessage;
                 ...
                break;

            case MidiMessageType.ProgramChange:
                var programMessage = args.Message as MidiProgramChangeMessage;
                 ...
                break;

            case MidiMessageType.PitchBendChange:
                var pitchBendChangeMessage = args.Message as MidiPitchBendChangeMessage;
                 ...
                break;

            ...

            default:
                // message type we don't handle above. Ignore
                break;
        }
    }
	````

<a href="#HOLTop"> -- Back to Top -- </a>

Example audio sample file playback

	````C#
    public sealed partial class MainPage : Page
    {
        const int numSamples = 9;

        enum Samples
        {
            BassDrum,
            ClosedHighHat,
            CrashCymbal,
            FloorTom,
            HighTom,
            MidTom,
            Snare,
            OpenHighHat,
            Steve
        }

        AudioGraph _graph;
        AudioFileInputNode[] _fileNodes = new AudioFileInputNode[numSamples];
        AudioDeviceOutputNode _deviceOutput;

        // create the audio graph and output
        private async void InitAudioGraph()
        {
            var settings = new AudioGraphSettings(AudioRenderCategory.Media);
            settings.QuantumSizeSelectionMode = QuantumSizeSelectionMode.LowestLatency; // pick lowest latency available to devices in the graph


            // create the audio graph
            _graph = (await AudioGraph.CreateAsync(settings)).Graph;
            if (_graph == null)
            {
                // failed to create audio graph
                MessageDialog dlg = new MessageDialog("Failed to create audio graph");
                await dlg.ShowAsync();
                return;
            }


            // create the output. You could also create file output here to stream to a temp file or similar
            _deviceOutput = (await _graph.CreateDeviceOutputNodeAsync()).DeviceOutputNode;
            if (_deviceOutput == null)
            {
                // failed to create audio output
                MessageDialog dlg = new MessageDialog("Failed to create device output");
                await dlg.ShowAsync();
                return;
            }


            // load all of the samples into graph nodes
            BuildFileNodes();

            // start playback
            _graph.Start();
        }


        // play a file node (a single sample)
        private void PlayFile(Samples sample)
        {
            // closed high hat should always stop an open high hat
            // so we cut off the open high hat if we're playing the closed one
            // Classic drum machines do this by putting both on the same channel

            if (sample == Samples.ClosedHighHat)
                _fileNodes[(int)Samples.OpenHighHat].Stop();

            // Make sure to reset and start up the node
            _fileNodes[(int)sample].Reset();
            _fileNodes[(int)sample].Start();
        }


        // load the samples into file nodes. This is very easy with AudioGraph compared to what
        // we had to do with WASAPI and similar.
        private async void BuildFileNodes()
        {
            // Init all the file input nodes
            Uri[] uris = new Uri[numSamples];

            uris[(int)Samples.Steve] = new Uri("ms-appx:///Assets/guggs_switch_gears.wav", UriKind.Absolute);
            uris[(int)Samples.BassDrum] = new Uri("ms-appx:///Assets/Drum-Bass.wav", UriKind.Absolute);
            uris[(int)Samples.ClosedHighHat] = new Uri("ms-appx:///Assets/Drum-Closed-Hi-Hat.wav", UriKind.Absolute);
            uris[(int)Samples.CrashCymbal] = new Uri("ms-appx:///Assets/Drum-Crash-Cymbal.wav", UriKind.Absolute);
            uris[(int)Samples.FloorTom] = new Uri("ms-appx:///Assets/Drum-Floor-Tom.wav", UriKind.Absolute);
            uris[(int)Samples.HighTom] = new Uri("ms-appx:///Assets/Drum-High-Tom.wav", UriKind.Absolute);
            uris[(int)Samples.MidTom] = new Uri("ms-appx:///Assets/Drum-Mid-Tom.wav", UriKind.Absolute);
            uris[(int)Samples.Snare] = new Uri("ms-appx:///Assets/Drum-Snare.wav", UriKind.Absolute);
            uris[(int)Samples.OpenHighHat] = new Uri("ms-appx:///Assets/Drum-Open-Hi-Hat.wav", UriKind.Absolute);

            // create a node for each uri
            for (int i = 0; i < numSamples; i++)
            {
                StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(uris[i]);
                var res = await _graph.CreateFileInputNodeAsync(f);

                if (res.Status != AudioFileNodeCreationStatus.Success)
                {
                    // Give up on this one if the node didn't init properly
                    continue;
                }

                  _fileNodes[i] = res.FileInputNode;
                  _fileNodes[i].FileCompleted += MainPage_FileCompleted;
                
                // By default, nodes are started. We want to have the nodes start off in the stop state.
                _fileNodes[i].Stop();
                _fileNodes[i].AddOutgoingConnection(_deviceOutput);

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            InitAudioGraph();
        }

        private void MainPage_FileCompleted(AudioFileInputNode sender, object args)
        {
            sender.Stop();
        }

        private void BassDrum_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PlayFile(Samples.BassDrum);
        }

        ...
    }
	````

<a href="#HOLTop"> -- Back to Top -- </a>

Audio Examples and code
  * [Build 2015 Drummer (sample playback)](https://github.com/Psychlist1972/Build-2015-Drummer)
  * [Official Audio Creation / AudioGraph Samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AudioCreation)
  * [Media Contrib Project](https://github.com/aarononeal/media-contrib)

MIDI Examples and code
  * [Windows 10 Launchpad MIDI (shows device monitoring and general IO)](https://github.com/Psychlist1972/Windows-10-Launchpad-MIDI)
  * [Windows 10 PowerShell MIDI](https://github.com/Psychlist1972/Windows-10-PowerShell-MIDI)
  * [Official Windows 10 MIDI Samples](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MIDI)

<a href="#HOLTop"> -- Back to Top -- </a>