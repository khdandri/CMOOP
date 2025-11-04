using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace lab.work._2._3
{
    public class printerPrinting
    {
        public string user { get; set; }
        public double time { get; set; }
        
        public printerPrinting(string user, double time)
        {
            this.user = user;
            this.time = time;
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
           
            List<printerPrinting> currentJob = new List<printerPrinting>
            {
                new printerPrinting("Alice", 2.5),
                new printerPrinting("Bob", 1.0),
                new printerPrinting("Charlie", 3.0),
                new printerPrinting("Diana", 0.5)
            };
            using (FileStream fs = new FileStream("buses.json", FileMode.Create))
            {
               await JsonSerializer.SerializeAsync<List<printerPrinting>>(fs, currentJob);
            }
            HashSet<printerPrinting> shortlist = new HashSet<printerPrinting>();
            Queue<printerPrinting> printQueue = new Queue<printerPrinting>(currentJob);
            Stack<printerPrinting> completedJobs = new Stack<printerPrinting>();
            repeat:
            Console.WriteLine("(1) - Print\n(2) - Watch users shortlist\n(3) - New user add\n(4) - Delete user\n(5) - End");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        using (FileStream fs = new FileStream("buses.json", FileMode.Open))
                        {
                            currentJob = await JsonSerializer.DeserializeAsync<List<printerPrinting>>(fs);
                        }
                        while (printQueue.Count > 0)
                        {
                            printerPrinting currentJob1 = printQueue.Dequeue();
                            Console.WriteLine($"Printing job for {currentJob1.user}...");
                            await Task.Delay((int)(currentJob1.time * 1000));
                            completedJobs.Push(currentJob1);
                            Console.WriteLine($"Completed job for {currentJob1.user}.");
                        }
                        Console.WriteLine("Do you want to continue\n(1) - Yes\n(2) - No");
                        int cont = int.Parse(Console.ReadLine());
                        if (cont == 1)
                        {
                            goto repeat;
                        }
                        break;
                    }
                case 2:
                    {
                        using (FileStream fs = new FileStream("buses.json", FileMode.Open))
                        {
                            currentJob = await JsonSerializer.DeserializeAsync<List<printerPrinting>>(fs);
                        }
                        Console.WriteLine("Current print jobs:");
                        foreach (var job in currentJob)
                        {
                            Console.WriteLine($"User: {job.user}, Time: {job.time} hours");
                        }
                        Console.WriteLine("Do you want to continue\n(1) - Yes\n(2) - No");
                        int cont = int.Parse(Console.ReadLine());
                        if (cont == 1)
                        {
                            goto repeat;
                        }
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("Enter user name:");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Enter print time in hours:");
                        double printTime = double.Parse(Console.ReadLine());
                        printerPrinting newJob = new printerPrinting(userName, printTime);
                        currentJob.Add(newJob);
                        printQueue.Enqueue(newJob);
                        Console.WriteLine($"Added new print job for {userName} with time {printTime} hours.");
                        using (FileStream fs = new FileStream("buses.json", FileMode.Create))
                        {
                            await JsonSerializer.SerializeAsync<List<printerPrinting>>(fs, currentJob);
                        }
                        Console.WriteLine("Do you want to continue\n(1) - Yes\n(2) - No");
                        int cont = int.Parse(Console.ReadLine());
                        if (cont == 1)
                        {
                            goto repeat;
                        }
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Enter user name to delete:");
                        string userNameToDelete = Console.ReadLine();
                        var jobToDelete = currentJob.FirstOrDefault(j => j.user == userNameToDelete);
                        if (jobToDelete != null)
                        {
                            currentJob.Remove(jobToDelete);
                            printQueue = new Queue<printerPrinting>(printQueue.Where(j => j.user != userNameToDelete));
                            Console.WriteLine($"Deleted print job for {userNameToDelete}.");
                            using (FileStream fs = new FileStream("buses.json", FileMode.Create))
                            {
                                await JsonSerializer.SerializeAsync<List<printerPrinting>>(fs, currentJob);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No print job found for {userNameToDelete}.");
                        }
                        Console.WriteLine("Do you want to continue\n(1) - Yes\n(2) - No");
                        int cont = int.Parse(Console.ReadLine());
                        if (cont == 1)
                        {
                            goto repeat;
                        }
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Ending program.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid choice.");
                        break;
                    }
            }
        }
    }
}
