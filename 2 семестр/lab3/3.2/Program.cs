using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab.work._3._2
{
    public class Telephone 
    {
     public string telephoneName { get; set; }
     public string producer { get; set; }
     public double price { get; set; }
     public int releaseInDays { get; set; }
     public int quantity { get; set; }
        public Telephone(string telephoneName, string producer, double price, int releaseInDays, int quantity)
        {
            this.telephoneName = telephoneName;
            this.producer = producer;
            this.price = price;
            this.releaseInDays = releaseInDays;
            this.quantity = quantity;
        }
    }
    internal class Program
                                                {
        static async Task Main(string[] args)
        {
            List<Telephone> telephones = new List<Telephone>()
            {
                new Telephone("iPhone 15 Pro", "Apple", 999.99, 30, 2),
                new Telephone("Galaxy S23 Ultra", "Samsung", 1199.99, 45, 2),
                new Telephone("Galaxy j2 Core", "Samsung", 99.99, 5, 1),
                new Telephone("Nokia3310", "Nokia", 49.99, 2, 4),
                new Telephone("Pixel 8 Pro", "Google", 899.99, 20, 10),
                new Telephone("OnePlus 11", "OnePlus", 699.99, 15, 12),
                new Telephone("Xperia 1 IV", "Sony", 1299.99, 60,7),
                new Telephone("Moto G Power", "Motorola", 249.99, 10, 2),
                new Telephone("Iphone SE", "Apple", 429.99, 25, 9),
                new Telephone("Galaxy A53", "Samsung", 449.99, 35, 4),
                new Telephone("Pixel 7a", "Google", 499.99, 18, 2),
                new Telephone("OnePlus Nord 2", "OnePlus", 399.99, 12, 6),
                new Telephone("Iphone 14", "Apple", 799.99, 28, 18),
                new Telephone("Galaxy Z Fold 4", "Samsung", 1799.99, 50, 12),
                new Telephone("Iphone 13 Mini", "Apple", 699.99, 22, 3)
            };

            using (FileStream fs = new FileStream("telephones.bin", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fs, telephones);
            }
            using (FileStream fs = new FileStream("telephones.bin", FileMode.Open))
            {
                telephones = await JsonSerializer.DeserializeAsync<List<Telephone>>(fs);
            }

            Console.WriteLine("Number of telephones:\n");
            int totalTelephones = telephones.Count;
            Console.WriteLine(totalTelephones);
            Console.WriteLine("--------------------\n");


            Console.WriteLine("Telephones that cost more than 100:\n");

            var priceMoreThan100 = from t in telephones
                                   where t.price > 100
                                   select t;
            foreach (var telephone in priceMoreThan100)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }

            Console.WriteLine("--------------------\n");




            Console.WriteLine("Telephones that cost 400 - 700:\n");
            var priceBetween400And700 = from t in telephones
                                        where t.price >= 400 && t.price <= 700
                                        select t;
            foreach (var telephone in priceBetween400And700)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }



            Console.WriteLine("--------------------\n");



            Console.WriteLine("choose the quantity of which producer you want to check\n");
            string producerInput = Console.ReadLine();
            switch (producerInput)
            {
                case "Apple":
                case "Samsung":
                case "Google":
                case "OnePlus":
                case "Nokia":
                case "Sony":
                case "Motorola":
                    var telephoneBrand = from t in telephones
                                         where t.producer.Contains(producerInput)
                                         select t;
                    int count = telephoneBrand.Count();
                    Console.WriteLine($"Number of telephones from {producerInput}: {count}");
                    await Task.Delay(10000);
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Producer not found in the list.");
                    break;
            }



            Console.WriteLine("--------------------\n");




            Console.WriteLine("Telephones with the minimal price:\n");
            double minPrice = telephones.Min(t => t.price);
            var cheapestTelephones = from t in telephones
                                     where t.price == minPrice
                                     select t;
            foreach (var telephone in cheapestTelephones)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }




            Console.WriteLine("--------------------\n");







            Console.WriteLine("Telephones with max price:\n");
            double maxPrice = telephones.Max(t => t.price);
            var mostExpensiveTelephones = from t in telephones
                                          where t.price == maxPrice
                                          select t;
            foreach (var telephone in mostExpensiveTelephones)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }




            Console.WriteLine("--------------------\n");





            Console.WriteLine("5 Telephones that cost the most:\n");
            var top5ExpensiveTelephones = telephones
                .OrderByDescending(t => t.price)
                .Take(5);
            foreach (var telephone in top5ExpensiveTelephones)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }



            Console.WriteLine("--------------------\n");




            Console.WriteLine("5 Telephones that cost the least of all:\n");
            var top5CheapestTelephones = telephones
                .OrderBy(t => t.price)
                .Take(5);
            foreach (var telephone in top5CheapestTelephones)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }



            Console.WriteLine("--------------------\n");




            Console.WriteLine("3 most oldest telephones:\n");
            var top3OldestTelephones = telephones
                .OrderByDescending(t => t.releaseInDays)
                .Take(3);
            foreach (var telephone in top3OldestTelephones)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }



            Console.WriteLine("--------------------\n");




            Console.WriteLine("3 most newest telephones:\n");
            var top3NewestTelephones = telephones
                .OrderBy(t => t.releaseInDays)
                .Take(3);
            foreach (var telephone in top3NewestTelephones)
            {
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }



            Console.WriteLine("--------------------\n");




            Console.WriteLine("Number of all brands:\n");
            var brandCounts = telephones
                .GroupBy(t => t.producer)
                .Select(g => new { Brand = g.Key, Count = g.Count() });
            foreach (var brand in brandCounts)
            {
                Console.WriteLine($"Brand: {brand.Brand}, Count: {brand.Count}");
            }



            Console.WriteLine("--------------------\n");




            Console.WriteLine("Quantity of all models:\n");
            var totalQuantity = telephones
                .GroupBy(t => t.telephoneName)
                .Select(g => new { Model = g.Key, TotalQuantity = g.Sum(t => t.quantity) });
            foreach (var model in totalQuantity)
            {
                Console.WriteLine($"Model: {model.Model}, Total Quantity: {model.TotalQuantity}");
            }



            Console.WriteLine("--------------------\n");



            Console.WriteLine("Phones filtred by year:\n");
            var phonesByYear = from t in telephones
                               where t.releaseInDays <= 365
                               select t;
           foreach (var telephone in phonesByYear) 
            { 
                Console.WriteLine($"Name: {telephone.telephoneName}, Producer: {telephone.producer}, Price: {telephone.price}, Release in days: {telephone.releaseInDays}");
            }


           Console.WriteLine("--------------------\n");
        }
                                                 }
}
