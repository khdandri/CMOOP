using System.Text;
using System.Text.Json;

namespace lab.work2._1
{
    [Serializable]
    public class Bus
    {
        public int bus_number { get; set; }
        public string driverName { get; set; }
        public double travelTime { get; set; }
        public string destination { get; set; }
        public Bus(int bus_number, string driverName, double travelTime, string destination)
        {
            this.bus_number = bus_number;
            this.driverName = driverName;
            this.travelTime = travelTime;
            this.destination = destination;


        }

    }

        
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            List<Bus> buses = new List<Bus>
            {
                new Bus(25, "Joe Doe", 1.5, "Kyiv"),
                new Bus(42, "Jae Lim", 2.0, "Poltava"),
                new Bus(78, "Alice Johnson", 2.5, "Kyiv"),
                new Bus(99, "Bob Bon", 3.0, "Lviv"),
                new Bus(12, "Charlie Davis", 1.0, "Lviv"),
                new Bus(34, "Din Bin", 2.2, "Kharkiv"),
                new Bus(56, "Fin Gen", 3.5, "Odessa")
            };
    
            if (!File.Exists("buses.json"))
            {
                using (FileStream fs = new FileStream("buses.json", FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, buses);
                    Console.WriteLine("Початковий список автобусів створено і збережено.");
                }
            }
            repeat:
            Console.WriteLine("0 - Переглянути список Водіїв\n 1 - Створити нового водія\n 2 - Видалити водія\n 3 - Корегувати іноформацію\n 4 - Вийти");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    {
                        List<Bus>? currentBuses;
                        using (FileStream fsRead = new FileStream("buses.json", FileMode.OpenOrCreate))
                        {
                            currentBuses = await JsonSerializer.DeserializeAsync<List<Bus>>(fsRead);
                        }
                        if (currentBuses == null || currentBuses.Count == 0)
                        {
                            Console.WriteLine("Список автобусів порожній.\n");
                            goto repeat;
                        }
                        else
                        {
                            Console.WriteLine("--- Список автобусів ---\n");
                            foreach (var bus in currentBuses)
                            {
                                Console.WriteLine($"Номер автобуса: {bus.bus_number}, Ім'я водія: {bus.driverName}, Час подорожі: {bus.travelTime}, Пункт призначення: {bus.destination}");
                            }
                            Console.WriteLine();
                            
                            goto repeat;

                        }
                    }
                case "1":
                    {
                        List<Bus>? currentBusesAdd;
                        using (FileStream fsRead = new FileStream("buses.json", FileMode.OpenOrCreate))
                        {
                            currentBusesAdd = await JsonSerializer.DeserializeAsync<List<Bus>>(fsRead);
                        }
                        if (currentBusesAdd == null)
                        {
                            currentBusesAdd = new List<Bus>();
                        }
                        Console.WriteLine("--- Додавання нового автобуса ---\n");

                        Console.Write("Введіть номер автобуса (ціле число): \n");
                        int newBusNumber = int.Parse(Console.ReadLine()!);
                        bool numberExists = currentBusesAdd.Any(b => b.bus_number == newBusNumber);
                        if (numberExists)
                        {
                            Console.WriteLine("Автобус с таким номером вже існує\n");
                            break;
                        }
                        else
                        {

                            Console.Write("Введіть ім'я водія: \n");
                            string newDriverName = Console.ReadLine()!;

                            Console.Write("Введіть час подорожі : \n");
                            double newTravelTime = double.Parse(Console.ReadLine()!);

                            Console.Write("Введіть пункт призначення: \n");
                            string newDestination = Console.ReadLine()!;
                            Bus newBus = new Bus(newBusNumber, newDriverName, newTravelTime, newDestination);

                            currentBusesAdd.Add(newBus);

                            using (FileStream fsWrite = new FileStream("buses.json", FileMode.Create))
                            {
                                await JsonSerializer.SerializeAsync(fsWrite, currentBusesAdd);
                            }

                            Console.WriteLine("Новий автобус успішно додано та збережено.\n");
                        }
                    }
                    break;
                case "2":
                    {
                        List<Bus>? currentBusesDelete;

                        using (FileStream fsRead = new FileStream("buses.json", FileMode.OpenOrCreate))
                        {
                            currentBusesDelete = await JsonSerializer.DeserializeAsync<List<Bus>>(fsRead);
                        }

                        if (currentBusesDelete == null || currentBusesDelete.Count == 0)
                        {
                            Console.WriteLine("Список автобусів порожній. Нічого видаляти.\n");
                            break;
                        }

                        Console.WriteLine("--- Видалення автобуса ---\n");
                        Console.Write("Введіть номер автобуса, який потрібно видалити: \n");

                        if (!int.TryParse(Console.ReadLine(), out int busNumberToDelete))
                        {
                            Console.WriteLine("Некоректний ввід. Потрібно ввести ціле число.\n");
                            break;
                        }

                        int busesRemovedCount = currentBusesDelete.RemoveAll(b => b.bus_number == busNumberToDelete);

                        if (busesRemovedCount > 0)
                        {
                            using (FileStream fsWrite = new FileStream("buses.json", FileMode.Create))
                            {
                                await JsonSerializer.SerializeAsync(fsWrite, currentBusesDelete);
                            }
                            Console.WriteLine($"Успішно видалено {busesRemovedCount} автобус(и) з номером {busNumberToDelete}.\n");
                        }
                        else
                        {
                            Console.WriteLine($"Автобус з номером {busNumberToDelete} не знайдено.\n");
                        }

                        break;
                    }
                case "3":
                    {
                        List<Bus>? currentBusesCorrect;
                        using (FileStream fsRead = new FileStream("buses.json", FileMode.OpenOrCreate))
                        {
                            currentBusesCorrect = await JsonSerializer.DeserializeAsync<List<Bus>>(fsRead);
                        }
                        if (currentBusesCorrect == null || currentBusesCorrect.Count == 0)
                        {
                            Console.WriteLine("Список автобусів порожній. Нічого корегувати.\n");
                            break;
                        }
                        Console.WriteLine("--- Корегування інформації автобуса ---\n");
                        Console.Write("Введіть номер автобуса, який потрібно корегувати: \n");
                        if (!int.TryParse(Console.ReadLine(), out int busNumberToEdit))
                        {
                            Console.WriteLine("Некоректний ввід. Потрібно ввести ціле число.\n");
                            break;
                        }
                        Bus? busToEdit = currentBusesCorrect.FirstOrDefault(b => b.bus_number == busNumberToEdit);
                        if (busToEdit != null)
                        {
                            Console.Write("Введіть нове ім'я водія (залиште порожнім для пропуску): \n");
                            string newDriverName = Console.ReadLine()!;
                            if (!string.IsNullOrWhiteSpace(newDriverName))
                            {
                                busToEdit.driverName = newDriverName;
                            }
                            Console.Write("Введіть новий час подорожі (залиште порожнім для пропуску): \n");
                            string travelTimeInput = Console.ReadLine()!;
                            if (double.TryParse(travelTimeInput, out double newTravelTime))
                            {
                                busToEdit.travelTime = newTravelTime;
                            }
                            Console.Write("Введіть новий пункт призначення (залиште порожнім для пропуску): \n");
                            string newDestination = Console.ReadLine()!;
                            if (!string.IsNullOrWhiteSpace(newDestination))
                            {
                                busToEdit.destination = newDestination;
                            }
                            using (FileStream fsWrite = new FileStream("buses.json", FileMode.Create))
                            {
                                await JsonSerializer.SerializeAsync(fsWrite, currentBusesCorrect);
                            }
                            Console.WriteLine("Інформація автобуса успішно оновлена.\n");
                        }
                        else
                        {
                            Console.WriteLine($"Автобус з номером {busNumberToEdit} не знайдено.\n");
                        }
                        break;
                    }
                    case "4":
                    {
                        Console.WriteLine("Вихід з програми.\n");
                        return;
                    }
                default:
                    Console.WriteLine("Невірний вибір\n");
                    return;

            }
        
        
        }
    }
}
