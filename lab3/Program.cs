using System.Text;

namespace labwork3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Виберіть спосіб заповнення масиву: \n1 - З файлу \n2 - З клавіатури \n3 - Випадковими числами");
            int choice = int.Parse(Console.ReadLine());
            int[] array = new int[10];

            switch (choice)
            {
                case 1:
                    string filePath = "ARRAY.TXT";
                    if (File.Exists(filePath))
                    {
                        array = File.ReadAllText(filePath)
                            .Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();
                    }
                    else
                    {
                        Console.WriteLine("Файл ARRAY.TXT не знайдено.");
                        return;
                    }
                    break;
                case 2:
                    Console.WriteLine("Введіть 10 чисел через пробіл:");
                    array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    Console.WriteLine("Ви ввели менше або більше 10 чисел!");
                    break;
                case 3:
                    Random rand = new Random();
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = rand.Next(-50, 50);
                    }
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    return;
            }

            Console.WriteLine("Початковий масив: " + string.Join(", ", array));

            for (int i = 4; i < array.Length; i += 5)
            {
                array[i] = array.Skip(i - 4).Take(4).Max();
            }

            Console.WriteLine("Модифікований масив: " + string.Join(", ", array));

            int negativeCount = array.Count(x => x < 0);
            Console.WriteLine("Кількість від'ємних елементів: " + negativeCount);

        }
    }
}