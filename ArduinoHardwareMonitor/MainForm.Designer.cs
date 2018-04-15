namespace ArduinoHardwareMonitor
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.checkedlistbox_Sensors = new System.Windows.Forms.CheckedListBox();
            this.lbl_Sensors = new System.Windows.Forms.Label();
            this.statusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.slbl_AppStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_SendData = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedlistbox_Sensors
            // 
            this.checkedlistbox_Sensors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedlistbox_Sensors.FormattingEnabled = true;
            this.checkedlistbox_Sensors.Location = new System.Drawing.Point(12, 33);
            this.checkedlistbox_Sensors.Name = "checkedlistbox_Sensors";
            this.checkedlistbox_Sensors.Size = new System.Drawing.Size(285, 274);
            this.checkedlistbox_Sensors.TabIndex = 0;
            this.checkedlistbox_Sensors.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedlistbox_Sensors_ItemCheck);
            // 
            // lbl_Sensors
            // 
            this.lbl_Sensors.AutoSize = true;
            this.lbl_Sensors.Location = new System.Drawing.Point(12, 9);
            this.lbl_Sensors.Name = "lbl_Sensors";
            this.lbl_Sensors.Size = new System.Drawing.Size(90, 13);
            this.lbl_Sensors.TabIndex = 1;
            this.lbl_Sensors.Text = "Watched sensors";
            // 
            // statusStrip_Main
            // 
            this.statusStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slbl_AppStatus});
            this.statusStrip_Main.Location = new System.Drawing.Point(0, 318);
            this.statusStrip_Main.Name = "statusStrip_Main";
            this.statusStrip_Main.Size = new System.Drawing.Size(309, 22);
            this.statusStrip_Main.TabIndex = 2;
            this.statusStrip_Main.Text = "statusStrip1";
            // 
            // slbl_AppStatus
            // 
            this.slbl_AppStatus.Name = "slbl_AppStatus";
            this.slbl_AppStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // timer_SendData
            // 
            this.timer_SendData.Interval = 1000;
            this.timer_SendData.Tick += new System.EventHandler(this.timer_SendData_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "I\'m here!";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Arduino Harware Monitor";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 340);
            this.Controls.Add(this.statusStrip_Main);
            this.Controls.Add(this.lbl_Sensors);
            this.Controls.Add(this.checkedlistbox_Sensors);
            this.Name = "MainForm";
            this.Text = "Arduino Hardware Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.statusStrip_Main.ResumeLayout(false);
            this.statusStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedlistbox_Sensors;
        private System.Windows.Forms.Label lbl_Sensors;
        private System.Windows.Forms.StatusStrip statusStrip_Main;
        private System.Windows.Forms.ToolStripStatusLabel slbl_AppStatus;
        private System.Windows.Forms.Timer timer_SendData;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

