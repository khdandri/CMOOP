using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab.work2._2
{
    public class Words
    {
        public string? word { get; set; }
        public Words() { }

        public Words(string word)
        {
            this.word = word;
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "D:/ProgramFiles/repos/lab.work.2.2/firstFile/-file1.txt";

        start:
            Console.WriteLine("Select which .txt you want to process (1) (2) (3):\n" +
                              "(4) - Quit");
            string? fileNumber = Console.ReadLine();

            switch (fileNumber)
            {
                case "1":
                case "2":
                case "3":
                    {
                        List<Words>? wordData = null;

                        try
                        {
                            string currentFilePath = $"firstFile/file{fileNumber}.txt";
                            string fileContent = File.ReadAllText(currentFilePath);

                            wordData = JsonSerializer.Deserialize<List<Words>>(fileContent);

                            if (wordData != null)
                            {
                                Console.WriteLine("Loaded words:");
                                foreach (var word in wordData)
                                {
                                    Console.WriteLine($"- {word.word} -");
                                }
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine($"Error: File not found at path: firstFile/file{fileNumber}.txt");
                            Console.WriteLine("Please ensure the correct file exists in the project directory.");
                            goto start;
                        }
                        catch (JsonException)
                        {
                            Console.WriteLine("Error: Failed to decode JSON. Ensure the file contains a correct JSON array of Words objects (e.g., [{\"word\":\"hello\"}, {\"word\":\"world\"}]).");
                            goto start;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                            goto start;
                        }

                        Console.WriteLine("\nChoose what to do next:\n1 - Check the number of repetitions\n2 - Choose another file\n3 - End");
                        string? path = Console.ReadLine();

                        switch (path)
                        {
                            case "1":
                                {
                                    Console.WriteLine("Which word you want to check up?");
                                    string? userAnswer = Console.ReadLine();

                                    if (string.IsNullOrWhiteSpace(userAnswer) || wordData == null)
                                    {
                                        Console.WriteLine("Invalid input or file data is empty.");
                                        goto start;
                                    }

                                    int matchesCount = wordData
                                        .Count(w => w.word != null && w.word.Equals(userAnswer, StringComparison.OrdinalIgnoreCase));

                                    Console.WriteLine($"The word '{userAnswer}' appears {matchesCount} times (case-insensitive check).");

                                    Console.WriteLine("Do you want to check other words in this file? (Yes - 1 | No - 2)");
                                    string? answer = Console.ReadLine();

                                    if (answer == "1")
                                    {
                                        goto start;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            case "2":
                                goto start;
                            case "3":
                                return;
                            default:
                                Console.WriteLine("Invalid option. Returning to menu.");
                                goto start;
                        }
                    }
                case "4":
                    break;
                default:
                    Console.WriteLine("Invalid file number. Returning to menu.");
                    goto start;
            }
        }
    }
}
