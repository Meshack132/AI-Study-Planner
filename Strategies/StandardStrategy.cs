using AI_Study_Planner.Models;
using AI_Study_Planner.Strategies;
using System;
using System.Collections.Generic;

namespace AI_Study_Planner.Strategies
{
    public class StandardStrategy : IScheduleStrategy
    {
        public void Schedule(List<StudyTask> tasks)
        {
            Console.WriteLine("\n📅 Standard Schedule Generated");
            Console.WriteLine("----------------------------");
            int energy = 100;
            int streak = 0;

            foreach (var task in tasks)
            {
                energy -= task.Difficulty * 10;
                streak++;

                Console.WriteLine($"» [{task.Deadline:ddd}] {task.Topic} " +
                                $"({task.DurationHours}h) - Energy: {energy}%");

                if (energy < 30)
                {
                    Console.WriteLine("   [BREAK] - Low energy recharge");
                    energy += 50;
                    streak = 0;
                }
            }

            Console.WriteLine($"\n🔥 Current Streak: {streak} days");
        }
    }
}