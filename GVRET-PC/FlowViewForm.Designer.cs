namespace GVRET
{
    partial class FlowViewForm
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
            this.listFrameIDs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVal7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVal6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVal5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtVal4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtVal3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVal2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVal1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVal0 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRef7 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRef6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRef5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRef4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRef3 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRef2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRef1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRef0 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ckAutoRef = new System.Windows.Forms.CheckBox();
            this.ckCapture = new System.Windows.Forms.CheckBox();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.canDataGrid1 = new GVRET.CANDataGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlaybackSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // listFrameIDs
            // 
            this.listFrameIDs.FormattingEnabled = true;
            this.listFrameIDs.ItemHeight = 16;
            this.listFrameIDs.Location = new System.Drawing.Point(16, 36);
            this.listFrameIDs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listFrameIDs.Name = "listFrameIDs";
            this.listFrameIDs.Size = new System.Drawing.Size(139, 276);
            this.listFrameIDs.Sorted = true;
            this.listFrameIDs.TabIndex = 1;
            this.listFrameIDs.SelectedIndexChanged += new System.EventHandler(this.listFrameIDs_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Frame IDs Found:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVal7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtVal6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtVal5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtVal4);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtVal3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtVal2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtVal1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtVal0);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(404, 176);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(205, 130);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Values:";
            // 
            // txtVal7
            // 
            this.txtVal7.Location = new System.Drawing.Point(153, 91);
            this.txtVal7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal7.Name = "txtVal7";
            this.txtVal7.Size = new System.Drawing.Size(37, 22);
            this.txtVal7.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "7";
            // 
            // txtVal6
            // 
            this.txtVal6.Location = new System.Drawing.Point(107, 91);
            this.txtVal6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal6.Name = "txtVal6";
            this.txtVal6.Size = new System.Drawing.Size(37, 22);
            this.txtVal6.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(107, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "6";
            // 
            // txtVal5
            // 
            this.txtVal5.Location = new System.Drawing.Point(59, 91);
            this.txtVal5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal5.Name = "txtVal5";
            this.txtVal5.Size = new System.Drawing.Size(37, 22);
            this.txtVal5.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 75);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "5";
            // 
            // txtVal4
            // 
            this.txtVal4.Location = new System.Drawing.Point(9, 91);
            this.txtVal4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal4.Name = "txtVal4";
            this.txtVal4.Size = new System.Drawing.Size(37, 22);
            this.txtVal4.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 75);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "4";
            // 
            // txtVal3
            // 
            this.txtVal3.Location = new System.Drawing.Point(153, 46);
            this.txtVal3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal3.Name = "txtVal3";
            this.txtVal3.Size = new System.Drawing.Size(37, 22);
            this.txtVal3.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "3";
            // 
            // txtVal2
            // 
            this.txtVal2.Location = new System.Drawing.Point(107, 46);
            this.txtVal2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal2.Name = "txtVal2";
            this.txtVal2.Size = new System.Drawing.Size(37, 22);
            this.txtVal2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "2";
            // 
            // txtVal1
            // 
            this.txtVal1.Location = new System.Drawing.Point(59, 46);
            this.txtVal1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal1.Name = "txtVal1";
            this.txtVal1.Size = new System.Drawing.Size(37, 22);
            this.txtVal1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "1";
            // 
            // txtVal0
            // 
            this.txtVal0.Location = new System.Drawing.Point(9, 46);
            this.txtVal0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVal0.Name = "txtVal0";
            this.txtVal0.Size = new System.Drawing.Size(37, 22);
            this.txtVal0.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRef7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtRef6);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtRef5);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtRef4);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtRef3);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtRef2);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtRef1);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtRef0);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(404, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(205, 130);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reference Values:";
            // 
            // txtRef7
            // 
            this.txtRef7.Location = new System.Drawing.Point(153, 91);
            this.txtRef7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef7.Name = "txtRef7";
            this.txtRef7.Size = new System.Drawing.Size(37, 22);
            this.txtRef7.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 75);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "7";
            // 
            // txtRef6
            // 
            this.txtRef6.Location = new System.Drawing.Point(107, 91);
            this.txtRef6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef6.Name = "txtRef6";
            this.txtRef6.Size = new System.Drawing.Size(37, 22);
            this.txtRef6.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(107, 75);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 17);
            this.label11.TabIndex = 12;
            this.label11.Text = "6";
            // 
            // txtRef5
            // 
            this.txtRef5.Location = new System.Drawing.Point(59, 91);
            this.txtRef5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef5.Name = "txtRef5";
            this.txtRef5.Size = new System.Drawing.Size(37, 22);
            this.txtRef5.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(59, 75);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 17);
            this.label12.TabIndex = 10;
            this.label12.Text = "5";
            // 
            // txtRef4
            // 
            this.txtRef4.Location = new System.Drawing.Point(9, 91);
            this.txtRef4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef4.Name = "txtRef4";
            this.txtRef4.Size = new System.Drawing.Size(37, 22);
            this.txtRef4.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 75);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 17);
            this.label13.TabIndex = 8;
            this.label13.Text = "4";
            // 
            // txtRef3
            // 
            this.txtRef3.Location = new System.Drawing.Point(153, 46);
            this.txtRef3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef3.Name = "txtRef3";
            this.txtRef3.Size = new System.Drawing.Size(37, 22);
            this.txtRef3.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(153, 28);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 17);
            this.label14.TabIndex = 6;
            this.label14.Text = "3";
            // 
            // txtRef2
            // 
            this.txtRef2.Location = new System.Drawing.Point(107, 46);
            this.txtRef2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef2.Name = "txtRef2";
            this.txtRef2.Size = new System.Drawing.Size(37, 22);
            this.txtRef2.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(107, 28);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 17);
            this.label15.TabIndex = 4;
            this.label15.Text = "2";
            // 
            // txtRef1
            // 
            this.txtRef1.Location = new System.Drawing.Point(59, 46);
            this.txtRef1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef1.Name = "txtRef1";
            this.txtRef1.Size = new System.Drawing.Size(37, 22);
            this.txtRef1.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(59, 28);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 17);
            this.label16.TabIndex = 2;
            this.label16.Text = "1";
            // 
            // txtRef0
            // 
            this.txtRef0.Location = new System.Drawing.Point(9, 46);
            this.txtRef0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRef0.Name = "txtRef0";
            this.txtRef0.Size = new System.Drawing.Size(37, 22);
            this.txtRef0.TabIndex = 1;
            this.txtRef0.TextChanged += new System.EventHandler(this.txtRef0_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 28);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 17);
            this.label17.TabIndex = 0;
            this.label17.Text = "0";
            // 
            // ckAutoRef
            // 
            this.ckAutoRef.AutoSize = true;
            this.ckAutoRef.Location = new System.Drawing.Point(404, 16);
            this.ckAutoRef.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckAutoRef.Name = "ckAutoRef";
            this.ckAutoRef.Size = new System.Drawing.Size(129, 21);
            this.ckAutoRef.TabIndex = 17;
            this.ckAutoRef.Text = "Auto Reference";
            this.ckAutoRef.UseVisualStyleBackColor = true;
            // 
            // ckCapture
            // 
            this.ckCapture.AutoSize = true;
            this.ckCapture.Enabled = false;
            this.ckCapture.Location = new System.Drawing.Point(216, 254);
            this.ckCapture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckCapture.Name = "ckCapture";
            this.ckCapture.Size = new System.Drawing.Size(124, 21);
            this.ckCapture.TabIndex = 18;
            this.ckCapture.Text = "Capture Traffic";
            this.ckCapture.UseVisualStyleBackColor = true;
            this.ckCapture.CheckedChanged += new System.EventHandler(this.ckCapture_CheckedChanged);
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
            this.groupBox3.Location = new System.Drawing.Point(175, 16);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(212, 230);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Playback Control";
            // 
            // ckLoop
            // 
            this.ckLoop.Location = new System.Drawing.Point(37, 124);
            this.ckLoop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckLoop.Name = "ckLoop";
            this.ckLoop.Size = new System.Drawing.Size(143, 30);
            this.ckLoop.TabIndex = 31;
            this.ckLoop.Text = "Loop Playback";
            this.ckLoop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckLoop.UseVisualStyleBackColor = true;
            // 
            // btnReverse
            // 
            this.btnReverse.Image = global::GVRET.Properties.Resources.ReverseHS;
            this.btnReverse.Location = new System.Drawing.Point(75, 28);
            this.btnReverse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(31, 28);
            this.btnReverse.TabIndex = 29;
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // lblFrames
            // 
            this.lblFrames.Location = new System.Drawing.Point(12, 193);
            this.lblFrames.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrames.Name = "lblFrames";
            this.lblFrames.Size = new System.Drawing.Size(185, 16);
            this.lblFrames.TabIndex = 28;
            this.lblFrames.Text = "x of y";
            this.lblFrames.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(16, 167);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(177, 16);
            this.label19.TabIndex = 27;
            this.label19.Text = "Current Frame:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnForwardOne
            // 
            this.btnForwardOne.Image = global::GVRET.Properties.Resources.NavForward;
            this.btnForwardOne.Location = new System.Drawing.Point(171, 28);
            this.btnForwardOne.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnForwardOne.Name = "btnForwardOne";
            this.btnForwardOne.Size = new System.Drawing.Size(31, 28);
            this.btnForwardOne.TabIndex = 26;
            this.btnForwardOne.UseVisualStyleBackColor = true;
            this.btnForwardOne.Click += new System.EventHandler(this.btnForwardOne_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::GVRET.Properties.Resources.PlayHS;
            this.btnPlay.Location = new System.Drawing.Point(139, 28);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(31, 28);
            this.btnPlay.TabIndex = 25;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::GVRET.Properties.Resources.StopHS;
            this.btnStop.Location = new System.Drawing.Point(107, 28);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(31, 28);
            this.btnStop.TabIndex = 24;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = global::GVRET.Properties.Resources.PauseHS;
            this.btnPause.Location = new System.Drawing.Point(41, 28);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(31, 28);
            this.btnPause.TabIndex = 23;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnBackOne
            // 
            this.btnBackOne.Image = global::GVRET.Properties.Resources.NavBack;
            this.btnBackOne.Location = new System.Drawing.Point(9, 28);
            this.btnBackOne.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBackOne.Name = "btnBackOne";
            this.btnBackOne.Size = new System.Drawing.Size(31, 28);
            this.btnBackOne.TabIndex = 22;
            this.btnBackOne.UseVisualStyleBackColor = true;
            this.btnBackOne.Click += new System.EventHandler(this.btnBackOne_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(33, 73);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(146, 17);
            this.label18.TabIndex = 20;
            this.label18.Text = "Playback Speed (ms):";
            // 
            // numPlaybackSpeed
            // 
            this.numPlaybackSpeed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPlaybackSpeed.Location = new System.Drawing.Point(37, 92);
            this.numPlaybackSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numPlaybackSpeed.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numPlaybackSpeed.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPlaybackSpeed.Name = "numPlaybackSpeed";
            this.numPlaybackSpeed.Size = new System.Drawing.Size(135, 22);
            this.numPlaybackSpeed.TabIndex = 21;
            this.numPlaybackSpeed.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPlaybackSpeed.ValueChanged += new System.EventHandler(this.numPlaybackSpeed_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // canDataGrid1
            // 
            this.canDataGrid1.Location = new System.Drawing.Point(623, -1);
            this.canDataGrid1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.canDataGrid1.Name = "canDataGrid1";
            this.canDataGrid1.Size = new System.Drawing.Size(360, 314);
            this.canDataGrid1.TabIndex = 0;
            this.canDataGrid1.Load += new System.EventHandler(this.canDataGrid1_Load);
            // 
            // FlowViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 324);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ckCapture);
            this.Controls.Add(this.ckAutoRef);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listFrameIDs);
            this.Controls.Add(this.canDataGrid1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FlowViewForm";
            this.Text = "Flow View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlowViewForm_FormClosing);
            this.Load += new System.EventHandler(this.FlowViewForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlaybackSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GVRET.CANDataGrid canDataGrid1;
        private System.Windows.Forms.ListBox listFrameIDs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtVal7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVal6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVal5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtVal4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtVal3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVal2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVal1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVal0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRef7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRef6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRef5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRef4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRef3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRef2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRef1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtRef0;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox ckAutoRef;
        private System.Windows.Forms.CheckBox ckCapture;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numPlaybackSpeed;
        private System.Windows.Forms.Button btnBackOne;
        private System.Windows.Forms.Button btnForwardOne;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblFrames;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox ckLoop;
    }
}