using System;

namespace DelegateExercise
{
    delegate void MyDelegate(string message);
    delegate int CalcDelegate(int a, int b);

    internal class Program
    {
        static void Main(string[] args)
        {
            Methods methods = new Methods();
            MyDelegate myDelegate = methods.Method1;
            myDelegate += Methods.Method2;

            myDelegate("Hello!");

            Console.ReadKey();

            CalcDelegate calc = (x, y) => x + y;
            calc = (x, y) => x - y;
            Console.WriteLine(calc(3, 5));

            static void Calculate(int x, int y, Func<int, int, int> calcu)
            {
                int result = calcu(x, y);
                Console.WriteLine(result);

            }

            Calculate(3, 5, (x, y) => x + y);
        }
    }

    class Methods
    {
        public void Method1(string message)
        {
            Console.WriteLine("Method1: " + message);
        }

        public static void Method2(string message)
        {
            Console.WriteLine("Method2: " + message);
        }
    }
}
