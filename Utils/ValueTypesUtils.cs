using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public static class ValueTypesUtils
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

        public static string RemoveLastChar(this string s, char c)
        {
            int index = s.LastIndexOf(c); //remover ultima coma
            if (index > -1)
                return s.Remove(index, 1);
            return s;
        }

        public static string RemoveDigits(this string key)
        {
            return Regex.Replace(key, @"\d", "");
        }
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.IndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        public static bool ToBool(this string @this)
        {
            string s = @this.Substring(0, 1).ToLower();
            if (s == "t" || s == "1" || s == "s" || s == "y" || s == "o") return true;
            return false;
        }

        public static bool ToBool(this int @this)
        {
            return @this.ToString().ToBool();
        }

        public static char ToChar(this string @this)
        {
            return @this.ToCharArray()[0];
        }
    }
}
