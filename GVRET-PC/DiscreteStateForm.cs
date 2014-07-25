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
    public partial class DiscreteStateForm : Form
    {
        private MainForm parent;
        private List<CANFrame> frameCache;
        private List<UniqueFrameData> foundID = new List<UniqueFrameData>();
        private bool initialBaseline = true;

        public DiscreteStateForm()
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
            int found = -1;
            for (int x = 0; x < foundID.Count; x++)
            {
                if (foundID[x].ID == frame.ID)
                {
                    found = x;
                    break;
                }
            }

            if (found == -1)
            {
                UniqueFrameData tempData = new UniqueFrameData();
                tempData.ID = frame.ID;
                tempData.referenceBitfield = bytesToUInt64(frame.data);
                tempData.staticBits = 0xFFFFFFFFFFFFFFFF; //that'd be 64 binary 1's
                foundID.Add(tempData);   
            }

            UInt64 newdata = bytesToUInt64(frame.data);
            UInt64 bitDiffs = newdata ^ foundID[found].referenceBitfield;
            //The above XOR will cause bitDiffs to have a 1 anywhere the bits were different.
            //This would be wherever a change occurred in bits from the reference.
            //This needs to be inverted so that a 1 appears wherever the bits were the same.
            bitDiffs = ~bitDiffs;
            //Now that we've got a variable with a 1 in the bit positions that did not change
            //we can AND it with the staticBits member for the frame ID. This will cause there to be a 1
            //in this field only where 1 is currently there and in the bitDiffs variable. The
            //end result is that staticBits will only have a 1 where the bits have NEVER changed
            //in any frame we've seen. These are candidates for trying to find a switched value.
            UInt64 staticTemp = foundID[found].staticBits & bitDiffs;

            if (initialBaseline)
            {
                //Since this is the initial baseline we store the new static bits
                foundID[found].staticBits = staticTemp;
            }
            else
            {
                //initial baseline is already complete so compare the result to what is stored. See if there is a change
                if (foundID[found].staticBits != staticTemp)
                {

                }
            }
            //enqueue frame
            frameCache.Add(frame);
        }

        private UInt64 bytesToUInt64(byte[] bytes)
        {
            if (bytes.Length < 8) return 0;
            UInt64 tempVal = 0;
            for (int x = 0; x < 8; x++)
            {
                tempVal += (UInt64)bytes[x] << (8 * x);
            }
            return tempVal;
        }

        private void DiscreteStateForm_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            frameCache = new List<CANFrame>(50000);
            parent.onGotCANFrame += GotCANFrame;




            //do stuff
            System.Media.SystemSounds.Beep.Play(); //play a ding sound




            parent.onGotCANFrame -= GotCANFrame;
        }
    }

    class UniqueFrameData
    {
        public int ID;
        public UInt64 referenceBitfield;
        public UInt64 staticBits;
    }
}
