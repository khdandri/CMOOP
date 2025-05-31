using System;
using System.Text;

public class UserInputOperations
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Введіть E-mail адресу для перевірки:");
        string userEmail = Console.ReadLine();

        if (!string.IsNullOrEmpty(userEmail))
        {
            bool isEmailValid = IsValidEmail(userEmail);
            Console.WriteLine($"Введена E-mail адреса \"{userEmail}\" - {(isEmailValid ? "правильний" : "неправильний")} вираз");
        }
        else
        {
            Console.WriteLine("Ви не ввели E-mail адресу.");
        }

        Console.WriteLine("\n----------------------------------------\n");

        Console.WriteLine("Введіть рядок для об'єднання слів:");
        string userInputString = Console.ReadLine();

        if (!string.IsNullOrEmpty(userInputString))
        {
            string[] words = SplitString(userInputString, ' ');

            if (words.Length < 2)
            {
                Console.WriteLine($"Вхідний рядок: \"{userInputString}\"");
                Console.WriteLine("Недостатньо слів для об'єднання.");
                Console.WriteLine($"Кількість слів у новому рядку: {words.Length}");
            }
            else
            {
                string combinedString = "";
                for (int i = 0; i < words.Length - 1; i += 2)
                {
                    combinedString += words[i] + words[i + 1] + " ";
                }

                if (words.Length % 2 != 0)
                {
                    combinedString += words[words.Length - 1];
                }
                else if (combinedString.Length > 0)
                {
                    if (combinedString.EndsWith(" "))
                    {
                        combinedString = combinedString.Substring(0, combinedString.Length - 1);
                    }
                }

                string[] newWords = SplitString(combinedString, ' ');

                Console.WriteLine($"Вхідний рядок: \"{userInputString}\"");
                Console.WriteLine($"Новий рядок: \"{combinedString}\"");
                Console.WriteLine($"Кількість слів у новому рядку: {newWords.Length}");
            }
        }
        else
        {
            Console.WriteLine("Ви не ввели рядок для об'єднання слів.");
        }
    }

    static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || !char.IsLetter(email[0]))
        {
            return false;
        }

        int atIndex = -1;
        for (int i = 0; i < email.Length; i++)
        {
            char c = email[i];
            if (!char.IsLetterOrDigit(c) && c != '@' && c != '.')
            {
                return false;
            }
            if (c == '@')
            {
                if (atIndex != -1 || i == 0 || i == email.Length - 1)
                {
                    return false;
                }
                atIndex = i;
            }
        }

        if (atIndex == -1 || atIndex >= email.Length - 5)
        {
            return false;
        }

        string domainPart = email.Substring(atIndex + 1);
        int dotIndex = domainPart.LastIndexOf('.');
        if (dotIndex == -1 || dotIndex == 0 || dotIndex == domainPart.Length - 1)
        {
            return false;
        }

        string suffix = domainPart.Substring(dotIndex + 1);
        if (suffix != "com" && suffix != "localhost")
        {
            return false;
        }

        string localPart = email.Substring(0, atIndex);
        for (int i = 0; i < localPart.Length; i++)
        {
            if (!char.IsLetterOrDigit(localPart[i]))
            {
                return false;
            }
        }

        string namePart = domainPart.Substring(0, dotIndex);
        if (string.IsNullOrEmpty(namePart))
        {
            return false;
        }
        for (int i = 0; i < namePart.Length; i++)
        {
            if (!char.IsLetterOrDigit(namePart[i]))
            {
                return false;
            }
        }

        return true;
    }

    static string[] SplitString(string str, char separator)
    {
        if (string.IsNullOrEmpty(str))
        {
            return new string[0];
        }

        int count = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == separator)
            {
                count++;
            }
        }

        string[] result = new string[count + 1];
        int startIndex = 0;
        int resultIndex = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == separator)
            {
                result[resultIndex++] = str.Substring(startIndex, i - startIndex);
                startIndex = i + 1;
            }
        }
        result[resultIndex] = str.Substring(startIndex);

        return result;
    }
}