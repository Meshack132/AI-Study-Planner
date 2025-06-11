using AI_Study_Planner.Factories;
using AI_Study_Planner.Models;
using AI_Study_Planner.Services;
using AI_Study_Planner.Strategies;
using AIStudyPlanner.Services;
using System;
using System.Collections.Generic;

namespace AI_Study_Planner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AI Study Planner v1.0");
            Console.WriteLine("========================\n");

            // Use fully qualified name to resolve ambiguity
            var taskFactory = new Factories.TaskFactory();
            var tasks = new List<StudyTask>();

            while (true)
            {
                Console.WriteLine("\n1. Add Task\n2. Generate Schedule\n3. Export Plan\n4. Exit");
                Console.Write(">> Select option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        tasks.Add(taskFactory.CreateTask());
                        break;

                    case "2":
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("! No tasks added yet!");
                            break;
                        }

                        Console.Write("\nChoose schedule type (1=Standard 2=Pomodoro): ");
                        IScheduleStrategy strategy = Console.ReadLine() == "2"
                            ? (IScheduleStrategy)new PomodoroStrategy()  // Explicit interface cast
                            : new StandardStrategy();

                        var scheduler = new StudyScheduler(strategy);
                        scheduler.GenerateSchedule(tasks);
                        break;

                    case "3":
                        ExportService.ExportPlan(tasks);
                        break;

                    case "4":
                        Console.WriteLine("\nHappy studying! ");
                        return;

                    default:
                        Console.WriteLine("! Invalid option");
                        break;
                }
            }
        }
    }
}