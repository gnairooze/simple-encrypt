using System;
using System.Threading.Tasks;
using System.IO;
using SimpleEncrypt;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("SimpleEncrypt Key Generator");
        Console.WriteLine("===========================");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Generate new encryption key");
            Console.WriteLine("2. Generate and save key to file");
            Console.WriteLine("3. Exit");
            Console.Write("\nEnter your choice (1-3): ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    GenerateAndDisplayKey();
                    break;
                case 2:
                    await GenerateAndSaveKeyAsync();
                    break;
                case 3:
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                    break;
            }
        }
    }

    static void GenerateAndDisplayKey()
    {
        Console.WriteLine("\nGenerating new encryption key...");
        byte[] key = SecureEncryption.GenerateKey();

        Console.WriteLine("\nKey in different formats:");
        Console.WriteLine("------------------------");
        Console.WriteLine($"Base64: {Convert.ToBase64String(key)}");
        Console.WriteLine($"Hex:    {BitConverter.ToString(key).Replace("-", "")}");
        Console.WriteLine($"Length: {key.Length} bytes (256 bits)");
    }

    static async Task GenerateAndSaveKeyAsync()
    {
        Console.Write("\nEnter the path to save the key (default: encryption.key): ");
        string? path = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrWhiteSpace(path))
        {
            path = "encryption.key";
        }

        try
        {
            Console.WriteLine("\nGenerating new encryption key...");
            byte[] key = SecureEncryption.GenerateKey();

            // Save the key in both formats
            string base64Key = Convert.ToBase64String(key);
            string hexKey = BitConverter.ToString(key).Replace("-", "");

            // Create the content with both formats and a timestamp
            var content = new StringBuilder();
            content.AppendLine("# Encryption Key Generated: " + DateTime.UtcNow.ToString("o"));
            content.AppendLine("# WARNING: Keep this file secure and never share it!");
            content.AppendLine();
            content.AppendLine("# Base64 Format (32 bytes, 256 bits)");
            content.AppendLine(base64Key);
            content.AppendLine();
            content.AppendLine("# Hex Format (32 bytes, 256 bits)");
            content.AppendLine(hexKey);

            await File.WriteAllTextAsync(path, content.ToString());

            Console.WriteLine($"\nKey successfully saved to: {Path.GetFullPath(path)}");
            Console.WriteLine("\nFile contains both Base64 and Hex formats of the key.");
            Console.WriteLine("WARNING: Keep this file secure and never share it!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError saving key: {ex.Message}");
        }
    }
} 