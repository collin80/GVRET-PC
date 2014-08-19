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
        MicroLibrary.MicroTimer fastTimer;

        public FileLoadingForm()
        {
            InitializeComponent();
            fastTimer = new MicroLibrary.MicroTimer();
            fastTimer.MicroTimerElapsed +=
               new MicroLibrary.MicroTimer.MicroTimerElapsedEventHandler(OnTimedEvent);
            fastTimer.Interval = 20000; //in microseconds
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
            loadedFrames[playbackPos].timestamp = DateTime.Now;
            parent.sideloadFrame(loadedFrames[playbackPos]);
            //updateFrameCounter();
        }

        private void loadCSVFile(string filename)
        {
            CANFrame thisFrame;
            long temp;

            System.Globalization.NumberStyles style;

            if (ckUseHex.Checked) style = System.Globalization.NumberStyles.HexNumber;
            else style = System.Globalization.NumberStyles.Integer;

            StreamReader csvStream = new StreamReader(filename);
            csvStream.ReadLine(); //ignore header line
            loadedFrames = new List<CANFrame>();
            while (!csvStream.EndOfStream)
            {
                string thisLine = csvStream.ReadLine();
                if (thisLine.Length > 1)
                {
                    string[] theseTokens = thisLine.Split(',');
                    thisFrame = new CANFrame();
                    if (theseTokens[0].Length > 10)
                    {
                        temp = Int64.Parse(theseTokens[0]);
                        thisFrame.timestamp = DateTime.MinValue.AddTicks(temp);
                    }
                    else
                    {
                        thisFrame.timestamp = DateTime.Now;
                    }
                    thisFrame.ID = int.Parse(theseTokens[1], style);
                    thisFrame.extended = bool.Parse(theseTokens[2]);
                    thisFrame.bus = int.Parse(theseTokens[3]);
                    thisFrame.len = int.Parse(theseTokens[4]);
                    for (int c = 0; c < 8; c++) thisFrame.data[c] = 0;
                    for (int d = 0; d < thisFrame.len; d++) thisFrame.data[d] = byte.Parse(theseTokens[5 + d], style);
                    loadedFrames.Add(thisFrame);
                }
            }
            csvStream.Close();
        }

        private void loadMicrochipFile(string filename)
        {
            CANFrame thisFrame;
            bool inComment = false;
            StreamReader logStream = new StreamReader(filename);
            string thisLine;

            loadedFrames = new List<CANFrame>();

            while (!logStream.EndOfStream)
            {
                thisLine = logStream.ReadLine();

                if (thisLine.StartsWith("//"))
                {
                    inComment = !inComment;
                }
                else
                {

                    /*
                    tokens:
                    0 = timestamp
                    1 = Transmission direction
                    2 = ID
                    3 = Data byte length
                    4-x = The data bytes
                    */
                    if (thisLine.Length > 1 && !inComment)
                    {
                        string[] theseTokens = thisLine.Split(';');
                        thisFrame = new CANFrame();
                        thisFrame.timestamp = DateTime.Now;
                        thisFrame.ID = parseNumberString(theseTokens[2]);
                        if (thisFrame.ID <= 0x7FF) thisFrame.extended = false;
                        else thisFrame.extended = true;
                        thisFrame.bus = 0;
                        thisFrame.len = int.Parse(theseTokens[3]);
                        for (int c = 0; c < 8; c++) thisFrame.data[c] = 0;
                        for (int d = 0; d < thisFrame.len; d++) thisFrame.data[d] = (byte)parseNumberString(theseTokens[4 + d]);
                        loadedFrames.Add(thisFrame);
                    }
                }
            }
            logStream.Close();
        }

        //Used to parse a number that may or may not be in hex format. Checks for 0x before a number
        //to specify that it is hex, otherwise assumes decimal.
        private int parseNumberString(string valu)
        {
            int result = 0;
            if (valu.ToLower().StartsWith("0x"))
            {
                result = int.Parse(valu.Substring(2), System.Globalization.NumberStyles.HexNumber);
            }
            else result = int.Parse(valu);
            return result;
        }

        private void loadLogFile(string filename)
        {
            CANFrame thisFrame;
            StreamReader logStream = new StreamReader(filename);
            string thisLine;
            System.Globalization.NumberStyles style;

            if (ckUseHex.Checked) style = System.Globalization.NumberStyles.HexNumber;
            else style = System.Globalization.NumberStyles.Integer;

            loadedFrames = new List<CANFrame>();
            
            while (!logStream.EndOfStream)
            {
                thisLine = logStream.ReadLine();

                if (thisLine.StartsWith("***")) continue;

                /*
                tokens:
                0 = timestamp
                1 = Transmission direction
                2 = Channel
                3 = ID
                4 = Type (s = standard, I believe x = extended)
                5 = Data byte length
                6-x = The data bytes
                */
                if (thisLine.Length > 1)
                {
                    string[] theseTokens = thisLine.Split(' ');
                    thisFrame = new CANFrame();
                    thisFrame.timestamp = DateTime.Now;
                    thisFrame.ID = int.Parse(theseTokens[3].Substring(2), style);
                    if (theseTokens[4] == "s") thisFrame.extended = false;
                    else thisFrame.extended = true;                    
                    thisFrame.bus = int.Parse(theseTokens[2]) - 1;
                    thisFrame.len = int.Parse(theseTokens[5]);
                    for (int c = 0; c < 8; c++) thisFrame.data[c] = 0;
                    for (int d = 0; d < thisFrame.len; d++) thisFrame.data[d] = byte.Parse(theseTokens[6 + d], style);
                    loadedFrames.Add(thisFrame);
                }
            }
            logStream.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string fnSmall = filename.ToLower();
                
                if (fnSmall.EndsWith("csv")) loadCSVFile(filename);
                if (fnSmall.EndsWith("log")) loadLogFile(filename);
                if (fnSmall.EndsWith("can")) loadMicrochipFile(filename);

                numFrames = loadedFrames.Count;
                //updateFrameCounter();
            }
        }

        private void numPlaybackSpeed_ValueChanged(object sender, EventArgs e)
        {
            fastTimer.Interval = (int)numPlaybackSpeed.Value;
        }

        private void btnBackOne_Click(object sender, EventArgs e)
        {
            fastTimer.Stop(); //pushing this button halts automatic playback
            playbackActive = false;

            updatePosition(false);
            
        }

        private void btnForwardOne_Click(object sender, EventArgs e)
        {
            fastTimer.Stop(); //pushing this button halts automatic playback
            playbackActive = false;

            updatePosition(true);
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            fastTimer.Stop(); //pushing this button halts automatic playback
            playbackActive = false;
            playbackPos = 0;
            updateFrameCounter();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            fastTimer.Stop(); //pushing this button pauses automatic playback
            playbackActive = false;
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            playbackActive = true;
            playbackForward = false;
            fastTimer.Start();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            playbackActive = true;
            playbackForward = true;
            fastTimer.Start();
        }

        private void OnTimedEvent(object sender,
                                  MicroLibrary.MicroTimerEventArgs timerEventArgs)
        {
            //Debug.Print("tick");
            if (!playbackActive)
            {
                fastTimer.Stop();
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            updateFrameCounter();
        }
    }
}
