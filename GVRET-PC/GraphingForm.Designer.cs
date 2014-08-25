namespace GVRET
{
    partial class GraphingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.listFrameIDs = new System.Windows.Forms.ListBox();
            this.display = new GraphLib.PlotterDisplayEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMask1 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtID1 = new System.Windows.Forms.TextBox();
            this.btnRefresh1 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtScale1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBias1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pbColor1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtByte1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRefresh2 = new System.Windows.Forms.Button();
            this.txtMask2 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtID2 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtScale2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBias2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pbColor2 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtByte2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefresh3 = new System.Windows.Forms.Button();
            this.txtMask3 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtID3 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtScale3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBias3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pbColor3 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtByte3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRefresh4 = new System.Windows.Forms.Button();
            this.txtMask4 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtID4 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtScale4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBias4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pbColor4 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtByte4 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor3)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Frame IDs Found:";
            // 
            // listFrameIDs
            // 
            this.listFrameIDs.FormattingEnabled = true;
            this.listFrameIDs.Location = new System.Drawing.Point(3, 25);
            this.listFrameIDs.Name = "listFrameIDs";
            this.listFrameIDs.Size = new System.Drawing.Size(105, 329);
            this.listFrameIDs.Sorted = true;
            this.listFrameIDs.TabIndex = 3;
            this.listFrameIDs.SelectedIndexChanged += new System.EventHandler(this.listFrameIDs_SelectedIndexChanged);
            // 
            // display
            // 
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BackgroundColorBot = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.display.BackgroundColorTop = System.Drawing.Color.Navy;
            this.display.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.display.DashedGridColor = System.Drawing.Color.Blue;
            this.display.DoubleBuffering = true;
            this.display.Location = new System.Drawing.Point(114, 5);
            this.display.Name = "display";
            this.display.PlaySpeed = 0F;
            this.display.Size = new System.Drawing.Size(888, 349);
            this.display.SolidGridColor = System.Drawing.Color.Blue;
            this.display.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMask1);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtID1);
            this.groupBox1.Controls.Add(this.btnRefresh1);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtScale1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtBias1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.pbColor1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtByte1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(166, 360);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 184);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graph 1";
            // 
            // txtMask1
            // 
            this.txtMask1.Location = new System.Drawing.Point(36, 60);
            this.txtMask1.Name = "txtMask1";
            this.txtMask1.Size = new System.Drawing.Size(100, 20);
            this.txtMask1.TabIndex = 11;
            this.txtMask1.Text = "FFFFFFFF";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 60);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 13);
            this.label19.TabIndex = 10;
            this.label19.Text = "Mask:";
            // 
            // txtID1
            // 
            this.txtID1.Location = new System.Drawing.Point(36, 15);
            this.txtID1.Name = "txtID1";
            this.txtID1.Size = new System.Drawing.Size(100, 20);
            this.txtID1.TabIndex = 9;
            // 
            // btnRefresh1
            // 
            this.btnRefresh1.Location = new System.Drawing.Point(5, 152);
            this.btnRefresh1.Name = "btnRefresh1";
            this.btnRefresh1.Size = new System.Drawing.Size(131, 23);
            this.btnRefresh1.TabIndex = 11;
            this.btnRefresh1.Text = "Refresh";
            this.btnRefresh1.UseVisualStyleBackColor = true;
            this.btnRefresh1.Click += new System.EventHandler(this.btnRefresh1_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(21, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "ID:";
            // 
            // txtScale1
            // 
            this.txtScale1.Location = new System.Drawing.Point(36, 105);
            this.txtScale1.Name = "txtScale1";
            this.txtScale1.Size = new System.Drawing.Size(100, 20);
            this.txtScale1.TabIndex = 7;
            this.txtScale1.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Scale:";
            // 
            // txtBias1
            // 
            this.txtBias1.Location = new System.Drawing.Point(36, 82);
            this.txtBias1.Name = "txtBias1";
            this.txtBias1.Size = new System.Drawing.Size(100, 20);
            this.txtBias1.TabIndex = 5;
            this.txtBias1.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Bias:";
            // 
            // pbColor1
            // 
            this.pbColor1.BackColor = System.Drawing.Color.Red;
            this.pbColor1.Location = new System.Drawing.Point(36, 128);
            this.pbColor1.Name = "pbColor1";
            this.pbColor1.Size = new System.Drawing.Size(100, 20);
            this.pbColor1.TabIndex = 3;
            this.pbColor1.TabStop = false;
            this.pbColor1.Click += new System.EventHandler(this.pbColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Color:";
            // 
            // txtByte1
            // 
            this.txtByte1.Location = new System.Drawing.Point(36, 38);
            this.txtByte1.Name = "txtByte1";
            this.txtByte1.Size = new System.Drawing.Size(100, 20);
            this.txtByte1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bytes:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRefresh2);
            this.groupBox2.Controls.Add(this.txtMask2);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.txtID2);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtScale2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtBias2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pbColor2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtByte2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(344, 361);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 183);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graph 2";
            // 
            // btnRefresh2
            // 
            this.btnRefresh2.Location = new System.Drawing.Point(6, 152);
            this.btnRefresh2.Name = "btnRefresh2";
            this.btnRefresh2.Size = new System.Drawing.Size(131, 23);
            this.btnRefresh2.TabIndex = 12;
            this.btnRefresh2.Text = "Refresh";
            this.btnRefresh2.UseVisualStyleBackColor = true;
            this.btnRefresh2.Click += new System.EventHandler(this.btnRefresh1_Click);
            // 
            // txtMask2
            // 
            this.txtMask2.Location = new System.Drawing.Point(36, 60);
            this.txtMask2.Name = "txtMask2";
            this.txtMask2.Size = new System.Drawing.Size(100, 20);
            this.txtMask2.TabIndex = 13;
            this.txtMask2.Text = "FFFFFFFF";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 60);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(36, 13);
            this.label23.TabIndex = 12;
            this.label23.Text = "Mask:";
            // 
            // txtID2
            // 
            this.txtID2.Location = new System.Drawing.Point(36, 15);
            this.txtID2.Name = "txtID2";
            this.txtID2.Size = new System.Drawing.Size(100, 20);
            this.txtID2.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 15);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "ID:";
            // 
            // txtScale2
            // 
            this.txtScale2.Location = new System.Drawing.Point(36, 106);
            this.txtScale2.Name = "txtScale2";
            this.txtScale2.Size = new System.Drawing.Size(100, 20);
            this.txtScale2.TabIndex = 7;
            this.txtScale2.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Scale:";
            // 
            // txtBias2
            // 
            this.txtBias2.Location = new System.Drawing.Point(36, 83);
            this.txtBias2.Name = "txtBias2";
            this.txtBias2.Size = new System.Drawing.Size(100, 20);
            this.txtBias2.TabIndex = 5;
            this.txtBias2.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Bias:";
            // 
            // pbColor2
            // 
            this.pbColor2.BackColor = System.Drawing.Color.Lime;
            this.pbColor2.Location = new System.Drawing.Point(36, 129);
            this.pbColor2.Name = "pbColor2";
            this.pbColor2.Size = new System.Drawing.Size(100, 20);
            this.pbColor2.TabIndex = 3;
            this.pbColor2.TabStop = false;
            this.pbColor2.Click += new System.EventHandler(this.pbColor_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Color:";
            // 
            // txtByte2
            // 
            this.txtByte2.Location = new System.Drawing.Point(36, 38);
            this.txtByte2.Name = "txtByte2";
            this.txtByte2.Size = new System.Drawing.Size(100, 20);
            this.txtByte2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bytes:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefresh3);
            this.groupBox3.Controls.Add(this.txtMask3);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.txtID3);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.txtScale3);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtBias3);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.pbColor3);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtByte3);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(522, 361);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(141, 183);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Graph 3";
            // 
            // btnRefresh3
            // 
            this.btnRefresh3.Location = new System.Drawing.Point(6, 151);
            this.btnRefresh3.Name = "btnRefresh3";
            this.btnRefresh3.Size = new System.Drawing.Size(131, 23);
            this.btnRefresh3.TabIndex = 14;
            this.btnRefresh3.Text = "Refresh";
            this.btnRefresh3.UseVisualStyleBackColor = true;
            this.btnRefresh3.Click += new System.EventHandler(this.btnRefresh1_Click);
            // 
            // txtMask3
            // 
            this.txtMask3.Location = new System.Drawing.Point(36, 60);
            this.txtMask3.Name = "txtMask3";
            this.txtMask3.Size = new System.Drawing.Size(100, 20);
            this.txtMask3.TabIndex = 13;
            this.txtMask3.Text = "FFFFFFFF";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 60);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(36, 13);
            this.label24.TabIndex = 12;
            this.label24.Text = "Mask:";
            // 
            // txtID3
            // 
            this.txtID3.Location = new System.Drawing.Point(36, 15);
            this.txtID3.Name = "txtID3";
            this.txtID3.Size = new System.Drawing.Size(100, 20);
            this.txtID3.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 15);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(21, 13);
            this.label21.TabIndex = 11;
            this.label21.Text = "ID:";
            // 
            // txtScale3
            // 
            this.txtScale3.Location = new System.Drawing.Point(36, 106);
            this.txtScale3.Name = "txtScale3";
            this.txtScale3.Size = new System.Drawing.Size(100, 20);
            this.txtScale3.TabIndex = 7;
            this.txtScale3.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Scale:";
            // 
            // txtBias3
            // 
            this.txtBias3.Location = new System.Drawing.Point(36, 83);
            this.txtBias3.Name = "txtBias3";
            this.txtBias3.Size = new System.Drawing.Size(100, 20);
            this.txtBias3.TabIndex = 5;
            this.txtBias3.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Bias:";
            // 
            // pbColor3
            // 
            this.pbColor3.BackColor = System.Drawing.Color.Blue;
            this.pbColor3.Location = new System.Drawing.Point(36, 129);
            this.pbColor3.Name = "pbColor3";
            this.pbColor3.Size = new System.Drawing.Size(100, 20);
            this.pbColor3.TabIndex = 3;
            this.pbColor3.TabStop = false;
            this.pbColor3.Click += new System.EventHandler(this.pbColor_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Color:";
            // 
            // txtByte3
            // 
            this.txtByte3.Location = new System.Drawing.Point(36, 38);
            this.txtByte3.Name = "txtByte3";
            this.txtByte3.Size = new System.Drawing.Size(100, 20);
            this.txtByte3.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Bytes:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRefresh4);
            this.groupBox4.Controls.Add(this.txtMask4);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.txtID4);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.txtScale4);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtBias4);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.pbColor4);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.txtByte4);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Location = new System.Drawing.Point(699, 361);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 183);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Graph 4";
            // 
            // btnRefresh4
            // 
            this.btnRefresh4.Location = new System.Drawing.Point(6, 151);
            this.btnRefresh4.Name = "btnRefresh4";
            this.btnRefresh4.Size = new System.Drawing.Size(131, 23);
            this.btnRefresh4.TabIndex = 15;
            this.btnRefresh4.Text = "Refresh";
            this.btnRefresh4.UseVisualStyleBackColor = true;
            this.btnRefresh4.Click += new System.EventHandler(this.btnRefresh1_Click);
            // 
            // txtMask4
            // 
            this.txtMask4.Location = new System.Drawing.Point(36, 61);
            this.txtMask4.Name = "txtMask4";
            this.txtMask4.Size = new System.Drawing.Size(100, 20);
            this.txtMask4.TabIndex = 13;
            this.txtMask4.Text = "FFFFFFFF";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 61);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "Mask:";
            // 
            // txtID4
            // 
            this.txtID4.Location = new System.Drawing.Point(36, 15);
            this.txtID4.Name = "txtID4";
            this.txtID4.Size = new System.Drawing.Size(100, 20);
            this.txtID4.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(21, 13);
            this.label22.TabIndex = 11;
            this.label22.Text = "ID:";
            // 
            // txtScale4
            // 
            this.txtScale4.Location = new System.Drawing.Point(36, 106);
            this.txtScale4.Name = "txtScale4";
            this.txtScale4.Size = new System.Drawing.Size(100, 20);
            this.txtScale4.TabIndex = 7;
            this.txtScale4.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Scale:";
            // 
            // txtBias4
            // 
            this.txtBias4.Location = new System.Drawing.Point(36, 84);
            this.txtBias4.Name = "txtBias4";
            this.txtBias4.Size = new System.Drawing.Size(100, 20);
            this.txtBias4.TabIndex = 5;
            this.txtBias4.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Bias:";
            // 
            // pbColor4
            // 
            this.pbColor4.BackColor = System.Drawing.Color.Fuchsia;
            this.pbColor4.Location = new System.Drawing.Point(36, 129);
            this.pbColor4.Name = "pbColor4";
            this.pbColor4.Size = new System.Drawing.Size(100, 20);
            this.pbColor4.TabIndex = 3;
            this.pbColor4.TabStop = false;
            this.pbColor4.Click += new System.EventHandler(this.pbColor_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 129);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Color:";
            // 
            // txtByte4
            // 
            this.txtByte4.Location = new System.Drawing.Point(36, 38);
            this.txtByte4.Name = "txtByte4";
            this.txtByte4.Size = new System.Drawing.Size(100, 20);
            this.txtByte4.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Bytes:";
            // 
            // GraphingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 548);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.display);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listFrameIDs);
            this.Name = "GraphingForm";
            this.Text = "GraphingForm";
            this.Load += new System.EventHandler(this.GraphingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listFrameIDs;
        private GraphLib.PlotterDisplayEx display;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtScale1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBias1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pbColor1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtByte1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtScale2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBias2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbColor2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtByte2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtScale3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBias3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pbColor3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtByte3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtScale4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBias4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pbColor4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtByte4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMask1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtID1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMask2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtID2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtMask3;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtID3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMask4;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtID4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnRefresh1;
        private System.Windows.Forms.Button btnRefresh2;
        private System.Windows.Forms.Button btnRefresh3;
        private System.Windows.Forms.Button btnRefresh4;
    }
}