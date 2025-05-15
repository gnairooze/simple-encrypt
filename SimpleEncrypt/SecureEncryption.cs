using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleEncrypt
{
    /// <summary>
    /// Provides secure encryption and decryption functionality using AES encryption.
    /// </summary>
    public class SecureEncryption : IDisposable
    {
        private readonly byte[] _key;
        private readonly Aes _aes;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the SecureEncryption class with the specified key.
        /// </summary>
        /// <param name="key">The encryption key. Must be 32 bytes (256 bits) for AES-256.</param>
        /// <exception cref="ArgumentNullException">Thrown when the key is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the key length is not 32 bytes.</exception>
        public SecureEncryption(byte[] key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Encryption key cannot be null.");
            
            if (key.Length != 32)
                throw new ArgumentException("Key must be exactly 32 bytes (256 bits) for AES-256 encryption.", nameof(key));

            _key = new byte[32];
            Buffer.BlockCopy(key, 0, _key, 0, 32);

            _aes = Aes.Create();
            _aes.Key = _key;
            _aes.Mode = CipherMode.CBC;
            _aes.Padding = PaddingMode.PKCS7;
        }

        /// <summary>
        /// Encrypts a string using AES encryption.
        /// </summary>
        /// <param name="plainText">The string to encrypt.</param>
        /// <returns>The encrypted string in Base64 format.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the plainText is null.</exception>
        public string EncryptString(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText), "Plain text cannot be null.");

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = EncryptBytes(plainBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts an encrypted string.
        /// </summary>
        /// <param name="encryptedText">The encrypted string in Base64 format.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the encryptedText is null.</exception>
        /// <exception cref="FormatException">Thrown when the encryptedText is not a valid Base64 string.</exception>
        /// <exception cref="CryptographicException">Thrown when decryption fails.</exception>
        public string DecryptString(string encryptedText)
        {
            if (encryptedText == null)
                throw new ArgumentNullException(nameof(encryptedText), "Encrypted text cannot be null.");

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = DecryptBytes(encryptedBytes);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (FormatException)
            {
                throw new FormatException("The encrypted text is not a valid Base64 string.");
            }
        }

        /// <summary>
        /// Encrypts a byte array using AES encryption.
        /// </summary>
        /// <param name="plainBytes">The byte array to encrypt.</param>
        /// <returns>The encrypted byte array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the plainBytes is null.</exception>
        public byte[] EncryptBytes(byte[] plainBytes)
        {
            if (plainBytes == null)
                throw new ArgumentNullException(nameof(plainBytes), "Plain bytes cannot be null.");

            _aes.GenerateIV();
            byte[] iv = _aes.IV;

            using (var encryptor = _aes.CreateEncryptor())
            using (var msEncrypt = new MemoryStream())
            {
                // Write the IV at the beginning of the stream
                msEncrypt.Write(iv, 0, iv.Length);

                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var bwEncrypt = new BinaryWriter(csEncrypt))
                {
                    bwEncrypt.Write(plainBytes);
                }

                return msEncrypt.ToArray();
            }
        }

        /// <summary>
        /// Decrypts an encrypted byte array.
        /// </summary>
        /// <param name="encryptedBytes">The encrypted byte array.</param>
        /// <returns>The decrypted byte array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the encryptedBytes is null.</exception>
        /// <exception cref="CryptographicException">Thrown when decryption fails.</exception>
        public byte[] DecryptBytes(byte[] encryptedBytes)
        {
            if (encryptedBytes == null)
                throw new ArgumentNullException(nameof(encryptedBytes), "Encrypted bytes cannot be null.");

            if (encryptedBytes.Length < 16)
                throw new CryptographicException("Encrypted data is too short to be valid.");

            // Extract the IV from the beginning of the stream
            byte[] iv = new byte[16];
            Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
            _aes.IV = iv;

            using (var decryptor = _aes.CreateDecryptor())
            using (var msDecrypt = new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var msPlain = new MemoryStream())
            {
                csDecrypt.CopyTo(msPlain);
                return msPlain.ToArray();
            }
        }

        /// <summary>
        /// Generates a cryptographically secure random key for AES-256 encryption.
        /// </summary>
        /// <returns>A 32-byte (256-bit) random key.</returns>
        public static byte[] GenerateKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[32];
                rng.GetBytes(key);
                return key;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _aes?.Dispose();
                    Array.Clear(_key, 0, _key.Length);
                }
                _disposed = true;
            }
        }

        ~SecureEncryption()
        {
            Dispose(false);
        }
    }
} 