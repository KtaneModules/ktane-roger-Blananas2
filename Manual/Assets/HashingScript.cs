using System;
using System.Security.Cryptography;
using System.Text;

public class HashingScript
{
    public static string Sha1(string input)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
            return hashHex;
        }
    }
}