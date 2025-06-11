using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Study_Planner.Models;
using AI_Study_Planner.Utilities;

namespace AI_Study_Planner.Strategies
{
    public class PomodoroStrategy : IScheduleStrategy
    {
        private readonly UserConfig _config;

        public PomodoroStrategy(UserConfig config)
        {
            _config = config;
        }

        public bool IsPriority(string topic)
        {
            return _config.PrioritySubjects.Contains(topic);
        }
        public void Schedule(List<StudyTask> tasks)
        {
            ConsoleHelper.ShowHeader("⏱️ Pomodoro Schedule");
            int dailyPomodoros = 0;
            int maxDailyPomodoros = _config.MaxHoursPerDay * 60 / _config.PomodoroDuration;
            int streak = 0;

            foreach (var task in tasks)
            {
                int pomodoros = (int)Math.Ceiling(task.DurationHours * 60.0 / _config.PomodoroDuration);

              
                if (dailyPomodoros + pomodoros > maxDailyPomodoros)
                {
                    ConsoleHelper.ShowWarning($"! Daily limit reached ({maxDailyPomodoros} pomodoros)");
                    dailyPomodoros = 0;
                    streak = 0;
                }

                dailyPomodoros += pomodoros;
                streak++;

                Console.WriteLine($"» [{task.Deadline:ddd}] {_config.PreferredStudyTime} " +
                                  $"{task.Topic} ({pomodoros} × {_config.PomodoroDuration}min)");

                
                if (pomodoros > 1)
                {
                    Console.WriteLine($"   Includes {pomodoros - 1} breaks " +
                                     $"({_config.BreakDuration}min each)");
                }
            }

            Console.WriteLine($"\n🔥 Streak: {streak} days | " +
                             $"Pomodoro: {_config.PomodoroDuration}min | " +
                             $"Break: {_config.BreakDuration}min");
        }
    }
}