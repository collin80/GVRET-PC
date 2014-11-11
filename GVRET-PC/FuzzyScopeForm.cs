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
    public partial class FuzzyScopeForm : Form
    {
        private MainForm parent;
        private List<CANFrame> frameCache = new List<CANFrame>(50000); //use a dynamic array with large initial size
        private List<SearchItem> searchItems = new List<SearchItem>();

        public FuzzyScopeForm()
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
            //if we are currently capturing and this frame matches the ID we'd like
            //to capture then place it into the buffer.
            if (ckCapture.Checked)
            {
                //enqueue frame
                frameCache.Add(frame);                
            }
        }

        private void FuzzyScopeForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int val;
            val = Utility.ParseStringToNum(txtNewItem.Text);
            if (val == 0) return;
            listSearchItems.Items.Add(txtNewItem.Text);
            SearchItem newItem = new SearchItem();
            newItem.value = val;
            newItem.startFrame = frameCache.Count;
            searchItems.Add(newItem);
        }

        private void trackFuzzy_Scroll(object sender, EventArgs e)
        {
            lblFuzzy.Text = trackFuzzy.Value.ToString();
        }

        private void ckCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckCapture.Checked) //unchecked. That's our cue to process the input list against search list
            {

            }
        }

        private void fuzzyProcess() 
        {
            if (searchItems.Count < 2) return; //don't do a thing without at least two data points
            treeMatches.Nodes.Clear(); //clear any old matches

        }

        //try to find the number of bytes necessary to store the value given. It is possible
        //to set whether to assume signed or unsigned storage. Unsigned is assumed if no parameter given.
        private int getMaxRequiredBytes(bool signed = false)
        {
            int maxValue = -10000;
            for (int x = 0; x < searchItems.Count; x++)
            {
                if (searchItems[x].value > maxValue) maxValue = searchItems[x].value;
            }
            if (signed)
            {
                if (maxValue < 128) return 1;
                if (maxValue < 32768) return 2;
                return 4;
            }
            else
            {
                if (maxValue < 256) return 1;
                if (maxValue < 65536) return 2;
                return 4;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FuzzyScopeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.onGotCANFrame -= GotCANFrame;
        }

        private void txtNewItem_TextChanged(object sender, EventArgs e)
        {

        }

    }

    struct SearchItem
    {
        public int value; //the value to try to match against
        public int startFrame; //which frame in the buffer should we start searching at for this value?
    }

    enum OperationType 
    {
        NORMAL,
        LITTLE_ENDIAN,
        BIG_ENDIAN
    }

    struct PotentialMatch
    {
        public int searchValue;
        public double scalingFactor;
        public double biasFactor;
        public OperationType operation;
    }

}
