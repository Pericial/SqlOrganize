using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class StringUtils
    {
        public static char GetNextChar(this char c)
        {
            // convert char to ascii
            int ascii = (int)c;
            // get the next ascii
            int nextAscii = ascii + 1;
            // convert ascii to char
            char nextChar = (char)nextAscii;

            return nextChar;
        }
    }
}
