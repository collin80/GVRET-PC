namespace GVRET
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnConnect = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frameID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frameExt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frameBus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pbActivity = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPortNum = new System.Windows.Forms.ComboBox();
            this.btnContLog = new System.Windows.Forms.Button();
            this.cbCAN1Speed = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCAN2Speed = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rEToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActivity)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DtrEnable = true;
            this.serialPort1.PortName = "COM5";
            this.serialPort1.RtsEnable = true;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(867, 45);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(115, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect To Dongle";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(708, 177);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(136, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Enable Auto Capture";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(845, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Clear List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(708, 206);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(156, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Auto Scroll Display Window";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(705, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "# of frames captured:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(819, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "TEMP";
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(707, 353);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(269, 23);
            this.btnSaveLog.TabIndex = 15;
            this.btnSaveLog.Text = "Save Displayed Data to File";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "CSV Files|*.csv|Text File|*.txt";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Timestamp,
            this.frameID,
            this.frameExt,
            this.frameBus,
            this.Length,
            this.data});
            this.dataGridView1.Location = new System.Drawing.Point(12, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(683, 359);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // Timestamp
            // 
            this.Timestamp.FillWeight = 130F;
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.Width = 130;
            // 
            // frameID
            // 
            this.frameID.FillWeight = 75F;
            this.frameID.HeaderText = "ID";
            this.frameID.Name = "frameID";
            this.frameID.Width = 75;
            // 
            // frameExt
            // 
            this.frameExt.HeaderText = "Ext";
            this.frameExt.Name = "frameExt";
            this.frameExt.Width = 50;
            // 
            // frameBus
            // 
            this.frameBus.HeaderText = "Bus";
            this.frameBus.Name = "frameBus";
            this.frameBus.Width = 50;
            // 
            // Length
            // 
            this.Length.HeaderText = "Len";
            this.Length.Name = "Length";
            this.Length.Width = 50;
            // 
            // data
            // 
            this.data.HeaderText = "Data";
            this.data.Name = "data";
            this.data.Width = 280;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(819, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "TEMP";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(706, 263);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "# of frames displaying:";
            // 
            // pbActivity
            // 
            this.pbActivity.BackColor = System.Drawing.Color.White;
            this.pbActivity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbActivity.Location = new System.Drawing.Point(12, 424);
            this.pbActivity.Name = "pbActivity";
            this.pbActivity.Size = new System.Drawing.Size(969, 117);
            this.pbActivity.TabIndex = 34;
            this.pbActivity.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(462, 408);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Activity Graph";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(703, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "COM Port:";
            // 
            // cbPortNum
            // 
            this.cbPortNum.FormattingEnabled = true;
            this.cbPortNum.Location = new System.Drawing.Point(771, 45);
            this.cbPortNum.Name = "cbPortNum";
            this.cbPortNum.Size = new System.Drawing.Size(90, 21);
            this.cbPortNum.TabIndex = 6;
            // 
            // btnContLog
            // 
            this.btnContLog.Location = new System.Drawing.Point(706, 323);
            this.btnContLog.Name = "btnContLog";
            this.btnContLog.Size = new System.Drawing.Size(270, 24);
            this.btnContLog.TabIndex = 40;
            this.btnContLog.Text = "Start Continuous Logging";
            this.btnContLog.UseVisualStyleBackColor = true;
            this.btnContLog.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // cbCAN1Speed
            // 
            this.cbCAN1Speed.FormattingEnabled = true;
            this.cbCAN1Speed.Items.AddRange(new object[] {
            "DISABLED",
            "125K",
            "250K",
            "500K",
            "1M"});
            this.cbCAN1Speed.Location = new System.Drawing.Point(746, 98);
            this.cbCAN1Speed.Name = "cbCAN1Speed";
            this.cbCAN1Speed.Size = new System.Drawing.Size(90, 21);
            this.cbCAN1Speed.TabIndex = 42;
            this.cbCAN1Speed.SelectedIndexChanged += new System.EventHandler(this.cbCAN1Speed_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(705, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "CAN0";
            // 
            // cbCAN2Speed
            // 
            this.cbCAN2Speed.FormattingEnabled = true;
            this.cbCAN2Speed.Items.AddRange(new object[] {
            "DISABLED",
            "125K",
            "250K",
            "500K",
            "1M"});
            this.cbCAN2Speed.Location = new System.Drawing.Point(880, 98);
            this.cbCAN2Speed.Name = "cbCAN2Speed";
            this.cbCAN2Speed.Size = new System.Drawing.Size(90, 21);
            this.cbCAN2Speed.TabIndex = 44;
            this.cbCAN2Speed.SelectedIndexChanged += new System.EventHandler(this.cbCAN2Speed_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(842, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "CAN1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(813, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "CAN SPEED:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(708, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(274, 23);
            this.button2.TabIndex = 46;
            this.button2.Text = "Set Speeds";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.rEToolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(987, 24);
            this.menuStrip1.TabIndex = 47;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.fileToolStripMenuItem.Text = "&Flow View";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(19, 20);
            this.toolStripMenuItem1.Text = "&";
            // 
            // rEToolsToolStripMenuItem
            // 
            this.rEToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleToggleToolStripMenuItem,
            this.multiToggleToolStripMenuItem,
            this.rampToolStripMenuItem,
            this.taperToolStripMenuItem});
            this.rEToolsToolStripMenuItem.Name = "rEToolsToolStripMenuItem";
            this.rEToolsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.rEToolsToolStripMenuItem.Text = "&RE Tools";
            // 
            // singleToggleToolStripMenuItem
            // 
            this.singleToggleToolStripMenuItem.Name = "singleToggleToolStripMenuItem";
            this.singleToggleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.singleToggleToolStripMenuItem.Text = "&Single Toggle";
            // 
            // multiToggleToolStripMenuItem
            // 
            this.multiToggleToolStripMenuItem.Name = "multiToggleToolStripMenuItem";
            this.multiToggleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.multiToggleToolStripMenuItem.Text = "&Multi Toggle";
            // 
            // rampToolStripMenuItem
            // 
            this.rampToolStripMenuItem.Name = "rampToolStripMenuItem";
            this.rampToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rampToolStripMenuItem.Text = "&Ramp";
            // 
            // taperToolStripMenuItem
            // 
            this.taperToolStripMenuItem.Name = "taperToolStripMenuItem";
            this.taperToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.taperToolStripMenuItem.Text = "&Taper";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 548);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbCAN2Speed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCAN1Speed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnContLog);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pbActivity);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnSaveLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbPortNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Generalized Vehicle Reverse Engineering Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActivity)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pbActivity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPortNum;
        private System.Windows.Forms.Button btnContLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn frameID;
        private System.Windows.Forms.DataGridViewTextBoxColumn frameExt;
        private System.Windows.Forms.DataGridViewTextBoxColumn frameBus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.ComboBox cbCAN1Speed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCAN2Speed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rEToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleToggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiToggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taperToolStripMenuItem;
    }
}

