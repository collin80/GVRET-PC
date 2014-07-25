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

        private static int cacheSize = 5000;
        
        private CANFrame[] frameCache = new CANFrame[cacheSize];
        private int frameCacheReadPos = 0;
        private int frameCacheWritePos = 0;

        private int targettedID = 0; //which ID are we looking for?

        private List<int> foundID = new List<int>();

        private bool playbackActive = false, playbackForward = true;

        private byte[] refBytes = new byte[8];
        private byte[] currBytes = new byte[8];

        public FlowViewForm()
        {
            InitializeComponent();
        }

        public void setParent(MainForm val) 
        {
            parent = val;
            parent.onGotCANFrame += GotCANFrame;
        }

        private void updateFrameCounter() 
        {
            lblFrames.Text = (frameCacheReadPos + 1).ToString() + " of " + frameCacheWritePos.ToString();
        }

        //try to update position within the cache. Bool specifies direction
        private void updatePosition(bool forward)
        {
            if (forward)
            {
                if (frameCacheReadPos < (frameCacheWritePos - 1)) frameCacheReadPos++;
            }
            else
            {
                if (frameCacheReadPos > 0) frameCacheReadPos--;
            }

            if (ckAutoRef.Checked)
            {
                refBytes = currBytes;
            }

            currBytes = frameCache[frameCacheReadPos].data;
        }

        //Update the view for the data grid, potentially the reference bytes, and the current bytes
        private void updateDataView() 
        {
            txtRef0.Text = refBytes[0].ToString("X2");
            txtRef1.Text = refBytes[1].ToString("X2");
            txtRef2.Text = refBytes[2].ToString("X2");
            txtRef3.Text = refBytes[3].ToString("X2");
            txtRef4.Text = refBytes[4].ToString("X2");
            txtRef5.Text = refBytes[5].ToString("X2");
            txtRef6.Text = refBytes[6].ToString("X2");
            txtRef7.Text = refBytes[7].ToString("X2");

            txtVal0.Text = currBytes[0].ToString("X2");
            txtVal1.Text = currBytes[1].ToString("X2");
            txtVal2.Text = currBytes[2].ToString("X2");
            txtVal3.Text = currBytes[3].ToString("X2");
            txtVal4.Text = currBytes[4].ToString("X2");
            txtVal5.Text = currBytes[5].ToString("X2");
            txtVal6.Text = currBytes[6].ToString("X2");
            txtVal7.Text = currBytes[7].ToString("X2");

            canDataGrid1.updateData(currBytes, false); //don't cause display update yet
            canDataGrid1.setReference(refBytes); //allow auto display update

            updateFrameCounter();

        }

        public void GotCANFrame(CANFrame frame) 
        { 
            //first of all, always try to keep track of every ID we've ever seen while this form is open
            //if we find one we haven't seen yet then add it
            //foundID is really only used as a fast way to know if the frame ID has been seen before.
            //I believe it to be faster to search than directly searching entries in the listbox
            if (!foundID.Contains(frame.ID))
            {
                foundID.Add(frame.ID);
                listFrameIDs.Items.Add(frame.ID.ToString("X2"));
            }

            //if we are currently capturing and this frame matches the ID we'd like
            //to capture then place it into the buffer.
            if (ckCapture.Checked)
            {
                if (frame.ID == targettedID)
                {
                    if (frameCacheWritePos == cacheSize) return;
                    //enqueue frame
                    frameCache[frameCacheWritePos++] = frame;

                    //The special significance of this if is that it fills out the frame
                    //on screen because we will be showing that frame by default according to the program
                    //so it makes sense to ensure we see it immediately.
                    if (frameCacheWritePos == 1) updateDataView();
                    else updateFrameCounter();
                }
            }
        }

        private void canDataGrid1_Load(object sender, EventArgs e)
        {

        }

        private void FlowViewForm_Load(object sender, EventArgs e)
        {
            updateFrameCounter();
        }

        private void FlowViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.onGotCANFrame -= GotCANFrame;
        }

        private void numPlaybackSpeed_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numPlaybackSpeed.Value;
        }

        private void listFrameIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFrameIDs.SelectedIndex > -1)
            {
                targettedID = int.Parse(listFrameIDs.Items[listFrameIDs.SelectedIndex].ToString(), System.Globalization.NumberStyles.HexNumber);
                ckCapture.Enabled = true;
            }
            else
            {
                ckCapture.Enabled = false;
                ckCapture.Checked = false;
            }
        }

        private void btnBackOne_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button halts automatic playback
            playbackActive = false;

            updatePosition(false);
            updateDataView();
        }

        private void btnForwardOne_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button halts automatic playback
            playbackActive = false;

            updatePosition(true);
            updateDataView();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button halts automatic playback
            playbackActive = false;
            frameCacheReadPos = 0;
            updateDataView();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button pauses automatic playback
            playbackActive = false;
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            playbackActive = true;
            playbackForward = false;
            timer1.Start();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            playbackActive = true;
            playbackForward = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!playbackActive)
            {
                timer1.Stop();
                return;
            }
            if (playbackForward)
            {
                updatePosition(true);
            }
            else
            {
                updatePosition(false);
            }

            updateDataView();

            if (frameCacheReadPos == 0) playbackActive = false;
            if (frameCacheReadPos == frameCacheWritePos) playbackActive = false;
        }
    }
}
