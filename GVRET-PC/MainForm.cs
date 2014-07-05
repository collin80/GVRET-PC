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
using System.Net.Sockets;
using System.Net;

/*
 * allow connection to multiple servers and/or serial ports
 * 
 * real-time logging to file so that computer glitch still captures most of the log
 */


namespace GVRET
{
    public partial class MainForm : Form
    {
        byte[] inputBuffer = new byte[2048];
        byte[] keyBytes = new byte[16]; //storage for our enc/dec key

        List<CANFrame> frameCache = new List<CANFrame>(10000); //initially we allocate 10,000 entries in the list

        int rx_state = 0;
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
            onGotZBFrame += GotZBFrame;
            buildFrame = new CANFrame();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            int btr,c;
            SerialPort sp = (SerialPort)sender;

            lock (inputBuffer)
            {
                //Debug.Print("DataReceived: Got some son!");
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
            
            //Debug.WriteLine("Starting Process Input");
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
                    Debug.Print(rx_state.ToString());
                    switch (rx_state)
                    {
                        case 0:
                            if (c == 0x14) rx_state++;
                            break;
                        default:
                            buildFrame.timestamp = DateTime.Now;
                                onGotZBFrame(buildFrame); //call the listeners
                                frameCount++;
                                buildFrame = new ZBFrame();
                            }
				            break;
                    }
                }
            }
        }

        public void GotZBFrame(ZBFrame zb)
        {
            byte[] tempbytes = new byte[16];
            byte[] decbytes = new byte[16];

            frameCache.Add(zb);
            if (contLogging) writeOneLogLine(zb); //if continuous logging is on then log this frame

            if (!DisplayFrames) return;

            showFrame(zb);

            if (checkBox1.Checked) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            //if (dataGridView1.SortOrder == SortOrder.None) Debug.Print("No sort");
            //Resort the thing if it was previously sorted.
            if (dataGridView1.SortOrder == SortOrder.Ascending)
                dataGridView1.Sort(dataGridView1.SortedColumn, ListSortDirection.Ascending);
            if (dataGridView1.SortOrder == SortOrder.Descending)
                dataGridView1.Sort(dataGridView1.SortedColumn, ListSortDirection.Descending);
        }


        //Displays the given frame in the data view grid.
        private void showFrame(ZBFrame zb)
        {
            StringBuilder data = new StringBuilder();
            int temp;

            if (filterPAN != 0 && filterPAN != zb.PAN_ID) return; //doesn't match filter for PAN
            if (filterSrc != 0 && filterSrc != zb.SrcID) return; //doesn't match filter for source addr
            if (filterDest != 0 && filterDest != zb.DestID) return; //doesn't match filter for dest addr

            int n = dataGridView1.Rows.Add();

            dataGridView1.Rows[n].Cells[0].Value = zb.timestamp.ToString("dd MMM yy HH:mm:ss.fff");
            dataGridView1.Rows[n].Cells[1].Value = zb.FrameCtl.ToString("X4"); //Frame control bytes                            
            dataGridView1.Rows[n].Cells[2].Value = zb.PAN_ID.ToString("X4"); //PAN ID
            dataGridView1.Rows[n].Cells[4].Value = zb.DestID.ToString("X4"); //Dest ID
            dataGridView1.Rows[n].Cells[3].Value = zb.SrcID.ToString("X4"); //Source ID
            temp = zb.data.Length + 11;
            dataGridView1.Rows[n].Cells[5].Value = temp.ToString("X2"); //Length
            data.Clear();
            for (int c = 0; c < zb.data.Length; c++)
            {
                data.Append(zb.data[c].ToString("X2"));

                data.Append(" ");
            }
            dataGridView1.Rows[n].Cells[6].Value = data.ToString(); //Payload
            int energy = zb.ed - 91;
            dataGridView1.Rows[n].Cells[7].Value = energy.ToString() + "dBm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioSerial.Checked) {
                if (serialPort1.IsOpen)
                {
                    runBGThread = false;
                    Thread.Sleep(500);
                    serialPort1.Close();
                    btnConnect.Text = "Connect To Dongle";
                }
                else
                {
                    serialPort1.PortName = cbPortNum.Text;
                    serialPort1.Open();
                    runBGThread = true;
                    bufferProc.RunWorkerAsync();
                    trafficGraph.RunWorkerAsync();
                    btnConnect.Text = "Disconnect from Dongle";
                    serialSendChannelReq((byte)Properties.Settings.Default.Channel);
                }
            }

            if (radioIP.Checked)
            {
                if (client.Connected)
                {
                    clientStream.Close();
                    client.Close();
                    runBGThread = false;
                    btnConnect.Text = "Connect To Dongle";
                }
                else
                {
                    serverLocation = new IPEndPoint(IPAddress.Parse(txtIPAddr.Text), 1218);
                    client.Connect(serverLocation);
                    clientStream = client.GetStream();
                    clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                    runBGThread = true;
                    bufferProc.RunWorkerAsync();
                    trafficGraph.RunWorkerAsync();
                    btnConnect.Text = "Disconnect from Dongle";
                }
            }
        }


        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            Debug.Print("Got TCP/IP packet");

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }
                //message has successfully been received - Do something with it
                lock (inputBuffer)
                {
                    //Debug.Print("DataReceived: Got some son!");                    
                    for (int c = 0; c < bytesRead; c++)
                    {
                        inputBuffer[bufferWritePointer] = (byte)message[c];
                        bufferWritePointer = (bufferWritePointer + 1) % 2048;
                        //Debug.Print(message[c].ToString("X2"));
                    }
                    //Debug.Print("DataReceived: bytes in buff: " + bytesInBuffer().ToString());
                }              
            }

            tcpClient.Close();
        }

        //sends traffic to all connected clients
        public void networkwrite(byte[] buffer)
        {
           try
           {
                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
           }
           catch
           {
                clientStream.Close(); //obviously it died                
           }
        }

        public void networkwrite(byte[] buffer, int size)
        {
            try
            {
                clientStream.Write(buffer, 0, size);
                clientStream.Flush();
            }
            catch
            {
                clientStream.Close(); //obviously it died                
            }
        }



        private void RefreshFrameDisplay() 
        {
            dataGridView1.Rows.Clear();
            foreach (ZBFrame zb in frameCache)
            {
                showFrame(zb);
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
                        if (radioSerial.Checked) serialPort1.Write(buffer, 0, 1);
                        if (radioIP.Checked) networkwrite(buffer, 1);
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

        public void SendRadioFrame(int Dest, int Src, int PAN, byte[] data, bool isConfig=false)
        {
            byte[] buffer = new byte[128];
            byte[] encB = new byte[16];
            byte[] encB2 = new byte[16];
            int c, d;
            StringBuilder dbug = new StringBuilder();
            ZBFrame myFrame = new ZBFrame();
            myFrame.timestamp = DateTime.Now;
            buffer[0] = 0x14; //start of a frame transmit
            buffer[1] = (byte)(8 + data.Length); //length of this frame including the 6 bytes necessary to send pan, dest, src
            buffer[2] = (byte)(PAN & 0xFF); //PAN ID = 0x1234 - AVR is least significant byte first so plan accordingly
            buffer[3] = (byte)(PAN >> 8);
            buffer[4] = (byte)(Dest & 0xFF);
            buffer[5] = (byte)(Dest >> 8);
            buffer[6] = (byte)(Src & 0xFF);
            buffer[7] = (byte)(Src >> 8);

            //if this is a config frame then we need to calculate the two validation bytes
            if (isConfig)
            {
                //xor of first 14 bytes
                d = data[0];
                for (c = 1; c < 14; c++) d ^= data[c];
                data[14] = (byte)d;

                //additive sum of first 14 bytes 
                d = data[0];
                for (c = 1; c < 14; c++) d += data[c];
                data[15] = (byte)(d & 0xFF);
            }


            myFrame.DestID = Dest;
            myFrame.SrcID = Src;
            myFrame.PAN_ID = PAN;
            myFrame.FrameCtl = 0;

            //Debug.Print(data.Length.ToString());
            //Debug.Print(buffer[1].ToString());

            if (data.Length != 16 && data.Length != 32)
            {
                Debug.Print("Try sending a real frame, genius!");
                return;
            }
            
            /*
            dbug.Clear();
            for (int l = 0; l < data.Length; l++)
            {
                dbug.Append(data[l].ToString("X2"));
            }
            Debug.Print(dbug.ToString());
            */

            encB = EncryptFrame(data,0);

            for (c = 0; c < 16; c++)
            {
                buffer[8 + c] = encB[c];
                
            }

            /*
            dbug.Clear();
            for (int l = 0; l < 32; l++)
            {
                dbug.Append(buffer[8 + l].ToString("X2"));
            }
            Debug.Print(dbug.ToString()); */


            if (data.Length == 32)
            {
                encB2 = EncryptFrame(data, 16);
                for (c = 0; c < 16; c++)
                {
                    buffer[24 + c] = encB2[c];
                }
            }

            /*dbug.Clear();
            for (int l = 0; l < 32; l++)
            {
                dbug.Append(buffer[8 + l].ToString("X2"));
            }
            Debug.Print(dbug.ToString()); */

            myFrame.data = data;
            GotZBFrame(myFrame); //send our newly constructed frame to the frame displaying code. We'll know it was sent from this program because
            //FrameCtl = 0
            buffer[24 + data.Length] = 0;
            buffer[24 + data.Length + 1] = 0;

            for (int ii = 0; ii < buffer[1] + 2; ii++)
            {
                //Debug.Print(buffer[ii].ToString("X2"));
            }

            if (radioSerial.Checked) serialPort1.Write(buffer, 0, buffer[1] + 2);
            if (radioIP.Checked) networkwrite(buffer, buffer[1] + 2);
        }

        public void SendDataRequest(int Dest, int Src, int PAN, int reqNum, byte Side)
        {
            byte[] buffer = new byte[16];
            Random myRand = new Random();
            
            buffer[0] = Side;
            buffer[1] = (byte)reqNum;
            buffer[2] = (byte)myRand.Next(255); //all the rest of this is ignored but will be
            buffer[3] = (byte)myRand.Next(255); //required when enc is enabled
            buffer[4] = (byte)myRand.Next(255); //these all being random makes traffic analysis
            buffer[5] = (byte)myRand.Next(255); //really tough.
            buffer[6] = (byte)myRand.Next(255);
            buffer[7] = (byte)myRand.Next(255);
            buffer[8] = (byte)myRand.Next(255);
            buffer[9] = (byte)myRand.Next(255);
            buffer[10] = (byte)myRand.Next(255);
            buffer[11] = (byte)myRand.Next(255);
            buffer[12] = (byte)myRand.Next(255);
            buffer[13] = (byte)myRand.Next(255);
            buffer[14] = (byte)myRand.Next(255);
            buffer[15] = (byte)myRand.Next(255);
            SendRadioFrame(Dest, Src, PAN, buffer, true);
        }

        //writes data to the device in question
        public void SendDataWrite(int Dest, int Src, int PAN, int reqNum, byte side, byte[] data)
        {

            byte[] buffer = new byte[16];
            StringBuilder build = new StringBuilder();
            int c;
            
            buffer[0] = (byte)(side + 2);
            buffer[1] = (byte)reqNum;
            int max = data.Length;
            if (max > 14) max = 14;
            for (c = 0; c < max; c++) buffer[c + 2] = data[c];
            //for (c = 0; c < 16; c++) build.Append(buffer[c].ToString("X2"));
             //Debug.Print(build.ToString());

            SendRadioFrame(Dest, Src, PAN, buffer, true);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public byte[] DecryptFrame(byte[] Frame) {
            
            byte[] decipheredText = new byte[16];
      
            /*
              key for the old system: d54b711a5ec0fc950f6436eb77a2b9b7
             * */
            if (txtEncKey.Text.Length > 31)
            {
                aes_sys.InvCipher(Frame, decipheredText);
                return decipheredText;
            }
            else
            {
                return Frame;
            }
        }

        public byte[] EncryptFrame(byte[] Frame, int offset)
        {
            byte[] EncText = new byte[16];
            byte[] plainText = new byte[16];

            for (int c = 0; c < 16; c++) plainText[c] = Frame[offset + c];

            //if (txtEncKey.Text.Length > 31)
            //{   
                aes_sys.Cipher(plainText, EncText);
                return EncText;
            //}
            //else 
           // {
            //    return plainText;
           // }

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selRow = e.RowIndex;
            try
            {
                txtPAN.Text = dataGridView1.Rows[selRow].Cells[2].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[selRow].Cells[3].Value.ToString();
            }
            catch (Exception eeee)
            {
            }
        }

        public delegate void ZBFrameDelegate(ZBFrame zb);
        public event ZBFrameDelegate onGotZBFrame;

        private void button2_Click(object sender, EventArgs e)
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
                    bytes = encoding.GetBytes("Time Stamp,Frame Ctl,PAN ID,SRC,DEST,LEN,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13,D14,D15,D16");
                    outStream.Write(bytes, 0, bytes.Length);
                    outStream.WriteByte(10);

                    for (int c = 0; c < dataGridView1.Rows.Count; c++)
                    {
                        //the timestamp
                        bytes = encoding.GetBytes(dataGridView1.Rows[c].Cells[0].Value.ToString());
                        outStream.Write(bytes, 0, bytes.Length);
                        outStream.WriteByte(44);

                        //the hex encoded stuff: framectl, pan, src, dest, len
                        for (int d = 1; d < 6; d++)
                        {
                            bytes = encoding.GetBytes(dataGridView1.Rows[c].Cells[d].Value.ToString());
                            outStream.Write(bytes, 0, bytes.Length);
                            outStream.WriteByte(44); //a comma
                        }

                        //the final stuff is hex encoded and all in a long string... can't have it
                        delim[0] = ' ';
                        temp_str = dataGridView1.Rows[c].Cells[dataGridView1.Rows[c].Cells.Count - 1].Value.ToString().Split(delim);
                        for (int d = 0; d < temp_str.Length; d++)
                        {
                            if (temp_str[d] != "")
                            {
                                bytes = encoding.GetBytes(temp_str[d]);
                                outStream.Write(bytes, 0, bytes.Length);
                                outStream.WriteByte(44); //a comma
                            }
                        }
                        outStream.WriteByte(10);
                    }

                   outStream.Close();
                }
            }
            
        }

        public void SendEEPROMReq(int Dest, int Src, int PAN, int offset)
        {
            byte[] buffer = new byte[16];

            buffer[0] = 1; //car side
            buffer[1] = 16; //16 = EEPROM Read
            buffer[2] = (byte)(offset % 256);
            buffer[3] = (byte)(offset / 256);
            buffer[4] = 0; //event log
            buffer[5] = 1;
            buffer[6] = 2;
            buffer[7] = 3;
            buffer[8] = 4;
            buffer[9] = 5;
            buffer[10] = 6;
            buffer[11] = 7;
            buffer[12] = 8;
            buffer[13] = 9;
            buffer[14] = 10;
            buffer[15] = 11;

            SendRadioFrame(Dest, Src, PAN, buffer, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int PAN, addr;

            PAN = addr = 0;

            EEPROMDump frmDump;
            try
            {
                PAN = int.Parse((string)txtPAN.Text, System.Globalization.NumberStyles.HexNumber);
                addr = int.Parse((string)txtAddress.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception etx)
            {
            }
            frmDump = new EEPROMDump();
            frmDump.SetParent(this);
            frmDump.setParams(PAN, addr);
            frmDump.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EEPROMViewer frmView = new EEPROMViewer();
            frmView.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EEPROMViewer frmView;
            frmView = new EEPROMViewer();
            //frmView.SetParent(this);
            frmView.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int PAN, addr;
            PAN = int.Parse((string)txtPAN.Text, System.Globalization.NumberStyles.HexNumber);
            addr = int.Parse((string)txtAddress.Text, System.Globalization.NumberStyles.HexNumber);

            FirmwareUpdate frmFirmware;
            frmFirmware = new FirmwareUpdate();
            frmFirmware.setLoadingVals(PAN, addr);
            frmFirmware.SetParent(this);
            frmFirmware.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEncKey_TextChanged(object sender, EventArgs e)
        {
            if (txtEncKey.Text.Length > 31)
            {
                SetKey();
                Properties.Settings.Default.EncKey = txtEncKey.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void SetKey() {
            for (int i = 0; i < 16; i++)
            {
                keyBytes[i] = Byte.Parse(txtEncKey.Text.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            aes_sys = new Aes(Aes.KeySize.Bits128, keyBytes);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CarSideConfig frmCarConfig;
            int PAN, addr;

            //temp[i] = Byte.Parse(textBox1.Text.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

            PAN = int.Parse(txtPAN.Text, System.Globalization.NumberStyles.HexNumber);
            addr = int.Parse(txtAddress.Text, System.Globalization.NumberStyles.HexNumber);

            frmCarConfig = new CarSideConfig();
            frmCarConfig.setLoadingVals(PAN, addr);
            frmCarConfig.SetParent(this);
            frmCarConfig.Show();

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

        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Channel = (cbChannel.SelectedIndex + 11);
            serialSendChannelReq((byte)Properties.Settings.Default.Channel);
            Properties.Settings.Default.Save();
        }

        private void serialSendChannelReq(byte channel)
        {
            byte[] buff = new byte[3];
            buff[0] = 0x7e; //tells the usb dongle that this is a config request
            buff[1] = 2; //2 = set channel
            buff[2] = channel;
            Debug.Print("Output: " + buff[2].ToString());
            //if (serialPort1.IsOpen) serialPort1.Write(buff, 0, 3);
            //else Debug.Print("Tried to send channel set with no serial port open!");
        }

        private void txtPANFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                filterPAN = int.Parse(txtPANFilter.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch (FormatException fe)
            {
                filterPAN = 0;
            }
            RefreshFrameDisplay();
        }

        private void txtSrcFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                filterSrc = int.Parse(txtSrcFilter.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch (FormatException fee)
            {
                filterSrc = 0;
            }
            RefreshFrameDisplay();
        }

        private void txtDestFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                filterDest = int.Parse(txtDestFilter.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch (FormatException feee)
            {
                filterDest = 0;
            }
            RefreshFrameDisplay();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
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


        private void writeOneLogLine(ZBFrame thisFrame)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes;
            int temp;

            try
            {

                bytes = encoding.GetBytes(thisFrame.timestamp.ToString("dd MMM yy HH:mm:ss.fff"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.FrameCtl.ToString("X4"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.PAN_ID.ToString("X4"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.DestID.ToString("X4"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                bytes = encoding.GetBytes(thisFrame.SrcID.ToString("X4"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                temp = thisFrame.data.Length + 11;
                bytes = encoding.GetBytes(temp.ToString("X4"));
                continuousOutput.Write(bytes, 0, bytes.Length);
                continuousOutput.WriteByte(44);

                for (temp = 0; temp < thisFrame.data.Length; temp++)
                {
                    bytes = encoding.GetBytes(thisFrame.data[temp].ToString("X2"));
                    continuousOutput.Write(bytes, 0, bytes.Length);
                    continuousOutput.WriteByte(44);
                }

                continuousOutput.WriteByte(10);
            }
            catch
            {
                contLogging = false; //stop trying to log on error.
            }
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
