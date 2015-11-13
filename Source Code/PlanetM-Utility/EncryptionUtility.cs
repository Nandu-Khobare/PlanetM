using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PlanetM_Utility
{
    /// <summary>
    /// Encryption Util is used for Encrypt / Decrypt any text using 
    /// MD5, SHA1 Symmetric Encryption Algorithms
    /// 
    /// 
    /// </summary>

    public enum CryptoAlgoritms : int
    {
        Rijndael_MD5 = 0,
        Rijndael_SHA1,
        RC2_MD5,
        RC2_SHA1,
        DES,
        TripleDes_MD5,
        TripleDes_SHA1
    }

    public class EncryptionUtility
    {
        private static string saltValue = "AvadaKeDavara";
        private static string initVector = "`1234567890-=~!@";
        private static int keySize = 128;
        private static int passwordIterations = 2;

        public EncryptionUtility()
        {

        }

        public static string EncryptText(CryptoAlgoritms algorithm, string plainText, string password)
        {
            int passmethod = 0;
            string encrypted = "";
            string alg = "";
            switch (algorithm)
            {
                case CryptoAlgoritms.Rijndael_MD5:
                    alg = "MD5";
                    passmethod = 0;
                    break;
                case CryptoAlgoritms.Rijndael_SHA1:
                    alg = "SHA1";
                    passmethod = 0;
                    break;
                case CryptoAlgoritms.RC2_MD5:
                    alg = "MD5";
                    passmethod = 1;
                    break;
                case CryptoAlgoritms.RC2_SHA1:
                    alg = "SHA1";
                    passmethod = 1;
                    break;
                case CryptoAlgoritms.DES:
                    alg = "";
                    passmethod = 2;
                    break;
                case CryptoAlgoritms.TripleDes_MD5:
                    alg = "MD5";
                    passmethod = 3;
                    break;
                case CryptoAlgoritms.TripleDes_SHA1:
                    alg = "SHA1";
                    passmethod = 3;
                    break;
            }

            if (0 == passmethod)
            {
                encrypted = RijndaelEncrypt(alg, plainText, password);
            }

            if (1 == passmethod)
            {
                encrypted = RC2Encrypt(alg, plainText, password);
            }

            if (2 == passmethod)
            {
                encrypted = DESEncrypt(alg, plainText, password);
            }
            if (3 == passmethod)
            {
                encrypted = TripleDESEncrypt(alg, plainText, password);
            }
            return encrypted;
        }

        public static string DecryptText(CryptoAlgoritms algorithm, string cipherText, string password)
        {
            int passmethod = 0;
            string decrypted = "";
            string alg = "";
            switch (algorithm)
            {
                case CryptoAlgoritms.Rijndael_MD5:
                    alg = "MD5";
                    passmethod = 0;
                    break;
                case CryptoAlgoritms.Rijndael_SHA1:
                    alg = "SHA1";
                    passmethod = 0;
                    break;
                case CryptoAlgoritms.RC2_MD5:
                    alg = "MD5";
                    passmethod = 1;
                    break;
                case CryptoAlgoritms.RC2_SHA1:
                    alg = "SHA1";
                    passmethod = 1;
                    break;
                case CryptoAlgoritms.DES:
                    alg = "";
                    passmethod = 2;
                    break;
                case CryptoAlgoritms.TripleDes_MD5:
                    alg = "MD5";
                    passmethod = 3;
                    break;
                case CryptoAlgoritms.TripleDes_SHA1:
                    alg = "SHA1";
                    passmethod = 3;
                    break;
            }

            if (0 == passmethod)
            {
                decrypted = RijndaelDecrypt(alg, cipherText, password);
            }

            if (1 == passmethod)
            {
                decrypted = RC2Decrypt(alg, cipherText, password);
            }
            if (2 == passmethod)
            {
                decrypted = DESDecrypt(alg, cipherText, password);
            }
            if (3 == passmethod)
            {
                decrypted = TripleDESDecrypt(alg, cipherText, password);
            }
            return decrypted;
        }

        private static string RijndaelEncrypt(string algorithm, string PlainText, string PassWord)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);

            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);
            cryptStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptStream.FlushFinalBlock();

            byte[] cipherTextbytes = memStream.ToArray();

            memStream.Close();
            cryptStream.Close();

            return Convert.ToBase64String(cipherTextbytes);
        }

        private static string RijndaelDecrypt(string algorithm, string CipherText, string PassWord)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] ciphertextBytes = Convert.FromBase64String(CipherText);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream(ciphertextBytes);
            CryptoStream cryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[ciphertextBytes.Length];
            int decryptedByteCount = cryptStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memStream.Close();
            cryptStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        private static string RC2Encrypt(string algorithm, string PlainText, string PassWord)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
            rc2.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = rc2.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);

            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);
            cryptStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptStream.FlushFinalBlock();

            byte[] cipherTextbytes = memStream.ToArray();

            memStream.Close();
            cryptStream.Close();

            return Convert.ToBase64String(cipherTextbytes);

        }

        private static string RC2Decrypt(string algorithm, string CipherText, string PassWord)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] ciphertextBytes = Convert.FromBase64String(CipherText);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RC2CryptoServiceProvider symmetricKey = new RC2CryptoServiceProvider();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream(ciphertextBytes);
            CryptoStream cryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[ciphertextBytes.Length];
            int decryptedByteCount = cryptStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memStream.Close();
            cryptStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        private static string DESEncrypt(string algorithm, string PlainText, string PassWord)
        {
            if (8 != PassWord.Length)
            {
                throw new Exception("For DES Algorithm, password value should be 8 character length");
            }
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            /*PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);
            */
            byte[] keyBytes = Encoding.ASCII.GetBytes(PassWord);

            DESCryptoServiceProvider symmetricKey = new DESCryptoServiceProvider();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);

            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);
            cryptStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptStream.FlushFinalBlock();

            byte[] cipherTextbytes = memStream.ToArray();

            memStream.Close();
            cryptStream.Close();

            return Convert.ToBase64String(cipherTextbytes);
        }

        private static string DESDecrypt(string algorithm, string CipherText, string PassWord)
        {
            if (8 != PassWord.Length)
            {
                throw new Exception("For DES Algorithm, password value should be 8 character length");
            }

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] ciphertextBytes = Convert.FromBase64String(CipherText);

            /*PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);
            */
            byte[] keyBytes = Encoding.ASCII.GetBytes(PassWord);
            DESCryptoServiceProvider symmetricKey = new DESCryptoServiceProvider();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream(ciphertextBytes);
            CryptoStream cryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[ciphertextBytes.Length];
            int decryptedByteCount = cryptStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memStream.Close();
            cryptStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        private static string TripleDESEncrypt(string algorithm, string PlainText, string PassWord)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);

            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);
            cryptStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptStream.FlushFinalBlock();

            byte[] cipherTextbytes = memStream.ToArray();

            memStream.Close();
            cryptStream.Close();

            return Convert.ToBase64String(cipherTextbytes);
        }

        private static string TripleDESDecrypt(string algorithm, string CipherText, string PassWord)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] ciphertextBytes = Convert.FromBase64String(CipherText);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(PassWord, saltValueBytes, algorithm, passwordIterations);
            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            TripleDESCryptoServiceProvider symmetricKey = new TripleDESCryptoServiceProvider();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memStream = new MemoryStream(ciphertextBytes);
            CryptoStream cryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[ciphertextBytes.Length];
            int decryptedByteCount = cryptStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memStream.Close();
            cryptStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
