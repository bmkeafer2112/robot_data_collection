using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ORiN2.interop.CAO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Timers;

namespace Smart_Manufacturing
{
    /// <summary>
    /// A user interface control to display what device is connected and sending data
    /// </summary>
    /// <param name="IPAddress">String that holds the IP Address of the device being monitored</param>
    /// <param name="DeviceType">String that holds the Device type</param>
    public class MonitorItem : UserControl
    {
        // Declarations
        CaoController caoCtrl;
        CaoRobot caoRobot;
        CaoEngine caoEng;
        CaoWorkspaces caoWss;
        CaoWorkspace caoWs;
        CaoControllers caoCtrls;
        CaoVariable getPos;
        ConcurrentQueue<DensoData> queDensoData = new ConcurrentQueue<DensoData>();
        //Variable to hold error msg
        string errorMsg = "";
        //Variable that tracks connection to the robot
        bool robotConnected = true;

        /// <summary>
        /// Get or sets the IPaddress of the device
        /// </summary>
        public string IPAddress
        {
            get { return this.lblIPAddress.Text; }
            set { this.lblIPAddress.Text = value; }

        }

        public bool DeviceConnectedFromColor()
        {
            string connectedColor = "Color [Green]";
            if (pnlRobotConnected.BackColor.ToString().Equals(connectedColor))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Get or sets the Name of the device
        /// </summary>
        protected Device DeviceName = new Device(); 
        public virtual Device deviceName 
        {
            get { return DeviceName; } 
            set 
            { 
                DeviceName.Name = value.Name;
                DeviceName.Group = value.Group;
                DeviceName.Line = value.Line;
                DeviceName.DeviceType = value.DeviceType;
                DeviceName.IPAddress = value.IPAddress;
                DeviceName.Block = value.Block;
                DeviceName.Packet_Speed = value.Packet_Speed;
                lblIPAddress.Text = value.IPAddress;
                lblDeviceType.Text = value.DeviceType;
                lblGroup.Text = value.Group;
                lblLine.Text = value.Line;
                lblName.Text = value.Name;
                lblBlock.Text = value.Block;
                tmrPulling.Interval = value.Packet_Speed;
                lblPacketSpeed.Text = value.Packet_Speed.ToString();
                caoEng = new CaoEngine();
                caoWss = caoEng.Workspaces;
                caoWs = caoWss.Item(0);
                caoCtrls = caoWs.Controllers;
                caoCtrl = caoCtrls.Add("RC1", "CaoProv.DENSO.NetwoRC", "", "conn=eth:" + DeviceName.IPAddress);
                caoRobot = caoCtrl.AddRobot("Arm7");
                getPos = caoRobot.AddVariable("@CURRENT_POSITION", "");
                tmrRetryConnect.Interval = 10000;
            } 
        }
        

        public MonitorItem()
        {
            InitializeComponent();
        }

        public void MonitorItem_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                // Release a CaoController object
                Marshal.ReleaseComObject(getPos);
                getPos = null; 
                caoCtrl.Robots.Clear();
                Marshal.ReleaseComObject(caoRobot);
                caoRobot = null;
              
                caoEng.Workspaces.Item(0).Controllers.Remove(caoCtrl.Index);
                Marshal.ReleaseComObject(caoCtrl);
                caoCtrl = null;
                Marshal.ReleaseComObject(caoCtrls);
                caoCtrls = null;
                Marshal.ReleaseComObject(caoWs);
                caoWs = null;
                Marshal.ReleaseComObject(caoWss);
                caoWss = null;
                Marshal.ReleaseComObject(caoEng);
                caoEng = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ReleaseComm()
        {
            tmrQueDump.Stop();
            tmrPulling.Stop();
            tmrRetryConnect.Stop();
            try
            {
                // Release a CaoController object
                Marshal.ReleaseComObject(getPos);
                getPos = null;
                caoCtrl.Robots.Clear();
                Marshal.ReleaseComObject(caoRobot);
                caoRobot = null;

                caoEng.Workspaces.Item(0).Controllers.Remove(caoCtrl.Index);
                Marshal.ReleaseComObject(caoCtrl);
                caoCtrl = null;
                Marshal.ReleaseComObject(caoCtrls);
                caoCtrls = null;
                Marshal.ReleaseComObject(caoWs);
                caoWs = null;
                Marshal.ReleaseComObject(caoWss);
                caoWss = null;
                Marshal.ReleaseComObject(caoEng);
                caoEng = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private  void MonitorItem_Load(object sender, EventArgs e)
        {
            Form1 mParent = (Form1)this.Parent.Parent.Parent;
            tmrQueDump.Interval = mParent.SendMQTTTime;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void OnTimedEventPullData(object source, ElapsedEventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void lblIPAddress_Click(object sender, EventArgs e)
        {

        }
        void Activate()
        {
            
            
        }
      
        private void OnQueDump(object source, ElapsedEventArgs e)
        {
            if (backgroundWorker2.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker2.RunWorkerAsync();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(caoRobot);
            caoRobot = null;
            caoCtrls.Clear();
             
            System.Runtime.InteropServices.Marshal.ReleaseComObject(caoCtrl);
            caoCtrl = null;
            caoWss.Clear();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(caoCtrls);
            caoCtrls = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(caoWs);
            caoWs = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(caoWss);
            caoWss = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(caoEng);
            caoEng = null;
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Release a CaoController object
                caoCtrl.Robots.Remove(caoRobot);
                caoCtrl.Robots.Clear();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(caoRobot);
                caoRobot = null;
                if (caoCtrl != null)
                {
                    caoCtrls.Remove(caoCtrl.Name);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(caoCtrl);
                    caoCtrl = null;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(caoCtrls);
                caoCtrls = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(caoWs);
                caoWs = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(caoWss);
                caoWss = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(caoEng);
                caoEng = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tmrPulling.Enabled = true;
            tmrPulling.Start();
            robotConnected = true;
            lblRobotConnected.Text = "Connected";
            txtErrorMessage.Text = "";
            //MessageBox.Show("button2_Click " + this.robotConnected + " - " + this.IPAddress + " - " + pnlRobotConnected.BackColor.ToString());
            tmrRetryConnect.Enabled = false;
        }

        private void lblRobotConnected_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string buildPos = "Position :";
            // Setup arrays to get specific data from the Denso robot.
            DensoData getDensoData = new DensoData();
            int[] jointCurrent1 = new[] { 4, 1 };
            int[] jointTorque1 = new[] { 5, 1 };
            int[] jointCurrent2 = new[] { 4, 2 };
            int[] jointTorque2 = new[] { 5, 2 };
            int[] jointCurrent3 = new[] { 4, 3 };
            int[] jointTorque3 = new[] { 5, 3 };
            int[] jointCurrent4 = new[] { 4, 4 };
            int[] jointTorque4 = new[] { 5, 4 };
            int[] jointCurrent5 = new[] { 4, 5 };
            int[] jointTorque5 = new[] { 5, 5 };
            int[] jointCurrent6 = new[] { 4, 6 };
            int[] jointTorque6 = new[] { 5, 6 };

            try
            {
                //Declares a varible to hold the robot position. 
                Single[] tempSingle = (float[])getPos.Value;
                if (robotConnected)
                {
                    pnlRobotConnected.BackColor = Color.Green;
                    tmrRetryConnect.Stop();
                    tmrRetryConnect.Enabled = false;
                    //Iterate through all the values of the position
                    int i = 0;
                    foreach (Single n in tempSingle)
                    {
                        switch (i)
                        {
                            case 0:
                                getDensoData.Tool_Center_Point.x.Value = n;
                                break;
                            case 1:
                                getDensoData.Tool_Center_Point.y.Value = n;
                                break;
                            case 2:
                                getDensoData.Tool_Center_Point.z.Value = n;
                                break;
                            case 3:
                                getDensoData.Tool_Center_Point.rx.Value = n;
                                break;
                            case 4:
                                getDensoData.Tool_Center_Point.ry.Value = n;
                                break;
                            case 5:
                                getDensoData.Tool_Center_Point.rz.Value = n;
                                break;
                        }
                        i++;
                        // add each element to the label for displaying.
                        buildPos = buildPos + ", " + n.ToString();
                    }
                    // Retrieve and Display Current Data in percentages
                    getDensoData.Joints[1].amps.Value = (Single)caoRobot.Execute("GetJntData", jointCurrent1);
                    getDensoData.Joints[2].amps.Value = (Single)caoRobot.Execute("GetJntData", jointCurrent2);
                    getDensoData.Joints[3].amps.Value = (Single)caoRobot.Execute("GetJntData", jointCurrent3);
                    getDensoData.Joints[4].amps.Value = (Single)caoRobot.Execute("GetJntData", jointCurrent4);
                    getDensoData.Joints[5].amps.Value = (Single)caoRobot.Execute("GetJntData", jointCurrent5);
                    getDensoData.Joints[6].amps.Value = (Single)caoRobot.Execute("GetJntData", jointCurrent6);

                    // Retrieve and Display Torque Data in percentages of difference from command Torque
                    getDensoData.Joints[1].Torque.Value = (Single)caoRobot.Execute("GetJntData", jointTorque1);
                    getDensoData.Joints[2].Torque.Value = (Single)caoRobot.Execute("GetJntData", jointTorque2);
                    getDensoData.Joints[3].Torque.Value = (Single)caoRobot.Execute("GetJntData", jointTorque3);
                    getDensoData.Joints[4].Torque.Value = (Single)caoRobot.Execute("GetJntData", jointTorque4);
                    getDensoData.Joints[5].Torque.Value = (Single)caoRobot.Execute("GetJntData", jointTorque5);
                    getDensoData.Joints[6].Torque.Value = (Single)caoRobot.Execute("GetJntData", jointTorque6);

                    getDensoData.Joints[1].Name = "joint_1";
                    getDensoData.Joints[2].Name = "joint_2";
                    getDensoData.Joints[3].Name = "joint_3";
                    getDensoData.Joints[4].Name = "joint_4";
                    getDensoData.Joints[5].Name = "joint_5";
                    getDensoData.Joints[6].Name = "joint_6";

                    getDensoData.Robot_Name = DeviceName.Name;
                    
                    //Time stamp the data sent. 
                    getDensoData.Time_Stamp = DateTime.Now.ToString();
                    //Send collected data to que
                    queDensoData.Enqueue(getDensoData);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception Occurred - " + DeviceName.Name + " Should Disconnect");
                tmrPulling.Enabled = false;
                robotConnected = false;
                pnlRobotConnected.BackColor = Color.Red;
                tmrRetryConnect.Enabled = true;
                tmrRetryConnect.Start();
                //lblRobotConnected.Text = "Disconnected";
                //txtErrorMessage.Text = ex.Message;
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker2 = sender as BackgroundWorker;
            if (this.Parent.Parent.Parent != null)
            {
                Form1 mParent = (Form1)this.Parent.Parent.Parent;
                DensoData getDensoData = new DensoData();
                JsonSerializerOptions devinsOptions = new JsonSerializerOptions();
                string sendString = "/" + DeviceName.Group + "/" + DeviceName.Line + "/" + DeviceName.Block + "/" + "ROBOT" + "/" + DeviceName.Name;
//              string sendString = "/" + DeviceName.Group + "/" + DeviceName.Line + "/" + DeviceName.Block + "/" + DeviceName.DeviceType + "/" + DeviceName.Name;
                int maxCount = queDensoData.Count;
                if (maxCount > 0)
                {
                    while (queDensoData.TryDequeue(out getDensoData))
                    {
                        string jasonString = JsonConvert.SerializeObject(getDensoData, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        mParent.SendMqtt(jasonString, sendString);
                    }

                }
                
            }

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void OnTimedEventAutoRetry(object source, ElapsedEventArgs e)
        {
            robotConnected = true;
            tmrPulling.Enabled = true;
        }
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tmrPulling = new System.Timers.Timer();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tmrQueDump = new System.Timers.Timer();
            this.pnlRobotConnected = new System.Windows.Forms.Panel();
            this.lblRobotConnected = new System.Windows.Forms.Label();
            this.gertpos = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.txtErrorMessage = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblDeviceType = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.lblPacketSpeed = new System.Windows.Forms.Label();
            this.tmrRetryConnect = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.tmrPulling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrQueDump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrRetryConnect)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, -75);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.BackColor = System.Drawing.Color.Black;
            this.lblIPAddress.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblIPAddress.ForeColor = System.Drawing.Color.White;
            this.lblIPAddress.Location = new System.Drawing.Point(356, 17);
            this.lblIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(114, 20);
            this.lblIPAddress.TabIndex = 1;
            this.lblIPAddress.Text = "192.168.100.200";
            this.lblIPAddress.Click += new System.EventHandler(this.lblIPAddress_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.BackColor = System.Drawing.Color.Black;
            this.lblGroup.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblGroup.ForeColor = System.Drawing.Color.White;
            this.lblGroup.Location = new System.Drawing.Point(3, 17);
            this.lblGroup.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(49, 20);
            this.lblGroup.TabIndex = 1;
            this.lblGroup.Text = "Name";
            this.lblGroup.Click += new System.EventHandler(this.label2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(914, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 0;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(850, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 0;
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(783, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 0;
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(711, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 0;
            this.label5.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(567, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 0;
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(914, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 0;
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(847, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 0;
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(783, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 0;
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(711, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 0;
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(639, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 0;
            this.label12.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(567, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 13);
            this.label13.TabIndex = 0;
            this.label13.Visible = false;
            // 
            // tmrPulling
            // 
            this.tmrPulling.Enabled = true;
            this.tmrPulling.Interval = 1000D;
            this.tmrPulling.SynchronizingObject = this;
            this.tmrPulling.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimedEventPullData);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(356, -1);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 20);
            this.label14.TabIndex = 1;
            this.label14.Text = "IP Address: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Black;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(3, -1);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 20);
            this.label15.TabIndex = 1;
            this.label15.Text = "Business:";
            this.label15.Click += new System.EventHandler(this.label2_Click);
            // 
            // tmrQueDump
            // 
            this.tmrQueDump.Enabled = true;
            this.tmrQueDump.Interval = 10000D;
            this.tmrQueDump.SynchronizingObject = this;
            this.tmrQueDump.Elapsed += new System.Timers.ElapsedEventHandler(this.OnQueDump);
            // 
            // pnlRobotConnected
            // 
            this.pnlRobotConnected.BackColor = System.Drawing.Color.Orange;
            this.pnlRobotConnected.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.pnlRobotConnected.Location = new System.Drawing.Point(459, 18);
            this.pnlRobotConnected.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlRobotConnected.Name = "pnlRobotConnected";
            this.pnlRobotConnected.Size = new System.Drawing.Size(69, 16);
            this.pnlRobotConnected.TabIndex = 2;
            // 
            // lblRobotConnected
            // 
            this.lblRobotConnected.AutoSize = true;
            this.lblRobotConnected.BackColor = System.Drawing.Color.Black;
            this.lblRobotConnected.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblRobotConnected.ForeColor = System.Drawing.Color.White;
            this.lblRobotConnected.Location = new System.Drawing.Point(459, -1);
            this.lblRobotConnected.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRobotConnected.Name = "lblRobotConnected";
            this.lblRobotConnected.Size = new System.Drawing.Size(80, 20);
            this.lblRobotConnected.TabIndex = 1;
            this.lblRobotConnected.Text = "Connected";
            this.lblRobotConnected.Click += new System.EventHandler(this.lblRobotConnected_Click);
            // 
            // gertpos
            // 
            this.gertpos.AutoSize = true;
            this.gertpos.Location = new System.Drawing.Point(567, 31);
            this.gertpos.Name = "gertpos";
            this.gertpos.Size = new System.Drawing.Size(0, 13);
            this.gertpos.TabIndex = 0;
            this.gertpos.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(537, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Black;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(675, 1);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 20);
            this.label17.TabIndex = 5;
            this.label17.Text = "Error:";
            // 
            // txtErrorMessage
            // 
            this.txtErrorMessage.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.txtErrorMessage.Location = new System.Drawing.Point(717, 1);
            this.txtErrorMessage.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtErrorMessage.Multiline = true;
            this.txtErrorMessage.Name = "txtErrorMessage";
            this.txtErrorMessage.Size = new System.Drawing.Size(277, 35);
            this.txtErrorMessage.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Black;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(63, -1);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 20);
            this.label18.TabIndex = 7;
            this.label18.Text = "Line:";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.Color.Black;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblLine.ForeColor = System.Drawing.Color.White;
            this.lblLine.Location = new System.Drawing.Point(63, 17);
            this.lblLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(49, 20);
            this.lblLine.TabIndex = 8;
            this.lblLine.Text = "Name";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Black;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(172, -1);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 20);
            this.label20.TabIndex = 9;
            this.label20.Text = "Device Type:";
            // 
            // lblDeviceType
            // 
            this.lblDeviceType.AutoSize = true;
            this.lblDeviceType.BackColor = System.Drawing.Color.Black;
            this.lblDeviceType.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblDeviceType.ForeColor = System.Drawing.Color.White;
            this.lblDeviceType.Location = new System.Drawing.Point(172, 17);
            this.lblDeviceType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeviceType.Name = "lblDeviceType";
            this.lblDeviceType.Size = new System.Drawing.Size(49, 20);
            this.lblDeviceType.TabIndex = 10;
            this.lblDeviceType.Text = "Name";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Black;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(259, -1);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 20);
            this.label22.TabIndex = 11;
            this.label22.Text = "Device Name:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Black;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(259, 17);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(98, 20);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Device Name";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Black;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(109, -1);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 20);
            this.label16.TabIndex = 13;
            this.label16.Text = "Block:";
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.BackColor = System.Drawing.Color.Black;
            this.lblBlock.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblBlock.ForeColor = System.Drawing.Color.White;
            this.lblBlock.Location = new System.Drawing.Point(109, 17);
            this.lblBlock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(49, 20);
            this.lblBlock.TabIndex = 14;
            this.lblBlock.Text = "Name";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Black;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(609, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 20);
            this.label19.TabIndex = 16;
            this.label19.Text = "Rate (ms)";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // lblPacketSpeed
            // 
            this.lblPacketSpeed.AutoSize = true;
            this.lblPacketSpeed.BackColor = System.Drawing.Color.Black;
            this.lblPacketSpeed.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblPacketSpeed.ForeColor = System.Drawing.Color.White;
            this.lblPacketSpeed.Location = new System.Drawing.Point(609, 19);
            this.lblPacketSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPacketSpeed.Name = "lblPacketSpeed";
            this.lblPacketSpeed.Size = new System.Drawing.Size(41, 20);
            this.lblPacketSpeed.TabIndex = 17;
            this.lblPacketSpeed.Text = "1000";
            // 
            // tmrRetryConnect
            // 
            this.tmrRetryConnect.Enabled = true;
            this.tmrRetryConnect.Interval = 10000D;
            this.tmrRetryConnect.SynchronizingObject = this;
            this.tmrRetryConnect.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimedEventAutoRetry);
            // 
            // MonitorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblPacketSpeed);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblBlock);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblDeviceType);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.txtErrorMessage);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.gertpos);
            this.Controls.Add(this.lblRobotConnected);
            this.Controls.Add(this.pnlRobotConnected);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "MonitorItem";
            this.Size = new System.Drawing.Size(996, 40);
            this.Load += new System.EventHandler(this.MonitorItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tmrPulling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrQueDump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrRetryConnect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Timers.Timer tmrPulling;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Timers.Timer tmrQueDump;
        private System.Windows.Forms.Panel pnlRobotConnected;
        private System.Windows.Forms.Label lblRobotConnected;
        private System.Windows.Forms.Label gertpos;
        private Button button2;
        private Label label17;
        private TextBox txtErrorMessage;
        private Label label18;
        private Label lblLine;
        private Label label20;
        private Label label22;
        private Label lblName;
        private Label lblDeviceType;
        private Label label16;
        private Label lblBlock;
        private Label label19;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Label lblPacketSpeed;
        private System.Timers.Timer tmrRetryConnect;

        public FormClosingEventHandler FormClosing { get; private set; }
    
}
}
