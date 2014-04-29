using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Aliencube.CryptoService.ConsoleApp
{
    /// <summary>
    /// This represents the main program entity.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Executes the console app.
        /// </summary>
        /// <param name="args">List of parameters manually set.</param>
        public static void Main(string[] args)
        {
            Splash();
            try
            {
                ProcessRequests(args);
            }
            catch (Exception ex)
            {
                ShowMessage(ex);
                ShowUsage();
            }
        }

        #region Methods

        /// <summary>
        ///	Shows the splash message.
        /// </summary>
        private static void Splash()
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("{0} v{1}", fvi.ProductName, fvi.FileVersion));
            sb.AppendLine("------------------------------");

            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="ex">Exception.</param>
        private static void ShowMessage(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ooops!");
            Console.ResetColor();
            Console.WriteLine(ex.Message);
        }

        /// <summary>
        /// Shows the usage message.
        /// </summary>
        private static void ShowUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("  AlienCryptoService.exe (/e|/d) /m:\"[TEXT]\" [/f:[FILENAME]] [/o:[FILENAME]]");
            sb.AppendLine();
            sb.AppendLine("Configuration:");
            sb.AppendLine("  Enter correct information into AlienCryptoService.exe.config before executing this application.");
            sb.AppendLine();
            sb.AppendLine("Parameters:");
            sb.AppendLine("  /e             Encrypt the text or text file.");
            sb.AppendLine("  /d             Decrypt the text or text file.");
            sb.AppendLine("  /t:\"[TEXT]\"  Text to encrypt or decrypt. It must be enclosed by double quotes.");
            sb.AppendLine("  /f:[FILENAME]  Filename containing text to encrypt or decrypt.");
            sb.AppendLine("  /o:[FILENAME]  Filename to store text encrypted or decrypted.");
            sb.AppendLine();
            sb.AppendLine("  NOTE: /e and /d cannot be used at the same time.");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Checks whether the key value has valid length or not.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <param name="provider">Cryption provider name.</param>
        /// <param name="validKeyLength">Valid key length for the cryption provider.</param>
        /// <returns>Returns <c>True</c>, if the key has valid length; otherwise returns <c>False.</c>.</returns>
        private static bool IsValidKey(string key, string provider, out int validKeyLength)
        {
            var length = 0;
            switch (provider.ToLower())
            {
                case "aes":
                    length = 32;
                    break;

                case "des":
                    length = 8;
                    break;

                case "rc2":
                    length = 16;
                    break;

                case "rijndael":
                    length = 32;
                    break;

                case "tripledes":
                    length = 24;
                    break;
            }
            validKeyLength = length;
            return length == key.Length;
        }

        /// <summary>
        /// Checks whether the vector value has valid length or not.
        /// </summary>
        /// <param name="vector">Vector value.</param>
        /// <param name="provider">Cryption provider name.</param>
        /// <param name="validVectorLength">Valid vector length for the cryption provider.</param>
        /// <returns>Returns <c>True</c>, if the vector has valid length; otherwise returns <c>False.</c>.</returns>
        private static bool IsValidVector(string vector, string provider, out int validVectorLength)
        {
            var length = 0;
            switch (provider.ToLower())
            {
                case "aes":
                    length = 16;
                    break;

                case "des":
                    length = 8;
                    break;

                case "rc2":
                    length = 8;
                    break;

                case "rijndael":
                    length = 16;
                    break;

                case "tripledes":
                    length = 8;
                    break;
            }
            validVectorLength = length;
            return length == vector.Length;
        }

        /// <summary>
        /// Processes the requests.
        /// </summary>
        /// <param name="args">List of parameters manually set.</param>
        /// <exception cref="ArgumentException">Thrown when arguments are not in expected format.</exception>
        /// <exception cref="ConfigurationErrorsException">Thrown when a passphrase is not defined.</exception>
        /// <exception cref="FileNotFoundException">Thrown when a file does not exist on the given path.</exception>
        /// <exception cref="FormatException">Thrown when a passphrase does not have a proper length.</exception>
        private static void ProcessRequests(string[] args)
        {
            var provider = ConfigurationManager.AppSettings["CryptionProvider"];
            var key = ConfigurationManager.AppSettings["PassPhrase.Key"];
            var vector = ConfigurationManager.AppSettings["PassPhrase.Vector"];

            if (String.IsNullOrWhiteSpace(key))
                throw new ConfigurationErrorsException("Key value has not been defined.");

            int validKeyLength;
            if (!IsValidKey(key, provider, out validKeyLength))
                throw new FormatException(String.Format("PassPhrase.Key must be {0} characters long.", validKeyLength));

            if (String.IsNullOrWhiteSpace(vector))
                throw new ConfigurationErrorsException("Vector value has not been defined.");

            int validVectorLength;
            if (!IsValidVector(key, provider, out validVectorLength))
                throw new FormatException(String.Format("PassPhrase.Vector must be {0} characters long.", validVectorLength));

            if (args.Count(p => !p.StartsWith("/")) > 0)
                throw new ArgumentException("Invalid arguments");

            var direction = CryptoDirection.Unknown;
            var text = String.Empty;
            var filepath = String.Empty;
            var outputpath = String.Empty;
            foreach (var arg in args)
            {
                if (arg.ToLower() == "/h")
                    continue;

                if (arg.ToLower() == "/e")
                {
                    direction = CryptoDirection.Encrypt;
                    continue;
                }

                if (arg.ToLower() == "/d")
                {
                    direction = CryptoDirection.Decrypt;
                    continue;
                }

                if (arg.StartsWith("/T") || arg.StartsWith("/t"))
                {
                    text = Regex.Replace(arg, "^/[Tt]:\"?(.+)\"?$", "$1", RegexOptions.IgnoreCase);
                    continue;
                }

                if (arg.StartsWith("/F") || arg.StartsWith("/f"))
                {
                    filepath = Regex.Replace(arg, "^/[Ff]:\"?(.+)\"?$", "$1", RegexOptions.IgnoreCase);
                    continue;
                }

                if (arg.StartsWith("/O") || arg.StartsWith("/o"))
                {
                    outputpath = Regex.Replace(arg, "^/[Oo]:\"?(.+)\"?$", "$1", RegexOptions.IgnoreCase);
                    continue;
                }
            }

            var service = new SymmetricService(provider);
            var sb = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(text) &&
                (String.IsNullOrWhiteSpace(filepath) || String.IsNullOrWhiteSpace(outputpath)))
            {
                var transformed = service.Transform(text, direction);
                sb.AppendLine("Original:    " + text);
                sb.AppendLine("Transformed: " + transformed);
            }
            else if (String.IsNullOrWhiteSpace(text) &&
                     (!String.IsNullOrWhiteSpace(filepath) && !String.IsNullOrWhiteSpace(outputpath)))
            {
                if (!File.Exists(filepath))
                    throw new FileNotFoundException("File to transform does not exist.");

                var transformed = service.Transform(File.ReadAllText(filepath), direction);
                File.WriteAllText(outputpath, transformed);

                sb.AppendLine("Original:    " + filepath);
                sb.AppendLine("Transformed: " + outputpath);
            }
            else
                throw new ArgumentException("Either text or filepath/outputpath needs to be defined.");

            Console.WriteLine(sb.ToString());
        }

        #endregion Methods
    }
}