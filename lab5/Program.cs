
namespace labWork5;

using System;

public enum KindOf { SystemPrograms, UserPrograms, Documents }

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public Person(string firstName, string lastName, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, born {BirthDate.ToShortDateString()}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Person)
            return FirstName == ((Person)obj).FirstName && LastName == ((Person)obj).LastName && BirthDate == ((Person)obj).BirthDate;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, BirthDate);
    }
}

public class File
{
    public string Name { get; set; }
    public Person Author { get; set; }
    public double Size { get; set; }

    public File(string name, Person author, double size)
    {
        Name = name;
        Author = author;
        Size = size;
    }

    public File() { }

    public override string ToString()
    {
        return $"File: Name={Name}, Author={Author}, Size={Size}";
    }

    public override bool Equals(object obj)
    {
        if (obj is File)
            return Name == ((File)obj).Name && Author.Equals(((File)obj).Author) && Size == ((File)obj).Size;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Author, Size);
    }
}

public class Folder
{
    private string _folderName;
    private KindOf _kind;
    private DateTime _creationDate;
    private File[] _files;

    public Folder(string folderName, KindOf kind, DateTime creationDate, File[] files)
    {
        _folderName = folderName;
        _kind = kind;
        _creationDate = creationDate;
        _files = files;
    }

    public Folder()
    {
        _files = new File[0];
    }

    public string FolderName
    {
        get => _folderName;
        set
        {
            if (!string.IsNullOrEmpty(value))
                _folderName = value;
            else
                throw new ArgumentException("Folder name cannot be empty.");
        }
    }

    public KindOf Kind
    {
        get => _kind;
        set => _kind = value;
    }

    public DateTime CreationDate
    {
        get => _creationDate;
        set => _creationDate = value;
    }

    public File[] Files
    {
        get => _files;
        set => _files = value ?? new File[0];
    }

    public double TotalSize
    {
        get
        {
            double total = 0;
            foreach (var file in _files)
            {
                total += file.Size;
            }
            return total;
        }
    }

    public File this[int index]
    {
        get
        {
            if (index >= 0 && index < _files.Length)
                return _files[index];
            else
                throw new IndexOutOfRangeException("Index is out of range.");
        }
    }

    public void AddFiles(params File[] newFiles)
    {
        if (newFiles != null)
        {
            Array.Resize(ref _files, _files.Length + newFiles.Length);
            newFiles.CopyTo(_files, _files.Length - newFiles.Length);
        }
    }

    public override string ToString()
    {
        return $"Folder: Name={FolderName}, Kind={Kind}, Created={CreationDate}, TotalSize={TotalSize}, FilesCount={Files.Length}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Folder)
        {
            Folder other = (Folder)obj;
            return FolderName == other.FolderName && Kind == other.Kind && CreationDate == other.CreationDate;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FolderName, Kind, CreationDate);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Person person1 = new Person("John", "Doe", new DateTime(1990, 5, 15));
        Person person2 = new Person("Jane", "Smith", new DateTime(1985, 10, 20));

        File file1 = new File("Document1.txt", person1, 1.2);
        File file2 = new File("Image1.jpg", person2, 2.5);
        File file3 = new File("App1.exe", person1, 5.0);

        Folder folder1 = new Folder("MyFolder", KindOf.Documents, DateTime.Now, new File[] { file1, file2 });

        Console.WriteLine(folder1);

        Console.WriteLine($"File at index 0: {folder1[0]}");

        folder1.FolderName = "NewFolder";
        folder1.Kind = KindOf.UserPrograms;

        folder1.AddFiles(file3);

        Console.WriteLine(folder1);

        Folder[] folders = new Folder[] { folder1, new Folder("AnotherFolder", KindOf.SystemPrograms, DateTime.Now.AddDays(-1), new File[] { file1 }) };

        Folder maxFolder = null;
        Folder minFolder = null;
        double maxSize = double.MinValue;
        double minSize = double.MaxValue;

        foreach (var folder in folders)
        {
            if (folder.TotalSize > maxSize)
            {
                maxSize = folder.TotalSize;
                maxFolder = folder;
            }

            if (folder.TotalSize < minSize)
            {
                minSize = folder.TotalSize;
                minFolder = folder;
            }
        }

        Console.WriteLine($"Folder with max size: {maxFolder}");
        Console.WriteLine($"Folder with min size: {minFolder}");
    }
}
