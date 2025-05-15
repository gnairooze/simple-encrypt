# SimpleEncryptSample

This is a sample .NET 8 console application that demonstrates how to use the [SimpleEncrypt](../SimpleEncrypt/README.md) library to securely encrypt and decrypt data using AES-256.

## What It Does
- **Reads an encryption key** from an `encryption.key` file (in Base64 or Hex format)
- **Prompts the user** for a string to encrypt
- **Encrypts** the string using the key and displays the encrypted Base64 output
- **Decrypts** the encrypted string using the same key and displays the original text
- **Verifies** that the decrypted text matches the original input

## How It Uses SimpleEncrypt
- The app uses the `SecureEncryption` class from the SimpleEncrypt library.
- The key is loaded from a file and passed to the `SecureEncryption` constructor.
- The `EncryptString` and `DecryptString` methods are used for encryption and decryption.

## Usage
1. **Generate a key** (if you don't have one) using the KeyGenerator project:
   ```sh
   dotnet run --project ../KeyGenerator
   ```
   Choose option 2 to save the key as `encryption.key`.

2. **Build and run the sample:**
   ```sh
   dotnet build
   dotnet run --project SimpleEncryptSample
   ```

3. **Follow the prompts** to enter text for encryption and see the results.

## Limitations
- The sample expects the key file to be named `encryption.key` and located in the project root (it is copied to the output directory on build).
- The key must be exactly 32 bytes (256 bits) in Base64 (44 chars) or Hex (64 chars) format.
- The sample is for demonstration purposes and does not implement advanced error handling or secure key management for production.

## Secure Key Storage Advice
- **Never hardcode encryption keys in your source code.**
- For development or simple scenarios, storing the key in a file (with restricted permissions) may be sufficient.
- For production or sensitive applications, use a secure vault or key management service, such as:
  - **Azure Key Vault** (for Azure apps)
  - **AWS Secrets Manager** or **AWS KMS** (for AWS apps)
  - **HashiCorp Vault** (for on-premises or multi-cloud)
  - **Windows Data Protection API (DPAPI)** for Windows apps
- Always restrict access to the key to only those processes/users that require it.

## References
- [SimpleEncrypt Library](../SimpleEncrypt/README.md)
- [Microsoft Docs: Securely storing application secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)

---

**Note:** This sample is for educational purposes. For real-world applications, always follow your organization's security best practices for key management. 
