using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRET
{
    class Utility
    {
        //turn a string into an integer with support for hex, binary, and decimal
        //as well as automatic handling of exceptions
        //static so it can be called from anywhere. It has no class state.
        static public int ParseStringToNum(string input)
        {
            int temp = 0;
            try
            {
                input = input.ToUpper();
                if (input.StartsWith("0X")) //hex number
                {
                    temp = int.Parse(input.Substring(2), System.Globalization.NumberStyles.HexNumber);
                }
                else if (input.StartsWith("B")) //binary number
                {
                    input = input.Substring(1); //remove the B
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] == '1') temp += 1 << (input.Length - i - 1);
                    }
                }
                else //decimal number
                {
                    temp = int.Parse(input);
                }
            }
            catch (Exception c)
            {
                Debug.Print(c.ToString());
            }
            return temp;
        }

        //Returns # of milliseconds since the start of today. This gives something to fill in
        //time stamps with if we don't have anything better to use.
        static public UInt32 GetTimeMS() 
        {
            DateTime stamp = DateTime.Now;
            return (UInt32)(((stamp.Hour * 3600) + (stamp.Minute * 60) + (stamp.Second) * 1000) + stamp.Millisecond);
        } 
    }
}
