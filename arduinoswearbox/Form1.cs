using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;

namespace arduinoswearbox
{

    public partial class Form1 : Form
    {
        // Speech Recognition setup


        // Arduino/COM setup
        SerialPort port;
        String[] ports;
        bool isConnected = false;
        public Form1()
        {
            InitializeComponent();

            // Speech


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

        private void sendToArduino(String cmd)
        {
            if (isConnected)
            {
                port.Write(cmd + "\n");
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

}
