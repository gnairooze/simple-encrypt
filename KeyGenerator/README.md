# KeyGenerator

A .NET 8 console application that generates secure encryption keys for use with the SimpleEncrypt library. This tool helps you create cryptographically secure random keys and save them in a format that can be used by other applications in the solution.

## Features

- Generates cryptographically secure random 32-byte (256-bit) keys using `RNGCryptoServiceProvider`
- Displays the generated key in both Base64 and Hex formats
- Option to save the key to a file (with a default name of `encryption.key`) along with a timestamp and a warning header
- Simple, interactive console interface

## Usage

### Building the Project

To build the KeyGenerator, run the following command from the solution root:

```sh
dotnet build
```

### Running the KeyGenerator

Launch the KeyGenerator by running:

```sh
dotnet run --project KeyGenerator
```

### Interactive Menu

Once running, the KeyGenerator presents a menu with the following options:

1. **Generate new encryption key** – This option generates a new key and prints it (in Base64 and Hex) to the console.
2. **Generate and save key to file** – This option generates a new key and prompts you for a file path (or uses the default, `encryption.key`). The key is then saved (in both Base64 and Hex formats) along with a timestamp and a warning header.
3. **Exit** – Exits the application.

### Example Output (Option 1)

```
SimpleEncrypt Key Generator
===========================

Choose an option:
1. Generate new encryption key
2. Generate and save key to file
3. Exit

Enter your choice (1-3): 1

Generating new encryption key...

Key in different formats:
------------------------
Base64: 8K7X9P2mN5vL3jH1kF4dR7tY0wQ8cB2nM6xV9zA5sE3hJ
Hex:    8K7X9P2mN5vL3jH1kF4dR7tY0wQ8cB2nM6xV9zA5sE3hJ
Length: 32 bytes (256 bits)
```

### Example Output (Option 2)

If you choose option 2, you'll be prompted for a file path (or press Enter to use the default, `encryption.key`). The generated key (in Base64 and Hex) is then saved along with a header (for example):

```
# Encryption Key Generated: 2023-10-01T12:00:00.000Z
# WARNING: Keep this file secure and never share it!

# Base64 Format (32 bytes, 256 bits)
8K7X9P2mN5vL3jH1kF4dR7tY0wQ8cB2nM6xV9zA5sE3hJ

# Hex Format (32 bytes, 256 bits)
8K7X9P2mN5vL3jH1kF4dR7tY0wQ8cB2nM6xV9zA5sE3hJ
```

## Notes

- **Security:**  
  – The generated key is cryptographically secure and suitable for AES-256 encryption (as used by the SimpleEncrypt library).  
  – Always store your key securely (for example, in a vault or a file with restricted permissions) and never hardcode it in your source code.

- **Integration:**  
  – The generated key file (for example, `encryption.key`) is intended to be used by other projects (such as SimpleEncryptSample) that require an encryption key.

- **Educational Use:**  
  – This tool is provided for educational and demonstration purposes. In production, follow your organization's security guidelines for key management.

## References

- [SimpleEncrypt Library](../SimpleEncrypt/README.md)  
- [Microsoft Docs: RNGCryptoServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rngcryptoserviceprovider)

---

**Note:** This README is for educational purposes. Always follow your organization's security best practices for key generation and storage. 