using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Lab1
{
    [Serializable]
    public class Bus
    {
        public int busNumber;
        public string driverName;
        public double travelTime;
        public string destination;

        public Bus(int busNumber, string driverName, double travelTime, string destination)
        {
            this.busNumber = busNumber;
            this.driverName = driverName;
            this.travelTime = travelTime;
            this.destination = destination;
        }

        public override string ToString()
        {
            return $"Bus Number: {busNumber}, Driver: {driverName}, Travel Time: {travelTime} hours, Destination: {destination}";
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Bus[] buses = new Bus[]
            {
                new Bus(25, "Joe Doe", 1.5, "Kyiv"),
                new Bus(42, "Jae Lim", 2.0, "Poltava"),
                new Bus(78, "Alice Johnson", 2.5, "Kyiv"),
                new Bus(99, "Bob Bon", 3.0, "Lviv"),
                new Bus(12, "Charlie Davis", 1.0, "Lviv"),
                new Bus(34, "Din Bin", 2.2, "Kharkiv"), 
                new Bus(56, "Fin Gen", 3.5, "Odessa")
            };

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("buses_array.dat", FileMode.Create))
            {
                formatter.Serialize(fs, buses);
                Console.WriteLine("Serialization of bus array complete into buses_array.dat.");
            }

            Bus[] deserializedBuses;
            using (FileStream fs = new FileStream("buses_array.dat", FileMode.Open))
            {
                deserializedBuses = (Bus[])formatter.Deserialize(fs);
                Console.WriteLine("Deserialization complete from buses_array.dat.\n");
            }

            Console.WriteLine("--- Все десериализованные автобусы ---");
            foreach (Bus b in deserializedBuses)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine("--------------------------------------\n");

            Console.WriteLine("Оберіть пункт призначення (введіть номер): 1. Kyiv. 2. Poltava. 3. Lviv. 4. Odessa.");
            string destinationpoint = Console.ReadLine();

            string targetDestination = null;
            switch (destinationpoint)
            {
                case "1": targetDestination = "Kyiv"; break;
                case "2": targetDestination = "Poltava"; break;
                case "3": targetDestination = "Lviv"; break;
                case "4": targetDestination = "Odessa"; break;
                default:
                    Console.WriteLine("Invalid input. Please select a valid destination point.");
                    return; 
            }

            var filteredBuses = deserializedBuses
                .Where(b => b.destination == targetDestination)
                .Where(b => b.driverName.Length < 8);

            Console.WriteLine($"\n--- Автобуси до {targetDestination} з іменем водія < 8 символів ---");

            if (filteredBuses.Any())
            {
                foreach (var bus in filteredBuses)
                {
                    Console.WriteLine(bus);
                }
            }
            else
            {
                Console.WriteLine($"Немає автобусів до {targetDestination}, що відповідають критерію довжини імені водія (< 8).");
            }
        }
    }
}
