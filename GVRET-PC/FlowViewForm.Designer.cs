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
            this.canDataGrid1 = new GVRET.CANDataGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // canDataGrid1
            // 
            this.canDataGrid1.Location = new System.Drawing.Point(12, 12);
            this.canDataGrid1.Name = "canDataGrid1";
            this.canDataGrid1.Size = new System.Drawing.Size(270, 255);
            this.canDataGrid1.TabIndex = 0;
            this.canDataGrid1.Load += new System.EventHandler(this.canDataGrid1_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FlowViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.canDataGrid1);
            this.Name = "FlowViewForm";
            this.Text = "FlowViewForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CANDataGrid canDataGrid1;
        private System.Windows.Forms.Button button1;
    }
}