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

/*
 * This frame sending code is a generic way to send frames based on a variety of criteria.
 * Frames are constructed in the grid based first of all on the ID, length, and data. These
 * frames are then possible to send out. But, it goes deeper than that. When should they be sent out?
 * Well, that's where trigger comes in. If no trigger is set then the frame is sent once each time
 * the En checkbox is checked (it will immediately uncheck). 
 * 
 * Otherwise the trigger can take a few forms:
 * 1. It can specify milliseconds (10ms, 5ms, etc). This will cause the frame to send every so many ms
 * 2. It can specify also a number of times to send (10x, 20x, etc) So combined it might look like (5ms 10x)
 * 3. It can also specify to send when a given ID comes in (id=0x200).
 * All of the above can be specified at once: (id=0x200 5ms 10x) means to trigger on reception of 0x200 and
 * to send 10 frames 5ms apart. Note that this will retrigger if another 0x200 frame comes in - keep that in mind
 * Additionally, multiple triggers can be specified by separating with a comma:
 * (id=0x200,1000ms) means to send once every time frame 0x200 comes in and also send every 1000ms anyway
 * Lastly, the bus to match against can be constrained with bus0 or bus1:
 * id=0x200 bus0,1000ms
 * 
 * The modifier field is used to modify the frame data based on some criteria. For instance, it can be used
 * to implement a counter for multiple frames being sent out. The syntax is to specify the data byte and
 * how to modify it:
 * d0 = d0 + 1 will increment d0 by one each time a new frame is sent
 * d0 = d0 + 2 AND 0xF will increment by two but also AND with 0x0F to mask out in case the counter is not a full byte
 * d0 = d2 will take the value from d2 and copy it to d0.
 * 
 * But, it also can take values from incoming frames. specify the id and byte if thus needed:
 * d0 = id:0x200:d4 will set d0 to the d4 byte from the most recent 0x200 frame.
 * d0 = bus:0:id:0x200:d4 ensures that we only use data from bus 0 (in case a frame of same ID as found on bus 1 as well)
 * 
 * All modifiers accept frames, data bytes, addition, subtraction, AND, OR, XOR
 * 
 * Once again, multiple conditions are separated with commas:
 * d0 = d0 + 1,d1 = id:0x200:d3 + id:0x200:d4 AND 0xF0
 * 
 * Order of operations is strictly from left to right. If this messes up a statement then break it up.
 * Values calculated can be reused later in the modifier:
 * d0 = d1 + 2,d0 = d0 * 2
 * will cause the value of d0 to be D1+2 and then multiply it by two. Since there is a left to right rule it could
 * have been d0=d1+2*2 anyway but you get the point.
 */



namespace GVRET
{
    public partial class FrameSender : Form
    {
        public MainForm parent;
        private List<CANFrame> frames;
        private List<FrameSendData> sendingData;
        Multimedia.Timer fastTimer;

        public FrameSender()
        {
            InitializeComponent();
            frames = new List<CANFrame>();
            sendingData = new List<FrameSendData>();
            fastTimer = new Multimedia.Timer();
            fastTimer.Period = 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrameSender_Load(object sender, EventArgs e)
        {
            fastTimer.Tick += new System.EventHandler(this.HandleTick);
            fastTimer.Start();
        }

        /// <summary>
        /// Called every millisecond to set the system update figures and send frames if necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="timerEventArgs"></param>
        private void HandleTick(object sender, EventArgs e)
        {
            for (int i = 0; i < sendingData.Count; i++)
            {
                if (!sendingData[i].enabled) continue; //abort any processing on this if it is not enabled.
                if (sendingData[i].triggers == null) return;
                for (int j = 0; j < sendingData[i].triggers.Length; j++)
                {
                    if (sendingData[i].triggers[j].currCount >= sendingData[i].triggers[j].maxCount) continue; //don't process if we've sent max frames we were supposed to
                    if (!sendingData[i].triggers[j].readyCount) continue; //don't tick if not ready to tick
                    //is it time to fire?
                    if (++sendingData[i].triggers[j].msCounter >= sendingData[i].triggers[j].milliseconds)
                    {
                        sendingData[i].triggers[j].msCounter = 0;
                        sendingData[i].count++;
                        sendingData[i].triggers[j].currCount++;
                        doModifiers(i);
                        updateGridRow(i);
                        parent.SendCANFrame(sendingData[i], sendingData[i].bus);
                        if (sendingData[i].triggers[j].ID > 0) sendingData[i].triggers[j].readyCount = false; //reset flag if this is a timed ID trigger
                    }
                }
            }
        }

        /// <summary>
        /// given an index into the sendingData list we run the modifiers that it has set up
        /// </summary>
        /// <param name="idx">The index into the sendingData list</param>
        private void doModifiers(int idx)
        { 
            int shadowReg = 0; //shadow register we use to accumulate results
            int first=0, second=0;            
            ModifierOp tempOp;

            if (sendingData[idx].modifiers == null) return;

            for (int i = 0; i < sendingData[idx].modifiers.Length; i++)
            {
                for (int j = 0; j < sendingData[idx].modifiers[i].operations.Length; j++)
                {
                    tempOp = sendingData[idx].modifiers[i].operations[j]; //makes the code lines a lot shorter
                    if (tempOp.first.ID == -1)
                    {
                        first = shadowReg;
                    }
                    else first = fetchOperand(idx, tempOp.first);
                    second = fetchOperand(idx, tempOp.second);
                    switch (tempOp.operation)
                    {
                        case ModifierOperationType.ADDITION:
                            shadowReg = first + second;
                            break;
                        case ModifierOperationType.AND:
                            shadowReg = first & second;
                            break;
                        case ModifierOperationType.DIVISION:
                            shadowReg = first / second;
                            break;
                        case ModifierOperationType.MULTIPLICATION:
                            shadowReg = first * second;
                            break;
                        case ModifierOperationType.OR:
                            shadowReg = first | second;
                            break;
                        case ModifierOperationType.SUBTRACTION:
                            shadowReg = first - second;
                            break;
                        case ModifierOperationType.XOR:
                            shadowReg = first ^ second;
                            break;
                    }
                }
                //Finally, drop the result into the proper data byte
                sendingData[idx].data[sendingData[idx].modifiers[i].destByte] = (byte)shadowReg;
            }
        }

        
        /// <summary>
        /// Updaet the DataGridView with the newest info from sendingData
        /// </summary>
        /// <param name="idx"></param>
        private void updateGridRow(int idx)
        {
            FrameSendData temp = sendingData[idx];
            int gridLine = idx;
            StringBuilder dataString = new StringBuilder();
            dataGridView1.Rows[gridLine].Cells[7].Value = temp.count.ToString();
            for (int i = 0; i < temp.len; i++)
            {
                dataString.Append("0x");
                dataString.Append(temp.data[i].ToString("X2"));
                dataString.Append(" ");
            }
            dataGridView1.Rows[gridLine].Cells[4].Value = dataString.ToString();
        }

        private int fetchOperand(int idx, ModifierOperand op)
        {
            CANFrame tempFrame;
            if (op.ID == 0) //numeric constant
            {
                return op.databyte;
            }
            else if (op.ID == -2)
            {
                return sendingData[idx].data[op.databyte];
            }
            else //look up external data byte
            {
                tempFrame = lookupFrame(op.ID, op.bus);
                if (tempFrame != null)
                {
                    return tempFrame.data[op.databyte];
                }
                else return 0;
            }
        }


        /// <summary>
        /// Try to find the most recent frame given the input criteria
        /// </summary>
        /// <param name="ID">The ID to find</param>
        /// <param name="bus">Which bus to look on (-1 if you don't care)</param>
        /// <returns></returns>
        public CANFrame lookupFrame(int ID, int bus)
        {
            for (int a = 0; a < frames.Count; a++)
            {
                if ((ID == frames[a].ID) && ((bus == frames[a].bus) || bus == -1))
                {           
                    return frames[a];
                }
            }
            return null;
        }

        /// <summary>
        /// Set parent form so that we can call methods from it.
        /// </summary>
        /// <param name="val">Reference to the parent form</param>
        public void setParent(MainForm val)
        {
            parent = val;
            parent.onGotCANFrame += GotCANFrame;
        }

        public delegate void GotCANDelegate(CANFrame frame);
        /// <summary>
        /// Event callback for reception of canbus frames
        /// </summary>
        /// <param name="frame">The frame that came in</param>
        public void GotCANFrame(CANFrame frame)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new GotCANDelegate(GotCANFrame), frame);
            }
            else
            {
                //search list of frames and add if no frame with this ID is found
                //or update if one was found.
                bool found = false;
                for (int a = 0; a < frames.Count; a++)
                {
                    if ((frame.ID == frames[a].ID) && (frame.bus == frames[a].bus))
                    {
                        found = true;
                        frames[a].len = frame.len;
                        frames[a].data = frame.data;
                    }
                }
                if (!found)
                {
                    frames.Add(frame);
                }

                //now that frame cache has been updated, try to see if this incoming frame
                //satisfies any triggers

                for (int b = 0; b < sendingData.Count; b++)
                {
                    for (int c = 0; c < sendingData[b].triggers.Length; c++)
                    {
                        Trigger thisTrigger = sendingData[b].triggers[c];
                        if (thisTrigger.ID > 0) 
                        {
                            if ((thisTrigger.bus == frame.bus) || (thisTrigger.bus == -1))
                            {
                                if (thisTrigger.ID == frame.ID)
                                {
                                    //seems to match this trigger.
                                    if (thisTrigger.currCount < thisTrigger.maxCount)
                                    {
                                        if (thisTrigger.milliseconds == 0) //don't want to time the frame, send it right away
                                        {
                                            sendingData[b].triggers[c].currCount++;
                                            sendingData[b].count++;
                                            doModifiers(b);
                                            updateGridRow(b);
                                            parent.SendCANFrame(sendingData[b], sendingData[b].bus);
                                        }
                                        else //instead of immediate sending we start the timer
                                        {
                                            sendingData[b].triggers[c].readyCount = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Process a single line from the dataGrid. Right now it seems to not trigger at all after the first adding of the code but that seems to maybe
        /// be because whichever field you where just in will show up as nothing to the code.
        /// </summary>
        /// <param name="line"></param>
        private void processModifierText(int line) 
        {
            String modString;            

            //Example line:
            //d0 = D0 + 1,d1 = id:0x200:d3 + id:0x200:d4 AND 0xF0
            //This is certainly much harder to parse than the trigger definitions.
            //the left side of the = has to be D0 to D7. After that there is a string of
            //data that for ease of parsing will require spaces between tokens
            if (dataGridView1.Rows[line].Cells[6].Value != null && dataGridView1.Rows[line].Cells[6].Value != "")
            {
                modString = ((string)dataGridView1.Rows[line].Cells[6].Value).ToUpper();
                string[] mods = modString.Split(','); //extract all the modifiers on this line
                sendingData[line].modifiers = new Modifier[mods.Length];
                for (int i = 0; i < mods.Length; i++)
                {
                    sendingData[line].modifiers[i] = new Modifier();
                    sendingData[line].modifiers[i].destByte = 0;
                    //now split by space to extract tokens
                    string[] modToks = mods[i].Split(' ');
                    if (modToks.Length >= 5) //any valid modifier that this code can process has at least 5 tokens (D0 = D0 + 1)
                    {
                        //valid token assignment will have a data byte as the first token and = as the second
                        if (modToks[0].Length == 2 && modToks[0].StartsWith("D"))
                        {
                            int numOperations = ((modToks.Length - 5) / 2) + 1;
                            sendingData[line].modifiers[i].operations = new ModifierOp[numOperations];
                            sendingData[line].modifiers[i].destByte = int.Parse(modToks[0].Substring(1));
                            //Now start at token 2 and extract all operations. All ops past the first one
                            //use the implicit shadow register as the first operand.The contents of the shadow
                            //register are what is copied to the destination byte at the end.
                            //each op is of the form <first> <op> <second>. first and second could have more subtokens
                            string[] firstToks;
                            string[] secondToks;
                            int currToken = 2;
                            for (int j = 0; j < numOperations; j++)
                            {
                                sendingData[line].modifiers[i].operations[j] = new ModifierOp();
                                sendingData[line].modifiers[i].operations[j].first = new ModifierOperand();
                                sendingData[line].modifiers[i].operations[j].second = new ModifierOperand();
                                if (j == 0)
                                {
                                    firstToks = modToks[currToken++].ToUpper().Split(':');
                                    parseOperandString(firstToks, ref sendingData[line].modifiers[i].operations[j].first);
                                }
                                else //the rest of the ops implicitly use the shadow register as first
                                {
                                    sendingData[line].modifiers[i].operations[j].first.ID = -1;
                                }
                                sendingData[line].modifiers[i].operations[j].operation = parseOperation(modToks[currToken++].ToUpper());
                                secondToks = modToks[currToken++].ToUpper().Split(':');
                                sendingData[line].modifiers[i].operations[j].second.bus = sendingData[line].bus;
                                sendingData[line].modifiers[i].operations[j].second.ID = sendingData[line].ID;
                                parseOperandString(secondToks, ref sendingData[line].modifiers[i].operations[j].second);
                            }
                        }
                    }
                    else //must be a direct assignment, aka D0 = id:0x230:d3 in which case we'll create a dummy op to do this
                    {
                        int numOperations = 1;
                        sendingData[line].modifiers[i].operations = new ModifierOp[numOperations];
                        sendingData[line].modifiers[i].destByte = int.Parse(modToks[0].Substring(1));
                        sendingData[line].modifiers[i].operations[0] = new ModifierOp();
                        sendingData[line].modifiers[i].operations[0].operation = ModifierOperationType.ADDITION;
                        sendingData[line].modifiers[i].operations[0].first = new ModifierOperand();
                        sendingData[line].modifiers[i].operations[0].second = new ModifierOperand();
                        sendingData[line].modifiers[i].operations[0].second.ID = 0;
                        sendingData[line].modifiers[i].operations[0].second.databyte = (byte)0;
                        string[] firstToks = modToks[2].ToUpper().Split(':');
                        parseOperandString(firstToks, ref sendingData[line].modifiers[i].operations[0].first);
                    }
                }
            }
            //there is no else for the modifiers. We'll accept there not being any
        }


        private void processTriggerText(int line)
        {
            String trigger;

            //Example line:
            //id=0x200 5ms 10x bus0,1000ms
            //trigger has two levels of syntactic parsing. First you split by comma to get each
            //actual trigger. Then you split by spaces to get the tokens within each trigger
            if (dataGridView1.Rows[line].Cells[5].Value != null)
            {
                trigger = ((string)dataGridView1.Rows[line].Cells[5].Value).ToUpper();
                string[] triggers = trigger.Split(',');
                sendingData[line].triggers = new Trigger[triggers.Length];
                for (int k = 0; k < triggers.Length; k++)
                {
                    sendingData[line].triggers[k] = new Trigger();
                    //start out by setting defaults - should be moved to constructor for class Trigger.
                    sendingData[line].triggers[k].bus = -1; //-1 means we don't care which
                    sendingData[line].triggers[k].ID = -1; //the rest of these being -1 means nothing has changed it
                    sendingData[line].triggers[k].maxCount = -1;
                    sendingData[line].triggers[k].milliseconds = -1;
                    sendingData[line].triggers[k].currCount = 0;
                    sendingData[line].triggers[k].msCounter = 0;
                    sendingData[line].triggers[k].readyCount = true;

                    string[] trigToks = triggers[k].Split(' ');
                    foreach (string tok in trigToks)
                    {
                        if (tok.Substring(0, 3) == "ID=")
                        {
                            sendingData[line].triggers[k].ID = Utility.ParseStringToNum(tok.Substring(3));
                            if (sendingData[line].triggers[k].maxCount == -1) sendingData[line].triggers[k].maxCount = 10000000;
                            if (sendingData[line].triggers[k].milliseconds == -1) sendingData[line].triggers[k].milliseconds = 0; //by default don't count, just send it upon trigger
                            sendingData[line].triggers[k].readyCount = false; //won't try counting until trigger hits
                        }
                        else if (tok.EndsWith("MS"))
                        {
                            sendingData[line].triggers[k].milliseconds = Utility.ParseStringToNum(tok.Substring(0, tok.Length - 2));
                            if (sendingData[line].triggers[k].maxCount == -1) sendingData[line].triggers[k].maxCount = 10000000;
                            if (sendingData[line].triggers[k].ID == -1) sendingData[line].triggers[k].ID = 0;
                        }
                        else if (tok.EndsWith("X"))
                        {
                            sendingData[line].triggers[k].maxCount = Utility.ParseStringToNum(tok.Substring(0, tok.Length - 1));
                            if (sendingData[line].triggers[k].ID == -1) sendingData[line].triggers[k].ID = 0;
                            if (sendingData[line].triggers[k].milliseconds == -1) sendingData[line].triggers[k].milliseconds = 10;
                        }
                        else if (tok.StartsWith("BUS"))
                        {
                            sendingData[line].triggers[k].bus = Utility.ParseStringToNum(tok.Substring(3));
                        }
                    }
                    //now, find anything that wasn't set and set it to defaults
                    if (sendingData[line].triggers[k].maxCount == -1) sendingData[line].triggers[k].maxCount = 10000000;
                    if (sendingData[line].triggers[k].milliseconds == -1) sendingData[line].triggers[k].milliseconds = 100;
                    if (sendingData[line].triggers[k].ID == -1) sendingData[line].triggers[k].ID = 0;
                }
            }
            else //setup a default single shot trigger
            {
                sendingData[line].triggers = new Trigger[1];
                sendingData[line].triggers[0] = new Trigger();
                sendingData[line].triggers[0].bus = -1;
                sendingData[line].triggers[0].ID = 0;
                sendingData[line].triggers[0].maxCount = 1;
                sendingData[line].triggers[0].milliseconds = 10;
            }
        }



        //Turn a set of tokens into an operand
        private void parseOperandString(string[] tokens, ref ModifierOperand operand)
        {
            //example string -> bus:0:id:200:d3

            operand.bus = -1;
            operand.ID = -2;
            operand.databyte = 0;

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "BUS")
                {
                    operand.bus = Utility.ParseStringToNum(tokens[++i]);
                }
                else if (tokens[i] == "ID")
                {
                    operand.ID = Utility.ParseStringToNum(tokens[++i]);
                }
                else if (tokens[i].Length == 2 && tokens[i].StartsWith("D"))
                {
                    operand.databyte = Utility.ParseStringToNum(tokens[i].Substring(1));
                }
                else
                {
                    operand.databyte = Utility.ParseStringToNum(tokens[i]);
                    operand.ID = 0; //special ID to show this is a number not a look up.
                }
            }
        }

        private ModifierOperationType parseOperation(string op)
        {
            switch (op) 
            {
                case "+":
                    return ModifierOperationType.ADDITION;
                    break;
                case "-":
                    return ModifierOperationType.SUBTRACTION;
                    break;
                case "*":
                    return ModifierOperationType.MULTIPLICATION;
                    break;
                case "/":
                    return ModifierOperationType.DIVISION;
                    break;
                case "&":
                    return ModifierOperationType.AND;
                    break;
                case "AND":
                    return ModifierOperationType.AND;
                    break;
                case "|":
                    return ModifierOperationType.OR;
                    break;
                case "OR":
                    return ModifierOperationType.OR;
                    break;
                case "^":
                    return ModifierOperationType.XOR;
                    break;
                case "XOR":
                    return ModifierOperationType.XOR;
                    break;
            }
            return ModifierOperationType.ADDITION;
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //processGrid(e.RowIndex);
        }

        private void FrameSender_Leave(object sender, EventArgs e)
        {
            fastTimer.Stop();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int rec = e.RowIndex;
            if (rec != -1) sendingData.RemoveAt(rec);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void processCellChange(int line, int col)
        {
            FrameSendData tempData;
            int tempVal;

            //if this is a new line then create the base object for the line
            if ((line >= sendingData.Count) || (sendingData[line] == null))
            {
                tempData = new FrameSendData();
                sendingData.Add(tempData);
            }

            sendingData[line].count = 0;

            switch (col)
            {
                case 0: //Enable check box
                    if (dataGridView1.Rows[line].Cells[0].Value != null)
                    {
                        sendingData[line].enabled = (bool)dataGridView1.Rows[line].Cells[0].Value;
                    }
                    else sendingData[line].enabled = false;
                    break;
                case 1: //Bus designation
                    tempVal = Utility.ParseStringToNum((string)dataGridView1.Rows[line].Cells[1].Value);
                    if (tempVal < 0) tempVal = 0;
                    if (tempVal > 1) tempVal = 1;
                    sendingData[line].bus = tempVal;
                    break;
                case 2: //ID field
                    tempVal = Utility.ParseStringToNum((string)dataGridView1.Rows[line].Cells[2].Value);
                    if (tempVal < 1) tempVal = 1;
                    if (tempVal > 0x7FFFFFFF) tempVal = 0x7FFFFFFF;
                    sendingData[line].ID = tempVal;
                    if (sendingData[line].ID <= 0x7FF) sendingData[line].extended = false;
                    else sendingData[line].extended = true;
                    break;
                case 3: //length field
                    tempVal = Utility.ParseStringToNum((string)dataGridView1.Rows[line].Cells[3].Value);
                    if (tempVal < 0) tempVal = 0;
                    if (tempVal > 8) tempVal = 8;
                    sendingData[line].len = tempVal;
                    break;
                case 4: //Data bytes
                    for (int i = 0; i < 8; i++) sendingData[line].data[i] = 0;

                    string[] tokens = ((string)dataGridView1.Rows[line].Cells[4].Value).Split(' ');
                    for (int j = 0; j < tokens.Length; j++)
                    {
                        sendingData[line].data[j] = (byte)Utility.ParseStringToNum(tokens[j]);
                    }
                    break;
                case 5: //triggers
                    processTriggerText(line);
                    break;
                case 6: //modifiers
                    processModifierText(line);
                    break;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            processCellChange(e.RowIndex, e.ColumnIndex);
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res;
            //First zap all existing data. Ask first. Might not need to actually ask here since you can abort the file open
            //and be OK that way too.
            if (sendingData.Count > 0)
            {
                res = MessageBox.Show("Proceding will erase the current\ncontents of the grid.", "Warning", MessageBoxButtons.YesNo);
                if (res == DialogResult.No) return;
            }
            res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                sendingData.Clear();
                dataGridView1.Rows.Clear();
                Stream inputFile = openFileDialog1.OpenFile();
                StreamReader inputReader = new StreamReader(inputFile);
                string line;
                int row;
                try
                {
                    while (!inputReader.EndOfStream)
                    {
                        line = inputReader.ReadLine();
                        string[] tokens = line.Split('#');
                        row = dataGridView1.Rows.Add();
                        if (tokens[0] == "T")
                        {
                            dataGridView1.Rows[row].Cells[0].Value = true;
                        }
                        else dataGridView1.Rows[row].Cells[0].Value = false;                        
                        for (int j = 1; j < 7; j++)
                        {
                            dataGridView1.Rows[row].Cells[j].Value = tokens[j];                            
                        }
                        for (int k = 0; k < 7; k++) processCellChange(row, k);
                    }
                }
                catch (Exception ee)
                {
                    Debug.Print(ee.ToString());
                }
            }
        }

        private void saveGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder buildStr = new StringBuilder();
            DialogResult res;
            res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                Stream outputFile = saveFileDialog1.OpenFile();
                StreamWriter outputWriter = new StreamWriter(outputFile);
                try
                {
                    for (int i = 0; i < sendingData.Count; i++) 
                    {
                        buildStr.Clear();
                        if (dataGridView1.Rows[i].Cells[0].Value != null) 
                        {
                            if ((bool)dataGridView1.Rows[i].Cells[0].Value == true) 
                            {
                                buildStr.Append("T");
                            }
                            else buildStr.Append("F");       
                        }
                        else buildStr.Append("F");
                        buildStr.Append("#");
                        for (int j = 1; j < 7; j++)
                        {
                            buildStr.Append(dataGridView1.Rows[i].Cells[j].Value);
                            buildStr.Append("#");
                        }
                        outputWriter.WriteLine(buildStr.ToString());
                    }
                }
                catch (Exception ee)
                {
                    Debug.Print(ee.ToString());
                }
                outputWriter.Flush();
                outputWriter.Close();
            }
        }

        //Note for the below three functions that they all handle the fact that a data view grid will always have an extra row that is blank
        //and available for a new record. Because of this the grid count is always one too high.
        private void button1_Click(object sender, EventArgs e) //enable all lines in the grid
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
                sendingData[i].enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) //disable all lines in the grid
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = false;
                sendingData[i].enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e) //delete all entries in the grid and data struct
        {
            if (dataGridView1.Rows.Count == 1) return;
            for (int i = dataGridView1.Rows.Count - 2; i >= 0; i--)
            {
                sendingData[i].enabled = false;
                sendingData.RemoveAt(i);
                dataGridView1.Rows.RemoveAt(i);                
            }
        }
    }

    //Here follows a bunch of classes that record and store the details for the trigger
    //and modifier system.

    //Stores a single trigger.
    class Trigger
    {
        public bool readyCount; //ready to do millisecond ticks?
        public int ID; //which ID to match against
        public int milliseconds; //interval for triggering
        public int msCounter; //how many MS have ticked since last trigger
        public int maxCount; //max # of these frames to trigger for
        public int currCount; //how many we have triggered for so far.
        public int bus; //which bus to monitor (-1 if we aren't picky)
    }

    //referece for a source location for a single modifier.
    //If ID is zero then this is a numeric operand. The number is then
    //stored in databyte
    //If ID is -1 then this is the temporary storage register. This is a shadow
    //register used to accumulate the results of a multi operation modifier.
    //if ID is -2 then this is a look up of our own data bytes stored in the class data.
    class ModifierOperand
    {
        public int ID;
        public int bus;
        public int databyte;
    }

    //list of operations that can be done between the two operands
    enum ModifierOperationType
    {
        ADDITION,
        SUBTRACTION,
        MULTIPLICATION,
        DIVISION,
        AND,
        OR,
        XOR,
        NOT
    }

    //A single modifier operation
    class ModifierOp
    {
        public ModifierOperand first;
        public ModifierOperand second;
        public ModifierOperationType operation;
    }

    //All the operations that entail a single modifier. For instance D0+1+D2 is two operations
    class Modifier
    {
        public int destByte;
        public ModifierOp[] operations;
    }

    //A single line from the data grid. Inherits from CANFrame so it stores a canbus frame
    //plus the extra stuff for the data grid
    class FrameSendData : CANFrame
    {
        public bool enabled;
        public int count;
        public Trigger[] triggers;
        public Modifier[] modifiers;
        //modifications
    }
}