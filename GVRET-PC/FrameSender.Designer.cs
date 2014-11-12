namespace GVRET
{
    partial class FrameSender
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtBus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMutator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTriggerCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bEnabled,
            this.txtBus,
            this.txtID,
            this.txtLen,
            this.Data,
            this.txtTrigger,
            this.txtMutator,
            this.txtTriggerCount});
            this.dataGridView1.Location = new System.Drawing.Point(9, 10);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(759, 352);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // bEnabled
            // 
            this.bEnabled.FillWeight = 50F;
            this.bEnabled.HeaderText = "En";
            this.bEnabled.Name = "bEnabled";
            this.bEnabled.Width = 50;
            // 
            // txtBus
            // 
            this.txtBus.FillWeight = 50F;
            this.txtBus.HeaderText = "Bus";
            this.txtBus.Name = "txtBus";
            this.txtBus.Width = 50;
            // 
            // txtID
            // 
            this.txtID.FillWeight = 50F;
            this.txtID.HeaderText = "ID";
            this.txtID.Name = "txtID";
            this.txtID.Width = 50;
            // 
            // txtLen
            // 
            this.txtLen.FillWeight = 75F;
            this.txtLen.HeaderText = "Len";
            this.txtLen.Name = "txtLen";
            this.txtLen.Width = 50;
            // 
            // Data
            // 
            this.Data.FillWeight = 140F;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.Width = 140;
            // 
            // txtTrigger
            // 
            this.txtTrigger.FillWeight = 155F;
            this.txtTrigger.HeaderText = "Trigger";
            this.txtTrigger.Name = "txtTrigger";
            this.txtTrigger.Width = 155;
            // 
            // txtMutator
            // 
            this.txtMutator.FillWeight = 155F;
            this.txtMutator.HeaderText = "Modifications";
            this.txtMutator.Name = "txtMutator";
            this.txtMutator.Width = 155;
            // 
            // txtTriggerCount
            // 
            this.txtTriggerCount.FillWeight = 60F;
            this.txtTriggerCount.HeaderText = "Count";
            this.txtTriggerCount.Name = "txtTriggerCount";
            this.txtTriggerCount.ReadOnly = true;
            this.txtTriggerCount.Width = 60;
            // 
            // FrameSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 370);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrameSender";
            this.Text = "FrameSender";
            this.Load += new System.EventHandler(this.FrameSender_Load);
            this.Leave += new System.EventHandler(this.FrameSender_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn bEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtBus;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtLen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtTrigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtMutator;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtTriggerCount;
    }
}