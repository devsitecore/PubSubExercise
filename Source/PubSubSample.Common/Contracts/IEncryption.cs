// <copyright file="IEncryption.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Common.Encryption
{
    using System;

    /// <summary>
    /// IEncryption
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// Encrypts to string.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>Encrypted string</returns>
        string EncryptToString(string plainText);

        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>Encrypted byte array</returns>
        byte[] Encrypt(string plainText);

        /// <summary>
        /// Decrypts the specified encrypted string.
        /// </summary>
        /// <param name="encryptedString">The encrypted string.</param>
        /// <returns>Plain text string</returns>
        string Decrypt(string encryptedString);

        /// <summary>
        /// Decrypts the specified encrypted value.
        /// </summary>
        /// <param name="encryptedValue">The encrypted value.</param>
        /// <returns>Plain text string</returns>
        string Decrypt(byte[] encryptedValue);
    }
}
