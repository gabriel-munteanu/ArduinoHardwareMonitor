using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ArduinoHardwareMonitor
{
    public partial class MainForm : Form
    {
        bool _notificationIconDisplayed = false;
        Computer _computer = new Computer();
        ArduinoCommunication arduinoCommunication = new ArduinoCommunication();
        int _displaySensorIndex = 0;

        public MainForm()
        {
            InitializeComponent();
            arduinoCommunication.NextSensorEvent += DisplayNextSensor;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadSensors();
            var thread = new Thread(SearchAndConnectToArduino);
            thread.Start(this);
            timer_SendData.Start();
        }

        private void LoadSensors()
        {
            slbl_AppStatus.Text = "Loading sensors";
            _computer.CPUEnabled = true;
            _computer.FanControllerEnabled = true;
            _computer.GPUEnabled = true;
            _computer.HDDEnabled = true;
            _computer.MainboardEnabled = true;
            _computer.RAMEnabled = true;

            _computer.Open();

            var _sensors = new List<ISensor>();
            var visitor = new SensorVisitor(_sensors);
            _computer.Traverse(visitor);

            foreach (var sensor in _sensors)
                checkedlistbox_Sensors.Items.Add(sensor);
        }

        private static void SearchAndConnectToArduino(object obj)
        {
            var form = obj as MainForm;
            form.slbl_AppStatus.Text = "Searching for Arduino";
            var result = form.arduinoCommunication.SearchAndConnectToArduino();
            if (result)
                form.slbl_AppStatus.Text = "Ready!";
            else
            {
                MessageBox.Show("Arduino could not be found on serial ports(0-10).\nPlease connect it and start the app.", "Arduino not available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        private void SendData()
        {
            if (checkedlistbox_Sensors.CheckedItems.Count == 0)
                arduinoCommunication.SendNothing();
            else
            {
                _displaySensorIndex = _displaySensorIndex % checkedlistbox_Sensors.CheckedItems.Count;                
                var sensor = checkedlistbox_Sensors.CheckedItems[_displaySensorIndex] as ISensor;
                sensor.Hardware.Update();
                arduinoCommunication.SendSensorValue(sensor, _displaySensorIndex + 1, checkedlistbox_Sensors.CheckedItems.Count);
            }
        }

        private void checkedlistbox_Sensors_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_displaySensorIndex >= checkedlistbox_Sensors.CheckedItems.Count)
                _displaySensorIndex = 0;
        }


        private delegate void displayNextSensorDelegate(object sender, EventArgs e);
        private void DisplayNextSensor(object sender, EventArgs e)
        {
            if (InvokeRequired)//this is invoked from another thread but it must be executed on the UI thread
            {
                BeginInvoke(new displayNextSensorDelegate(DisplayNextSensor), new object[] { null, null });
                return;
            }

            if (checkedlistbox_Sensors.CheckedItems.Count == 0)
                return;
            timer_SendData.Stop();
            _displaySensorIndex++;
            SendData();
            timer_SendData.Start();
        }

        private void timer_SendData_Tick(object sender, EventArgs e)
        {
            SendData();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                notifyIcon.Visible = true;
                if (!_notificationIconDisplayed)
                {
                    _notificationIconDisplayed = true;
                    notifyIcon.ShowBalloonTip(500);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            arduinoCommunication.Exit();
        }


    }
}
