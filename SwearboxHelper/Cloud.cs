using System;
using Google.Cloud.Speech.V1;
using System.Collections;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace SwearboxHelper
{
    public class Cloud
    {
        private SpeechClient client;
        public int forbidden = 0;

        public Cloud()
        {
            Task.Run(async () => client = await SpeechClient.CreateAsync());
        }

        

        public async void  StreamingMicRecognizeAsync(int seconds, Action<string> callback)
        {
            if (NAudio.Wave.WaveIn.DeviceCount < 1)
            {
                Console.WriteLine("No microphone!");
                return;
            }
            var context = new SpeechContext() { Phrases = { File.ReadLines(Utility.ForbiddenWords) } };
            var speech = SpeechClient.Create();
            var streamingCall = speech.StreamingRecognize();
            // Write the initial request with the config.
            await streamingCall.WriteAsync(
                new StreamingRecognizeRequest()
                {
                    StreamingConfig = new StreamingRecognitionConfig()
                    {
                        Config = new RecognitionConfig()
                        {
                            Encoding =
                            RecognitionConfig.Types.AudioEncoding.Linear16,
                            SampleRateHertz = 16000,
                            LanguageCode = "en",
                            SpeechContexts = { context },
                        },
                        SingleUtterance = false
                    }
                });
            // Print responses as they arrive.
            Task printResponses = Task.Run(async () =>
            {
                string build = "";
                while (await streamingCall.ResponseStream.MoveNext(
                    default(CancellationToken)))
                {
                    foreach (var result in streamingCall.ResponseStream
                        .Current.Results)
                    {
                        foreach (var alternative in result.Alternatives)
                        {
                            forbidden = Utility.CheckForbiddenWords(alternative.Transcript);
                            
                              
                            build += alternative.Transcript;
                            build += Environment.NewLine;
                            Console.WriteLine(alternative.Transcript);
                            
                        }
                    }
                    callback(build);
                }
            });
            // Read from the microphone and stream to API.
            object writeLock = new object();
            bool writeMore = true;
            var waveIn = new NAudio.Wave.WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
            waveIn.DataAvailable +=
                (object sender, NAudio.Wave.WaveInEventArgs args) =>
                {
                    lock (writeLock)
                    {
                        if (!writeMore) return;
                        streamingCall.WriteAsync(
                            new StreamingRecognizeRequest()
                            {
                                AudioContent = Google.Protobuf.ByteString
                                    .CopyFrom(args.Buffer, 0, args.BytesRecorded)
                            }).Wait();
                    }
                };
            waveIn.StartRecording();
            Console.WriteLine("Speak now.");
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            // Stop recording and shut down.
            waveIn.StopRecording();
            lock (writeLock) writeMore = false;
            await streamingCall.WriteCompleteAsync();
            await printResponses;
        }
    }

}
