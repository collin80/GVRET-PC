using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
 * to send 10 frames 5ms apart. 
 * Additionally, multiple triggers can be specified by separating with a comma:
 * (id=0x200,1000ms) means to send once every time frame 0x200 comes in and also send every 1000ms anyway
 * Lastly, the bus to match against can be constrained with bus0 or bus1:
 * id=0x200 bus0,1000ms
 * 
 * The modifier field is used to modify the frame data based on some criteria. For instance, it can be used
 * to implement a counter for multiple frames being sent out. The syntax is to specify the data byte and
 * how to modify it:
 * d0 = d0 + 1 will increment d0 by one each time a new frame is sent
 * d0 = d0 + 2 AND 0F will increment by two but also AND with 0x0F to mask out in case the counter is not a full byte
 * d0 = d2 will take the value from d2 and copy it to d0.
 * 
 * But, it also can take values from incoming frames. specify the id and byte if thus needed:
 * d0 = id:200:d4 will set d0 to the d4 byte from the most recent 0x200 frame.
 * d0 = bus:0:id:200:d4 ensures that we only use data from bus 0 (in case a frame of same ID as found on bus 1 as well)
 * 
 * All modifiers accept frames, data bytes, addition, subtraction, AND, OR, XOR
 * 
 * Once again, multiple conditions are separated with commas:
 * d0 = d0 + 1,d1 = id:0x200:d3 + id:0x200:d4 AND F0
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

        public FrameSender()
        {
            InitializeComponent();
            frames = new List<CANFrame>();
            sendingData = new List<FrameSendData>();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrameSender_Load(object sender, EventArgs e)
        {

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
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void processGrid(int line) 
        {
            String trigger, modString;
            FrameSendData tempData = new FrameSendData();

            for (int i = 0; i < 8; i++) tempData.data[i] = 0;

            tempData.ID = Utility.ParseStringToNum((string)dataGridView1.Rows[line].Cells[2].Value);
            tempData.bus = Utility.ParseStringToNum((string)dataGridView1.Rows[line].Cells[1].Value);
            tempData.len = Utility.ParseStringToNum((string)dataGridView1.Rows[line].Cells[3].Value);
            if (dataGridView1.Rows[line].Cells[0].Value != null)
            {
                tempData.enabled = (bool)dataGridView1.Rows[line].Cells[0].Value;
            }
            else tempData.enabled = false;
            string[] tokens = ((string)dataGridView1.Rows[line].Cells[4].Value).Split(' ');
            for (int j = 0; j < tokens.Length; j++)
            {
                tempData.data[j] = (byte)Utility.ParseStringToNum(tokens[j]);
            }
            tempData.count = 0;
            if (tempData.ID <= 0x7FF) tempData.extended = false;
            else tempData.extended = true;
            tempData.line = line;

            //Example line:
            //id=200 5ms 10x bus0,1000ms
            //trigger has two levels of syntactic parsing. First you split by comma to get each
            //actual trigger. Then you split by spaces to get the tokens within each trigger
            if (dataGridView1.Rows[line].Cells[5].Value != null)
            {
                trigger = ((string)dataGridView1.Rows[line].Cells[5].Value).ToUpper();
                string[] triggers = trigger.Split(',');
                tempData.triggers = new Trigger[triggers.Length];
                for (int k = 0; k < triggers.Length; k++)
                {
                    tempData.triggers[k] = new Trigger();
                    //start out by setting defaults - should be moved to constructor for class Trigger.
                    tempData.triggers[k].bus = 0;
                    tempData.triggers[k].ID = 0;
                    tempData.triggers[k].maxCount = 1000000000;
                    tempData.triggers[k].milliseconds = 1000;

                    string[] trigToks = triggers[k].Split(' ');
                    foreach (string tok in trigToks)
                    {
                        if (tok.Substring(0, 3) == "ID=")
                        {
                            tempData.triggers[k].ID = Utility.ParseStringToNum(tok.Substring(3));
                        }
                        else if (tok.EndsWith("MS"))
                        {
                            tempData.triggers[k].milliseconds = Utility.ParseStringToNum(tok.Substring(0, tok.Length - 2));
                        }
                        else if (tok.EndsWith("X"))
                        {
                            tempData.triggers[k].maxCount = Utility.ParseStringToNum(tok.Substring(0, tok.Length - 1));
                        }
                        else if (tok.StartsWith("BUS"))
                        {
                            tempData.triggers[k].bus = Utility.ParseStringToNum(tok.Substring(3));
                        }
                    }
                }
            }
            else //setup a default single shot trigger
            {
                tempData.triggers = new Trigger[1];
                tempData.triggers[0] = new Trigger();
                tempData.triggers[0].bus = 0;
                tempData.triggers[0].ID = 0;
                tempData.triggers[0].maxCount = 1;
                tempData.triggers[0].milliseconds = 10;
            }

            //Example line:
            //d0 = D0 + 1,d1 = id:200:d3 + id:200:d4 AND 0xF0
            //This is certainly much harder to parse than the trigger definitions.
            //the left side of the = has to be D0 to D7. After that there is a string of
            //data that for ease of parsing will require spaces between tokens
            if (dataGridView1.Rows[line].Cells[6].Value != null)
            {
                modString = ((string)dataGridView1.Rows[line].Cells[6].Value).ToUpper();
                string[] mods = modString.Split(','); //extract all the modifiers on this line
                tempData.modifiers = new Modifier[mods.Length];
                for (int i = 0; i < mods.Length; i++)
                {
                    tempData.modifiers[i] = new Modifier();
                    tempData.modifiers[i].destByte = 0;
                    //now split by space to extract tokens
                    string[] modToks = mods[i].Split(' ');
                    if (modToks.Length >= 5) //any valid modifier has at least 5 tokens (D0 = D0 + 1)
                    {
                        //valid token assignment will have a data byte as the first token and = as the second
                        if (modToks[0].Length == 2 && modToks[0].StartsWith("D"))
                        {
                            int numOperations = ((modToks.Length - 5) / 2) + 1;
                            tempData.modifiers[i].operations = new ModifierOp[numOperations];
                            tempData.modifiers[i].destByte = int.Parse(modToks[0].Substring(1));
                            //Now start at token 2 and extract all operations. All ops past the first one
                            //use the implicit shadow register as the first operand.The contents of the shadow
                            //register are what is copied to the destination byte at the end.
                            //each op is of the form <first> <op> <second>. first and second could have more subtokens
                            string[] firstToks;
                            string[] secondToks;
                            int currToken = 2;
                            for (int j = 0; j < numOperations; j++)
                            {
                                tempData.modifiers[i].operations[j] = new ModifierOp();
                                tempData.modifiers[i].operations[j].first = new ModifierOperand();
                                tempData.modifiers[i].operations[j].second = new ModifierOperand();
                                if (j == 0)
                                {
                                    firstToks = modToks[currToken++].ToUpper().Split(':');
                                    //copy our sending bus and ID to use as the default for the modifier too
                                    tempData.modifiers[i].operations[j].first.bus = tempData.bus;
                                    tempData.modifiers[i].operations[j].first.ID = tempData.ID;
                                    parseOperandString(firstToks, ref tempData.modifiers[i].operations[j].first);
                                }
                                else //the rest of the ops implicitly use the shadow register as first
                                {
                                    tempData.modifiers[i].operations[j].first.ID = -1;
                                }
                                tempData.modifiers[i].operations[j].operation = parseOperation(modToks[currToken++].ToUpper());
                                secondToks = modToks[currToken++].ToUpper().Split(':');
                                tempData.modifiers[i].operations[j].second.bus = tempData.bus;
                                tempData.modifiers[i].operations[j].second.ID = tempData.ID;
                                parseOperandString(secondToks, ref tempData.modifiers[i].operations[j].second);
                            }
                        }
                    }
                }
            }
            //there is no else for the modifiers. We'll accept there not being any

            sendingData.Add(tempData);
        }

        //Turn a set of tokens into an operand
        private void parseOperandString(string[] tokens, ref ModifierOperand operand)
        {
            //bus:0:id:200:d3
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
            processGrid(e.RowIndex);
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //processGrid(e.Row.Index);
        }
    }

    //Here follows a bunch of classes that record and store the details for the trigger
    //and modifier system.

    //Stores a single trigger.
    class Trigger
    {
        public int ID;
        public int milliseconds;
        public int maxCount;
        public int currCount;
        public int bus;
    }

    //referece for a source location for a single modifier.
    //If ID is zero then this is a numeric operand. The number is then
    //stored in databyte
    //If ID is -1 then this is the temporary storage register. This is a shadow
    //register used to accumulate the results of a multi operation modifier.
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
        public int line;
        public int count;
        public Trigger[] triggers;
        public Modifier[] modifiers;
        //modifications
    }
}