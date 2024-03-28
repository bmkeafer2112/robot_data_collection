
namespace Smart_Manufacturing
{
    partial class RC7MonitorItem
    {
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
            this.components = new System.ComponentModel.Container();
            this.lblPacketSpeed = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblDeviceType = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.txtErrorMessage = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.gertpos = new System.Windows.Forms.Label();
            this.lblRobotConnected = new System.Windows.Forms.Label();
            this.pnlRobotConnected = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.tmrQueDump = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmrPulling = new System.Windows.Forms.Timer(this.components);
            this.tmrRetryConnect = new System.Windows.Forms.Timer(this.components);
            this.jointsTableAdapter1 = new Smart_Manufacturing.postgresDataSetTableAdapters.jointsTableAdapter();
            this.controllerTableAdapter1 = new Smart_Manufacturing.postgresDataSetTableAdapters.controllerTableAdapter();
            this.everythingTableAdapter1 = new Smart_Manufacturing.postgresDataSetTableAdapters.everythingTableAdapter();
            this.SuspendLayout();
            // 
            // lblPacketSpeed
            // 
            this.lblPacketSpeed.AutoSize = true;
            this.lblPacketSpeed.BackColor = System.Drawing.Color.Black;
            this.lblPacketSpeed.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblPacketSpeed.ForeColor = System.Drawing.Color.White;
            this.lblPacketSpeed.Location = new System.Drawing.Point(619, 19);
            this.lblPacketSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPacketSpeed.Name = "lblPacketSpeed";
            this.lblPacketSpeed.Size = new System.Drawing.Size(41, 20);
            this.lblPacketSpeed.TabIndex = 48;
            this.lblPacketSpeed.Text = "1000";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Black;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(619, 2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 15);
            this.label19.TabIndex = 47;
            this.label19.Text = "Rate (ms)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Black;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(119, -1);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 20);
            this.label16.TabIndex = 45;
            this.label16.Text = "Block:";
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.BackColor = System.Drawing.Color.Black;
            this.lblBlock.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblBlock.ForeColor = System.Drawing.Color.White;
            this.lblBlock.Location = new System.Drawing.Point(119, 17);
            this.lblBlock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(49, 20);
            this.lblBlock.TabIndex = 46;
            this.lblBlock.Text = "Name";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Black;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(269, -1);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 15);
            this.label22.TabIndex = 43;
            this.label22.Text = "Device Name:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Black;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(269, 17);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(98, 20);
            this.lblName.TabIndex = 44;
            this.lblName.Text = "Device Name";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Black;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(182, -1);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 17);
            this.label20.TabIndex = 41;
            this.label20.Text = "Device Type:";
            // 
            // lblDeviceType
            // 
            this.lblDeviceType.AutoSize = true;
            this.lblDeviceType.BackColor = System.Drawing.Color.Black;
            this.lblDeviceType.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblDeviceType.ForeColor = System.Drawing.Color.White;
            this.lblDeviceType.Location = new System.Drawing.Point(182, 17);
            this.lblDeviceType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeviceType.Name = "lblDeviceType";
            this.lblDeviceType.Size = new System.Drawing.Size(49, 20);
            this.lblDeviceType.TabIndex = 42;
            this.lblDeviceType.Text = "Name";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Black;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(73, -1);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 20);
            this.label18.TabIndex = 39;
            this.label18.Text = "Line:";
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.Color.Black;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblLine.ForeColor = System.Drawing.Color.White;
            this.lblLine.Location = new System.Drawing.Point(73, 17);
            this.lblLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(49, 20);
            this.lblLine.TabIndex = 40;
            this.lblLine.Text = "Name";
            // 
            // txtErrorMessage
            // 
            this.txtErrorMessage.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.txtErrorMessage.Location = new System.Drawing.Point(733, 1);
            this.txtErrorMessage.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtErrorMessage.Multiline = true;
            this.txtErrorMessage.Name = "txtErrorMessage";
            this.txtErrorMessage.Size = new System.Drawing.Size(271, 35);
            this.txtErrorMessage.TabIndex = 38;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Black;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(696, 3);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 20);
            this.label17.TabIndex = 37;
            this.label17.Text = "Error:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(547, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 30);
            this.button2.TabIndex = 36;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // gertpos
            // 
            this.gertpos.AutoSize = true;
            this.gertpos.Location = new System.Drawing.Point(577, 30);
            this.gertpos.Name = "gertpos";
            this.gertpos.Size = new System.Drawing.Size(0, 13);
            this.gertpos.TabIndex = 29;
            this.gertpos.Visible = false;
            // 
            // lblRobotConnected
            // 
            this.lblRobotConnected.AutoSize = true;
            this.lblRobotConnected.BackColor = System.Drawing.Color.Black;
            this.lblRobotConnected.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRobotConnected.ForeColor = System.Drawing.Color.White;
            this.lblRobotConnected.Location = new System.Drawing.Point(469, -1);
            this.lblRobotConnected.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRobotConnected.Name = "lblRobotConnected";
            this.lblRobotConnected.Size = new System.Drawing.Size(70, 17);
            this.lblRobotConnected.TabIndex = 30;
            this.lblRobotConnected.Text = "Connected";
            // 
            // pnlRobotConnected
            // 
            this.pnlRobotConnected.BackColor = System.Drawing.Color.Orange;
            this.pnlRobotConnected.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.pnlRobotConnected.Location = new System.Drawing.Point(484, 18);
            this.pnlRobotConnected.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlRobotConnected.Name = "pnlRobotConnected";
            this.pnlRobotConnected.Size = new System.Drawing.Size(54, 16);
            this.pnlRobotConnected.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Black;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(13, -1);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 20);
            this.label15.TabIndex = 34;
            this.label15.Text = "Business:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(366, -1);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 15);
            this.label14.TabIndex = 33;
            this.label14.Text = "IP Address: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(577, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 13);
            this.label13.TabIndex = 28;
            this.label13.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(649, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 27;
            this.label12.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(721, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 26;
            this.label11.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(793, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 18;
            this.label10.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(857, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 25;
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(924, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 24;
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(577, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 23;
            this.label7.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(721, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 22;
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(793, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 21;
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(860, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 20;
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(924, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 19;
            this.label2.Visible = false;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.BackColor = System.Drawing.Color.Black;
            this.lblGroup.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblGroup.ForeColor = System.Drawing.Color.White;
            this.lblGroup.Location = new System.Drawing.Point(13, 17);
            this.lblGroup.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(49, 20);
            this.lblGroup.TabIndex = 32;
            this.lblGroup.Text = "Name";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.BackColor = System.Drawing.Color.Black;
            this.lblIPAddress.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            this.lblIPAddress.ForeColor = System.Drawing.Color.White;
            this.lblIPAddress.Location = new System.Drawing.Point(366, 17);
            this.lblIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(114, 20);
            this.lblIPAddress.TabIndex = 31;
            this.lblIPAddress.Text = "192.168.100.200";
            // 
            // tmrQueDump
            // 
            this.tmrQueDump.Enabled = true;
            this.tmrQueDump.Interval = 10000;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tmrPulling
            // 
            this.tmrPulling.Enabled = true;
            this.tmrPulling.Interval = 1000;
            this.tmrPulling.Tick += new System.EventHandler(this.tmrPulling_Tick);
            // 
            // tmrRetryConnect
            // 
            this.tmrRetryConnect.Enabled = true;
            this.tmrRetryConnect.Interval = 10000;
            this.tmrRetryConnect.Tick += new System.EventHandler(this.tmrRetryConnect_Tick);
            // 
            // jointsTableAdapter1
            // 
            this.jointsTableAdapter1.ClearBeforeFill = true;
            // 
            // controllerTableAdapter1
            // 
            this.controllerTableAdapter1.ClearBeforeFill = true;
            // 
            // everythingTableAdapter1
            // 
            this.everythingTableAdapter1.ClearBeforeFill = true;
            // 
            // RC7MonitorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "RC7MonitorItem";
            this.Size = new System.Drawing.Size(1015, 44);
            this.Load += new System.EventHandler(this.RC7MonitorItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPacketSpeed;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblDeviceType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.TextBox txtErrorMessage;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label gertpos;
        private System.Windows.Forms.Label lblRobotConnected;
        private System.Windows.Forms.Panel pnlRobotConnected;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Timer tmrQueDump;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmrPulling;
        private System.Windows.Forms.Timer tmrRetryConnect;
        private postgresDataSetTableAdapters.controllerTableAdapter controllerTableAdapter1;
        private postgresDataSetTableAdapters.jointsTableAdapter jointsTableAdapter1;
        private postgresDataSetTableAdapters.everythingTableAdapter everythingTableAdapter1;
    }
}
