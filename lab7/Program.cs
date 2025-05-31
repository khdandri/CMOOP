using System;
using System.Collections.Generic;
namespace labWork7
{
    using System;
    using System.Collections.Generic;

    using System;
    using System.Collections.Generic;

    public interface IPrint
    {
        void Print();
    }

    public interface ISearch
    {
        bool Search(string value);
    }

    public class Disk : IPrint, ISearch
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public double Size { get; set; }
        public string Genre { get; set; }

        public Disk(string name, string author, double size, string genre)
        {
            // Перевірки на null і порожні значення
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.");
            if (string.IsNullOrEmpty(author))
                throw new ArgumentException("Author cannot be null or empty.");
            if (size <= 0)
                throw new ArgumentException("Size must be positive.");
            if (string.IsNullOrEmpty(genre))
                throw new ArgumentException("Genre cannot be null or empty.");

            Name = name;
            Author = author;
            Size = size;
            Genre = genre;
        }

        public void Print()
        {
            Console.WriteLine($"Disk: Name={Name}, Author={Author}, Size={Size}, Genre={Genre}");
        }

        public bool Search(string value)
        {
            if (value == null)
                return false;

            return Name.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                   Author.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                   Genre.Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class DVD : Disk
    {
        public int Duration { get; set; }

        public DVD(string name, string author, double size, string genre, int duration)
            : base(name, author, size, genre)
        {
            if (duration <= 0)
                throw new ArgumentException("Duration must be positive.");

            Duration = duration;
        }

        public void Print()
        {
            base.Print(); // Викликаємо метод базового класу
            Console.WriteLine($"DVD: Duration={Duration} minutes");
        }

        public new bool Search(string value)
        {
            if (value == null)
                return false;

            return base.Search(value) || Duration.ToString().Contains(value);
        }
    }

    public class CD : Disk
    {
        public int Tracks { get; set; }

        public CD(string name, string author, double size, string genre, int tracks)
            : base(name, author, size, genre)
        {
            if (tracks <= 0)
                throw new ArgumentException("Tracks must be positive.");

            Tracks = tracks;
        }

        public void Print()
        {
            base.Print(); // Викликаємо метод базового класу
            Console.WriteLine($"CD: Tracks={Tracks} tracks");
        }

        public new bool Search(string value)
        {
            if (value == null)
                return false;

            return base.Search(value) || Tracks.ToString().Contains(value);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<object> media = new List<object>();

            try
            {
                media.Add(new DVD("The Matrix", "Wachowski", 4.7, "Sci-Fi", 136));
                media.Add(new CD("Thriller", "Michael Jackson", 0.7, "Pop", 9));
                media.Add(new Disk("Random Access Memories", "Daft Punk", 1.0, "Electronic"));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error creating media: {e.Message}");
                return;
            }

            foreach (var item in media)
            {
                try
                {
                    if (item is IPrint printable)
                    {
                        printable.Print();
                    }

                    if (item is ISearch searchable)
                    {
                        if (searchable.Search("Jackson"))
                        {
                            Console.WriteLine("Found item containing 'Jackson'");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unexpected error occurred: {e.Message}");
                }
            }

            Console.ReadKey();
        }
    }
}