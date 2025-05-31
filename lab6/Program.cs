using System;
namespace labWork6;

public abstract class Product
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public abstract double CalculateDiscount();

    public override string ToString()
    {
        return $"Product: Name={Name}, Price={Price}";
    }
}

public class Book : Product
{
    public string Author { get; set; }

    public Book(string name, double price, string author) : base(name, price)
    {
        Author = author;
    }

    public override double CalculateDiscount()
    {
        return Price * 0.1;
    }

    public override string ToString()
    {
        return base.ToString() + $", Author={Author}";
    }
}

public class ElectronicDevice : Product
{
    public int WarrantyMonths { get; set; }

    public ElectronicDevice(string name, double price, int warranty) : base(name, price)
    {
        WarrantyMonths = warranty;
    }

    public override double CalculateDiscount()
    {
        return Price * 0.05;
    }

    public override string ToString()
    {
        return base.ToString() + $", Warranty={WarrantyMonths} months";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Product[] products = {
            new Book("C# Basics", 25.00, "John Doe"),
            new ElectronicDevice("Laptop", 1200.00, 12),
            new Book("OOP in C#", 30.00, "Jane Smith")
        };

        foreach (var product in products)
        {
            Console.WriteLine(product);
            Console.WriteLine($"Discount: {product.CalculateDiscount()}");

            if (product is Book book)
            {
                Console.WriteLine($"Book Author: {book.Author}");
            }

            if (product is ElectronicDevice device)
            {
                Console.WriteLine($"Device Warranty: {device.WarrantyMonths}");
            }
        }
    }
}