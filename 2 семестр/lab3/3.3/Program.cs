using System.Text;

namespace lab.work._3._3
{
    internal class Program
    {
        public class Company
        {
            
            public int salary { get; set; }
            public Company(int salary)
            {
                this.salary = salary;
            }

        }

        public class Employee : Company
        { 
            public Employee(int salary) : base(salary)
            {
            }
            public string employeeName { get; set; }
            public int dayOfBirth { get; set; }
            public int monthOfBirth { get; set; }
            public int yearOfBirth { get; set; }
            public int workExperience { get; set; }
            public bool hasHigherEducation { get; set; }
            
            public Employee(string employeeName, int dayOfBirth, int monthOfBirth, int yearOfBirth, int workExperience, bool hasHigherEducation, int salary) : base(salary)
            {
                this.employeeName = employeeName;
                this.dayOfBirth = dayOfBirth;
                this.monthOfBirth = monthOfBirth;
                this.yearOfBirth = yearOfBirth;
                this.workExperience = workExperience;
                this.hasHigherEducation = hasHigherEducation;
            }
        }


        public class  President : Employee 
        {
        public President(string employeeName, int dayOfBirth, int monthOfBirth, int yearOfBirth, int workExperience, bool hasHigherEducation, int salary) : base(employeeName, dayOfBirth, monthOfBirth, yearOfBirth, workExperience, hasHigherEducation, salary)
            {
            }
        }


        public class Manager : Employee
        {
            public Manager(string employeeName, int dayOfBirth, int monthOfBirth, int yearOfBirth, int workExperience, bool hasHigherEducation, int salary) : base(employeeName, dayOfBirth, monthOfBirth, yearOfBirth, workExperience, hasHigherEducation, salary)
            {
            }
        }


        public class Worker : Employee
        {
            public Worker(string employeeName, int dayOfBirth, int monthOfBirth, int yearOfBirth, int workExperience, bool hasHigherEducation, int salary) : base(employeeName, dayOfBirth, monthOfBirth, yearOfBirth, workExperience, hasHigherEducation, salary)
            {
            }
        }


        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;


            List<Company> companies = new List<Company>()
            {
                new President("Volodymyr Smith", 15, 10, 1965, 30, true, 200000),
                new Manager("Arsen Doe", 22, 9, 1975, 20, true, 100000),
                new Manager("Olena Johnson", 5, 3, 1980, 15, true, 95000),
                new Manager ("Ivan Brown", 12, 10, 1985, 10, false, 90000),
                new Manager ("Maria Davis", 30, 1, 1990, 8, true, 85000),
                new Worker("Petro Wilson", 18, 7, 1995, 5, false, 60000),
                new Worker("Anna Martinez", 25, 4, 1992, 7, true, 65000),
                new Worker("Olga Anderson", 3, 12, 1988, 12, true, 70000),
                new Worker("Dmytro Taylor", 9, 10, 1993, 6, false, 62000),
                new Worker("Kateryna Thomas", 14, 2, 1991, 9, true, 68000),
                new Worker("Andriy Schevchenko", 29, 10, 1987, 14, true, 72000),
                new Worker("Svitlana White", 7, 5, 1994, 4, false, 59000)
            };

            Console.WriteLine("Quntity of employees in the company: " + companies.Count);


            Console.WriteLine("------------------\n");


            Console.WriteLine("Total salary expenses of the company: " + companies.Sum(c => c.salary));


            Console.WriteLine("------------------\n");


            Console.WriteLine("Collection with 10 workers with the highest expirience");

            var top10ExperiencedWorkers = companies
                .OfType<Employee>()
                .OrderByDescending(e => e.workExperience)
                .Take(10);

            List<Employee> top10List = top10ExperiencedWorkers.ToList();

            foreach (var worker in top10List)
            {
                Console.WriteLine($"{worker.employeeName}, Work Experience: {worker.workExperience} years");
            }

            var youngestWorker = top10List
                .OrderByDescending(e => e.yearOfBirth)
                .ThenByDescending(e => e.monthOfBirth)
                .ThenByDescending(e => e.dayOfBirth)
                .Where(e => e.hasHigherEducation == true)
                .FirstOrDefault();
            Console.WriteLine($"The youngest worker with higher education among the top 10 experienced workers is: {youngestWorker.employeeName}, born on {youngestWorker.dayOfBirth}/{youngestWorker.monthOfBirth}/{youngestWorker.yearOfBirth}.");


            Console.WriteLine("------------------\n");



            Console.WriteLine("The youngest and the oldest worker in the company:");
            var youngest = companies
                .OfType<Employee>()
                .OrderByDescending(e => e.yearOfBirth)
                .ThenByDescending(e => e.monthOfBirth)
                .ThenByDescending(e => e.dayOfBirth)
                .FirstOrDefault();
            var oldest = companies
                .OfType<Employee>()
                .OrderBy(e => e.yearOfBirth)
                .ThenBy(e => e.monthOfBirth)
                .ThenBy(e => e.dayOfBirth)
                .FirstOrDefault();
            Console.WriteLine($"Youngest: {youngest.employeeName}, born on {youngest.dayOfBirth}/{youngest.monthOfBirth}/{youngest.yearOfBirth}.");
            Console.WriteLine($"Oldest: {oldest.employeeName}, born on {oldest.dayOfBirth}/{oldest.monthOfBirth}/{oldest.yearOfBirth}.");


            Console.WriteLine("------------------\n");




            Console.WriteLine("Collection of employees that were born in october:");
            var octoberBornEmployees = companies
                .OfType<Employee>()
                .Where(e => e.monthOfBirth == 10);
            var employeeObject = octoberBornEmployees
                 .GroupBy(e => e.GetType().Name)
                 .OrderBy(g => g.Key);
            foreach (var group in employeeObject)
            {
                Console.WriteLine($" На професії {group.Key} {group.Count()}людей, які народилися в жовтні ");
                foreach (var employee in group)
                {
                    Console.WriteLine($" - {employee.employeeName}, дата народження: {employee.dayOfBirth}/{employee.monthOfBirth}/{employee.yearOfBirth}");
                }
            }


            Console.WriteLine("------------------\n");


            var sirtByNameVolodymyr = companies
                .OfType<Employee>()
                .Where(e => e.employeeName.Contains("Volodymyr"))
                .OrderBy(e => e.employeeName);
            List<Employee> volodymyrList = sirtByNameVolodymyr.ToList();
            var youngestVolodymyr = volodymyrList
                .OrderByDescending(e => e.yearOfBirth)
                .ThenByDescending(e => e.monthOfBirth)
                .ThenByDescending(e => e.dayOfBirth)
                .FirstOrDefault();
            Console.WriteLine("Вітаємо " +  youngestVolodymyr.employeeName +" з премією у розмірі " + youngestVolodymyr.salary / 3.0 + " грн!");
        }
    }
}
