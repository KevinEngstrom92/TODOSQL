using System;
using static System.Console;

namespace TODO
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shouldRun = true;

            while (shouldRun)
            {
                Clear();

                WriteLine("1. Add todo");
                WriteLine("2. List todos");
                WriteLine("3. Exit");

                ConsoleKeyInfo keyPressed = ReadKey(true);

                Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:

                        break;

                    case ConsoleKey.D3:

                        shouldRun = false;

                        break;
                }
            }
        }
    }
}
