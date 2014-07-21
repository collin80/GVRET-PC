using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GVRET
{
    public partial class FlowViewForm : Form
    {

        public MainForm parent;

        public FlowViewForm()
        {
            InitializeComponent();
        }

        public void setParent(MainForm val) 
        {
            parent = val;
            parent.onGotCANFrame += GotCANFrame;
        }

        public void GotCANFrame(CANFrame frame) 
        { 

        }

        private void canDataGrid1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[8];
            Random myRand = new Random();
            myRand.NextBytes(data);
            canDataGrid1.updateData(data);
        }

        private void FlowViewForm_Load(object sender, EventArgs e)
        {

        }

        private void FlowViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.onGotCANFrame -= GotCANFrame;
        }
    }
}
