using System;
using System.Linq;
using System.Globalization;
using System.Text;

public enum TypeOfWork
{
    Home,
    Business,
    Server
}

public class Person
{
    private string name;
    private string surname;
    private DateTime birthDate;
    public string Name { get => name; set => name = value; }
    public string Surname { get => surname; set => surname = value; }
    public DateTime BirthDate { get => birthDate; set => birthDate = value; }
    public int BirthYear { get => birthDate.Year; set => birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
    public Person(string name, string surname, DateTime birthDate)
    {
        this.name = name;
        this.surname = surname;
        this.birthDate = birthDate;
    }
    public Person()
    {
        name = "Невідоме ім'я";
        surname = "Невідоме прізвище";
        birthDate = new DateTime(1900, 1, 1);
    }
    public override string ToString() => $"Власник: {Name} {Surname}, Дата народження: {BirthDate.ToShortDateString()}";
    public string ToShortString() => $"{Name} {Surname}";
}

public class Device
{
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Device(string name, double price, DateTime releaseDate)
    {
        Name = name;
        Price = price;
        ReleaseDate = releaseDate;
    }
    public Device()
    {
        Name = "Невідомий пристрій";
        Price = 0.0;
        ReleaseDate = DateTime.Now;
    }
    public override string ToString() => $"Пристрій: {Name}, Вартість: {Price}, Дата випуску: {ReleaseDate.ToShortDateString()}";
}

public class Computer
{
    private Person owner;
    private TypeOfWork purpose;
    private string ipAddress;
    private Device[] devices;

    public Person Owner { get => owner; set { if (value == null) throw new ArgumentNullException(nameof(value), "Власник не може бути null."); owner = value; } }
    public TypeOfWork Purpose { get => purpose; set { if (!Enum.IsDefined(typeof(TypeOfWork), value)) throw new ArgumentException("Невірне значення для призначення.", nameof(value)); purpose = value; } }
    public string IpAddress { get => ipAddress; set { if (string.IsNullOrWhiteSpace(value) || value.Split('.').Length != 4) throw new ArgumentException("Недійсна IP-адреса.", nameof(value)); ipAddress = value; } }
    public Device[] Devices { get => devices; set => devices = value; }
    public double TotalPrice { get => devices?.Sum(d => d.Price) ?? 0.0; }
    public bool this[TypeOfWork index] => purpose == index;

    public Computer(Person owner, TypeOfWork purpose, string ipAddress, Device[] devices)
    {
        this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
        this.purpose = purpose;
        this.ipAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
        this.devices = new Device[0];
        AddDevices(devices);
    }
    public Computer()
    {
        this.owner = new Person();
        this.purpose = TypeOfWork.Home;
        this.ipAddress = "0.0.0.0";
        this.devices = new Device[0];
    }

    public void AddDevices(params Device[] newDevices)
    {
        if (newDevices == null || newDevices.Length == 0) return;
        var currentLength = devices.Length;
        Array.Resize(ref devices, currentLength + newDevices.Length);
        Array.Copy(newDevices, 0, devices, currentLength, newDevices.Length);
    }
    public override string ToString()
    {
        string devicesInfo = devices.Length > 0 ? string.Join("\n  - ", devices.Select(d => d.ToString())) : "Немає встановлених пристроїв";

        return $"=== Повна інформація ===\n" +
               $"Власник: {Owner.ToShortString()}\n" +
               $"Призначення: {Purpose}\n" +
               $"IP-адреса: {IpAddress}\n" +
               $"Сумарна вартість пристроїв: {TotalPrice}\n" +
               $"Встановлені пристрої:\n  - {devicesInfo}";
    }

    public string ToShortString()
    {
        return $"Комп'ютер [IP: {IpAddress}, Власник: {Owner.ToShortString()}, Вартість: {TotalPrice}]";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Computer myComputer = new Computer();
        Console.WriteLine("Скорочена інформація про комп'ютер (за замовчуванням):");
        Console.WriteLine(myComputer.ToShortString());
        Console.WriteLine();

        Console.WriteLine("Перевірка індексатора на відповідність призначенню:");
        Console.WriteLine($"Призначення - Home: {myComputer[TypeOfWork.Home]}");
        Console.WriteLine($"Призначення - Business: {myComputer[TypeOfWork.Business]}");
        Console.WriteLine($"Призначення - Server: {myComputer[TypeOfWork.Server]}");
        Console.WriteLine();

        try
        {
            myComputer.Owner = new Person("Анна", "Петренко", new DateTime(1990, 7, 25));
            myComputer.Purpose = TypeOfWork.Business;
            myComputer.IpAddress = "192.168.1.50";
            myComputer.Devices = new Device[]
            {
                new Device("Монітор", 7500, new DateTime(2023, 11, 5)),
                new Device("Системний блок", 25000, new DateTime(2022, 9, 1))
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("Повна інформація про оновлений комп'ютер:");
        Console.WriteLine(myComputer.ToString());
        Console.WriteLine();

        Console.WriteLine("Додаємо нові пристрої...");
        myComputer.AddDevices(
            new Device("Клавіатура", 1200, new DateTime(2024, 2, 10)),
            new Device("Миша", 850, new DateTime(2024, 2, 10))
        );

        Console.WriteLine("Оновлена повна інформація після додавання пристроїв:");
        Console.WriteLine(myComputer.ToString());
        Console.WriteLine();

        Computer[] computers = new Computer[]
        {
            myComputer,
            new Computer(new Person("Ігор", "Сидоренко", new DateTime(1985, 4, 12)), TypeOfWork.Server, "10.0.0.2", new Device[0]),
            new Computer(new Person("Олена", "Мельник", new DateTime(1998, 9, 3)), TypeOfWork.Home, "192.168.0.10", new Device[] { new Device("Принтер", 4000, new DateTime(2021, 6, 1)) }),
            new Computer(new Person("Дмитро", "Коваль", new DateTime(1977, 12, 8)), TypeOfWork.Server, "203.0.113.15", new Device[] { new Device("Сервер", 80000, new DateTime(2023, 1, 1)), new Device("Диск SSD", 5000, new DateTime(2024, 5, 2)), new Device("RAID-контролер", 9000, new DateTime(2023, 8, 8)), new Device("Оперативна пам'ять", 3000, new DateTime(2024, 3, 3)) }),
            new Computer(new Person("Наталія", "Войцех", new DateTime(2000, 2, 20)), TypeOfWork.Business, "172.16.5.1", new Device[] { new Device("Сканер", 2000, new DateTime(2022, 4, 4)), new Device("Проектор", 15000, new DateTime(2023, 1, 1)) })
        };

        Console.WriteLine("IP-адреси всіх комп'ютерів у масиві:");
        foreach (var computer in computers)
        {
            Console.WriteLine(computer.IpAddress);
        }
        Console.WriteLine();

        int maxDeviceCount = computers.Max(c => c.Devices.Length);

        Console.WriteLine($"Скорочена інформація про комп'ютери з найбільшою кількістю пристроїв ({maxDeviceCount} шт.):");
        foreach (var computer in computers)
        {
            if (computer.Devices.Length == maxDeviceCount)
            {
                Console.WriteLine(computer.ToShortString());
            }
        }
    }
}