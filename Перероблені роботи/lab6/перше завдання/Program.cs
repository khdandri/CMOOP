public class WildAnimal
{
    public string Name { get; set; }
    public System.DateTime DateOfBirth { get; set; }
    public WildAnimal(string name, System.DateTime dateOfBirth)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }
    public virtual void DisplayInfo()
    {
        System.Console.WriteLine($"Name: {Name}, Date of Birth: {DateOfBirth.ToShortDateString()}");
    }
   public void UpdateInfo(string name, System.DateTime dateOfBirth)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }
}
public class PredatoryAnimal : WildAnimal
{
    public string location { get; set; }
    public double AverageNumberOfAnimals { get; set; }
    public double AnimalPopulationChangeRate { get; set; }
    public PredatoryAnimal(string name, System.DateTime dateOfBirth, string location, double averageNumberOfAnimals, double animalPopulationChangeRate)
        : base(name, dateOfBirth)
    {
        this.location = location;
        AverageNumberOfAnimals = averageNumberOfAnimals;
        AnimalPopulationChangeRate = animalPopulationChangeRate;
    }
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        System.Console.WriteLine($"Location: {location}, Average Number of Animals: {AverageNumberOfAnimals}, Population Change Rate: {AnimalPopulationChangeRate}");
    }
    public void UpdatePredatoryInfo(string name, System.DateTime dateOfBirth, string location, double averageNumberOfAnimals, double animalPopulationChangeRate)
    {
        UpdateInfo(name, dateOfBirth);
        this.location = location;
        AverageNumberOfAnimals = averageNumberOfAnimals;
        AnimalPopulationChangeRate = animalPopulationChangeRate;
    }
    public bool IsPopulationIncreasing()
    {
        return AnimalPopulationChangeRate > 1.1;
    }
}
class Program
{
    static void Main(string[] args)
    {
        WildAnimal lion = new WildAnimal("Lion", new System.DateTime(2015, 5, 1));
        lion.DisplayInfo();

        Console.WriteLine("Updating Lion's info...");
        lion.UpdateInfo("African Lion", new System.DateTime(2014, 6, 15));
        lion.DisplayInfo();

        Console.WriteLine();

        PredatoryAnimal tiger = new PredatoryAnimal("Tiger", new System.DateTime(2016, 3, 10), "India", 2500, 1.2);
        tiger.DisplayInfo();
        Console.WriteLine("Is population increasing?...");
        if (tiger.IsPopulationIncreasing())
        {
            System.Console.WriteLine("Yes, the tiger population is increasing.");
        }
        else
        {
            System.Console.WriteLine("No, the tiger population is not increasing.");
        }
        Console.WriteLine("Updating Tiger's info...");
        tiger.UpdatePredatoryInfo("Bengal Tiger", new System.DateTime(2015, 4, 20), "India, Mumbai", 3000, 1.3);

        PredatoryAnimal leopard = new PredatoryAnimal("Leopard", new System.DateTime(2017, 8, 5), "Africa", 1500, 1.05);
        leopard.DisplayInfo();
        Console.WriteLine("Is population increasing?...");
        if 
            (leopard.IsPopulationIncreasing())
        {
            System.Console.WriteLine("Yes, the leopard population is increasing.");
        }
        else
        {
            System.Console.WriteLine("No, the leopard population is not increasing.");
        }
    }
}