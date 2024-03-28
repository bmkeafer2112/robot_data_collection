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
    public partial class RC7MonitorItem : UserControl
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
                caoCtrl = caoCtrls.Add("RC1", "CaoProv.DENSO.NetwoRC", "", "conn=eth:" + DeviceName.IPAddress);
                caoRobot = caoCtrl.AddRobot("Arm7");
                getPos = caoRobot.AddVariable("@CURRENT_POSITION", "");
                //getAngle = caoRobot.AddVariable("@CURRENT_ANGLE", "");
                //getErrorCode = caoCtrl.AddVariable("@ERROR_CODE", "");
                tmrRetryConnect.Interval = 10000;
            }
        }

        public RC7MonitorItem()
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
        private void RC7MonitorItem_Load(object sender, EventArgs e)
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
/*            double[] currentPos = new double[6];*/
            double[] destPos = new double[6];
      
            // Setup arrays to get specific data from the Denso robot.
            DensoData getDensoData = new DensoData();

            try
            {
                //Declares a varible to hold the robot position. 
                Single[] currentPos = (Single[])getPos.Value;
/*               Single[] angleSingle = (Single[])getAngle.Value;
                int insertRtn = 0; 
                int tempError = (int)getErrorCode.Value;*/
                Single[] Amps = (Single[])caoRobot.Execute("GetSrvData", 4);
                Single[] Torques = (Single[])caoRobot.Execute("GetSrvData", 5);
                caoRobot.Execute("StartSrvLog");

                Single[] SrvLog = (Single[])caoRobot.Execute("GetSrvData", 5);
/*              Single[] MotorSpeeds = (Single[])caoRobot.Execute("GetSrvData", 1);
                Single[] JointPosition = (Single[])caoRobot.Execute("GetSrvData", 8);
                Single[] ToolendSpeeds = (Single[])caoRobot.Execute("GetSrvData", 17);
                Single[] ToolendPosSpeed = (Single[])caoRobot.Execute("GetSrvData", 18);*/

                /////////////////////////////////////////////////////////////////////////////#caoRobot.Execute("StartLog")

                if (robotConnected)
                {
                    pnlRobotConnected.BackColor = Color.Green;
                    tmrRetryConnect.Stop();
                    tmrRetryConnect.Enabled = false;
                    DateTime currentTime = DateTime.Now;
                    everythingTableAdapter1.Insert(currentTime, DeviceName.Name, currentPos[0], currentPos[1], currentPos[2], currentPos[3], currentPos[4], currentPos[5],
                            Torques[0], Torques[1], Torques[2], Torques[3], Torques[4], Torques[5], Amps[0], Amps[1], Amps[2], Amps[3], Amps[4], Amps[5]);

                    

                    //Iterate through all the values of the position
                    /* int i = 0;
                     foreach (float n in tempSingle)
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
                         if (i < 6)
                         {
                             currentPos[i] = n;
                             destPos[i] = 0;
                         }
                         i++;
                         // add each element to the label for displaying.
                         buildPos = buildPos + ", " + n.ToString();
                     }*/
/*
                    for (i = 0; i <= 6; i++)
                    {
                        getDensoData.Joints[i].Angle = angleSingle[i];
                        getDensoData.Joints[i].amps.Value = Amps[i];
                        getDensoData.Joints[i].Torque.Value = Torques[i];
                        insertRtn =  jointsTableAdapter1.Insert(DeviceName.Name, currentTime, i, Torques[i], Amps[i], angleSingle[i], MotorSpeeds[i]);

                        // add each element to the label for displaying.
                        buildPos = buildPos + ", " + angleSingle[i].ToString();
                        buildPos = buildPos + ", " + Amps[i].ToString();
                        buildPos = buildPos + ", " + Torques[i].ToString();
                    }*/


                    //controllerTableAdapter1.Insert(currentTime, currentPos[0], currentPos[1], currentPos[2], currentPos[3], currentPos[4], currentPos[5], 0.0, 0.0, tempError, DeviceName.Name, destPos[0], destPos[1], destPos[2], destPos[3], destPos[4], destPos[5]);
/*                    everythingTableAdapter1.Insert(currentTime, DeviceName.Name, currentPos[0], currentPos[1], currentPos[2], currentPos[3], currentPos[4], currentPos[5],
                            destPos[0], destPos[1], destPos[2], destPos[3], destPos[4], destPos[5], ToolendSpeeds[0], tempError, 0.0,
                            Torques[0], Torques[1], Torques[2], Torques[3], Torques[4], Torques[5], Amps[0], Amps[1], Amps[2], Amps[3], Amps[4], Amps[5],
                            angleSingle[0], angleSingle[1], angleSingle[2], angleSingle[3], angleSingle[4], angleSingle[5], ToolendPosSpeed[0], ToolendPosSpeed[1],
                            ToolendPosSpeed[2], ToolendPosSpeed[3], ToolendPosSpeed[4], ToolendPosSpeed[5]);*/


                    /*                    getDensoData.Joints[1].Name = "joint_1";
                                        getDensoData.Joints[2].Name = "joint_2";
                                        getDensoData.Joints[3].Name = "joint_3";
                                        getDensoData.Joints[4].Name = "joint_4";
                                        getDensoData.Joints[5].Name = "joint_5";
                                        getDensoData.Joints[6].Name = "joint_6";
                    */
                    //Send collected data to que
                    //queDensoData.Enqueue(getDensoData);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Occurred - " + DeviceName.Name + " Should Disconnect  :" + ex.Message);
                tmrPulling.Enabled = false;
                robotConnected = false;
                pnlRobotConnected.BackColor = Color.Red;
                tmrRetryConnect.Enabled = true;
                tmrRetryConnect.Start();
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
