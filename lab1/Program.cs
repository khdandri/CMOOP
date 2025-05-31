using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

[Serializable]
class Bus
{
    public int NumBus { get; set; }
    public string DriverName { get; set; }
    public double Time { get; set; }
    public string Destination { get; set; }

    public Bus(int numBus, string driverName, double time, string destination)
    {
        NumBus = numBus;
        DriverName = driverName;
        Time = time;
        Destination = destination;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var buses = new List<Bus>
        {
            new Bus(216, "Korolev", 15.35, "Kyiv"),
            new Bus(16, "Makarenko", 17.00, "Odessa"),
            new Bus(450, "Kuts", 10.30, "Kharkiv"),
            new Bus(10, "Gleb", 14.45, "Odessa"),
            new Bus(220, "Shah", 9.00, "Kyiv"),
            new Bus(330, "Shtefan", 17.45, "Lviv"),
            new Bus(330, "Cheshko", 17.45, "Lviv")
        };

        Console.WriteLine("Input your destination: 1-Kyiv, 2-Odessa, 3-Kharkiv, 4-Lviv");
        string op = Console.ReadLine();

        string destination = op switch
        {
            "1" => "Kyiv",
            "2" => "Odessa",
            "3" => "Kharkiv",
            "4" => "Lviv",
            _ => null
        };

        if (destination != null)
        {
            var filteredBuses = buses.FindAll(bus => bus.Destination == destination && bus.DriverName.Length <= 7);
            if (filteredBuses.Count > 0)
            {
                Console.WriteLine($"Buses to {destination}:");
                foreach (var bus in filteredBuses)
                {
                    Console.WriteLine($"Bus Number: {bus.NumBus}, Driver: {bus.DriverName}, Time: {bus.Time}");
                }

                string jsonString = JsonSerializer.Serialize(filteredBuses);
                File.WriteAllText("buses.json", jsonString);
                Console.WriteLine("objects are serialized to file в файл buses.json");

                string readJsonString = File.ReadAllText("buses.json");
                var deserializedBuses = JsonSerializer.Deserialize<List<Bus>>(readJsonString);
                Console.WriteLine("objects are serialized from file buses.json");

                foreach (var bus in deserializedBuses)
                {
                    Console.WriteLine($"Bus Number: {bus.NumBus}, Driver: {bus.DriverName}, Time: {bus.Time}");
                }
            }
            else
            {
                Console.WriteLine($"No buses available to {destination}.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }
}
