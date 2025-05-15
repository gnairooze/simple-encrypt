using System;
using System.Text;
using System.IO;
using SimpleEncrypt;

class Program
{
    static byte[]? ReadKeyFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Key file '{filePath}' not found.");
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);
            string? base64Key = null;
            string? hexKey = null;

            // Look for the key in both formats
            foreach (string line in lines)
            {
                if (line.StartsWith("#")) continue; // Skip comments
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (base64Key == null && line.Length == 44) // Base64 key is 44 chars
                {
                    try
                    {
                        byte[] test = Convert.FromBase64String(line);
                        if (test.Length == 32) base64Key = line;
                    }
                    catch { }
                }
                else if (hexKey == null && line.Length == 64) // Hex key is 64 chars
                {
                    try
                    {
                        byte[] test = new byte[32];
                        for (int i = 0; i < 32; i++)
                        {
                            test[i] = Convert.ToByte(line.Substring(i * 2, 2), 16);
                        }
                        hexKey = line;
                    }
                    catch { }
                }
            }

            if (base64Key != null)
            {
                return Convert.FromBase64String(base64Key);
            }
            else if (hexKey != null)
            {
                byte[] key = new byte[32];
                for (int i = 0; i < 32; i++)
                {
                    key[i] = Convert.ToByte(hexKey.Substring(i * 2, 2), 16);
                }
                return key;
            }

            Console.WriteLine("Error: No valid key found in the file.");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading key file: {ex.Message}");
            return null;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("SimpleEncrypt Sample");
        Console.WriteLine("====================\n");

        // Read the encryption key from file
        string keyFilePath = "encryption.key";
        byte[]? key = ReadKeyFromFile(keyFilePath);
        
        if (key == null)
        {
            Console.WriteLine("Failed to read encryption key. Exiting.");
            return;
        }

        Console.WriteLine("Successfully loaded encryption key from file.");

        // Prompt user for input
        Console.Write("\nEnter text to encrypt: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("No input provided. Exiting.");
            return;
        }

        // Encrypt
        string encrypted;
        using (var encryptor = new SecureEncryption(key))
        {
            encrypted = encryptor.EncryptString(input);
        }
        Console.WriteLine("\nEncrypted (Base64):");
        Console.WriteLine(encrypted);

        // Decrypt
        string decrypted;
        using (var decryptor = new SecureEncryption(key))
        {
            decrypted = decryptor.DecryptString(encrypted);
        }
        Console.WriteLine("\nDecrypted:");
        Console.WriteLine(decrypted);

        // Check
        Console.WriteLine("\nMatch: " + (input == decrypted ? "Yes" : "No"));
    }
}
