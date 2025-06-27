public class Goods
{
    public string Name { get; set; }
    public string StoreDepartment { get; set; }
    public double price { get; set; }
    public int Quantity { get; set; }
    public System.DateTime DateOfExpiration { get; set; } = System.DateTime.Now;
    public Goods(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration)
    {
        Name = name;
        StoreDepartment = storeDepartment;
        this.price = price;
        Quantity = quantity;
        this.DateOfExpiration = DateOfExpiration;
    }
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Department: {StoreDepartment}, Price: {price}, Quantity: {Quantity}, DateOfExpiration: {DateOfExpiration}");
    }
    public void UpdateInfo(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration)
    {
        Name = name;
        StoreDepartment = storeDepartment;
        this.price = price;
        Quantity = quantity;
        this.DateOfExpiration = DateOfExpiration;
    }
}
public class Bread : Goods
{
    public string TypeOfBread { get; set; }
    public Bread(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration, string TypeOfBread)
        : base(name, storeDepartment, price, quantity, DateOfExpiration)
    {
        this.TypeOfBread = TypeOfBread;
    }
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"TypeOfBread: {TypeOfBread}");
    }
    public void UpdateBreadInfo(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration, string TypeOfBread)
    {
        base.UpdateInfo(name, storeDepartment, price, quantity, DateOfExpiration);
        this.TypeOfBread = TypeOfBread;
    }
}
public class DairyProduct : Goods
{
    public string TypeOfDairy { get; set; }
    public DairyProduct(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration, string TypeOfDairy)
        : base(name, storeDepartment, price, quantity, DateOfExpiration)
    {
        TypeOfDairy = TypeOfDairy;
    }
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"TypeOfDairy: {TypeOfDairy}");
    }
    public void UpdateDairyInfo(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration, string TypeOfDairy)
    {
        base.UpdateInfo(name, storeDepartment, price, quantity, DateOfExpiration);
        this.TypeOfDairy = TypeOfDairy;
    }
}
public class Cheese : DairyProduct
{
    public string TypeOfCheese { get; set; }
    public Cheese(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration, string TypeOfDairy, string TypeOfCheese)
        : base(name, storeDepartment, price, quantity, DateOfExpiration, TypeOfDairy)
    {
        TypeOfCheese = TypeOfCheese;
    }
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"TypeOfCheese: {TypeOfCheese}");
    }
    public void UpdateCheeseInfo(string name, string storeDepartment, double price, int quantity, System.DateTime DateOfExpiration, string TypeOfDairy, string TypeOfCheese)
    {
        base.UpdateDairyInfo(name, storeDepartment, price, quantity, DateOfExpiration, TypeOfDairy);
        this.TypeOfCheese = TypeOfCheese;
    }
}
class Program
{
    static void Main(string[] args)
    {Console.WriteLine("Which department of the store you want to choose?(Bakery(1) or Dairy(2)?)");
        if (Console.ReadLine() == "1")
        { 
          Goods bread = new Bread("Rye Bread", "Bakery", 1.50, 100, DateTime.Now.AddDays(7), "Rye");
        bread.DisplayInfo();
        Console.WriteLine();
        Goods bread2 = new Bread("White Bread", "Bakery", 1.20, 150, DateTime.Now.AddDays(5), "White");
        bread2.DisplayInfo();
        Console.WriteLine();
        Goods bread3 = new Bread("Whole Wheat Bread", "Bakery", 1.80, 80, DateTime.Now.AddDays(10), "Whole Wheat");
        bread3.DisplayInfo();
        Console.WriteLine();
        }
        else if(Console.ReadLine() == "2")
        {
            Console.WriteLine("You have chosen Dairy department, here is dairy products or cheese. What you want to choose?(Dairy products(1) or Cheese(2)?)");
            if (Console.ReadLine() == "1")
            {
                 Goods dairyProduct = new DairyProduct("Milk", "Dairy", 0.99, 200, DateTime.Now.AddDays(10), "Whole");
        dairyProduct.DisplayInfo();
        Console.WriteLine();
        Goods dairyProduct2 = new DairyProduct("Yogurt", "Dairy", 1.20, 150, DateTime.Now.AddDays(14), "Greek");
        dairyProduct2.DisplayInfo();
        Console.WriteLine();
        Goods dairyProduct3 = new DairyProduct("Butter", "Dairy", 2.00, 100, DateTime.Now.AddDays(20), "Salted");
        dairyProduct3.DisplayInfo();
        Console.WriteLine();
            }
            else if (Console.ReadLine() == "2")
            {
                  Goods cheese = new Cheese("Cheddar Cheese", "Dairy", 2.50, 50, DateTime.Now.AddDays(15), "Cheese", "Cheddar");
        cheese.DisplayInfo();
        Console.WriteLine();
        Goods cheese2 = new Cheese("Gouda Cheese", "Dairy", 3.00, 30, DateTime.Now.AddDays(20), "Cheese", "Gouda");
        cheese2.DisplayInfo();
        Console.WriteLine();
        Goods cheese3 = new Cheese("Blue Cheese", "Dairy", 4.00, 20, DateTime.Now.AddDays(25), "Cheese", "Blue");
        cheese3.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose either 1 or 2.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please choose either 1 or 2.");
            return;
        }


       
      
    }
}