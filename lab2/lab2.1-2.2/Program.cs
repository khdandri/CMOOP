using System;
using System.Text;
namespace LaboratoryWork2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Part2.1;  
            /*    double xStart = 2.0;  
                double xEnd = 12.0;  
                double dx = 0.5;      
                double[] aValues = { 0.5, 1.0, 1.5, 2.0 }; 

                Console.WriteLine(" a    |   x    |    y");
                Console.WriteLine("------------------------");

                foreach (double a in aValues)
                {
                    for (double x = xStart; x <= xEnd; x += dx)
                    {
                        double denominator = 1 + a * x;
                        if (denominator == 0)
                        {
                            Console.WriteLine($" a={a}, x={x}: Ділення на нуль!");
                            continue;
                        }

                        double fraction = (a * x) / denominator;
                        double y = Math.Log(Math.Abs(fraction));

                        Console.WriteLine($" {a,4} | {x,5} | {y,8:F4}");
                    }
                    Console.WriteLine(); 
                }*/
            //Part2.2;
            /* double[] xValues = { 0.5, 6.5, 13.5 };
             double epsilon = 1e-6;

             Console.WriteLine(" x    |   S(x) (Ряд)   |  y(x) = sin(pi*x/3) |  Різниця");
             Console.WriteLine("--------------------------------------------------------");

             foreach (double x in xValues)
             {
                 double sum = Math.Sqrt(3) / 2;
                 double term;
                 int n = 1;

                 do
                 {
                     long fact = 1;
                     for (int i = 1; i <= n; i++)
                         fact *= i;

                     double numerator = Math.Sin(Math.PI / 3 + n * Math.PI / 2) * Math.Pow(Math.PI, n) * Math.Pow(x - 1, n);
                     double denominator = Math.Pow(3, n) * fact;
                     term = numerator / denominator;

                     sum += term; 
                     n++;

                 } while (Math.Abs(term) >= epsilon); 

                 double exactValue = Math.Sin(Math.PI * x / 3);
                 double difference = Math.Abs(sum - exactValue);

                 Console.WriteLine($" {x,4} | {sum,12:F6} | {exactValue,12:F6}          | {difference,10:E4}");
             }*/


        }
    }
}