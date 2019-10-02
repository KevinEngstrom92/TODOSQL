using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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


                        CreateTask(title, dueDate);

                        break;

                    case ConsoleKey.D2:

                        WriteLine("ID".PadRight(5, ' ')+"Title".PadRight(30, ' ')+"Due Date".PadRight(20, ' ')+"Completed");
                        WriteLine("----------------------------------------------------");

                        var tasks = FetchAllTasks();


                        foreach (var task in tasks)
                        {
                            if (task == null) continue;

                            WriteLine($"{task.Id}  {task.Title}{task.DueDate.ToString().PadLeft(25, ' ')}");
                        }

                        //Write(id.PadRight(5, ' '));
                        //Write(title.PadRight(30, ' '));
                        //Write(dueDate.PadRight(20, ' '));
                        //WriteLine(completed);

                        ReadKey(true);

                        break;

                    case ConsoleKey.D3:

                        shouldRun = false;

                        break;
                }
            }
        }

        private static List<Task> FetchAllTasks()
        {

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = @" 
                            SELECT [Id]
                            ,[Title]
                            ,[DueDate]
                            ,[Completed]
                            FROM [TODO].[dbo].[Task]
                                                        ";
            SqlCommand command = new SqlCommand(sql, connection);

            List<Task> taskList = new List<Task>();
            connection.Open();

            //Skicka SQL kommando till servern
            //Servern svarar nu i dataReader
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {

                int id = int.Parse(dataReader["Id"].ToString());
                string title = dataReader["Title"].ToString();

                

                DateTime.TryParse(dataReader["DueDate"].ToString(), out DateTime dueDate);
                Task tasken = new Task(id, title, dueDate);

                // string completed = dataReader["Completed"].ToString();

             


                taskList.Add(tasken);

            }

            //Console.WriteLine("Hello Database Yes?");
            connection.Close();


            return taskList;
        }

        private static void CreateTask(string title, DateTime dueDate)
        {

            string sql = $"INSERT INTO Task (Title, DueDate) VALUES (@TITLE, @DUEDATE)";
            
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("TITLE", title);
            command.Parameters.AddWithValue("DUEDATE", dueDate);
            int result = command.ExecuteNonQuery();

            connection.Close();
            Console.Clear();
            Console.WriteLine(result);
            Console.ReadKey();


        }



    }
}
