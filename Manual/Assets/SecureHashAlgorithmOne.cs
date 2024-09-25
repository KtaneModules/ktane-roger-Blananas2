using System;

namespace ModifiedMessageCode
{
    public class SecureHashAlgorithmOne
    {
        private ulong[] _HInitial = { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0 };
        private ulong[] _KInitial = { 0x5A827999, 0x6ED9EBA1, 0x8F1BBCDC, 0xCA62C1D6 };

        private ulong[] _H = { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0 };
        private ulong[] _K = { 0x5A827999, 0x6ED9EBA1, 0x8F1BBCDC, 0xCA62C1D6 };

        private ulong[] _LetterConstants = new ulong[5];

        /// <summary>
        /// Creates a new instance of SHA1 hash algorithm.
        /// </summary>
        /// <param name="H">Initial hash result values, as an array of 5 ulongs. Leave the array blank and of length 0 if you wish to use the standard SHA1 hash.</param>
        /// <param name="K">Constants to be used in the hashing algorithm, as an array of 4 ulongs. Do the same as H if you wish to use the standard SHA1 hash.</param>
        /// <exception cref="ArgumentException">When H is not empty but also not of length 5, or K is not empty but also not of length 4.</exception>
        public SecureHashAlgorithmOne(ulong[] H, ulong[] K)
        {
            if (H.Length != 0)
            {
                if (H.Length != 5)
                    throw new ArgumentException("H.Length must be 5.");
                else
                    for (int i = 0; i < _H.Length; i++)
                        _HInitial[i] = H[i];
            }
            if (K.Length != 0)
            {
                if (K.Length != 4)
                    throw new ArgumentException("K.Length must be 4.");
                else
                    for (int i = 0; i < _K.Length; i++)
                        _KInitial[i] = K[i];
            }
        }

        /// <summary>
        /// Calculates the SHA1 hash of a given input.
        /// </summary>
        /// <param name="message">UTF-8 string representing the input.</param>
        /// <returns>A string of 40 hex digits, representing the final hash of the message.</returns>
        public string MessageCode(string message)
        {
            for (int i = 0; i < 5; i++)
                _H[i] = _HInitial[i];

            for (int i = 0; i < 4; i++)
                _K[i] = _KInitial[i];

            string bitstring = "";
            for (int i = 0; i < message.Length; i++)
                bitstring += LeftZeroPad(Convert.ToString(message[i], 2), 8);

            bitstring = HashPad(bitstring);

            for (int i = 0; i < bitstring.Length / 512; i++)
                MessageCodeBlock(bitstring.Substring(512 * i, 512));

            string result = "";
            for (int i = 0; i < _H.Length; i++)
                result += LeftZeroPad(Convert.ToString((uint)_H[i], 16), 8);
            return result;
        }

        private void MessageCodeBlock(string block)
        {
            ulong[] words = new ulong[80];
            for (int i = 0; i < 16; i++)
                words[i] = Convert.ToUInt64(block.Substring(32 * i, 32), 2);

            for (int i = 0; i < _H.Length; i++)
                _LetterConstants[i] = _H[i];

            for (int t = 0; t < 80; t++)
            {
                if (15 < t)
                    words[t] = ShiftCircleLeft(words[t - 3] ^ words[t - 8] ^ words[t - 14] ^ words[t - 16], 1);

                ulong temp = Add(Add(Add(Add(ShiftCircleLeft(_LetterConstants[0], 5), f(t, _LetterConstants[1], _LetterConstants[2], _LetterConstants[3])), _LetterConstants[4]), words[t]), K(t));
                _LetterConstants[4] = _LetterConstants[3];
                _LetterConstants[3] = _LetterConstants[2];
                _LetterConstants[2] = ShiftCircleLeft(_LetterConstants[1], 30);
                _LetterConstants[1] = _LetterConstants[0];
                _LetterConstants[0] = temp;
            }

            for (int i = 0; i < _H.Length; i++)
                _H[i] = Add(_H[i], _LetterConstants[i]);
        }

        string LeftZeroPad(string s, int length)
        {
            string r = s;
            if (s.Length >= length)
                return s;
            while (r.Length < length)
                r = "0" + r;
            return r;
        }

        private ulong ShiftCircleLeft(ulong x, int n)
        {
            return ((x << n) | (x >> (32 - n))) & uint.MaxValue;
        }

        private ulong Add(ulong x, ulong y)
        {
            return (x + y) & uint.MaxValue;
        }

        private string HashPad(string bitstring)
        {
            string r = bitstring + "1";
            while (r.Length % 512 != 448)
                r += "0";
            r += ModifiedBinaryConvert(bitstring.Length);
            return r;
        }

        private string ModifiedBinaryConvert(int x)
        {
            string t = Convert.ToString(x, 2);
            while (t.Length < 64)
                t = "0" + t;
            return t;
        }

        private ulong f(int t, ulong B, ulong C, ulong D)
        {
            if (t < 20)
                return (B & C) | ((~B) & D);
            else if (t < 40)
                return B ^ C ^ D;
            else if (t < 60)
                return (B & C) | (B & D) | (C & D);
            else
                return B ^ C ^ D;
        }

        private ulong K(int t)
        {
            if (t < 20)
                return _K[0];
            else if (t < 40)
                return _K[1];
            else if (t < 60)
                return _K[2];
            else
                return _K[3];
        }
    }
}
