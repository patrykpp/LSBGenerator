using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;



namespace LSBGenerator___PD
{
    public class AES
    {
        private static byte[] _random = Encoding.ASCII.GetBytes("ascnzxjytofdebmnlpoka1234n09");

 
        public static string Syfrowanie(string tekst, string haslo)
        {
            if (string.IsNullOrEmpty(tekst))
                throw new ArgumentNullException("tekst");
            if (string.IsNullOrEmpty(haslo))
                throw new ArgumentNullException("sekret");

            string outStr = null;                       
            RijndaelManaged aesAlg = null;              

            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(haslo, _random);

                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(tekst);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return outStr;
        }

        
        public static string Deszyfrowanie(string ukryty_teskt, string haslo)
        {
            if (string.IsNullOrEmpty(ukryty_teskt))
                throw new ArgumentNullException("ukryty_teskt");
            if (string.IsNullOrEmpty(haslo))
                throw new ArgumentNullException("hasło");

         
            RijndaelManaged aesAlg = null;

            
            string teskt = null;

            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(haslo, _random);

                byte[] bytes = Convert.FromBase64String(ukryty_teskt);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                  
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = Czytaj_Bajty(msDecrypt);
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                           
                            teskt = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return teskt;
        }

        private static byte[] Czytaj_Bajty(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] bufor = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(bufor, 0, bufor.Length) != bufor.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return bufor;
        }
    }
}

