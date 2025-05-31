namespace labWork8;
using System;
using System.Collections.Generic;



public delegate double MathOperation(double a, double b);
public delegate bool FilterDelegate(int number);

public class Program
{
    public static double Add(double a, double b) => a + b;
    public static double Subtract(double a, double b) => a - b;
    public static double Multiply(double a, double b) => a * b;
    public static double Divide(double a, double b) => b != 0 ? a / b : double.NaN;

    public static bool IsEven(int n) => n % 2 == 0;
    public static bool IsPositive(int n) => n > 0;

    public static double PerformOperation(double a, double b, MathOperation operation)
    {
        return operation(a, b);
    }

    public static List<int> FilterArray(int[] array, FilterDelegate filter)
    {
        List<int> result = new List<int>();
        foreach (int num in array)
        {
            if (filter(num))
            {
                result.Add(num);
            }
        }
        return result;
    }

    public class Counter
    {
        public delegate void CounterEventHandler(int count);
        public event CounterEventHandler ThresholdReached;

        private int threshold;
        private int count;

        public Counter(int threshold)
        {
            this.threshold = threshold;
        }

        public void Increment()
        {
            count++;
            if (count == threshold)
            {
                OnThresholdReached(count);
            }
        }

        protected virtual void OnThresholdReached(int count)
        {
            ThresholdReached?.Invoke(count);
        }
    }

    static void Main(string[] args)
    {
        MathOperation add = Add;
        MathOperation subtract = Subtract;
        Console.WriteLine($"Add: {PerformOperation(5, 3, add)}");
        Console.WriteLine($"Subtract: {PerformOperation(5, 3, subtract)}");

        Console.WriteLine($"Multiply: {PerformOperation(5, 3, (a, b) => a * b)}");
        Console.WriteLine($"Divide: {PerformOperation(5, 3, (a, b) => b != 0 ? a / b : double.NaN)}");

        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> evens = FilterArray(numbers, IsEven);
        List<int> positives = FilterArray(numbers, n => n > 0);
        Console.WriteLine($"Evens: {string.Join(", ", evens)}");
        Console.WriteLine($"Positives: {string.Join(", ", positives)}");

        Counter counter = new Counter(5);
        counter.ThresholdReached += (count) => Console.WriteLine($"Threshold reached at {count}!");
        for (int i = 0; i < 10; i++)
        {
            counter.Increment();
        }
    }
}