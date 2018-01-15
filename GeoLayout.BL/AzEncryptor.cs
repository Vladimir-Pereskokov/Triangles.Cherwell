using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace GeoLayout.BL
{
    internal static class AzEncryptor
    {

        private static Dictionary<char, int> _colCharToInt;
        private static char[] _colIntToChar;

        private static void init()
        {
            if (_colCharToInt == null)
            {
                _colIntToChar = new char[26];
                _colCharToInt = new Dictionary<char, int>();
                for (int ascI = 65; ascI < 91; ascI++)
                {
                    var intN = ascI - 65;
                    var c = (char)ascI;
                    _colIntToChar[intN] = c;
                    _colCharToInt.Add(c, intN);
                }
            }
        }

        private static int ToInt(char c)
        {
            init();
            if (_colCharToInt.TryGetValue(char.ToUpper(c), out var Nr)) return Nr;
            return -1;
        }

        private static char ToChar(int value)
        {
            init();
            if (value > -1 && value < 26) return _colIntToChar[value];
            return char.MinValue;
        }


        public static int Decrypt(string address)
        {
            int intResult = -1;
            if (!string.IsNullOrWhiteSpace(address))
            {
                address = address.Trim();
                intResult = 0;
                for (int chrIDX = address.Length - 1; chrIDX > -1; chrIDX--)
                {
                    var intValue = ToInt(address[chrIDX]);
                    if (intValue < 0) return intValue; //invalid char                    
                    var pr = address.Length - chrIDX - 1;                    
                    intResult += intValue * (int)(Math.Pow(26, pr));
                }
            }
            return intResult;
        }


        public static int DecryptAZ(this string address) => Decrypt(address);

        public static bool TryDecryptAZ(this string address, out int result) {
            result = Decrypt(address);
            return result > -1;
        }

        public static string Encrypt(int address)
        {
            if (address > -1)
            {
                var sb = new StringBuilder();
                if (address > 25)
                {
                    int currentNr = address;
                    while (currentNr > -1)
                    {
                        int modulo = currentNr % 26;
                        currentNr -= modulo;
                        if (modulo != 0 || currentNr > 0)
                        {
                            sb.Append(ToChar(modulo));
                            currentNr /= 26;
                        }
                    }
                }
                else sb.Append(ToChar(address));

                if (sb.Length > 1)
                {
                    var result = new char[sb.Length];
                    sb.CopyTo(0, result, 0, sb.Length);
                    Array.Reverse<char>(result);
                    return new string(result);
                }
                else return sb.ToString();
            }
            return null;
        }

        public static string EncryptAZ(this int address) => Encrypt(address);


        public static bool TryEncryptAZ(this int address, out string result) {
            result = Encrypt(address);
            return !string.IsNullOrEmpty(result);
        }

    }
}
