using System.Reflection.PortableExecutable;
using System.Text.Unicode;
using System.Text;
using System;

namespace labWork9
{
    using System;
    using System.IO;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirName = "Data";
            string dataDir = Path.Combine(baseDir, dataDirName);
            string inputFileName = "input.txt";
            string inputFilePath = Path.Combine(dataDir, inputFileName);
            string outputFileName = "output.txt";
            string outputFilePath = Path.Combine(dataDir, outputFileName);
            string copyDirName = "Backup";
            string copyDir = Path.Combine(baseDir, copyDirName);
            string copyFilePath = Path.Combine(copyDir, "copied_input.txt");

            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
                Console.WriteLine($"Directory created: {dataDir}");
            }

            try
            {
                Console.WriteLine("Enter text to write to input.txt:");
                string textToWrite = Console.ReadLine();

                using (StreamWriter writer = new StreamWriter(inputFilePath, false, Encoding.UTF8))
                {
                    writer.WriteLine(textToWrite);
                    Console.WriteLine($"Text written to: {inputFilePath}");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error writing to file: {e.Message}");
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(inputFilePath, Encoding.UTF8))
                using (StreamWriter writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string processedLine = line.ToUpper();
                        writer.WriteLine(processedLine);
                    }
                    Console.WriteLine($"Data read from {inputFilePath} and written to {outputFilePath}");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error reading/writing files: {e.Message}");
                return;
            }

            if (!Directory.Exists(copyDir))
            {
                Directory.CreateDirectory(copyDir);
                Console.WriteLine($"Directory created: {copyDir}");
            }

            try
            {
                File.Copy(inputFilePath, copyFilePath, true);
                Console.WriteLine($"File copied from {inputFilePath} to {copyFilePath}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error copying file: {e.Message}");
                return;
            }

            /* 7. Видалення input.txt (Закоментовано для безпеки)
             try
            {
                File.Delete(inputFilePath);
                Console.WriteLine($"File deleted: {inputFilePath}");
            }
           catch (IOException e)
           {
                 Console.WriteLine($"Error deleting file: {e.Message}");
            }*/

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}