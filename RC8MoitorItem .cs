using Newtonsoft.Json;
using ORiN2.interop.CAO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Npgsql;

namespace Smart_Manufacturing
{
    public partial class RC8MonitorItem : UserControl
    {
        // Declarations
        CaoController caoCtrl;
        CaoRobot caoRobot;
        CaoEngine caoEng;
        CaoWorkspaces caoWss;
        CaoWorkspace caoWs;
        CaoControllers caoCtrls;
        CaoVariable getPos;
/*        CaoVariable getAngle;*/
        CaoVariable getErrorCode;
        //ConcurrentQueue<DensoData> queDensoData = new ConcurrentQueue<DensoData>();


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
                try
                {
                    //caoCtrl = caoCtrls.Add("RC9", "CaoProv.DENSO.RC8", "", "@EventDisale=true,server=" + DeviceName.IPAddress);
                    caoCtrl = caoCtrls.Add("RC9", "CaoProv.DENSO.RC8", "", "conn=eth:" + DeviceName.IPAddress);
                    caoRobot = caoCtrl.AddRobot("Arm7");
                    getPos = caoRobot.AddVariable("@CURRENT_POSITION", "");
                }
                catch (Exception ex)
                {

                    tmrPulling.Enabled = false;
                    robotConnected = false;
                    pnlRobotConnected.BackColor = Color.Red;
                    tmrRetryConnect.Enabled = true;
                    tmrRetryConnect.Start();
                    lblRobotConnected.Text = "Disconnected";
                    errorMsg = ex.Message;

                }
                //getAngle = caoRobot.AddVariable("@CURRENT_ANGLE", "");
                //getErrorCode = caoCtrl.AddVariable("@ERROR_CODE", "");
                tmrRetryConnect.Interval = 10000;
            }
        }

        public RC8MonitorItem()
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
                Marshal.ReleaseComObject(getErrorCode);
                getErrorCode = null;
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
        private void RC8MonitorItem_Load(object sender, EventArgs e)
        {
            Form1 mParent = (Form1)this.Parent.Parent.Parent;
            tmrQueDump.Interval = mParent.SendMQTTTime;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Retrieves the information from the robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OnTimedEventPullData(object source, ElapsedEventArgs e)
        {
           
        }
        private void lblIPAddress_Click(object sender, EventArgs e)
        {

        }
        void Activate()
        {


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
            errorMsg = "";
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

            /*            int[] jointCurrent1 = new[] { 4, 1 };
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
                          int[] jointTorque6 = new[] { 5, 6 };*/

            //Prep the label to display the position

            try
            {

                //Declares a varible to hold the robot position. 

                if (robotConnected && getPos != null)
                {
                    Double[] currentPos = (Double[])getPos.Value;
                    Double[] Amps = (Double[])caoRobot.Execute("GetSrvData", 4);
                    Double[] Torques = (Double[])caoRobot.Execute("GetSrvData", 5);
                    Double[] loadFactors = (Double[])caoRobot.Execute("GetSrvData", 7);
                    //double[] tempSingle = (double[])getPos.Value;
                    pnlRobotConnected.BackColor = Color.Green;
                    tmrRetryConnect.Stop();
                    tmrRetryConnect.Enabled = false;
                    DateTime currentTime = DateTime.Now;
                    everythingTableAdapter1.Insert(currentTime, DeviceName.Name, currentPos[0], currentPos[1], currentPos[2], currentPos[3], currentPos[4], currentPos[5],
                            Torques[0], Torques[1], Torques[2], Torques[3], Torques[4], Torques[5], Amps[0], Amps[1], Amps[2], Amps[3], Amps[4], Amps[5],
                            loadFactors[0], loadFactors[1], loadFactors[2], loadFactors[3], loadFactors[4], loadFactors[5]);
                    //Iterate through all the values of the position
                    /*int i = 0;
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
                    getDensoData.Joints[1].amps.Value = (Double)caoRobot.Execute("GetSrvJntData", jointCurrent1);
                    getDensoData.Joints[2].amps.Value = (Double)caoRobot.Execute("GetSrvJntData", jointCurrent2);
                    getDensoData.Joints[3].amps.Value = (Double)caoRobot.Execute("GetSrvJntData", jointCurrent3);
                    getDensoData.Joints[4].amps.Value = (Double)caoRobot.Execute("GetSrvJntData", jointCurrent4);
                    getDensoData.Joints[5].amps.Value = (Double)caoRobot.Execute("GetSrvJntData", jointCurrent5);
                    getDensoData.Joints[6].amps.Value = (Double)caoRobot.Execute("GetSrvJntData", jointCurrent6);

                    // Retrieve and Display Torque Data in percentages of difference from command Torque
                    getDensoData.Joints[1].Torque.Value = (Double)caoRobot.Execute("GetSrvJntData", jointTorque1);
                    getDensoData.Joints[2].Torque.Value = (Double)caoRobot.Execute("GetSrvJntData", jointTorque2);
                    getDensoData.Joints[3].Torque.Value = (Double)caoRobot.Execute("GetSrvJntData", jointTorque3);
                    getDensoData.Joints[4].Torque.Value = (Double)caoRobot.Execute("GetSrvJntData", jointTorque4);
                    getDensoData.Joints[5].Torque.Value = (Double)caoRobot.Execute("GetSrvJntData", jointTorque5);
                    getDensoData.Joints[6].Torque.Value = (Double)caoRobot.Execute("GetSrvJntData", jointTorque6);

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
                    queDensoData.Enqueue(getDensoData);*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Occurred - " + DeviceName.Name + " Should Disconnect");
                tmrPulling.Enabled = false;
                robotConnected = false;
                pnlRobotConnected.BackColor = Color.Red;
                tmrRetryConnect.Enabled = true;
                tmrRetryConnect.Start();
                ReleaseComm();
                //lblRobotConnected.Text = "Disconnected";
                errorMsg = ex.Message;
            }
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void OnTimedEventAutoRetry(object source, ElapsedEventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtErrorMessage.Text = errorMsg;
        }

        private void tmrRetryConnect_Tick(object sender, EventArgs e)
        {
            robotConnected = true;
            tmrPulling.Enabled = true;
        }

        private void tmrPulling_Tick(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }
    }
}
