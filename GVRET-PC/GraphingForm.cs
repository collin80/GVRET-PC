using GraphLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private int targettedID = 0; //which ID are we looking for?

        private static int cacheSize = 10000;
        private CANFrame[] frameCache = new CANFrame[cacheSize];
        private int frameCacheWritePos = 0;

        GraphData[] Graphs = new GraphData[4];

        public GraphingForm()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                Graphs[i].bias = 0;
                Graphs[i].scale = 1.0;                
            }

            Graphs[0].color = pbColor1.BackColor;
            Graphs[1].color = pbColor2.BackColor;
            Graphs[2].color = pbColor3.BackColor;
            Graphs[3].color = pbColor4.BackColor;
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

                if (frame.ID == targettedID)
                {

                }
            }
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

            display.SetDisplayRangeX(0, frameCacheWritePos);
            display.SetGridDistanceX((float)(frameCacheWritePos / 10.0));

            for (int graph = 0; graph < 4; graph++)
            {
                if (Graphs[graph].valueCache == null)
                    setupGraph(graph, -1, -1);
                display.DataSources.Add(new DataSource());
                display.DataSources[graph].Name = "Graph " + (graph + 1);
                display.DataSources[graph].OnRenderXAxisLabel += RenderXLabel;

                display.DataSources[graph].Length = frameCacheWritePos;
                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;
                display.DataSources[graph].AutoScaleY = false;
                display.DataSources[graph].SetDisplayRangeY((float)Graphs[graph].minVal, (float)Graphs[graph].maxVal);
                display.DataSources[graph].SetGridDistanceY((float)((Graphs[graph].maxVal - Graphs[graph].minVal) / 10.0));
                display.DataSources[graph].OnRenderYAxisLabel = RenderYLabel;
                display.DataSources[graph].GraphColor = Graphs[graph].color;
                fillDataSource(display.DataSources[graph], graph);
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

        private void setupGraph(int which, int v1, int v2) 
        {
            double bias, scale;
            
            Graphs[which].valueCache = new double[frameCacheWritePos];
            Graphs[which].minVal = 9999999.0;
            Graphs[which].maxVal = -9999999.0;

            bias = Graphs[which].bias;
            scale = Graphs[which].scale;

            if (v1 == -1) return;

            //three options here. There can be no val2 in which case this is a single byte value
            //and we can process from there. The second value can be larger than the first in which
            //case this is big endian. The second value can be smaller than the first in which case
            //this is little endian.
            if ((v2 == -1) || (v1 == v2)) //single byte value
            {
                for (int j = 0; j < frameCacheWritePos; j++)
                {
                    Graphs[which].valueCache[j] = (frameCache[j].data[v1] - bias) * scale;
                    if (Graphs[which].valueCache[j] > Graphs[which].maxVal) Graphs[which].maxVal = Graphs[which].valueCache[j];
                    if (Graphs[which].valueCache[j] < Graphs[which].minVal) Graphs[which].minVal = Graphs[which].valueCache[j];
                }
            }
            else if (v2 > v1)  //big endian
            {
                double tempValue;
                int numBytes = (v2 - v1) + 1;
                for (int j = 0; j < frameCacheWritePos; j++)
                {
                    tempValue = 0.0;
                    for (int c = 0; c < numBytes; c++)
                    {
                        tempValue += frameCache[j].data[v2 - c] * (256 * c);
                    }
                    Graphs[which].valueCache[j] = (tempValue - bias) * scale;
                    if (Graphs[which].valueCache[j] > Graphs[which].maxVal) Graphs[which].maxVal = Graphs[which].valueCache[j];
                    if (Graphs[which].valueCache[j] < Graphs[which].minVal) Graphs[which].minVal = Graphs[which].valueCache[j];
                }
            }
            else //must be little endian then
            {
                double tempValue;
                int numBytes = (v1 - v2) + 1;
                for (int j = 0; j < frameCacheWritePos; j++)
                {
                    tempValue = 0.0;
                    for (int c = 0; c < numBytes; c++)
                    {
                        tempValue += frameCache[j].data[v2 + c] * (256 * c);
                    }
                    Graphs[which].valueCache[j] = (tempValue - bias) * scale;
                    if (Graphs[which].valueCache[j] > Graphs[which].maxVal) Graphs[which].maxVal = Graphs[which].valueCache[j];
                    if (Graphs[which].valueCache[j] < Graphs[which].minVal) Graphs[which].minVal = Graphs[which].valueCache[j];
                }
            }
        }

        private void txtByte1_Leave(object sender, EventArgs e)
        {
            TextBox thisBox = (TextBox)sender;
            int val1 = -1, val2 = -1;
            string[] values = thisBox.Text.Split('-');
            if (values.Length > 0)
            {
                val1 = int.Parse(values[0]);
                if (values.Length > 1)
                {
                    val2 = int.Parse(values[1]);
                }
            }

            setupGraph(0, val1, val2);
            setupGraphs();

        }

        private void listFrameIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFrameIDs.SelectedIndex > -1)
            {
                targettedID = int.Parse(listFrameIDs.Items[listFrameIDs.SelectedIndex].ToString(), System.Globalization.NumberStyles.HexNumber);
                loadFromMainScreen();
                setupGraphs();
            }
            else
            {
            }
        }

        private void loadFromMainScreen()
        {
            List<CANFrame> frames = parent.FrameCache;

            frameCacheWritePos = 0;

            for (int i = 0; i < frames.Count; i++)
            {
                if (frames[i].ID == targettedID)
                {
                    frameCache[frameCacheWritePos++] = frames[i];
                }
            }
        }

        private void txtBias1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBias1_Leave(object sender, EventArgs e)
        {
            TextBox thisBox = (TextBox)sender;
            double valu;
            valu = double.Parse(thisBox.Text);
            Graphs[0].bias = valu;
        }

        private void txtScale1_Leave(object sender, EventArgs e)
        {
            TextBox thisBox = (TextBox)sender;
            double valu;
            valu = double.Parse(thisBox.Text);
            Graphs[0].scale = valu;
        }
    }

    struct GraphData
    {
        public double[] valueCache;
        public double bias;
        public double scale;
        public double minVal;
        public double maxVal;
        public Color color;
    }

}
