using AI_Study_Planner.Models;
using AI_Study_Planner.Strategies;
using AI_Study_Planner.Utilities;
using System;
using System.Collections.Generic;

namespace AI_Study_Planner.Strategies
{
    public class StandardStrategy : IScheduleStrategy
    
    {
        private readonly UserConfig _config;

        public StandardStrategy(UserConfig config)
        {
            _config = config;
        }

        public bool IsPriority(string topic)
        {
            return _config.PrioritySubjects.Contains(topic);
        }
        public void Schedule(List<StudyTask> tasks)
        {
            ConsoleHelper.ShowHeader("📅 Standard Schedule");
            int dailyHours = 0;
            int streak = 0;

            foreach (var task in tasks)
            {
                // Check daily limit
                if (dailyHours + task.DurationHours > _config.MaxHoursPerDay)
                {
                    ConsoleHelper.ShowWarning($"! Daily limit reached ({_config.MaxHoursPerDay}h)");
                    dailyHours = 0;
                    streak = 0;
                }

                dailyHours += task.DurationHours;
                streak++;

                Console.WriteLine($"» [{task.Deadline:ddd}] {_config.PreferredStudyTime} " +
                                  $"{task.Topic} ({task.DurationHours}h)");

                // Priority warning
                if (_config.PrioritySubjects.Contains(task.Topic))
                    Console.WriteLine("   [PRIORITY] - Focus on this!");
            }

            Console.WriteLine($"\n🔥 Streak: {streak} days | Max hours/day: {_config.MaxHoursPerDay}h");
        }
    }
}
