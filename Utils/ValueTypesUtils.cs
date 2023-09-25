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

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

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

        public static string RemoveMultipleSpaces(this string @this)
        {
            return Regex.Replace(@this, @"\s+", " ");
        }

        /// <summary>
        /// https://www.dotnetperls.com/between-before-after
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string SubstringBetween(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }
    }
}
