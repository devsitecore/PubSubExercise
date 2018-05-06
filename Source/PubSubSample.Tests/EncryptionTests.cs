namespace PubSubSample.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PubSubSample.Common.Encryption;
    using System.Security.Cryptography;

    [TestClass]
    public class EncryptionTests
    {
        /// <summary>
        /// TestEncryptToString
        /// </summary>
        [TestMethod]
        public void TestEncryptToString()
        {
            string plainText = "Test string Goes Here";
            
            IEncryption simpleEncryption = new SimpleEncryption();
            string encryptedText = simpleEncryption.EncryptToString(plainText);

            //Make sure that the encryption method is making some transformation to the plain text
            Assert.AreNotEqual(plainText, encryptedText);
        }

        /// <summary>
        /// TestDecryptToString
        /// </summary>
        [TestMethod]
        public void TestDecryptToString()
        {
            IEncryption simpleEncryption = new SimpleEncryption();

            string plainText = "Test string Goes Here";

            string encryptedText = simpleEncryption.EncryptToString(plainText);
            string decryptedText = simpleEncryption.Decrypt(encryptedText);

            //Make sure that we are able to get back the same plain text
            Assert.AreEqual(plainText, decryptedText);
        }

        /// <summary>
        /// TestDecryptToString
        /// </summary>
        [TestMethod]
        public void TestDecryptSingleCharStringToString()
        {
            IEncryption simpleEncryption = new SimpleEncryption();

            string plainText = "a";

            string encryptedText = simpleEncryption.EncryptToString(plainText);
            string decryptedText = simpleEncryption.Decrypt(encryptedText);

            //Make sure that we are able to get back the same plain text
            Assert.AreEqual(plainText, decryptedText);
        }
    }
}
