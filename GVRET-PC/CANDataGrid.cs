using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GVRET
{
    public partial class CANDataGrid : UserControl
    {
        private byte[] data = new byte[8];
        private byte[] refData = new byte[8];

        private int sizeViewX, sizeViewY;
        private BufferedGraphicsContext myContext;
        private BufferedGraphics myBuffer;
        private Graphics gHandle;
        private Pen blackPen;

        public bool autoUpdateReference;
        
        public CANDataGrid()
        {
            InitializeComponent();

            sizeViewX = pbDataView.Width;
            sizeViewY = pbDataView.Height;
            myContext = BufferedGraphicsManager.Current;
            myBuffer = myContext.Allocate(pbDataView.CreateGraphics(), pbDataView.DisplayRectangle);
            gHandle = myBuffer.Graphics;
            blackPen = new Pen(Brushes.Black);
            autoUpdateReference = true;
        }

        public void setAutoRefUpdate(bool newVal) 
        {
            autoUpdateReference = newVal;
        }

        public void setReference(byte[] refVals, bool refresh = true) 
        {
            if (refVals.Length < 8) return;
            for (int x = 0; x < 8; x++) 
            {
                refData[x] = refVals[x];
            }
            if (refresh) refreshView();
        }

        public void updateData(byte[] newData, bool refresh = true)
        {
            if (newData.Length < 8) return;
            for (int x = 0; x < 8; x++)
            {
                if (autoUpdateReference) refData[x] = data[x];
                data[x] = newData[x];
            }
            if (refresh) refreshView();
        }

        /*
         * Takes the current values in data and redraws the whole 8x8 grid to match 
         */
        private void refreshView()
        {
            gHandle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gHandle.Clear(Color.White);

            //now, color the bitfield by seeing if a given bit is freshly set/unset in the new data
            //compared to the old. Bits that are not set in either are white, bits set in both are black
            //bits that used to be set but now are unset are red, bits that used to be unset but now are set
            //are green

            for (int y = 0; y < 8; y++)
            {
                byte thisByte = data[y];
                byte prevByte = refData[y];
                for (int x = 7; x >= 0; x--)
                {
                    bool thisBit = false;
                    bool prevBit = false;
                    Brush thisColor;
                    if ((thisByte & (1 << x)) == (1 << x)) thisBit = true;
                    if ((prevByte & (1 << x)) == (1 << x)) prevBit = true;
                    //now handle the four choices to get the proper color
                    if (thisBit)
                    {
                        if (prevBit)
                        {
                            thisColor = Brushes.Black;
                        }
                        else
                        {
                            thisColor = Brushes.Green;
                        }
                    }
                    else
                    {
                        if (prevBit)
                        {
                            thisColor = Brushes.Red;
                        }
                        else
                        {
                            thisColor = Brushes.White;
                        }
                    }
                    //now draw the square according to the above set color
                    gHandle.FillRectangle(thisColor, (7 - x) * 25, y * 25, 25, 25);
                }
            }
            //Now draw the grid back over the colored square
            for (int y = 0; y < 8; y++)
            {
                gHandle.DrawLine(blackPen, 0, y * 25, 200, y * 25);
            }
            for (int x = 0; x < 8; x++)
            {
                gHandle.DrawLine(blackPen, x * 25, 0, x * 25, 200);
            }

            myBuffer.Render();
        }

        private void CANDataGrid_Load(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
