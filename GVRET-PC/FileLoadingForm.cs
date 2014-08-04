using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GVRET
{
    public partial class FileLoadingForm : Form
    {

        MainForm parent;
        List<CANFrame> loadedFrames;
        private bool playbackActive = false, playbackForward = true;
        private int playbackPos = 0;
        private int numFrames;

        public FileLoadingForm()
        {
            InitializeComponent();
        }

        private void FileLoadingForm_Load(object sender, EventArgs e)
        {
            updateFrameCounter();
        }

        public void setParent(MainForm val)
        {
            parent = val;
            //parent.onGotCANFrame += GotCANFrame;
        }

        private void updateFrameCounter()
        {
            lblFrames.Text = (playbackPos + 1).ToString() + " of " + numFrames.ToString();
        }

        private void updatePosition(bool forward)
        {
            
            if (forward)
            {
                if (playbackPos < (numFrames - 1)) playbackPos++;
                else if (ckLoop.Checked) playbackPos = 0;
            }
            else
            {
                if (playbackPos > 0) playbackPos--;
                else if (ckLoop.Checked) playbackPos = numFrames - 1;
            }
            //Debug.Print(playbackPos.ToString());
            parent.sideloadFrame(loadedFrames[playbackPos]);
            updateFrameCounter();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CANFrame thisFrame;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader csvStream = new StreamReader(openFileDialog1.FileName);
                csvStream.ReadLine(); //ignore header line
                loadedFrames = new List<CANFrame>();
                while (!csvStream.EndOfStream)
                {
                    string thisLine = csvStream.ReadLine();
                    if (thisLine.Length > 1)
                    {
                        string[] theseTokens = thisLine.Split(',');
                        thisFrame = new CANFrame();
                        thisFrame.timestamp = DateTime.Parse(theseTokens[0]);
                        thisFrame.ID = int.Parse(theseTokens[1], System.Globalization.NumberStyles.HexNumber);
                        thisFrame.extended = bool.Parse(theseTokens[2]);
                        thisFrame.bus = int.Parse(theseTokens[3]);
                        thisFrame.len = int.Parse(theseTokens[4]);
                        for (int d = 0; d < 8; d++) thisFrame.data[d] = byte.Parse(theseTokens[5 + d], System.Globalization.NumberStyles.HexNumber);
                        loadedFrames.Add(thisFrame);
                    }
                }
                numFrames = loadedFrames.Count;
                updateFrameCounter();
            }
        }

        private void numPlaybackSpeed_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numPlaybackSpeed.Value;
        }

        private void btnBackOne_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button halts automatic playback
            playbackActive = false;

            updatePosition(false);
            
        }

        private void btnForwardOne_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button halts automatic playback
            playbackActive = false;

            updatePosition(true);
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //pushing this button halts automatic playback
            playbackActive = false;
            playbackPos = 0;
            updateFrameCounter();
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
            //Debug.Print("tick");
            if (!playbackActive)
            {
                timer1.Stop();
                return;
            }
            if (playbackForward)
            {
                //Debug.Print("forward");
                updatePosition(true);
            }
            else
            {
                //Debug.Print("back");
                updatePosition(false);
            }

            if (!ckLoop.Checked)
            {
                if (playbackPos == 0) playbackActive = false;
                if (playbackPos == (numFrames - 1)) playbackActive = false;
            }
        }
    }
}
