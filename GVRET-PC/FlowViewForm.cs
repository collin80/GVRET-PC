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

        public FlowViewForm()
        {
            InitializeComponent();
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
    }
}
