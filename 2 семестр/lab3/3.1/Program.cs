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

namespace lab.work._3._1
{
    [Serializable]
    public class Firm
    {
        public string firmName { get; set; }
        public int daysOfFoundation { get; set; }
        public string bisnessProfile { get; set; }
        public string directorName { get; set; }
        public int numberOfEmployees { get; set; }
        public string adress { get; set; }

        public Firm(string firmName, int daysOfFoundation, string bisnessProfile, string directorName, int numberOfEmployees, string adress)
        {
            this.firmName = firmName;
            this.daysOfFoundation = daysOfFoundation;
            this.bisnessProfile = bisnessProfile;
            this.directorName = directorName;
            this.numberOfEmployees = numberOfEmployees;
            this.adress = adress;
        }
    }
                        internal class Program
                         {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            List<Firm> firms = new List<Firm>()
            {
                new Firm("Tech Solutions", 1506, "Software Development", "Alice Johnson", 250, "123 Tech Ave, Silicon City,USA"),
                new Firm("Green Energy Co.", 1379, "Renewable Energy", "Bob Smith", 150, "456 Green St, Eco Town,USA"),
                new Firm("Market Masters", 143, "Marketing and Advertising", "Carol White", 100, "789 Market Rd, Adville,London"),
                new Firm("White Health", 911, "Healthcare Services", "David Black", 300, "321 Health Blvd, Med City,Canada"),
                new Firm("It Solutions", 361, "IT Consulting", "Eva White", 80, "654 IT Park, Techno City,Germany"),
                new Firm("ITServe", 1807, "IT Consulting", "Frank Green", 200, "987 Finance Ln, Money Town,London"),
                new Firm("Foodies United", 2504, "Food and Beverage", "Grace Brown", 120, "159 Food Ct, Gourmet City,Italy"),
                new Firm("Food Delights", 110, "Food and Beverage", "Hank Blue", 90, "753 Culinary St, Flavor Town,France")

            };

            using (FileStream fs = new FileStream("firms.jsom", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<List<Firm>>(fs, firms);
            }
            List<Firm> currentfirms = new List<Firm>();
            using (FileStream fsRead = new FileStream("firms.jsom", FileMode.OpenOrCreate))
            {
                currentfirms = await JsonSerializer.DeserializeAsync<List<Firm>>(fsRead);
            }



            foreach (var firm in currentfirms)
            {
                Console.WriteLine($"Firm Name: {firm.firmName}, Day of Foundation: {firm.daysOfFoundation},  Business Profile: {firm.bisnessProfile}, Director Name: {firm.directorName}, Number of Employees: {firm.numberOfEmployees}, Address: {firm.adress}\n");
            }

            Console.WriteLine("---------------------------------------------------\n");

            Console.WriteLine("Firms that contain 'Food' in their name:\n");

            var filtredNamesFood = from firm in currentfirms
                                   where firm.firmName.Contains("Food")
                                   select firm.firmName;
            foreach (var name in filtredNamesFood)
            {
                Console.WriteLine($"Firm Name with 'Food': {name}\n");
            }


            Console.WriteLine("---------------------------------------------------\n");



            Console.WriteLine("Business Profiles that contain 'Marketing':\n");
            var filtredbisnessProfileMarketing = from firm in currentfirms
                                                 where firm.bisnessProfile.Contains("Marketing")
                                                 select firm.bisnessProfile;
            foreach (var firmName in filtredbisnessProfileMarketing)
            {
                Console.WriteLine($"Business Profile with 'Marketing': {firmName}\n");
            }


            Console.WriteLine("---------------------------------------------------\n");



            Console.WriteLine("Business Profiles that contain 'Marketing' or 'IT':\n");
            var filredbisnessProfileMarketingAndIT = from firm in currentfirms
                                                     where firm.bisnessProfile.Contains("Marketing") || firm.bisnessProfile.Contains("IT")
                                                     select firm.bisnessProfile;
            foreach (var bisnessProfile in filredbisnessProfileMarketingAndIT)
            {
                Console.WriteLine($"Business Profile with 'Marketing' or 'IT': {bisnessProfile}\n");
            }


            Console.WriteLine("---------------------------------------------------\n");



            Console.WriteLine("Number of Employees greater than 100:\n");
            var filtredNumberOfEmployees = from firm in currentfirms
                                           where firm.numberOfEmployees > 100
                                           select firm;
            foreach (var firm in filtredNumberOfEmployees)
            {
                Console.WriteLine($"{firm.firmName}, Number of Employees {firm.numberOfEmployees}\n");
            }

            Console.WriteLine("---------------------------------------------------\n");



            Console.WriteLine("Firms with Number of Employees greater than 100 or less than 300, sorted by Number of Employees:\n");
            var sortedFirmsByNumberOfEmployees = from firm in currentfirms
                                                 where firm.numberOfEmployees > 100 || firm.numberOfEmployees < 300
                                                 select firm;
            foreach (var firm in sortedFirmsByNumberOfEmployees)
            {
                Console.WriteLine($"Firm Name: {firm.firmName}, Number of Employees: {firm.numberOfEmployees}\n");
            }


            Console.WriteLine("---------------------------------------------------\n");




            Console.WriteLine("Firms located in London:\n");
            var filtredFirmsByAdress = from firm in currentfirms
                                       where firm.adress.Contains("London")
                                       select firm;
            foreach (var firm in filtredFirmsByAdress)
            {
                Console.WriteLine($"Firm Name: {firm.firmName}, Address: {firm.adress}\n");
            }


            Console.WriteLine("---------------------------------------------------\n");




            Console.WriteLine("Firms with Director Name containing 'White':\n");
            var filtredFirmsBydirectorNameWhite = from firm in currentfirms
                                                  where firm.directorName.Contains("White")
                                                  select firm;
            foreach (var firm in filtredFirmsBydirectorNameWhite)
            {
                Console.WriteLine($"Firm Name: {firm.firmName}, Director Name: {firm.directorName}\n");
            }

            Console.WriteLine("---------------------------------------------------\n");




            Console.WriteLine("Firms with Days of Foundation greater than 2 years:\n");
            var sortedFirmsByFoundationDate = from firm in currentfirms
                                              where firm.daysOfFoundation > 730
                                              select firm;
            foreach (var firm in sortedFirmsByFoundationDate) 
                {
                Console.WriteLine($"Firm Name: {firm.firmName}, Days of Foundation: {firm.daysOfFoundation}\n");
                }


            Console.WriteLine("---------------------------------------------------\n");




            Console.WriteLine("Firms with Days of Foundation greater than 150 days, sorted by Days of Foundation in descending order:\n");
            var sortedFirmsBy150Days = from firm in currentfirms
                                         where firm.daysOfFoundation > 150
                                         select firm;
            foreach (var firm in sortedFirmsBy150Days)
                {
                Console.WriteLine($"Firm Name: {firm.firmName}, Days of Foundation: {firm.daysOfFoundation}\n");
                }


            Console.WriteLine("---------------------------------------------------\n");


            Console.WriteLine("Firms with Director Name containing 'Black' or Firm Name containing 'White':\n");
            var sortedFirmsBydirectorNameAndFirmName = from firm in currentfirms
                                                     where firm.directorName.Contains("Black") || firm.firmName.Contains("White")
                                                     select firm;
            foreach (var firm in sortedFirmsBydirectorNameAndFirmName)
                {
                Console.WriteLine($"Firm Name: {firm.firmName}, Director Name: {firm.directorName}\n");
                }


        }
                         }
}
