using System;
using System.Text;

namespace GuessMyNumber
{
    internal class Program
    {
        private static int totalUserPoints = 0;
        private static int totalComputerPoints = 0;

        public static void StartGame()
        {
            Console.WriteLine("Вітаємо в грі |GUESS MY NUMBER|!\n Натисніть ENTER, щоб перейти до гри\n");
            Console.ReadKey();
            Console.WriteLine("Перед початком гри повідомляємо:\n");
            Console.WriteLine("Перший рівень (числа від 1 до 10):");
            Console.WriteLine("  - Початкова кількість життів: 5");
            Console.WriteLine("  - За кожну неправильну відповідь: -1 життя");
            Console.WriteLine("  - Кількість раундів: 3");
            Console.WriteLine("  - Очки за раунд (гравець): кількість життів, що залишилися * 5");
            Console.WriteLine("  - Очки за раунд (комп'ютер, якщо гравець програв): початкові життя гравця * 5");
            Console.WriteLine("\nДругий рівень (числа від 10 до 100):");
            Console.WriteLine("  - Початкова кількість життів: 22");
            Console.WriteLine("  - За кожну неправильну відповідь: -1 життя");
            Console.WriteLine("  - Кількість раундів: 2");
            Console.WriteLine("  - Очки за раунд (гравець): кількість життів, що залишилися * 10");
            Console.WriteLine("  - Очки за раунд (комп'ютер, якщо гравець програв): початкові життя гравця * 10");
            Console.WriteLine("\nПідказка:");
            Console.WriteLine("  - Вартість: 1 життя");
            Console.WriteLine("  - Доступна після неправильної спроби");
            Console.WriteLine("\nGood luck and have fun!\n");
        }

        public static (int userPoints, int computerPoints) PlayRound(int min, int max, int initialLives, int pointsMultiplier)
        {
            Random random = new Random();
            int secretNumber = random.Next(min, max + 1);
            int currentLives = initialLives;
            int roundUserPoints = 0;
            int roundComputerPoints = 0;

            Console.WriteLine($"Загадано число від {min} до {max}. У вас {currentLives} життів.");

            while (currentLives > 0)
            {
                Console.Write($"Введіть вашу спробу: ");
                if (!int.TryParse(Console.ReadLine(), out int guess))
                {
                    Console.WriteLine("Некоректний ввід. Будь ласка, введіть число.");
                    continue;
                }

                if (guess == secretNumber)
                {
                    roundUserPoints = currentLives * pointsMultiplier;
                    Console.WriteLine($"Вітаємо! Ви вгадали число {secretNumber}!");
                    Console.WriteLine($"Ви отримали {roundUserPoints} очок за цей раунд.");
                    break;
                }
                else
                {
                    currentLives--;
                    Console.WriteLine($"Неправильно. У вас залишилось {currentLives} життів.");
                    if (currentLives > 0)
                    {
                        Console.Write("Бажаєте отримати підказку? (вартість 1 життя) (так/ні): ");
                        string hintAnswer = Console.ReadLine()?.ToLower();
                        if (hintAnswer == "так")
                        {
                            if (currentLives > 0)
                            {
                                currentLives--;
                                if (guess < secretNumber)
                                {
                                    Console.WriteLine("Загадане число більше за ваше.");
                                }
                                else
                                {
                                    Console.WriteLine("Загадане число менше за ваше.");
                                }
                                Console.WriteLine($"У вас залишилось {currentLives} життів.");
                            }
                            else
                            {
                                Console.WriteLine("У вас недостатньо життів для підказки.");
                            }
                        }
                    }
                }
            }

            if (currentLives == 0)
            {
                roundComputerPoints = initialLives * pointsMultiplier;
                Console.WriteLine($"Ви програли раунд. Загадане число було {secretNumber}.");
                Console.WriteLine($"Комп'ютер отримує {roundComputerPoints} очок за цей раунд.");
            }

            return (roundUserPoints, roundComputerPoints);
        }

        public static bool PlayLevel(int level, int minRange, int maxRange, int numRounds, int initialLivesPercentage, int pointsMultiplier)
        {
            Console.WriteLine($"\n----- Розпочинається {level}-й рівень -----");
            int levelUserPoints = 0;
            int levelComputerPoints = 0;
            int initialLives = (int)Math.Round((maxRange - minRange + 1) * (initialLivesPercentage / 100.0));

            for (int round = 1; round <= numRounds; round++)
            {
                Console.WriteLine($"\n--- Раунд {round} ---");
                int currentInitialLives = (int)Math.Round((maxRange - minRange + 1) * (initialLivesPercentage / 100.0));
                var (roundPoints, computerRoundPoints) = PlayRound(minRange, maxRange, currentInitialLives, pointsMultiplier);
                levelUserPoints += roundPoints;
                levelComputerPoints += computerRoundPoints;
                Console.WriteLine($"Результат раунду: Ви - {roundPoints} очок, Комп'ютер - {computerRoundPoints} очок.");
                Console.WriteLine($"Проміжні підсумки: Ви - {levelUserPoints} очок, Комп'ютер - {levelComputerPoints} очок.");
            }

            totalUserPoints += levelUserPoints;
            totalComputerPoints += levelComputerPoints;

            Console.WriteLine($"\n----- Завершено {level}-й рівень -----");
            Console.WriteLine($"Результат рівня: Ви - {levelUserPoints} очок, Комп'ютер - {levelComputerPoints} очок.");
            Console.WriteLine($"Загальний рахунок: Ви - {totalUserPoints} очок, Комп'ютер - {totalComputerPoints} очок.");

            return levelUserPoints > 0;

            static void Main(string[] args)
            {
                Console.OutputEncoding = Encoding.UTF8;

                StartGame();

                bool firstLevelPassed = PlayLevel(1, 1, 10, 3, 50, 5);

                if (firstLevelPassed)
                {
                    Console.Write("\nБажаєте перейти на другий рівень? (так/ні): ");
                    if (Console.ReadLine()?.ToLower() == "так")
                    {
                        PlayLevel(2, 10, 100, 2, 25, 10);
                    }
                    else
                    {
                        Console.WriteLine("\nДякуємо за гру!");
                        Console.WriteLine($"Ваш підсумковий рахунок: {totalUserPoints} очок.");
                    }
                }
                else
                {
                    Console.WriteLine("\nВи програли перший рівень. Другий рівень недоступний.");
                    Console.WriteLine($"Ваш підсумковий рахунок: {totalUserPoints} очок.");
                }

                Console.WriteLine("\nГра завершена!");
                Console.WriteLine($"Фінальний рахунок: Ви - {totalUserPoints} очок, Комп'ютер - {totalComputerPoints} очок.");
                if (totalUserPoints > totalComputerPoints)
                {
                    Console.WriteLine("Вітаємо! Ви перемогли!");
                }
                else if (totalComputerPoints > totalUserPoints)
                {
                    Console.WriteLine("Комп'ютер переміг.");
                }
                else
                {
                    Console.WriteLine("Нічия!");
                }

                Console.ReadKey();
            }
        }
    }
}