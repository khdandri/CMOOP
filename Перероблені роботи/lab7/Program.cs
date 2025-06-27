using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public interface IDevice
{
    string Name { get; set; }
    int YearOfManufacture { get; set; }
    double Weight { get; set; }
    bool HasEngine { get; }
    string GetDescription();
}
public interface IEngine
{
    string EngineType { get; set; }
    int PowerHP { get; set; }
}
public interface IPart
{
    string PartName { get; set; }
    double PartWeight { get; set; }
}

public class Part : IPart
{
    public string PartName { get; set; }
    public double PartWeight { get; set; }

    public Part(string name, double weight)
    {
        PartName = name;
        PartWeight = weight;
    }

    public override string ToString() => $"{PartName} (Вага: {PartWeight} кг)";
}
public class Engine : Part, IEngine
{
    public string EngineType { get; set; }
    public int PowerHP { get; set; }

    public Engine(string name, double weight, string type, int power)
        : base(name, weight)
    {
        EngineType = type;
        PowerHP = power;
    }

    public override string ToString() =>
        $"{base.ToString()}, Тип: {EngineType}, Потужність: {PowerHP} к.с.";
}
public abstract class Device : IDevice, IComparable<Device>, ICloneable
{
    public string Name { get; set; }
    public int YearOfManufacture { get; set; }
    public double Weight { get; set; }
    public List<IPart> Parts { get; protected set; }

    public bool HasEngine => Parts.OfType<IEngine>().Any();

    public Device(string name, int year, double weight)
    {
        Name = name;
        YearOfManufacture = year;
        Weight = weight;
        Parts = new List<IPart>();
    }

    public void AddPart(IPart part)
    {
        if (part != null)
        {
            Parts.Add(part);
            Weight += part.PartWeight;
        }
    }

    public abstract string GetDescription();

    public override string ToString()
    {
        var partsInfo = Parts.Any()
            ? string.Join(", ", Parts.Select(p => p.ToString()))
            : "Немає частин";
        return $"{GetDescription()}, Рік: {YearOfManufacture}, Вага: {Weight} кг, Частини: [{partsInfo}]";
    }
    public int CompareTo(Device other)
    {
        if (other == null) return 1;
        return this.YearOfManufacture.CompareTo(other.YearOfManufacture);
    }

    public abstract object Clone();
    protected List<IPart> CloneParts()
    {
        var clonedParts = new List<IPart>();
        foreach (var part in this.Parts)
        {
            if (part is ICloneable cloneablePart)
            {
                clonedParts.Add((IPart)cloneablePart.Clone());
            }
            else
            {
                clonedParts.Add(part);
            }
        }
        return clonedParts;
    }
}

public class Airplane : Device
{
    public int MaxAltitude { get; set; }

    public Airplane(string name, int year, double weight, int maxAltitude)
        : base(name, year, weight)
    {
        MaxAltitude = maxAltitude;
    }

    public override string GetDescription() => $"Літак '{Name}'";

    public override object Clone()
    {
        var newAirplane = new Airplane(this.Name, this.YearOfManufacture, this.Weight, this.MaxAltitude);
        newAirplane.Parts = this.CloneParts(); 
        return newAirplane;
    }
}

public class Helicopter : Device
{
    public int RotorBlades { get; set; }

    public Helicopter(string name, int year, double weight, int rotorBlades)
        : base(name, year, weight)
    {
        RotorBlades = rotorBlades;
    }

    public override string GetDescription() => $"Вертоліт '{Name}'";

    public override object Clone()
    {
        var newHelicopter = new Helicopter(this.Name, this.YearOfManufacture, this.Weight, this.RotorBlades);
        newHelicopter.Parts = this.CloneParts();
        return newHelicopter;
    }
}

public class HotAirBalloon : Device
{
    public double VolumeCubicMeters { get; set; }

    public HotAirBalloon(string name, int year, double weight, double volume)
        : base(name, year, weight)
    {
        VolumeCubicMeters = volume;
    }

    public override string GetDescription() => $"Повітряна куля '{Name}'";

    public override object Clone()
    {
        var newBalloon = new HotAirBalloon(this.Name, this.YearOfManufacture, this.Weight, this.VolumeCubicMeters);
        newBalloon.Parts = this.CloneParts();
        return newBalloon;
    }
}

public class HangGlider : Device
{
    public double Wingspan { get; set; }

    public HangGlider(string name, int year, double weight, double wingspan)
        : base(name, year, weight)
    {
        Wingspan = wingspan;
    }

    public override string GetDescription() => $"Дельтаплан '{Name}'";

    public override object Clone()
    {
        var newGlider = new HangGlider(this.Name, this.YearOfManufacture, this.Weight, this.Wingspan);
        newGlider.Parts = this.CloneParts();
        return newGlider;
    }
}

public class FlyingCarpet : Device
{
    public double AreaSquareMeters { get; set; }

    public FlyingCarpet(string name, int year, double weight, double area)
        : base(name, year, weight)
    {
        AreaSquareMeters = area;
    }

    public override string GetDescription() => $"Літаючий килим '{Name}'";

    public override object Clone()
    {
        var newCarpet = new FlyingCarpet(this.Name, this.YearOfManufacture, this.Weight, this.AreaSquareMeters);
        newCarpet.Parts = this.CloneParts();
        return newCarpet;
    }
}
public class Register
{
    private List<IDevice> allEquipment;

    public Register()
    {
        allEquipment = new List<IDevice>();
    }

    public void AddDevice(IDevice device)
    {
        if (device != null)
        {
            allEquipment.Add(device);
        }
    }

    public void PrintAllEquipment()
    {
        Console.WriteLine("\n--- Перелік всього обладнання ---");
        if (!allEquipment.Any())
        {
            Console.WriteLine("Реєстр порожній.");
            return;
        }
        foreach (var device in allEquipment)
        {
            Console.WriteLine(device);
        }
    }

    public void PrintElectronicEquipment()
    {
        Console.WriteLine("\n--- Електронне обладнання (з двигуном) ---");
        var equipmentWithEngine = allEquipment.Where(d => d.HasEngine);
        if (!equipmentWithEngine.Any())
        {
            Console.WriteLine("Електронне обладнання відсутнє.");
            return;
        }
        foreach (var device in equipmentWithEngine)
        {
            Console.WriteLine(device);
        }
    }

    public void PrintEquipmentWithoutEngine()
    {
        Console.WriteLine("\n--- Устаткування без двигунів ---");
        var equipmentWithoutEngine = allEquipment.Where(d => !d.HasEngine);
        if (!equipmentWithoutEngine.Any())
        {
            Console.WriteLine("Устаткування без двигунів відсутнє.");
            return;
        }
        foreach (var device in equipmentWithoutEngine)
        {
            Console.WriteLine(device);
        }
    }
    public void Sort(Comparison<IDevice> comparison)
    {
        allEquipment.Sort((x, y) => comparison(x, y));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Реєстр обладнання аероклубу ===\n");

        var aeroclubRegister = new Register();

        var airplane = new Airplane("Boeing 747", 1969, 183500, 13746);
        airplane.AddPart(new Engine("Турбіна №1", 5000, "Турбовентиляторний", 57000));
        airplane.AddPart(new Engine("Турбіна №2", 5000, "Турбовентиляторний", 57000));
        airplane.AddPart(new Part("Фюзеляж", 150000));

        var helicopter = new Helicopter("Bell 206", 1967, 770, 2);
        helicopter.AddPart(new Engine("Двигун АІ-450М", 200, "Турбовальний", 450));
        helicopter.AddPart(new Part("Несучий гвинт", 150));

        var hotAirBalloon = new HotAirBalloon("Cameron A-300", 2010, 250, 8500);
        hotAirBalloon.AddPart(new Part("Оболонка", 120));
        hotAirBalloon.AddPart(new Part("Горелка", 30));

        var hangGlider = new HangGlider("Moyes Litespeed", 2021, 30, 10.5);
        hangGlider.AddPart(new Part("Каркас", 20));

        var flyingCarpet = new FlyingCarpet("Magical Weave", 2023, 15, 5);
        flyingCarpet.AddPart(new Part("Бахрома", 1));

        aeroclubRegister.AddDevice(airplane);
        aeroclubRegister.AddDevice(helicopter);
        aeroclubRegister.AddDevice(hotAirBalloon);
        aeroclubRegister.AddDevice(hangGlider);
        aeroclubRegister.AddDevice(flyingCarpet);

        aeroclubRegister.PrintAllEquipment();
        aeroclubRegister.PrintElectronicEquipment();
        aeroclubRegister.PrintEquipmentWithoutEngine();

        Console.WriteLine("\n\n--- Сортування обладнання ---");

        Console.WriteLine("\nСортування за роком виробництва (за зростанням):");
        aeroclubRegister.Sort((d1, d2) => d1.YearOfManufacture.CompareTo(d2.YearOfManufacture));
        aeroclubRegister.PrintAllEquipment();

        Console.WriteLine("\nСортування за вагою (за зростанням):");
        aeroclubRegister.Sort((d1, d2) => d1.Weight.CompareTo(d2.Weight));
        aeroclubRegister.PrintAllEquipment();

        Console.WriteLine("\n\n--- Клонування об'єктів ---");

        var clonedHelicopter = (Helicopter)helicopter.Clone();
        clonedHelicopter.Name = "Cloned Bell 206";
        clonedHelicopter.YearOfManufacture = 2024;
        clonedHelicopter.RotorBlades = 4; 
        ((Engine)clonedHelicopter.Parts[0]).PowerHP = 600;

        Console.WriteLine("Оригінальний вертоліт:");
        Console.WriteLine(helicopter);

        Console.WriteLine("\nКлонований вертоліт (змінений):");
        Console.WriteLine(clonedHelicopter);

        aeroclubRegister.AddDevice(clonedHelicopter);

        Console.WriteLine("\nСписок після додавання клонованого вертольота:");
        aeroclubRegister.PrintAllEquipment();
    }
}