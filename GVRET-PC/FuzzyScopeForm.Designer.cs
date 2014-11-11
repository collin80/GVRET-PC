namespace GVRET
{
    partial class FuzzyScopeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuzzyScopeForm));
            this.ckCapture = new System.Windows.Forms.CheckBox();
            this.listSearchItems = new System.Windows.Forms.ListBox();
            this.treeMatches = new System.Windows.Forms.TreeView();
            this.trackFuzzy = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewItem = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblFuzzy = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackFuzzy)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckCapture
            // 
            this.ckCapture.AutoSize = true;
            this.ckCapture.Enabled = false;
            this.ckCapture.Location = new System.Drawing.Point(248, 318);
            this.ckCapture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckCapture.Name = "ckCapture";
            this.ckCapture.Size = new System.Drawing.Size(124, 21);
            this.ckCapture.TabIndex = 19;
            this.ckCapture.Text = "Capture Traffic";
            this.ckCapture.UseVisualStyleBackColor = true;
            this.ckCapture.CheckedChanged += new System.EventHandler(this.ckCapture_CheckedChanged);
            // 
            // listSearchItems
            // 
            this.listSearchItems.FormattingEnabled = true;
            this.listSearchItems.ItemHeight = 16;
            this.listSearchItems.Location = new System.Drawing.Point(16, 31);
            this.listSearchItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listSearchItems.Name = "listSearchItems";
            this.listSearchItems.Size = new System.Drawing.Size(159, 164);
            this.listSearchItems.TabIndex = 20;
            // 
            // treeMatches
            // 
            this.treeMatches.Location = new System.Drawing.Point(221, 31);
            this.treeMatches.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeMatches.Name = "treeMatches";
            this.treeMatches.Size = new System.Drawing.Size(367, 197);
            this.treeMatches.TabIndex = 21;
            // 
            // trackFuzzy
            // 
            this.trackFuzzy.LargeChange = 25;
            this.trackFuzzy.Location = new System.Drawing.Point(244, 261);
            this.trackFuzzy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackFuzzy.Maximum = 100;
            this.trackFuzzy.Name = "trackFuzzy";
            this.trackFuzzy.Size = new System.Drawing.Size(295, 56);
            this.trackFuzzy.SmallChange = 5;
            this.trackFuzzy.TabIndex = 22;
            this.trackFuzzy.TickFrequency = 10;
            this.trackFuzzy.Scroll += new System.EventHandler(this.trackFuzzy_Scroll);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(244, 241);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Fuzziness Percentage";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "Values to search:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(217, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Potential Matches:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNewItem
            // 
            this.txtNewItem.Location = new System.Drawing.Point(20, 204);
            this.txtNewItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNewItem.Name = "txtNewItem";
            this.txtNewItem.Size = new System.Drawing.Size(115, 22);
            this.txtNewItem.TabIndex = 26;
            this.txtNewItem.TextChanged += new System.EventHandler(this.txtNewItem_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(144, 204);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(32, 28);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(381, 313);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(191, 28);
            this.btnReset.TabIndex = 28;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // lblFuzzy
            // 
            this.lblFuzzy.AutoSize = true;
            this.lblFuzzy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFuzzy.Location = new System.Drawing.Point(547, 261);
            this.lblFuzzy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFuzzy.Name = "lblFuzzy";
            this.lblFuzzy.Size = new System.Drawing.Size(24, 25);
            this.lblFuzzy.TabIndex = 29;
            this.lblFuzzy.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(16, 241);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(220, 132);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Permutations";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(8, 102);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(177, 21);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "Gonzo (Try everything)";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(9, 76);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(136, 21);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Quarters (4, 1/4)";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(9, 48);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(161, 21);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Century (100, 1/100)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(9, 20);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(145, 21);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Decade (10, 1/10)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(599, 16);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(267, 356);
            this.textBox1.TabIndex = 31;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // FuzzyScopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 384);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblFuzzy);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNewItem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackFuzzy);
            this.Controls.Add(this.treeMatches);
            this.Controls.Add(this.listSearchItems);
            this.Controls.Add(this.ckCapture);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FuzzyScopeForm";
            this.Text = "Fuzzy Scope";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FuzzyScopeForm_FormClosing);
            this.Load += new System.EventHandler(this.FuzzyScopeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackFuzzy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckCapture;
        private System.Windows.Forms.ListBox listSearchItems;
        private System.Windows.Forms.TreeView treeMatches;
        private System.Windows.Forms.TrackBar trackFuzzy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewItem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblFuzzy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox1;
    }
}