using MQTTnet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Manufacturing
{
    public partial class Form1 : Form
    {
        
        //Declarations for the Class
        static System.Threading.CancellationToken cancellationToken;
        static MqttFactory factory = new MqttFactory();
        static MQTTnet.Client.IMqttClient mqttClient = factory.CreateMqttClient();
        static bool _tryReconnectMQTT = false;
        MQTTSettings myMQTTSettings = new MQTTSettings();
        public Devices devices = new Devices();
        int cLeft, rLeft = 0;
        int deviceIndex = 0;
        bool updating = false;
        private BindingSource dataSet1BindingSource;
        //private PictureBox pictureBox1;
        int controlHeight = 0;
        public int SendMQTTTime { get; set; }
        public Form1()
        {
            InitializeComponent();
            //myMQTTSettings = XMLFileClasses.ReadFromXmlFile<MQTTSettings>(@".\\MQTTConfig.xml");
            try
            {
                myMQTTSettings = XMLFileClasses.ReadFromXmlFile<MQTTSettings>(@".\\MQTTConfig.xml");
            }
            catch
            {
                myMQTTSettings.IPAddress = "192.168.1.101";
                myMQTTSettings.Port_Number = 1883;
                myMQTTSettings.Send_Time = 100;
                XMLFileClasses.WriteToXmlFile<MQTTSettings>(@".\\MQTTConfig.xml", myMQTTSettings, false);
            }
            SendMQTTTime = myMQTTSettings.Send_Time;
            textBox1.Text = myMQTTSettings.IPAddress;
            txtPacketTime.Text = SendMQTTTime.ToString();
        }
        /// <summary>
        /// Called when the form is loaded into memory.
        /// </summary>
        private async void Form1_OnLoad(object sender, EventArgs e)
        {
            //connectMqtt();
            displayMonitor();
            await PersistConnectionAsync(myMQTTSettings);

        }
        /// <summary>
        /// Called when the form is closing.
        /// <para>Sender, The ojbect that sent the call</para>
        /// <para>e, as event args</para>
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseCOMM(this, e);
        }

        /// <summary>
        /// Release COMM components that are connected.
        /// <para>Sender, The ojbect that sent the call</para>
        /// <para>e, as event args</para>
        /// </summary>
        private void ReleaseCOMM(object sender, FormClosingEventArgs e)
        {
            foreach (RC7MonitorItem d in this.pnlDisplay.Controls.OfType<RC7MonitorItem>())
            {
                d.MonitorItem_FormClosing(this, e);
            }
            foreach (RC8MonitorItem d in this.pnlDisplay.Controls.OfType<RC8MonitorItem>())
            {
                d.MonitorItem_FormClosing(this, e);
            }
        }

        /// <summary>
        /// Display the devices on the monitor form
        /// </summary>
        public void displayMonitor()
        {
            // Gets device list from a file
            //devices = XMLFileClasses.ReadFromXmlFile<Devices>(@".\\device list.xml");
            try
            {
                devices = XMLFileClasses.ReadFromXmlFile<Devices>(@".\\device list.xml");
            }
            catch
            {

            }
            //Iterates through the devices in the file
            foreach (Device n in devices.Items)
            {
                if (n.DeviceType == "ROBOT_RC8")
                {
                    AddNewMonitorItemRC8(n);

                }
                else
                {
                    AddNewMonitorItemRC7(n);

                }
            }
        }
        /// <summary>
        /// Adds a monitor item to monitor a device
        /// </summary>
        /// <typeparam name="deviceAdded"></typeparam>
        public RC7MonitorItem AddNewMonitorItemRC7(Device deviceAdded)
        {
            //Declarations
            RC7MonitorItem mntitem = new RC7MonitorItem();
            // Adds an item to the panel
            this.pnlDisplay.Controls.Add(mntitem);
            // Parameters for the new item that was added
            if (cLeft == 0)
            {
                mntitem.Top = 3;
            }
            else
                controlHeight = mntitem.Height;
            mntitem.Top = cLeft * (mntitem.Height + 1);
            mntitem.Left = 45;
            mntitem.IPAddress = deviceAdded.IPAddress;
            mntitem.deviceName = deviceAdded;
            // Add to item count
            cLeft = cLeft + 1;
            // Returns the new item
            return mntitem;
        }
        /// <summary>
        /// Adds a monitor item to monitor a device
        /// </summary>
        /// <typeparam name="deviceAdded"></typeparam>
        public RC8MonitorItem AddNewMonitorItemRC8(Device deviceAdded)
        {
            //Declarations
            RC8MonitorItem mntitem = new RC8MonitorItem();
            // Adds an item to the panel
            this.pnlDisplay.Controls.Add(mntitem);
            // Parameters for the new item that was added
            if (cLeft == 0)
            {
                mntitem.Top = 3;
            }
            else
                controlHeight = mntitem.Height;
            mntitem.Top = cLeft * (mntitem.Height + 1);
            mntitem.Left = 45;
            mntitem.IPAddress = deviceAdded.IPAddress;
            mntitem.deviceName = deviceAdded;
            // Add to item count
            cLeft = cLeft + 1;
            // Returns the new item
            return mntitem;
        }

        /// <summary>
        /// Shows the edit device list form
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            displayDevices();
            pnlEdit.Visible = true;
            button4.Visible = true;
            button2.Visible = false;
        }

        public void displayDevices()
        {
            // Set "Denso Robot" as default device type
            cbxDeviceType.SelectedItem = cbxDeviceType.Items[0];

            rLeft = 0;
            for (int i = 0; i < cLeft; i++)
            {
                AddNewEditMonitorItem();
            }
        }
        public RadioButton AddNewEditMonitorItem()
        {
            //Declarations

            RadioButton r1 = new RadioButton();
            r1.CheckedChanged += new EventHandler(radio_Checked);

            // Parameters for the new item that was added
            if (rLeft == 0)
            {
                r1.Checked = true;
            }
            else // offset the top for how many devices that are already on the panel
                r1.Top = rLeft * controlHeight + 3;

            r1.Text = rLeft.ToString();
            r1.Location = new Point(10, r1.Top + 3);
            r1.Font = new Font("Berlin Sans FB", 12);
            this.pnlDisplay.Controls.Add(r1);

            // Add to item count
            rLeft++;
            return r1;
        }
        /// <summary>
        /// Examines all radio buttons and puts the index of the one that is checked 
        /// into deviceIndex
        /// <para>Sender, The ojbect that sent the call</para>
        /// <para>e, as event args</para>
        /// </summary>

        private void radio_Checked(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            int index = 0;
            try
            {
                index = Int32.Parse(btn.Text);
                deviceIndex = index;
            }
            catch
            {
                // do nothing
            }

            txtDeviceName.Text = devices.Items[index].Name;
            txtBlock.Text = devices.Items[index].Block;
            txtGroup.Text = devices.Items[index].Group;
            txtIPAddress.Text = devices.Items[index].IPAddress;
            txtLine.Text = devices.Items[index].Line;

        }

        /// <summary>
        /// Clear the devices from the monitor form
        /// </summary>

        public void clearDevices()
        {
            foreach (RC7MonitorItem d in this.pnlDisplay.Controls.OfType<RC7MonitorItem>())
            {
                d.ReleaseComm();
            }
            pnlDisplay.Controls.Clear();
            devices.Items.Clear();
            cLeft = 0;
        }

        private void btdAddNew_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Device deviceAdd = new Device();
                deviceAdd.Name = txtDeviceName.Text;
                deviceAdd.Line = txtLine.Text;
                deviceAdd.Group = txtGroup.Text;
                deviceAdd.IPAddress = txtIPAddress.Text;
                deviceAdd.DeviceType = cbxDeviceType.Text;
                deviceAdd.Block = txtBlock.Text;
                deviceAdd.Packet_Speed = Int32.Parse(txtPacketSpeed.Text);
                devices.Items.Add(deviceAdd);
                XMLFileClasses.WriteToXmlFile<Devices>(@".\\device list.xml", devices, false);
                clearDevices();
                displayMonitor();
                displayDevices();
            }
            else
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            updating = true;
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Device deviceAdd = new Device();
                string msgString;
                msgString = "Device " + devices.Items[deviceIndex].Name + " has been updated.";
                deviceAdd.Name = txtDeviceName.Text;
                deviceAdd.Line = txtLine.Text;
                deviceAdd.Group = txtGroup.Text;
                deviceAdd.IPAddress = txtIPAddress.Text;
                deviceAdd.DeviceType = cbxDeviceType.Text;
                deviceAdd.Block = txtBlock.Text;
                deviceAdd.Packet_Speed = Int32.Parse(txtPacketSpeed.Text);
                devices.Items[deviceIndex] = deviceAdd;
                XMLFileClasses.WriteToXmlFile<Devices>(@".\\device list.xml", devices, false);
                MessageBox.Show(msgString);
                clearDevices();
                displayMonitor();
                displayDevices();
            }
            updating = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            foreach (Control item in pnlDisplay.Controls.OfType<RadioButton>().Reverse())
            {
                pnlDisplay.Controls.Remove(item);
                item.Dispose();
            }
            button4.Visible = false;
            button2.Visible = true;
            pnlEdit.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deviceName;

            deviceName = devices.Items[deviceIndex].Name;
            devices.Items.RemoveAt(deviceIndex);
            XMLFileClasses.WriteToXmlFile<Devices>(@".\\device list.xml", devices, false);
            clearDevices();
            displayMonitor();
            displayDevices();
            MessageBox.Show("Device " + deviceName + " has been removed.");
        }
        public static async void connectMqtt(MQTTSettings myMQTTSettings)
        {
            string clientID = Guid.NewGuid().ToString();
            string mqttURI = myMQTTSettings.IPAddress;
            int mqttPort = myMQTTSettings.Port_Number;

            bool mqttSecure = false;

            var options = new MQTTnet.Client.MqttClientOptionsBuilder()
                .WithTcpServer(mqttURI, mqttPort)
                .Build();
            // Do not initialize this variable here.
            try
            {
                await mqttClient.ConnectAsync(options, cancellationToken);
            }
            catch
            {

            }
        }
        public async void SendMqtt(string payload, string dvName)
        {

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(dvName)
                .WithPayload(payload)

                .WithRetainFlag()
                .Build();

            try
            {
                await mqttClient.PublishAsync(message, cancellationToken);
            }

            catch       //MQTTnet.Exceptions.MqttCommunicationException: 'The client is not connected.'

            {
                _tryReconnectMQTT = true;
            }
        }

        private static async Task TryReconnectAsync(CancellationToken cancellationToken)
        {
            var connected = mqttClient.IsConnected;
            while (!connected && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    //connectMqtt(myMQTTSettings);
                }
                catch
                {

                }
                connected = mqttClient.IsConnected;
                await Task.Delay(1000, cancellationToken);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Connects to the MQTT broker
        /// <para>MyMQTTSettings: The settings for the MQTT broker</para>
        /// </summary>

        public static async Task PersistConnectionAsync(MQTTSettings myMQTTSettings)
        {
            var connected = mqttClient.IsConnected;
            if (connected)
            {
                _tryReconnectMQTT = false;
            }
            else
            {
                _tryReconnectMQTT = true;
            }
            while (_tryReconnectMQTT)
            {
                if (!connected)
                {
                    try
                    {
                        connectMqtt(myMQTTSettings);
                    }
                    catch
                    {
                        Console.WriteLine("failed reconnect");
                    }
                }
                await Task.Delay(1000);
                connected = mqttClient.IsConnected;
            }
        }

        /// <summary>
        /// Validate the setting of the device
        /// <para>Sender, The ojbect that sent the call</para>
        /// <para>e, as event args</para>
        /// </summary>
        private void txtIPAddress_Validating(object sender,
                System.ComponentModel.CancelEventArgs e)
        {

            System.Net.IPAddress ipAddress;
            int i = 0;

            if (ValidateIPv4(txtIPAddress.Text))
            {
                if (IPAddress.TryParse(txtIPAddress.Text, out ipAddress))
                {
                    //valid ip

                    foreach (Device n in devices.Items)
                    {
                        if (i != deviceIndex && !(updating))
                        {
                            if (n.IPAddress == txtIPAddress.Text)
                            {

                                MessageBox.Show("This IP address is already in use by another device");
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            if (i == deviceIndex && !(updating))
                            {
                                if (n.IPAddress == txtIPAddress.Text)
                                {
                                    MessageBox.Show("This IP address is already in use by another device");
                                    e.Cancel = true;
                                }
                            }
                        }
                        i++;
                    }
                }
                else
                {
                    //is not valid ip
                    MessageBox.Show("Enter a valid IP address");
                    e.Cancel = true;
                }
            }
            else
            {
                MessageBox.Show("Enter a valid IP address");
                e.Cancel = true;
            }

        }

        /// <summary>
        /// Release COMM components that are connected.
        /// <para>ipString, Validates a string as an IP address</para>
        /// <para>e, as event args</para>
        /// </summary>
        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        /// <summary>
        /// Iterates through all the devices and flags any device that is disconnected
        /// </summary>
        private bool DevicesConnected()
        {
            bool connected = true;
            foreach (RC7MonitorItem i in this.pnlDisplay.Controls.OfType<RC7MonitorItem>())
            {
                //Should only be Green if it is connected.  This is not checked from the robotConnected variable because this
                //gets set to True when a device is disconnected to trigger the system to try again after a timer

                if (!i.DeviceConnectedFromColor())
                {
                    connected = false;
                }
            }
            foreach (MonitorItem i in this.pnlDisplay.Controls.OfType<MonitorItem>())
            {
                //Should only be Green if it is connected.  This is not checked from the robotConnected variable because this
                //gets set to True when a device is disconnected to trigger the system to try again after a timer

                if (!i.DeviceConnectedFromColor())
                {
                    connected = false;
                }
            }
            foreach (RC8MonitorItem i in this.pnlDisplay.Controls.OfType<RC8MonitorItem>())
            {
                //Should only be Green if it is connected.  This is not checked from the robotConnected variable because this
                //gets set to True when a device is disconnected to trigger the system to try again after a timer

                if (!i.DeviceConnectedFromColor())
                {
                    connected = false;
                }
            }
            return connected;
        }

        /// <summary>
        /// Show the about form to the user
        /// <para>Sender, The ojbect that sent the call</para>
        /// <para>e, as event args</para>
        /// </summary>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout newAbout = new frmAbout();
            newAbout.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlDisplay.Controls.OfType<RadioButton>().Reverse())
            {
                pnlDisplay.Controls.Remove(item);
                item.Dispose();
            }
            button4.Visible = false;
            button2.Visible = true;
            pnlEdit.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            displayDevices();
            pnlEdit.Visible = true;
            button4.Visible = true;
            button2.Visible = false;
        }

        private void btdAddNew_Click_1(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Device deviceAdd = new Device();
                deviceAdd.Name = txtDeviceName.Text;
                deviceAdd.Line = txtLine.Text;
                deviceAdd.Group = txtGroup.Text;
                deviceAdd.IPAddress = txtIPAddress.Text;
                deviceAdd.DeviceType = cbxDeviceType.Text;
                deviceAdd.Block = txtBlock.Text;
                deviceAdd.Packet_Speed = Int32.Parse(txtPacketSpeed.Text);
                devices.Items.Add(deviceAdd);
                XMLFileClasses.WriteToXmlFile<Devices>(@".\\device list.xml", devices, false);
                clearDevices();
                displayMonitor();
                displayDevices();
            }
            else
            {

            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            string deviceName;

            deviceName = devices.Items[deviceIndex].Name;
            devices.Items.RemoveAt(deviceIndex);
            XMLFileClasses.WriteToXmlFile<Devices>(@".\\device list.xml", devices, false);
            clearDevices();
            displayMonitor();
            displayDevices();
            MessageBox.Show("Device " + deviceName + " has been removed.");
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            updating = true;
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Device deviceAdd = new Device();
                string msgString;
                msgString = "Device " + devices.Items[deviceIndex].Name + " has been updated.";
                deviceAdd.Name = txtDeviceName.Text;
                deviceAdd.Line = txtLine.Text;
                deviceAdd.Group = txtGroup.Text;
                deviceAdd.IPAddress = txtIPAddress.Text;
                deviceAdd.DeviceType = cbxDeviceType.Text;
                deviceAdd.Block = txtBlock.Text;
                deviceAdd.Packet_Speed = Int32.Parse(txtPacketSpeed.Text);
                devices.Items[deviceIndex] = deviceAdd;
                XMLFileClasses.WriteToXmlFile<Devices>(@".\\device list.xml", devices, false);
                MessageBox.Show(msgString);
                clearDevices();
                displayMonitor();
                displayDevices();
            }
            updating = false;
        }

        private void pnlDisplay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool devicesConnected = false;
            //Check for MQTT Connection
            if (mqttClient.IsConnected)
            {
                panel2.BackColor = Color.Green;
            }
            else
                panel2.BackColor = Color.Red;

            //Check for Device Connectivity
            devicesConnected = this.DevicesConnected();
            if (devicesConnected)
            {
                pnlDevicesConnected.BackColor = Color.Green;
            }
            else
                pnlDevicesConnected.BackColor = Color.Red;
        }

        private void pnlEdit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbxDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Close out the appilication 
        /// <para>Sender, The ojbect that sent the call</para>
        /// <para>e, as event args</para>
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        
    }
}
