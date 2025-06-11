using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Study_Planner.Models;

namespace AI_Study_Planner.Factories
{
    public class TaskFactory
    {
        public StudyTask CreateTask()
        {
            Console.WriteLine("\n📝 New Study Task");
            var task = new StudyTask();

            Console.Write("Topic: ");
            task.Topic = Console.ReadLine();

            Console.Write("Duration (hours): ");
            task.DurationHours = int.Parse(Console.ReadLine());

            Console.Write("Difficulty (1-5): ");
            task.Difficulty = Math.Clamp(int.Parse(Console.ReadLine()), 1, 5);

            Console.Write("Deadline (yyyy-mm-dd): ");
            task.Deadline = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("✓ Task added!\n");
            return task;
        }
    }
}
