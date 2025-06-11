using AI_Study_Planner.Factories;
using AI_Study_Planner.Models;
using AI_Study_Planner.Services;
using AI_Study_Planner.Strategies;
using AI_Study_Planner.Utilities;
using System;
using System.Collections.Generic;

namespace AI_Study_Planner
{
    class Program
    {
        private static List<StudyTask> tasks;
        private static UserConfig userConfig;

        static void Main(string[] args)
        {
            ConsoleHelper.ShowHeader("🧠 AI Study Planner v2.0");

            // Load data
            tasks = StorageService.LoadTasks();
            userConfig = StorageService.LoadConfig();

            // Main menu
            while (true)
            {
                Console.WriteLine("\nMAIN MENU");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Generate Schedule");
                Console.WriteLine("3. View/Edit Tasks");
                Console.WriteLine("4. Export Plan");
                Console.WriteLine("5. User Settings");
                Console.WriteLine("6. Save & Exit");
                Console.Write(">> Select option: ");

                switch (Console.ReadLine())
                {
                    case "1": AddTask(); break;
                    case "2": GenerateSchedule(); break;
                    case "3": ViewEditTasks(); break;
                    case "4": ExportPlan(); break;
                    case "5": UserSettings(); break;
                    case "6": SaveAndExit(); return;
                    default: ConsoleHelper.ShowError("Invalid option"); break;
                }
            }
        }

        static void AddTask()
        {
            ConsoleHelper.ShowHeader("Add New Task");

            string topic = GetInput("Topic: ", "Cannot be empty");
            string duration = GetInput("Duration (hours): ", "1-24");
            string difficulty = GetInput("Difficulty (1-5): ", "1-5");
            string deadline = GetInput("Deadline (yyyy-mm-dd): ", DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"));

            if (ValidationService.ValidateTaskInput(topic, duration, difficulty, deadline, out StudyTask task))
            {
                tasks.Add(task);
                ConsoleHelper.ShowSuccess("✓ Task added!");
            }
        }

        static void GenerateSchedule()
        {
            if (tasks.Count == 0)
            {
                ConsoleHelper.ShowError("No tasks to schedule!");
                return;
            }

            ConsoleHelper.ShowHeader("Generate Schedule");
            Console.WriteLine("1. Standard Schedule");
            Console.WriteLine("2. Pomodoro Schedule");
            Console.Write(">> Choose schedule type: ");

            IScheduleStrategy strategy = Console.ReadLine() == "2"
                ? new PomodoroStrategy(userConfig)
                : new StandardStrategy(userConfig);

            var scheduler = new StudyScheduler(strategy);
            scheduler.GenerateSchedule(tasks);
        }

        static void ViewEditTasks()
        {
            ConsoleHelper.ShowHeader("Your Study Tasks");

            if (tasks.Count == 0)
            {
                ConsoleHelper.ShowWarning("No tasks yet. Add some first!");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                Console.WriteLine($"{i + 1}. [{task.Deadline:MMM dd}] {task.Topic} " +
                                 $"({task.DurationHours}h, Difficulty: {task.Difficulty}/5)");
            }

            Console.Write("\nEnter task number to edit/delete (0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
            {
                Console.WriteLine("\n1. Edit\n2. Delete");
                Console.Write(">> Action: ");

                if (Console.ReadLine() == "1")
                {
                    // Edit task logic
                    ConsoleHelper.ShowSuccess("Task editing not implemented yet");
                }
                else
                {
                    tasks.RemoveAt(index - 1);
                    ConsoleHelper.ShowSuccess("✓ Task deleted!");
                }
            }
        }

        static void ExportPlan()
        {
            if (tasks.Count == 0)
            {
                ConsoleHelper.ShowError("No tasks to export!");
                return;
            }

            ConsoleHelper.ShowHeader("Export Plan");
            Console.WriteLine("1. Markdown");
            Console.WriteLine("2. Calendar (ICS)");
            Console.WriteLine("3. Weekly Report");
            Console.WriteLine("4. Daily Report");
            Console.Write(">> Select format: ");

            switch (Console.ReadLine())
            {
                case "1": ExportService.ExportMarkdown(tasks); break;
                case "2": ExportService.ExportCalendar(tasks); break;
                case "3": ExportService.ExportWeeklyReport(tasks, userConfig); break;
                case "4": ExportService.ExportDailyReport(tasks, userConfig); break;
                default: ConsoleHelper.ShowError("Invalid option"); break;
            }
        }

        static void UserSettings()
        {
            ConsoleHelper.ShowHeader("User Settings");

            Console.WriteLine($"1. Priority Subjects: {string.Join(", ", userConfig.PrioritySubjects)}");
            Console.WriteLine($"2. Max Hours/Day: {userConfig.MaxHoursPerDay}");
            Console.WriteLine($"3. Preferred Time: {userConfig.PreferredStudyTime}");
            Console.WriteLine($"4. Pomodoro Duration: {userConfig.PomodoroDuration} min");
            Console.WriteLine($"5. Break Duration: {userConfig.BreakDuration} min");
            Console.Write("\n>> Setting to change (0 to cancel): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= 5)
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter priority subjects (comma separated): ");
                        userConfig.PrioritySubjects = new List<string>(
                            Console.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries));
                        break;
                    case 2:
                        Console.Write("Max hours per day: ");
                        if (int.TryParse(Console.ReadLine(), out int hours) && hours > 0 && hours <= 12)
                            userConfig.MaxHoursPerDay = hours;
                        else
                            ConsoleHelper.ShowError("Invalid value (1-12)");
                        break;
                    case 3:
                        Console.Write("Preferred time (morning/afternoon/evening): ");
                        var time = Console.ReadLine().ToLower();
                        if (new[] { "morning", "afternoon", "evening" }.Contains(time))
                            userConfig.PreferredStudyTime = time;
                        else
                            ConsoleHelper.ShowError("Invalid time option");
                        break;
                    case 4:
                        Console.Write("Pomodoro duration (minutes): ");
                        if (int.TryParse(Console.ReadLine(), out int pomo) && pomo > 0 && pomo <= 60)
                            userConfig.PomodoroDuration = pomo;
                        else
                            ConsoleHelper.ShowError("Invalid value (1-60)");
                        break;
                    case 5:
                        Console.Write("Break duration (minutes): ");
                        if (int.TryParse(Console.ReadLine(), out int brk) && brk > 0 && brk <= 30)
                            userConfig.BreakDuration = brk;
                        else
                            ConsoleHelper.ShowError("Invalid value (1-30)");
                        break;
                }
                StorageService.SaveConfig(userConfig);
                ConsoleHelper.ShowSuccess("✓ Settings saved!");
            }
        }

        static void SaveAndExit()
        {
            StorageService.SaveTasks(tasks);
            ConsoleHelper.ShowSuccess("\nYour study plan has been saved. Good luck! 💻🚀");
        }

        static string GetInput(string prompt, string defaultValue = "")
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }
    }
}