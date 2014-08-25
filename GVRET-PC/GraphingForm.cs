using GraphLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GVRET
{
    public partial class GraphingForm : Form
    {

        public MainForm parent;
        private List<int> foundID = new List<int>();

        private List<CANFrame>[] frames;

        GraphData[] Graphs = new GraphData[4];

        public GraphingForm()
        {
            InitializeComponent();
        }

        public void setParent(MainForm val)
        {
            parent = val;
            parent.onGotCANFrame += GotCANFrame;
        }

        public delegate void GotCANDelegate(CANFrame frame);
        public void GotCANFrame(CANFrame frame)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new GotCANDelegate(GotCANFrame), frame);
            }
            else
            {

                //first of all, always try to keep track of every ID we've ever seen while this form is open
                //if we find one we haven't seen yet then add it
                //foundID is really only used as a fast way to know if the frame ID has been seen before.
                //I believe it to be faster to search than directly searching entries in the listbox
                if (!foundID.Contains(frame.ID))
                {
                    foundID.Add(frame.ID);
                    listFrameIDs.Items.Add(frame.ID.ToString("X4"));
                }
                /*
                if (frame.ID == targettedID)
                {

                }
                 * */
            }
        }

        /*
         * Run through the whole frame cache and turn it into an ordered list of frames by ID.
        */
        private void parseFrameCache()
        {
            List<CANFrame> pFrames = parent.FrameCache;

            int numFrames = pFrames.Count;
            int numIDs = foundID.Count;

            frames = new List<CANFrame>[numIDs];

            for (int c = 0; c < numIDs; c++) frames[c] = new List<CANFrame>();

                for (int i = 0; i < numFrames; i++)
                {
                    for (int j = 0; j < numIDs; j++)
                    {
                        if (pFrames[i].ID == foundID[j])
                        {
                            frames[j].Add(pFrames[i]);
                        }
                    }
                }
        }

        private int getIdxForID(int ID) 
        {
            for (int j = 0; j < foundID.Count; j++)
            {
                if (foundID[j] == ID) return j;
            }
            return -1;
        }

        private int getMaxFrames()
        {
            int max = 0;
            for (int j = 0; j < frames.Length; j++) 
            {
                if (frames[j].Count > max) max = frames[j].Count;
            }
            return max;
        }

        private void GraphingForm_Load(object sender, EventArgs e)
        {
            List<CANFrame> frames = parent.FrameCache;

            for (int i = 0; i < frames.Count; i++)
            {
                if (!foundID.Contains(frames[i].ID))
                {
                    foundID.Add(frames[i].ID);
                    listFrameIDs.Items.Add(frames[i].ID.ToString("X4"));
                }
            }

            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        protected void fillDataSource(DataSource src, int idx)
        {
            for (int i = 0; i < src.Length; i++)
            {
                src.Samples[i].x = i;

                src.Samples[i].y = (float)Graphs[idx].valueCache[i];
            }
            src.OnRenderYAxisLabel = RenderYLabel;
        }


        private void setupGraphs()
        {
            this.SuspendLayout();

            display.DataSources.Clear();

            int totalFrames = getMaxFrames();

            display.SetDisplayRangeX(0, totalFrames);
            display.SetGridDistanceX((float)(totalFrames / 10.0));

            for (int graph = 0; graph < 4; graph++)
            {
                if (Graphs[graph].valueCache != null)
                {
                    display.DataSources.Add(new DataSource());
                    display.DataSources[graph].Name = "Graph " + (graph + 1);
                    display.DataSources[graph].OnRenderXAxisLabel += RenderXLabel;

                    display.DataSources[graph].Length = Graphs[graph].valueCache.Length;
                    display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;
                    display.DataSources[graph].AutoScaleY = false;
                    display.DataSources[graph].SetDisplayRangeY((float)Graphs[graph].minVal, (float)Graphs[graph].maxVal);
                    float YDist = (float)((Graphs[graph].maxVal - Graphs[graph].minVal) / 5.0);
                    if (YDist < 1.0f) YDist = 1.0f;
                    display.DataSources[graph].SetGridDistanceY(YDist);
                    display.DataSources[graph].OnRenderYAxisLabel = RenderYLabel;
                    display.DataSources[graph].GraphColor = Graphs[graph].color;
                    fillDataSource(display.DataSources[graph], graph);
                }
            }

            //ApplyColorSchema();

            this.ResumeLayout();
            display.Refresh();
        }

        private String RenderXLabel(DataSource s, int idx)
        {
            if (s.AutoScaleX)
            {
                //if (idx % 2 == 0)
                {
                    int Value = (int)(s.Samples[idx].x);
                    return "" + Value;
                }
                return "";
            }
            else
            {
                int Value = (int)(s.Samples[idx].x / 200);
                String Label = "" + Value + "\"";
                return Label;
            }
        }

        private String RenderYLabel(DataSource s, float value)
        {
            return String.Format("{0:0.0}", value);
        }


        private void pbColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((PictureBox)sender).BackColor = colorDialog1.Color;
            }
        }

        private void setupGraph(int which) 
        {
            float bias, scale;
            int v1, v2, numFrames, idx;
           
            Graphs[which].minVal = 9999999.0F;
            Graphs[which].maxVal = -9999999.0F;

            bias = Graphs[which].bias;
            scale = Graphs[which].scale;

            v1 = Graphs[which].B1;
            v2 = Graphs[which].B2;
            idx = getIdxForID(Graphs[which].ID);
            numFrames = frames[idx].Count;

            Debug.Print("SetupGraph for " + which.ToString() + " V1: " + v1.ToString() + " V2: " + v2.ToString() + " numFrames: " + numFrames.ToString());

            if (v1 == -1) return;

            //three options here. There can be no val2 in which case this is a single byte value
            //and we can process from there. The second value can be larger than the first in which
            //case this is big endian. The second value can be smaller than the first in which case
            //this is little endian.
            if ((v2 == -1) || (v1 == v2)) //single byte value
            {
                for (int j = 0; j < numFrames; j++)
                {
                    Graphs[which].valueCache[j] = ((frames[idx].ElementAt(j).data[v1] & Graphs[which].mask) + bias) * scale;
                    if (Graphs[which].valueCache[j] > Graphs[which].maxVal) Graphs[which].maxVal = Graphs[which].valueCache[j];
                    if (Graphs[which].valueCache[j] < Graphs[which].minVal) Graphs[which].minVal = Graphs[which].valueCache[j];
                }
            }
            else if (v2 > v1)  //big endian
            {
                float tempValue;
                int tempValInt;
                int numBytes = (v2 - v1) + 1;
                for (int j = 0; j < numFrames; j++)
                {
                    tempValInt = 0;
                    for (int c = 0; c < numBytes; c++)
                    {
                        tempValInt += (frames[idx].ElementAt(j).data[v2 - c] * (256 * c));
                    }
                    tempValInt &= Graphs[which].mask;
                    tempValue = (float)tempValInt;
                    Graphs[which].valueCache[j] = (tempValue + bias) * scale;
                    if (Graphs[which].valueCache[j] > Graphs[which].maxVal) Graphs[which].maxVal = Graphs[which].valueCache[j];
                    if (Graphs[which].valueCache[j] < Graphs[which].minVal) Graphs[which].minVal = Graphs[which].valueCache[j];
                }
            }
            else //must be little endian then
            {
                float tempValue;
                int tempValInt;
                int numBytes = (v1 - v2) + 1;
                for (int j = 0; j < numFrames; j++)
                {
                    tempValInt = 0;
                    for (int c = 0; c < numBytes; c++)
                    {
                        tempValInt += frames[idx].ElementAt(j).data[v2 + c] * (256 * c);
                    }
                    tempValInt &= Graphs[which].mask;
                    tempValue = (float)tempValInt;
                    Graphs[which].valueCache[j] = (tempValue + bias) * scale;
                    if (Graphs[which].valueCache[j] > Graphs[which].maxVal) Graphs[which].maxVal = Graphs[which].valueCache[j];
                    if (Graphs[which].valueCache[j] < Graphs[which].minVal) Graphs[which].minVal = Graphs[which].valueCache[j];
                }
            }
        }

        private void listFrameIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFrameIDs.SelectedIndex > -1)
            {
                //targettedID = int.Parse(listFrameIDs.Items[listFrameIDs.SelectedIndex].ToString(), System.Globalization.NumberStyles.HexNumber);
                //loadFromMainScreen();
                //setupGraphs();
            }
            else
            {
            }
        }

        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            int ID, B1, B2, whichGraph;
            int Mask;
                    
            float Bias, Scale;
            String strID, strMask, strBytes, strBias, strScale;
            Color thisColor;

            strID = "";
            strMask = "";
            strBytes = "";
            strBias = "";
            strScale = "";
            whichGraph = 0;
            thisColor = pbColor1.BackColor;

            if (sender.Equals(btnRefresh1))
            {
                strID = txtID1.Text;
                strMask = txtMask1.Text;
                strBytes = txtByte1.Text;
                strBias = txtBias1.Text;
                strScale = txtScale1.Text;
                thisColor = pbColor1.BackColor;
                whichGraph = 0;
            }
            else if (sender.Equals(btnRefresh2))
            {
                strID = txtID2.Text;
                strMask = txtMask2.Text;
                strBytes = txtByte2.Text;
                strBias = txtBias2.Text;
                strScale = txtScale2.Text;
                thisColor = pbColor2.BackColor;
                whichGraph = 1;
            }
            else if (sender.Equals(btnRefresh3))
            {
                strID = txtID3.Text;
                strMask = txtMask3.Text;
                strBytes = txtByte3.Text;
                strBias = txtBias3.Text;
                strScale = txtScale3.Text;
                thisColor = pbColor3.BackColor;
                whichGraph = 2;
            }
            else if (sender.Equals(btnRefresh4))
            {
                strID = txtID4.Text;
                strMask = txtMask4.Text;
                strBytes = txtByte4.Text;
                strBias = txtBias4.Text;
                strScale = txtScale4.Text;
                thisColor = pbColor4.BackColor;
                whichGraph = 3;
            }

            ID = int.Parse(strID, System.Globalization.NumberStyles.HexNumber);
            Mask = int.Parse(strMask, System.Globalization.NumberStyles.HexNumber);
            Bias = float.Parse(strBias);
            Scale = float.Parse(strScale);

            B1 = -1;
            B2 = -1;

            string[] values = strBytes.Split('-');
            Debug.Print("Split values: " + values.Length.ToString());
            if (values.Length > 0)
            {
                B1 = int.Parse(values[0]);
                if (values.Length > 1)
                {
                    B2 = int.Parse(values[1]);
                }
            }

            Graphs[whichGraph].bias = Bias;
            Graphs[whichGraph].scale = Scale;
            Graphs[whichGraph].mask = Mask;
            Graphs[whichGraph].B1 = B1;
            Graphs[whichGraph].B2 = B2;
            Graphs[whichGraph].ID = ID;
            Graphs[whichGraph].color = thisColor;

            //At long last all input value parsing is done. Now recalculate the given graph.

            //Sort all frames into lists based on ID
            parseFrameCache();

            int idx = getIdxForID(ID);
            if (idx > -1)
            {
                Graphs[whichGraph].valueCache = new float[frames[idx].Count];
                setupGraph(whichGraph);
                setupGraphs();                
            }
        }
    }

    struct GraphData
    {
        public float[] valueCache;
        public float min, max;
        public float bias, scale;
        public int mask;
        public float minVal, maxVal;
        public int ID;
        public int B1, B2;
        public Color color;
    }
}
