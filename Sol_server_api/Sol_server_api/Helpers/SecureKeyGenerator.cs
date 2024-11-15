using System;
using System.Security.Cryptography;

public static class SecureKeyGenerator
{
    public static string GenerateSecureKey(int keySizeInBytes)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomNumber = new byte[keySizeInBytes];
            rng.GetBytes(randomNumber);


            return Convert.ToBase64String(randomNumber);
        }
    }
}
