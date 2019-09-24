using System;
using TODO.Domain;
using static System.Console;

namespace TODO
{
    class Program
    {
        static Task[] taskList = new Task[100];

        static void Main(string[] args)
        {
            bool shouldRun = true;

            int taskIdCounter = 1;

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

                        Write("Title: ");

                        string title = ReadLine();

                        Write("Due date (yyyy-mm-dd hh:mm): ");

                        DateTime dueDate = DateTime.Parse(ReadLine());

                        taskList[GetIndexPosition()] = new Task(taskIdCounter++, title, dueDate);

                        break;

                    case ConsoleKey.D2:

                        WriteLine("ID  Title                   Due date    Completed   ");
                        WriteLine("----------------------------------------------------");

                        foreach (var task in taskList)
                        {
                            if (task == null) continue;

                            WriteLine($"{task.Id}  {task.Title}{task.DueDate.ToString().PadLeft(25, ' ')}");
                        }

                        ReadKey(true);

                        break;
                        
                    case ConsoleKey.D3:

                        shouldRun = false;

                        break;
                }
            }
        }

        static int GetIndexPosition()
        {
            int result = -1;
            for (int i = 0; i < taskList.Length; i++)
            {
                if (taskList[i] != null)
                {
                    continue;
                }
                if (taskList[i] == null)
                {
                    result = i;
                    break;
                }
                if (result == -1)
                {
                    throw new Exception("No avalible position");
                }
            }
            return result;
        }
    }
}
