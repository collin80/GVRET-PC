namespace GVRET
{
    partial class FileLoadingForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckLoop = new System.Windows.Forms.CheckBox();
            this.btnReverse = new System.Windows.Forms.Button();
            this.lblFrames = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnForwardOne = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnBackOne = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.numPlaybackSpeed = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.ckUseHex = new System.Windows.Forms.CheckBox();
            this.cbCANSend = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlaybackSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckLoop);
            this.groupBox3.Controls.Add(this.btnReverse);
            this.groupBox3.Controls.Add(this.lblFrames);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.btnForwardOne);
            this.groupBox3.Controls.Add(this.btnPlay);
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.btnPause);
            this.groupBox3.Controls.Add(this.btnBackOne);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.numPlaybackSpeed);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(159, 165);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Playback Control";
            // 
            // ckLoop
            // 
            this.ckLoop.Location = new System.Drawing.Point(28, 101);
            this.ckLoop.Name = "ckLoop";
            this.ckLoop.Size = new System.Drawing.Size(107, 24);
            this.ckLoop.TabIndex = 30;
            this.ckLoop.Text = "Loop Playback";
            this.ckLoop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckLoop.UseVisualStyleBackColor = true;
            // 
            // btnReverse
            // 
            this.btnReverse.Image = global::GVRET.Properties.Resources.ReverseHS;
            this.btnReverse.Location = new System.Drawing.Point(56, 23);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(23, 23);
            this.btnReverse.TabIndex = 29;
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // lblFrames
            // 
            this.lblFrames.Location = new System.Drawing.Point(6, 140);
            this.lblFrames.Name = "lblFrames";
            this.lblFrames.Size = new System.Drawing.Size(147, 23);
            this.lblFrames.TabIndex = 28;
            this.lblFrames.Text = "x of y";
            this.lblFrames.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(2, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(151, 21);
            this.label19.TabIndex = 27;
            this.label19.Text = "Current Frame:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnForwardOne
            // 
            this.btnForwardOne.Image = global::GVRET.Properties.Resources.NavForward;
            this.btnForwardOne.Location = new System.Drawing.Point(128, 23);
            this.btnForwardOne.Name = "btnForwardOne";
            this.btnForwardOne.Size = new System.Drawing.Size(23, 23);
            this.btnForwardOne.TabIndex = 26;
            this.btnForwardOne.UseVisualStyleBackColor = true;
            this.btnForwardOne.Click += new System.EventHandler(this.btnForwardOne_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::GVRET.Properties.Resources.PlayHS;
            this.btnPlay.Location = new System.Drawing.Point(104, 23);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(23, 23);
            this.btnPlay.TabIndex = 25;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::GVRET.Properties.Resources.StopHS;
            this.btnStop.Location = new System.Drawing.Point(80, 23);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 23);
            this.btnStop.TabIndex = 24;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = global::GVRET.Properties.Resources.PauseHS;
            this.btnPause.Location = new System.Drawing.Point(31, 23);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(23, 23);
            this.btnPause.TabIndex = 23;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnBackOne
            // 
            this.btnBackOne.Image = global::GVRET.Properties.Resources.NavBack;
            this.btnBackOne.Location = new System.Drawing.Point(7, 23);
            this.btnBackOne.Name = "btnBackOne";
            this.btnBackOne.Size = new System.Drawing.Size(23, 23);
            this.btnBackOne.TabIndex = 22;
            this.btnBackOne.UseVisualStyleBackColor = true;
            this.btnBackOne.Click += new System.EventHandler(this.btnBackOne_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "Playback Speed (us):";
            // 
            // numPlaybackSpeed
            // 
            this.numPlaybackSpeed.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numPlaybackSpeed.Location = new System.Drawing.Point(28, 75);
            this.numPlaybackSpeed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numPlaybackSpeed.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numPlaybackSpeed.Name = "numPlaybackSpeed";
            this.numPlaybackSpeed.Size = new System.Drawing.Size(101, 20);
            this.numPlaybackSpeed.TabIndex = 21;
            this.numPlaybackSpeed.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numPlaybackSpeed.ValueChanged += new System.EventHandler(this.numPlaybackSpeed_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Load From File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "csv";
            this.openFileDialog1.Filter = "GVRET Logs|*.csv|BusMaster Logs|*.log";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 250;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // ckUseHex
            // 
            this.ckUseHex.AutoSize = true;
            this.ckUseHex.Location = new System.Drawing.Point(8, 206);
            this.ckUseHex.Name = "ckUseHex";
            this.ckUseHex.Size = new System.Drawing.Size(134, 17);
            this.ckUseHex.TabIndex = 23;
            this.ckUseHex.Text = "File uses hex encoding";
            this.ckUseHex.UseVisualStyleBackColor = true;
            // 
            // cbCANSend
            // 
            this.cbCANSend.FormattingEnabled = true;
            this.cbCANSend.Items.AddRange(new object[] {
            "NONE",
            "0",
            "1",
            "FROM FILE"});
            this.cbCANSend.Location = new System.Drawing.Point(103, 179);
            this.cbCANSend.Name = "cbCANSend";
            this.cbCANSend.Size = new System.Drawing.Size(68, 21);
            this.cbCANSend.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Send To CANBus";
            // 
            // FileLoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 260);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCANSend);
            this.Controls.Add(this.ckUseHex);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileLoadingForm";
            this.Text = "Load Data";
            this.Load += new System.EventHandler(this.FileLoadingForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlaybackSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Label lblFrames;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnForwardOne;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnBackOne;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numPlaybackSpeed;
        private System.Windows.Forms.CheckBox ckLoop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox ckUseHex;
        private System.Windows.Forms.ComboBox cbCANSend;
        private System.Windows.Forms.Label label1;
    }
}