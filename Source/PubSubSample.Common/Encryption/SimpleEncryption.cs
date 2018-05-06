// <copyright file="SimpleEncryption.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Common.Encryption
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// SimpleAES
    /// http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp
    /// </summary>
    public sealed class SimpleEncryption : IEncryption
    {
        #region "Private Members"
        private byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };
        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleEncryption"/> class.
        /// </summary>
        public SimpleEncryption()
        {
            var symmetricAlgorithm = new RijndaelManaged();

            // Create an encryptor and a decryptor using our encryption method, key, and vector.
            this.EncryptorTransform = symmetricAlgorithm.CreateEncryptor(this.key, this.vector);
            this.DecryptorTransform = symmetricAlgorithm.CreateDecryptor(this.key, this.vector);

            // Used to translate bytes to text and vice versa
            this.UTFEncoder = new UTF8Encoding();
        }
        #endregion

        #region "Private Properties"

        /// <summary>
        /// Gets or sets the encryptor transform.
        /// </summary>
        /// <value>
        /// The encryptor transform.
        /// </value>
        private ICryptoTransform EncryptorTransform { get; set; }

        /// <summary>
        /// Gets or sets the decryptor transform.
        /// </summary>
        /// <value>
        /// The decryptor transform.
        /// </value>
        private ICryptoTransform DecryptorTransform { get; set; }

        /// <summary>
        /// Gets or sets the utf encoder.
        /// </summary>
        /// <value>
        /// The utf encoder.
        /// </value>
        private UTF8Encoding UTFEncoder { get; set; }
        #endregion

        #region "ISimpleEncryption Members"

        /// <summary>
        /// EncryptToString - Encrypt some text and return a string suitable for passing in a URL.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>
        /// Encrypted string
        /// </returns>
        public string EncryptToString(string plainText)
        {
            return this.ByteArrToString(this.Encrypt(plainText));
        }

        /// <summary>
        /// Encrypt - Encrypt some text and return an encrypted byte array.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>
        /// Encrypted byte array
        /// </returns>
        public byte[] Encrypt(string plainText)
        {
            // Translates our text value into a byte array.
            byte[] bytes = this.UTFEncoder.GetBytes(plainText);

            // Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            /*
             * We will have to write the unencrypted bytes to the stream,
             * then read the encrypted result back from the stream.
             */

            CryptoStream cs = new CryptoStream(memoryStream, this.EncryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();

            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);

            // Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// <summary>
        /// Decrypt - The other side: Decryption methods
        /// </summary>
        /// <param name="encryptedString">The encrypted string.</param>
        /// <returns>Plain text</returns>
        public string Decrypt(string encryptedString)
        {
            return this.Decrypt(this.StrToByteArray(encryptedString));
        }

        /// <summary>
        /// Decrypts the specified encrypted value.
        /// </summary>
        /// <param name="encryptedValue">The encrypted value.</param>
        /// <returns>Plain text</returns>
        public string Decrypt(byte[] encryptedValue)
        {
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream(encryptedStream, this.DecryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(encryptedValue, 0, encryptedValue.Length);
            decryptStream.FlushFinalBlock();

            encryptedStream.Position = 0;
            var decryptedBytes = new byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();

            return this.UTFEncoder.GetString(decryptedBytes);
        }
        #endregion

        #region "Private Methods"

        /// <summary>
        /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        /// System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        /// return encoding.GetBytes(str);
        /// However, this results in character values that cannot be passed in a URL.  So, instead, I just
        /// lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>byte array</returns>
        /// <exception cref="Exception">Invalid string value in StrToByteArray</exception>
        private byte[] StrToByteArray(string str)
        {
            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        /// <summary>
        /// Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
        /// System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        /// return enc.GetString(byteArr);
        /// </summary>
        /// <param name="byteArr">The byte arr.</param>
        /// <returns>string data</returns>
        private string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = string.Empty;

            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                {
                    tempStr += "00" + val.ToString();
                }
                else if (val < (byte)100)
                {
                    tempStr += "0" + val.ToString();
                }
                else
                {
                    tempStr += val.ToString();
                }
            }

            return tempStr;
        }
        #endregion
    }
}
