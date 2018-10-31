using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.IO.Ports;
using System.Speech.Recognition;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace arduinoswearbox
{

    public partial class Form1 : Form
    {
        // Speech Recognition setup
        SpeechRecognitionEngine speechRE = null;
        List<Word> words = new List<Word>();

        // Arduino/COM setup
        SerialPort port;
        String[] ports;
        bool isConnected = false;
        public Form1()
        {
            InitializeComponent();

            // Speech
            speechRE = new SpeechRecognitionEngine();
            speechRE.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
            speechRE.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(engine_SpeechRejected);
            loadGrammarAndCommands();
            speechRE.SetInputToDefaultAudioDevice();
            speechRE.RecognizeAsync(RecognizeMode.Multiple);

            // Arduino/COM 
            getAvailableComPorts();
            foreach (string port in ports)
            {
                arduinoComPortCombo.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    arduinoComPortCombo.SelectedItem = ports[0];
                }
            }
        }

        private void engine_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            sendToArduino("#FINE\n");
            speechRecognitionOutputTextbox.AppendText("#FINE");
            speechRecognitionOutputTextbox.AppendText(Environment.NewLine);

        }

        private void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            speechRecognitionOutputTextbox.AppendText(getKnownTextOrExecute(e.Result.Text));
            speechRecognitionOutputTextbox.AppendText(Environment.NewLine);

        }

        private string getKnownTextOrExecute(string text)
        {
            try
            {
                var cmd = words.Where(c => c.Text == text).First();

                if (cmd.IsShellCommand)
                {
                    Process proc = new Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = cmd.AttachedText;
                    proc.Start();
                    return "you just started: " + cmd.AttachedText;
                }
                else
                {
                    sendToArduino(cmd.AttachedText);
                    return cmd.AttachedText;
                }
            }
            catch (Exception)
            {
                return text;
            }
        }

        private void sendToArduino(String cmd)
        {
            if (isConnected)
            {
                port.Write(cmd + "\n");
            }
        }

        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\ForbiddenWords.txt");
                foreach (string line in lines)
                {
                    // skip commentblocks and empty lines..
                    if (line.StartsWith("--") || line == String.Empty) continue;

                    // split the line
                    var parts = line.Split(new char[] { '|' });

                    // add commandItem to the list for later lookup or execution
                    words.Add(new Word() { Text = parts[0], AttachedText = parts[1], IsShellCommand = (parts[2] == "true") });

                    // add the text to the known choices of speechengine
                    texts.Add(parts[0]);
                }
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRE.LoadGrammar(wordsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void arduinoConnect()
        {
            isConnected = true;
            string selectedPort = arduinoComPortCombo.GetItemText(arduinoComPortCombo.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.Write("#STAR\n");
            serialPortConnection.ForeColor = Color.Red;
            serialPortConnection.Text = "Disconnect";

        }

        void arduinoDisconnect()
        {
            isConnected = false;
            port.Write("#STOP\n");
            port.Close();
            serialPortConnection.ForeColor = Color.Lime;
            serialPortConnection.Text = "Connect";
        }

        private void serialPortConnection_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                arduinoConnect();
            }
            else
            {
                arduinoDisconnect();
            }
        }
    }



    public class Word
    {
        public Word() { }
        public string Text { get; set; }
        public string AttachedText { get; set; }
        public bool IsShellCommand { get; set; }
    }

}
