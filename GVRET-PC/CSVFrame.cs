using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Time Stamp,ID,Extended,Bus,LEN,D1,D2,D3,D4,D5,D6,D7,D8
namespace GVRET
{
    class CSVFrame
    {
        public DateTime timestamp;
        public int ID;
        public bool extended;
        public int bus;
        public int len;
        public byte d0;
        public byte d1;
        public byte d2;
        public byte d3;
        public byte d4;
        public byte d5;
        public byte d6;
        public byte d7;
    }
}
