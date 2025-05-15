# SimpleEncrypt

A simple and secure encryption library for .NET Standard 2.0 that provides AES-256 encryption and decryption functionality.

## Features

- AES-256 encryption (industry standard symmetric encryption)
- Secure key handling
- Support for both string and byte array encryption/decryption
- Proper IV (Initialization Vector) generation and management
- Cryptographically secure random key generation
- Implements IDisposable for proper resource cleanup

## Installation

Add the NuGet package to your project:

```xml
<PackageReference Include="SimpleEncrypt" Version="1.0.0" />
```

## Usage

### Basic String Encryption/Decryption

```csharp
// Generate a secure random key
byte[] key = SecureEncryption.GenerateKey();

// Create an instance of the encryption class
using (var encryption = new SecureEncryption(key))
{
    // Encrypt a string
    string plainText = "Hello, World!";
    string encrypted = encryption.EncryptString(plainText);
    
    // Decrypt the string
    string decrypted = encryption.DecryptString(encrypted);
    
    // decrypted == "Hello, World!"
}
```

### Working with Byte Arrays

```csharp
using (var encryption = new SecureEncryption(key))
{
    // Encrypt byte array
    byte[] plainBytes = Encoding.UTF8.GetBytes("Hello, World!");
    byte[] encryptedBytes = encryption.EncryptBytes(plainBytes);
    
    // Decrypt byte array
    byte[] decryptedBytes = encryption.DecryptBytes(encryptedBytes);
    string decrypted = Encoding.UTF8.GetString(decryptedBytes);
}
```

### Important Notes

1. The encryption key must be exactly 32 bytes (256 bits) for AES-256 encryption.
2. Always use the `using` statement when working with the `SecureEncryption` class to ensure proper resource cleanup.
3. Store your encryption keys securely. Never hardcode them in your source code.
4. The encrypted strings are returned in Base64 format for easy storage and transmission.
5. Each encryption operation uses a unique IV (Initialization Vector) for enhanced security.

## Security Considerations

- The library uses AES-256 encryption in CBC mode with PKCS7 padding.
- Each encryption operation generates a unique IV.
- The key is securely stored and cleared from memory when the object is disposed.
- The library uses cryptographically secure random number generation for key generation.
- All sensitive data is properly disposed of when no longer needed.

## License

MIT License 