using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using SwearboxHelper;
using System.Threading.Tasks;

namespace arduinoswearbox
{

    public partial class Form1 : Form
    {
        // Speech Recognition setup
        Cloud gCloud;

        // Arduino/COM setup
        SerialPort port;
        String[] ports;
        bool isConnected = false;
        int seconds = 0;
        public Form1()
        {
            InitializeComponent();

            // Speech
            gCloud = new Cloud();

            // Arduino/COM 
            getAvailableComPorts();
            secondsToListenCombo.SelectedIndex = secondsToListenCombo.Items.IndexOf("10");

        }

        public void testInfo(string text)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { testInfo(text); });
                return;
            }
            else
            {
                speechRecognitionOutputTextbox.AppendText(text);
                speechRecognitionOutputTextbox.AppendText("#FBD"+gCloud.forbidden.ToString()+"\n");
                sendToArduino("#FRBD" + gCloud.forbidden.ToString());
            }
        }

        private void sendToArduino(String cmd)
        {
            if (isConnected)
            {
                port.Write(cmd + "\n");
            }
        }

        void getAvailableComPorts()
        {
            arduinoComPortCombo.Items.Clear();
            ports = SerialPort.GetPortNames();
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
            sendToArduino("#STAR");
            serialPortConnection.ForeColor = Color.Red;
            serialPortConnection.Text = "Disconnect";

        }

        void arduinoDisconnect()
        {
            sendToArduino("#STOP");
            isConnected = false;
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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            getAvailableComPorts();
        }

        private void listenButton_Click(object sender, EventArgs e)
        {
            seconds = Convert.ToInt32(secondsToListenCombo.SelectedItem.ToString());
            if (isConnected)
            {
                speechRecognitionOutputTextbox.Clear();
                speechRecognitionOutputTextbox.AppendText("Listening\n");
                gCloud.StreamingMicRecognizeAsync(seconds, testInfo);
            }
        }
    }

}
