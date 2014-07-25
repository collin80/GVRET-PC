namespace GVRET
{
    partial class DiscreteStateForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.treeMatches = new System.Windows.Forms.TreeView();
            this.btnReset = new System.Windows.Forms.Button();
            this.ckInteractive = new System.Windows.Forms.CheckBox();
            this.numOutput = new System.Windows.Forms.NumericUpDown();
            this.numSeconds = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbInstruct = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numStates = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStates)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Potential Matches:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // treeMatches
            // 
            this.treeMatches.Location = new System.Drawing.Point(7, 23);
            this.treeMatches.Name = "treeMatches";
            this.treeMatches.Size = new System.Drawing.Size(252, 161);
            this.treeMatches.TabIndex = 26;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(7, 268);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(252, 23);
            this.btnReset.TabIndex = 29;
            this.btnReset.Text = "Let\'s Go!";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ckInteractive
            // 
            this.ckInteractive.AutoSize = true;
            this.ckInteractive.Checked = true;
            this.ckInteractive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckInteractive.Location = new System.Drawing.Point(7, 191);
            this.ckInteractive.Name = "ckInteractive";
            this.ckInteractive.Size = new System.Drawing.Size(106, 17);
            this.ckInteractive.TabIndex = 30;
            this.ckInteractive.Text = "Interactive Mode";
            this.ckInteractive.UseVisualStyleBackColor = true;
            // 
            // numOutput
            // 
            this.numOutput.Enabled = false;
            this.numOutput.Location = new System.Drawing.Point(7, 232);
            this.numOutput.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numOutput.Name = "numOutput";
            this.numOutput.Size = new System.Drawing.Size(83, 20);
            this.numOutput.TabIndex = 31;
            // 
            // numSeconds
            // 
            this.numSeconds.DecimalPlaces = 2;
            this.numSeconds.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numSeconds.Location = new System.Drawing.Point(96, 232);
            this.numSeconds.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numSeconds.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numSeconds.Name = "numSeconds";
            this.numSeconds.Size = new System.Drawing.Size(80, 20);
            this.numSeconds.TabIndex = 32;
            this.numSeconds.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Output to toggle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Toggle Freq";
            // 
            // lbInstruct
            // 
            this.lbInstruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInstruct.ForeColor = System.Drawing.Color.Red;
            this.lbInstruct.Location = new System.Drawing.Point(7, 298);
            this.lbInstruct.Name = "lbInstruct";
            this.lbInstruct.Size = new System.Drawing.Size(259, 57);
            this.lbInstruct.TabIndex = 35;
            this.lbInstruct.Text = "WAIT";
            this.lbInstruct.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numStates
            // 
            this.numStates.Enabled = false;
            this.numStates.Location = new System.Drawing.Point(182, 232);
            this.numStates.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numStates.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numStates.Name = "numStates";
            this.numStates.Size = new System.Drawing.Size(77, 20);
            this.numStates.TabIndex = 36;
            this.numStates.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "# of States";
            // 
            // DiscreteStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 355);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numStates);
            this.Controls.Add(this.lbInstruct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numSeconds);
            this.Controls.Add(this.numOutput);
            this.Controls.Add(this.ckInteractive);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeMatches);
            this.Name = "DiscreteStateForm";
            this.Text = "Single / Multi State";
            this.Load += new System.EventHandler(this.DiscreteStateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeMatches;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox ckInteractive;
        private System.Windows.Forms.NumericUpDown numOutput;
        private System.Windows.Forms.NumericUpDown numSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbInstruct;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numStates;
        private System.Windows.Forms.Label label4;
    }
}