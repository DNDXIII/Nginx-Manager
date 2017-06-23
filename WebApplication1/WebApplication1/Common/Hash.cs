using System;
using System.Text;
using System.Security.Cryptography;


public class Hash
{
  
    public static string ComputeHash(string plainText,
                                     string salt)
    {
    
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] saltBytes= Encoding.UTF8.GetBytes(salt);

        byte[] plainTextWithSaltBytes =
                new byte[plainTextBytes.Length + saltBytes.Length];

        // Copy plain text bytes into resulting array.
        for (int i = 0; i < plainTextBytes.Length; i++)
            plainTextWithSaltBytes[i] = plainTextBytes[i];

        // Append salt bytes to the resulting array.
        for (int i = 0; i < saltBytes.Length; i++)
            plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

        // Compute hash value
        return Convert.ToBase64String(SHA256.Create().ComputeHash(plainTextWithSaltBytes));

        
    }
}