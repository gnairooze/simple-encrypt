# Simple-Encrypt

This repository contains a set of projects that demonstrate secure encryption (using AES-256) in a .NET environment. Below is a summary of each project's README:

## SimpleEncrypt

**Summary:**  
The SimpleEncrypt project is a .NET Standard 2.0 class library that provides a `SecureEncryption` class. This class (using AES‑256 in CBC mode) encrypts and decrypts data (strings or byte arrays) using a 32‑byte (256‑bit) encryption key. The library is designed to be secure (using a unique IV per encryption) and easy to integrate.  
**Key points:**  
- **Features:** AES‑256 encryption, secure key handling, support for encrypting/decrypting strings and byte arrays, and proper disposal of sensitive resources.  
- **Usage:** Instantiate `SecureEncryption` (passing a 32‑byte key) and call `EncryptString` (or `EncryptBytes`) and `DecryptString` (or `DecryptBytes`).  
- **Security:** The key is never hardcoded; in production, use a secure vault (such as Azure Key Vault, AWS Secrets Manager, or HashiCorp Vault) or a file with restricted permissions.  
- **Limitations:** The key must be exactly 32 bytes (256 bits).  
- **Reference:** [SimpleEncrypt README](./SimpleEncrypt/README.md)

## KeyGenerator

**Summary:**  
KeyGenerator is a .NET 8 console application that generates cryptographically secure random 32‑byte (256‑bit) keys (using `RNGCryptoServiceProvider`) for use with the SimpleEncrypt library. It offers an interactive menu (via `dotnet run --project KeyGenerator`) with options to generate a key (and print it in Base64 and Hex) or to generate and save the key (along with a timestamp and a warning header) to a file (for example, `encryption.key`).  
**Key points:**  
- **Features:** Generates a cryptographically secure key, displays it in Base64 and Hex, and optionally saves it to a file.  
- **Usage:** Build (using `dotnet build`) and run (using `dotnet run --project KeyGenerator`) the project.  
- **Integration:** The generated key file (for example, `encryption.key`) is intended for use by other projects (such as SimpleEncryptSample) that require an encryption key.  
- **Educational Use:** Provided for educational and demonstration purposes; in production, follow your organization's security guidelines.  
- **Reference:** [KeyGenerator README](./KeyGenerator/README.md)

## SimpleEncryptSample

**Summary:**  
SimpleEncryptSample is a .NET 8 console application that demonstrates how to use the SimpleEncrypt library. It reads an encryption key (from an `encryption.key` file) and then prompts the user for a string to encrypt. The sample encrypts the string (using the key) and then decrypts it (using the same key) to verify that the decrypted text matches the original input.  
**Key points:**  
- **Features:** Reads a key (in Base64 or Hex) from a file, encrypts a user‑provided string, decrypts it, and verifies the result.  
- **Usage:** Build (using `dotnet build`) and run (using `dotnet run --project SimpleEncryptSample`) the project.  
- **Limitations:** The sample expects the key file (named `encryption.key`) to be present (and copied to the output directory on build) and the key must be exactly 32 bytes (256 bits).  
- **Secure Key Storage:** In production, use a secure vault (for example, Azure Key Vault, AWS Secrets Manager, or HashiCorp Vault) or a file with restricted permissions.  
- **Reference:** [SimpleEncryptSample README](./SimpleEncryptSample/README.md)

---

**Note:**  
This summary is for educational purposes. Always follow your organization's security best practices (for example, secure key storage, key rotation, and compliance) when using encryption in production. 