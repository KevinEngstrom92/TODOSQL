using Microsoft.Data.SqlClient;
using System;
using TODO.Domain;
using static System.Console;


namespace TODO
{
    class Program
    {
        //tre delar, Adress till instansen ; DatabasNamn ; autentisering

        //static string connectionString = "Data Source=.;Initial Catalog=TODO;Integrated Security=true";
        //static string connectionString = "Data Source=(local);Initial Catalog=TODO;Integrated Security=true";
        //static string connectionString = "Data Source=localhost;Initial Catalog=TODO;Integrated Security=true";
        //static string connectionString = "Data Source=127.0.0.1;Initial Catalog=TODO;Integrated Security=true"; FUNKAR EJ

        //static string connectionString = "Server=.;Database=TODO;Integrated Security=true";


        static string connectionString = "Data Source=.;Initial Catalog=TODO;Integrated Security=true";
       
        static int taskIdCounter = 1;
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

                        Write("Title: ");

                        string title = ReadLine();

                        Write("Due date (yyyy-mm-dd hh:mm): ");

                        DateTime dueDate = DateTime.Parse(ReadLine());


                         CreateTask( title, dueDate);

                        break;

                    case ConsoleKey.D2:

                        WriteLine("ID  Title                   Due date    Completed   ");
                        WriteLine("----------------------------------------------------");

                        var tasks = FetchAllTasks();


                        foreach (var task in tasks)
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

        private static Task[] FetchAllTasks()
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = @" 
                            SELECT [Id]
                            ,[Title]
                            ,[DueDate]
                            ,[Completed]
                            FROM [TODO].[dbo].[Task]
                                                        ";
            SqlCommand command = new SqlCommand(sql,connection);
            

            connection.Open();

            //Skicka SQL kommando till servern
            //Servern svarar nu i dataReader
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {

                string id = dataReader["Id"].ToString();
                string title = dataReader["Title"].ToString();
                string dueDate = dataReader["DueDate"].ToString();
                string completed = dataReader["Completed"].ToString();


                Write(id.PadRight(5, ' '));
                Write(title.PadRight(30, ' '));
                Write(dueDate.PadRight(20, ' '));
                WriteLine(completed);

            }

            Console.WriteLine("Hello Database Yes?");
            connection.Close();


            return new Task[100];
        }

        private static void CreateTask(string title, DateTime dueDate)
        {
            Task[] tasks = FetchAllTasks();

            tasks[GetIndexPosition()] = new Task(taskIdCounter++, title, dueDate);
            
        }

        
        static int GetIndexPosition()
        {

            Task[] tasks = FetchAllTasks();
            int result = -1;
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] != null)
                {
                    continue;
                }
                if (tasks[i] == null)
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
