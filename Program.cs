using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.txt");

    static void Main(string[] args)
    {
        Console.Title = "Task Manager";
        Console.WriteLine("Welcome to Task Manager\n");

        bool closeProgram = false;
        List<string> myList = LoadTasks(); 

        while (!closeProgram)
        {
            int choice = MainPage();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\n Add Task selected");
                    Console.Write("Enter your new task: ");
                    string newTask = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(newTask))
                    {
                        myList.Add(newTask);
                        SaveTasks(myList); 
                        Console.WriteLine($" Task added: \"{newTask}\"\n");
                    }
                    else
                    {
                        Console.WriteLine("Task cannot be empty.\n");
                    }
                    break;

                case 2:
                    Console.WriteLine("\n Your Task List:");
                    if (myList.Count == 0)
                    {
                        Console.WriteLine("  (No tasks yet)\n");
                    }
                    else
                    {
                        for (int i = 0; i < myList.Count; i++)
                        {
                            Console.WriteLine($"  Task #{i + 1}: {myList[i]}");
                        }
                        Console.WriteLine();
                    }
                    break;

                case 3:
                    Console.WriteLine("\n Delete Task selected");
                    if (myList.Count == 0)
                    {
                        Console.WriteLine("  (No tasks to delete)\n");
                        break;
                    }

                    for (int i = 0; i < myList.Count; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {myList[i]}");
                    }

                    Console.Write("\nEnter task number to delete: ");
                    string input = Console.ReadLine();

                    try
                    {
                        int taskNumber = Convert.ToInt32(input);
                        if (taskNumber >= 1 && taskNumber <= myList.Count)
                        {
                            Console.WriteLine($"❌ Deleted: \"{myList[taskNumber - 1]}\"\n");
                            myList.RemoveAt(taskNumber - 1);
                            SaveTasks(myList); 
                        }
                        else
                        {
                            Console.WriteLine(" Invalid task number.\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine(" Please enter a valid number.\n");
                    }
                    break;

                case 4:
                    Console.WriteLine("\n Exiting Task Manager...");
                    closeProgram = true;
                    break;
            }
        }
    }

    static List<string> LoadTasks()
    {
        List<string> tasks = new List<string>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            tasks.AddRange(lines);
        }

        return tasks;
    }

    static void SaveTasks(List<string> tasks)
    {
        File.WriteAllLines(filePath, tasks);
    }

    static int MainPage()
    {
        Console.WriteLine("----------------------");
        Console.WriteLine("\tMENU:");
        Console.WriteLine("  1 - Add Task");
        Console.WriteLine("  2 - Show All Tasks");
        Console.WriteLine("  3 - Delete Task");
        Console.WriteLine("  4 - Exit");
        Console.Write("Choose an option (1-4): ");
        string answer = Console.ReadLine();

        try
        {
            int numAnswer = Convert.ToInt32(answer);
            if (numAnswer >= 1 && numAnswer <= 4)
                return numAnswer;

            Console.WriteLine("Only numbers 1 to 4 allowed.\n");
            return MainPage();
        }
        catch
        {
            Console.WriteLine("Please enter a valid number.\n");
            return MainPage();
        }
    }
}
