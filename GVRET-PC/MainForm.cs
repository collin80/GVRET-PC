using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace GVRET
{
    enum STATE //keep this enum synchronized with the Arduino firmware project
    {
        IDLE,
        GET_COMMAND,
        BUILD_CAN_FRAME,
        TIME_SYNC,
        GET_DIG_INPUTS,
        GET_ANALOG_INPUTS,
        SET_DIG_OUTPUTS,
        SETUP_CANBUS
    }
    public partial class MainForm : Form
    {
        byte[] inputBuffer = new byte[2048];
        byte[] keyBytes = new byte[16]; //storage for our enc/dec key

        List<CANFrame> frameCache = new List<CANFrame>(10000); //initially we allocate 10,000 entries in the list

        STATE rx_state = STATE.IDLE;
        int rx_step = 0;
        int len;
        int frameCount = 0;
        byte[] buffer = new byte[128];
        CANFrame buildFrame;
        Stream continuousOutput;
        bool contLogging = false;

        int bufferReadPointer = 0, bufferWritePointer = 0;

        public delegate void ProcessDelegate();

        BackgroundWorker bufferProc = new BackgroundWorker();
        BackgroundWorker trafficGraph = new BackgroundWorker();

        bool runBGThread = true;
        bool DisplayFrames = false;
            
        public MainForm()
        {
            InitializeComponent();
            onGotCANFrame += GotCANFrame;
            buildFrame = new CANFrame();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            int btr,c;
            SerialPort sp = (SerialPort)sender;

            //Debug.Print("Data Received!");

            lock (inputBuffer)
            {
               // Debug.Print("locked and loaded!");
                btr = sp.BytesToRead;
                for (c = 0; c < btr; c++)
                {
                    inputBuffer[bufferWritePointer] = (byte)sp.ReadByte();
                    //Debug.Print(inputBuffer[bufferWritePointer].ToString("X2"));
                    bufferWritePointer = (bufferWritePointer + 1) % 2048;
                }
                //Debug.Print("DataReceived: bytes in buff: " + bytesInBuffer().ToString());
            }
        }

        //figures out how many bytes we've got stored in our input buffer
        private int bytesInBuffer() {
            if (bufferWritePointer > bufferReadPointer) {
                return bufferWritePointer - bufferReadPointer;
            }
            return ((bufferWritePointer + 2048) - bufferReadPointer) % 2048;
        }

        //scans over the input buffer to find full frames
        //if we find one we advance the read pointer and use this
        //to chop off the first part of the string that contains full frames. When
        //we're done we'll be left with either nothing in the buffer or just a partial
        //frame
        public void ProcessInput()
        {
            byte c;
            byte[] decbytes = new byte[16];
            
            c = 0;

            lock (inputBuffer)
            {

                while (1 == 1) //so long as we have some bytes to read
                {
                    
                    if (bufferReadPointer == bufferWritePointer) return; //if there is nothing to do then leave

                    c = inputBuffer[bufferReadPointer];
                    bufferReadPointer++;
                    bufferReadPointer = bufferReadPointer % 2048;
                    //Debug.Print(c.ToString("X2"));
                    //Debug.Print(rx_state.ToString());
                    switch (rx_state)
                    {
                        case STATE.IDLE:
                            if (c == 0xF1) rx_state = STATE.GET_COMMAND;
                            break;
                        case STATE.GET_COMMAND:
                            switch (c)
                            {
                                case 0: //receiving a can frame
                                    rx_state = STATE.BUILD_CAN_FRAME;
                                    rx_step = 0;
                                    break;
                                case 1: //we don't accept time sync commands from the firmware
                                    rx_state = STATE.IDLE;
                                    break;
                                case 2: //process a return reply for digital input states.
                                    rx_state = STATE.GET_DIG_INPUTS;
                                    rx_step = 0;
                                    break;
                                case 3: //process a return reply for analog inputs
                                    rx_state = STATE.GET_ANALOG_INPUTS;
                                    break;
                                case 4: //we set digital outputs we don't accept replies so nothing here.
                                    rx_state = STATE.IDLE;
                                    break;
                                case 5: //we set canbus specs we don't accept replies.
                                    rx_state = STATE.IDLE;
                                    break;
                            }
                            break;
                        case STATE.BUILD_CAN_FRAME:
                		    buffer[1 + rx_step] = c;
		                    switch (rx_step) {
		                    case 0:
			                    buildFrame.ID = c;
			                    break;
		                    case 1:
			                    buildFrame.ID |= c << 8;
			                    break;
		                    case 2:
			                    buildFrame.ID |= c << 16;
			                    break;
		                    case 3:
			                    buildFrame.ID |= c << 24;
			                    if ((buildFrame.ID & 1 << 31) == 1 << 31) 
			                    {
				                    buildFrame.ID &= 0x7FFFFFFF;
				                    buildFrame.extended = true;
			                    }
			                    else buildFrame.extended = false;
			                    break;
		                    case 4:
			                    buildFrame.len = c & 0xF;
			                    if (buildFrame.len > 8) buildFrame.len = 8;
			                    break;
		                    default:
			                    if (rx_step < buildFrame.len + 5)
			                    {
			                        buildFrame.data[rx_step - 5] = c;
			                    }
			                    else 
			                    {
				                    rx_state = STATE.IDLE;
				                    //this would be the checksum byte. Compute and compare.
				                    //byte temp8 = c;//checksumCalc(buff, step);
				                    //if (temp8 == c) 
				                    //{
                                        buildFrame.timestamp = DateTime.Now;
                                        onGotCANFrame(buildFrame); //call the listeners
                                        frameCount++;
                                        buildFrame = new CANFrame(); //easy way to clear it out				                
				                    //}
			                    }
			                    break;
		                    }
                    		rx_step++;
                            break;
                        case STATE.GET_ANALOG_INPUTS: //get 9 bytes - 2 per analog input plus checksum
                		    buffer[1 + rx_step] = c;
                            switch (rx_step)
                            {
                            case 0:
                                    break;
                            }
                            rx_step++;
                            break;
                        case STATE.GET_DIG_INPUTS: //get two bytes. One for digital in status and one for checksum.
                		    buffer[1 + rx_step] = c;
                            switch (rx_step)
                            {
                                case 0:
                                    break;
                                case 1:
                                    rx_state = STATE.IDLE;
                                    break;
                            }
                            rx_step++;
                            break;                         
                    }
                }
            }
        }

        public void GotCANFrame(CANFrame frame)
        {
            byte[] tempbytes = new byte[16];

            frameCache.Add(frame);
            if (contLogging) writeOneLogLine(frame); //if continuous logging is on then log this frame

            if (!DisplayFrames) return;

            showFrame(frame);

            if (checkBox1.Checked) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            //if (dataGridView1.SortOrder == SortOrder.None) Debug.Print("No sort");
            //Resort the thing if it was previously sorted.
            if (dataGridView1.SortOrder == SortOrder.Ascending)
                dataGridView1.Sort(dataGridView1.SortedColumn, ListSortDirection.Ascending);
            if (dataGridView1.SortOrder == SortOrder.Descending)
                dataGridView1.Sort(dataGridView1.SortedColumn, ListSortDirection.Descending);
        }


        //Displays the given frame in the data view grid.
        private void showFrame(CANFrame frame)
        {
            StringBuilder data = new StringBuilder();
            int temp;

            int n = dataGridView1.Rows.Add();
            //time, id, ext, bus, len, data
            dataGridView1.Rows[n].Cells[0].Value = frame.timestamp.ToString("dd MMM yy HH:mm:ss.fff");
            dataGridView1.Rows[n].Cells[1].Value = frame.ID.ToString("X8");
            dataGridView1.Rows[n].Cells[2].Value = frame.extended.ToString();
            dataGridView1.Rows[n].Cells[3].Value = frame.bus.ToString();
            dataGridView1.Rows[n].Cells[4].Value = frame.len.ToString();
            data.Clear();
            for (int c = 0; c < frame.len; c++)
            {
                data.Append(frame.data[c].ToString("X2"));
                data.Append(" ");
            }
            dataGridView1.Rows[n].Cells[5].Value = data.ToString(); //Payload
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                runBGThread = false;
                Thread.Sleep(500);
                serialPort1.Close();
                btnConnect.Text = "Connect To Dongle";
            }
            else
            {
                Debug.Print("Opening serial port");
                serialPort1.PortName = cbPortNum.Text;
                serialPort1.Open();
                runBGThread = true;
                bufferProc.RunWorkerAsync();
                trafficGraph.RunWorkerAsync();
                btnConnect.Text = "Disconnect from Dongle";
                byte[] tempBuff = {0xE7, 0xE7};
                serialPort1.Write(tempBuff, 0, 2);
            }
        }

        private void RefreshFrameDisplay() 
        {
            dataGridView1.Rows.Clear();
            foreach (CANFrame frame in frameCache)
            {
                showFrame(frame);
            }

            //now, if we've selected to auto scroll then go to the bottom of the list
            if (checkBox1.Checked) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;

            //Resort the thing if we've specified a desired sort order
            if (dataGridView1.SortOrder == SortOrder.Ascending)
                dataGridView1.Sort(dataGridView1.SortedColumn, ListSortDirection.Ascending);
            if (dataGridView1.SortOrder == SortOrder.Descending)
                dataGridView1.Sort(dataGridView1.SortedColumn, ListSortDirection.Descending);

            label11.Text = dataGridView1.Rows.Count.ToString();
        }

        public void UpdateLabels()
        {            
            int cnt = frameCache.Count;    
            label3.Text = cnt.ToString();
            label11.Text = dataGridView1.Rows.Count.ToString();            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (!DisplayFrames)
            {
                button4.Text = "Stop Auto Capture";
            }
            else //shut it down!
            {
                button4.Text = "Enable Auto Capture";
            }
            DisplayFrames = !DisplayFrames;

            //If we have freshly selected to display frames again then clear out the dataviewgrid
            //and put all the accumulated frames into view
            if (DisplayFrames)
            {
                RefreshFrameDisplay();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] theSerialPortNames = System.IO.Ports.SerialPort.GetPortNames();
            cbPortNum.Items.AddRange(theSerialPortNames);
            if (cbPortNum.Items.Count > 0) cbPortNum.SelectedIndex = 0;

            label3.Text = "";
            label11.Text = "";
            cbCAN1Speed.SelectedIndex = 0;
            cbCAN2Speed.SelectedIndex = 0;

            //a fast background thread that does nothing but check the buffer for new data
            bufferProc.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {                                
                do
                {
                    try
                    {
                        this.Invoke(new ProcessDelegate(ProcessInput));
                    }
                    catch (Exception ed)
                    {
                    }
                    Thread.Sleep(8);
                } while (runBGThread);
            });


            //slower moving thread that displays a graph of packet levels
            trafficGraph.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                byte[] buffer = new byte[1];
                int loops = 0;                                
                int sizeViewX, sizeViewY;
                sizeViewX = pbActivity.Width;
                sizeViewY = pbActivity.Height;
                Int32[] history = new Int32[125];
                int runningTotal;
                BufferedGraphicsContext myContext;
                BufferedGraphics myBuffer;
                myContext = BufferedGraphicsManager.Current;
                myBuffer = myContext.Allocate(pbActivity.CreateGraphics(), pbActivity.DisplayRectangle);
                Graphics gHandle = myBuffer.Graphics;
                double sx, sy, ex, ey;
                double yScale = 14.0;
                int highest=0;
                int historySpan = 5;
                Pen myPen = new Pen(Brushes.Black);
                int secCount = 0;

                do
                {
                    Thread.Sleep(80);
                    secCount++;
                    if (secCount >= 12)
                    {
                        secCount = 0;
                        try
                        {
                            this.Invoke(new ProcessDelegate(UpdateLabels));
                        }
                        catch (Exception ed)
                        {
                        }                        
                    }
                    loops++;
                    if (loops > 20)
                    {
                        loops = 0;
                        buffer[0] = 0x4b;
                        //if (radioSerial.Checked) serialPort1.Write(buffer, 0, 1);
                        //Debug.Print("BG Thread GO!");
                    }
                    if (loops % 6 == 0)
                    {
                        //copy all values back one.
                        for (int i = 0; i < 124; i++) history[i] = history[i + 1];
                        history[124] = frameCount; //fill last entry with current frame count
                        frameCount = 0;
                        runningTotal = 0;
                        highest = 0;

                        for (int i = 124 - historySpan; i < 125; i++)
                        {
                            runningTotal += history[i];
                        }

                        int tempTotal = runningTotal;
                        for (int i = 123; i > 25; i--)
                        {
                            tempTotal -= history[i + 1];
                            tempTotal += history[i - historySpan];
                            if (highest < tempTotal) highest = tempTotal;
                        }

                        //Debug.Print(highest.ToString());
                        if (highest < 4) highest = 4;
                
                        sx = sizeViewX;
                        sy = sizeViewY - yScale * runningTotal;
                        gHandle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        gHandle.Clear(Color.White);
                        
                        double figure = 0.0f;
                        yScale = 117 / highest;
                        float yPos;

                        //Debug.Print(highest.ToString());
                        //Debug.Print(yScale.ToString());
                        

                        for (int ct = 1; ct < 8; ct++)
                        {
                            yPos = (float)sizeViewY - (ct / 8.0f * (float)sizeViewY);
                            gHandle.DrawLine(myPen, sizeViewX - 10, yPos, sizeViewX, yPos);
                            figure = (double)highest * (double)((double)ct / 8);                         
                            gHandle.DrawString(figure.ToString("F1"), System.Drawing.SystemFonts.DefaultFont, Brushes.Black, (float)sizeViewX - 35.0f, yPos - 5.0f);
                        }


                        int run2 = runningTotal;
                        for (int i = 123; i > 25; i--)
                        {
                            run2 = runningTotal;
                            runningTotal -= history[i + 1];
                            runningTotal += history[i - historySpan];
                            ex = sizeViewX - (sizeViewX / 98.0) * (124 - i);
                            ey = sizeViewY - yScale * (runningTotal + run2) / 2.0;
                            if (ey < 0.0) ey = 0.0;
                            gHandle.DrawLine(myPen, (float)sx, (float)sy, (float)ex, (float)ey);
                            sx = ex;
                            sy = ey;
                        }
                        myBuffer.Render();
                    }
                } while (runBGThread);
            });

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            frameCache.Clear(); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        public void SendCANFrame(CANFrame frame)
        {
            byte[] buffer = new byte[20];
            int c, d;
            int ID;
            StringBuilder dbug = new StringBuilder();
            CANFrame myFrame = new CANFrame();
            myFrame.timestamp = DateTime.Now;

            ID = frame.ID;
            if (frame.extended) ID |= 1 << 31;

            buffer[0] = 0xF1; //start of a command over serial
            buffer[1] = 0; //command ID for sending a CANBUS frame
            buffer[2] = (byte)(ID & 0xFF); //four bytes of ID LSB first
            buffer[3] = (byte)(ID >> 8);
            buffer[4] = (byte)(ID >> 16);
            buffer[5] = (byte)(ID >> 24);
            buffer[6] = (byte)frame.len;
            for (c = 0; c < len; c++)
            {
                buffer[7 + c] = frame.data[c];
            }
            buffer[7 + frame.len] = 0;

            GotCANFrame(frame); //pretend we got this frame on the line so that even frames we send show up in the table
            //FrameCtl = 0

            serialPort1.Write(buffer, 0, buffer[6] + 8);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selRow = e.RowIndex;
            try
            {

            }
            catch (Exception eeee)
            {
            }
        }

        public delegate void CANFrameDelegate(CANFrame frame);
        public event CANFrameDelegate onGotCANFrame;

        //Allows another window to cause a frame to appear as if it came into the system via a normal route.
        //Mostly just used by file loading windows to inject frames from a file
        //Could also be used by node simulators
        public void sideloadFrame(CANFrame frame)
        {
            onGotCANFrame(frame);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if the form is closing and the serial port is open then shut things down.
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                runBGThread = false;
                Thread.Sleep(500); //give things some time to shut down
                bufferProc.Dispose();
                trafficGraph.Dispose();
                //Thread.Sleep(250);
                btnConnect.Text = "Connect To Dongle";
            }
            /*
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "CloseReason", e.CloseReason);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "Cancel", e.Cancel);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "FormClosing Event");
            */
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            contLogging = !contLogging;
            if (contLogging) //turn it on
            {
                saveFileDialog1.RestoreDirectory = true;
                contLogging = false; //temporarily unset it until we can be sure things are OK

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((continuousOutput = saveFileDialog1.OpenFile()) != null)
                    {
                        btnContLog.Text = "Stop Continuous Logging";
                        contLogging = true; //OK, the coast is clear
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        Byte[] bytes;
                        bytes = encoding.GetBytes("Time Stamp,Frame Ctl,PAN ID,SRC,DEST,LEN,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13,D14,D15,D16");
                        continuousOutput.Write(bytes, 0, bytes.Length);
                        continuousOutput.WriteByte(10);
                    }
                }

            }
            else //turn it off
            {
                btnContLog.Text = "Start Continuous Logging";
                continuousOutput.Close();
            }
        }


        private void writeOneLogLine(CANFrame thisFrame)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes;
            int temp;

            try
            {

                bytes = encoding.GetBytes(thisFrame.timestamp.ToString("dd MMM yy HH:mm:ss.fff"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.ID.ToString("X8"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.extended.ToString());
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.bus.ToString());
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.len.ToString());
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                for (temp = 0; temp < 8; temp++)
                {
                    bytes = encoding.GetBytes(thisFrame.data[temp].ToString("X2"));
                    continuousOutput.Write(bytes, 0, bytes.Length);
                    continuousOutput.WriteByte(44);
                }

                continuousOutput.WriteByte(13);
                continuousOutput.WriteByte(10);

            }
            catch
            {
                contLogging = false; //stop trying to log on error.
            }
        }

        private void setCANSpeeds(int Speed1, int Speed2)
        {
            buffer[0] = 0xF1; //start of a command over serial
            buffer[1] = 5; //setup canbus
            buffer[2] = (byte)(Speed1 & 0xFF); //four bytes of ID LSB first
            buffer[3] = (byte)(Speed1 >> 8);
            buffer[4] = (byte)(Speed1 >> 16);
            buffer[5] = (byte)(Speed1 >> 24);
            buffer[6] = (byte)(Speed2 & 0xFF); //four bytes of ID LSB first
            buffer[7] = (byte)(Speed2 >> 8);
            buffer[8] = (byte)(Speed2 >> 16);
            buffer[9] = (byte)(Speed2 >> 24);
            buffer[10] = 0;
            serialPort1.Write(buffer, 0, 11);

        }

        private void cbCAN1Speed_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbCAN2Speed_SelectedIndexChanged(object sender, EventArgs e)
        {   
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int Speed1 = 0, Speed2 = 0;
            if (cbCAN1Speed.SelectedIndex > 0)
            {
                switch (cbCAN1Speed.SelectedIndex) 
                {
                    case 1:
                        Speed1 = 125000;
                        break;
                    case 2:
                        Speed1 = 250000;
                        break;
                    case 3:
                        Speed1 = 500000;
                        break;
                    case 4:
                        Speed1 = 1000000;
                        break;
                }
            }
            if (cbCAN2Speed.SelectedIndex > 0)
            {
                switch (cbCAN2Speed.SelectedIndex)
                {
                    case 1:
                        Speed2 = 125000;
                        break;
                    case 2:
                        Speed2 = 250000;
                        break;
                    case 3:
                        Speed2 = 500000;
                        break;
                    case 4:
                        Speed2 = 1000000;
                        break;
                }                
            }
            Debug.Print("S1:" + Speed1.ToString() + " S2:" + Speed2.ToString());
            setCANSpeeds(Speed1, Speed2);
        }

        private void flowViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlowViewForm theForm = new FlowViewForm();
            theForm.setParent(this);
            theForm.Show();
        }

        private void singleToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fuzzyScopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FuzzyScopeForm theForm = new FuzzyScopeForm();
            theForm.setParent(this);
            theForm.Show();
        }

        private void saveFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream outStream;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes;
            string[] temp_str;
            char[] delim = new char[1];

            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((outStream = saveFileDialog1.OpenFile()) != null)
                {
                    bytes = encoding.GetBytes("Time Stamp,ID,Extended,Bus,LEN,D1,D2,D3,D4,D5,D6,D7,D8");
                    outStream.Write(bytes, 0, bytes.Length);
                    outStream.WriteByte(10);

                    for (int c = 0; c < dataGridView1.Rows.Count; c++)
                    {
                        //blast out timestamp, id, extended, bus, and length                        
                        for (int d = 0; d < 5; d++)
                        {
                            bytes = encoding.GetBytes(dataGridView1.Rows[c].Cells[d].Value.ToString());
                            outStream.Write(bytes, 0, bytes.Length);
                            outStream.WriteByte(44); //a comma
                        }

                        //the final stuff is hex encoded and all in a long string... can't have it
                        delim[0] = ' ';
                        temp_str = dataGridView1.Rows[c].Cells[5].Value.ToString().Split(delim);
                        for (int d = 0; d < temp_str.Length; d++)
                        {
                            if (temp_str[d] != "")
                            {
                                bytes = encoding.GetBytes(temp_str[d]);
                                outStream.Write(bytes, 0, bytes.Length);
                                outStream.WriteByte(44); //a comma
                            }
                        }


                        for (int d = temp_str.Length - 1; d < 8; d++) 
                        {
                            outStream.WriteByte(48);
                            outStream.WriteByte(44);
                        }

                        outStream.WriteByte(13);
                        outStream.WriteByte(10);
                    }

                    outStream.Close();
                }
            }
        }

        private void loadFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileLoadingForm theForm = new FileLoadingForm();
            theForm.setParent(this);
            theForm.Show();
        }
    }

    //not much of a class... more of a struct. Just stores a bunch of data in a compact format.
    public class CANFrame
    {
        public int ID;
        public int bus;
        public bool extended;
        public int len;
        public byte[] data = new byte[8];
        public DateTime timestamp;
    }

}
