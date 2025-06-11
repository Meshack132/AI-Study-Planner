using AI_Study_Planner.Models;
using AI_Study_Planner.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Study_Planner.Services
{
    public class StudyScheduler
    {
        private readonly IScheduleStrategy _strategy;
        private int _fatigueLevel;
        private int _streak;

        public StudyScheduler(IScheduleStrategy strategy)
        {
            _strategy = strategy;
            _fatigueLevel = 0;
            _streak = 0;
        }

        public void GenerateSchedule(List<StudyTask> tasks)
        {
            tasks.Sort((a, b) => a.Deadline.CompareTo(b.Deadline));
            _strategy.Schedule(tasks);
            UpdateFatigue(tasks);
        }

        private void UpdateFatigue(List<StudyTask> tasks)
        {
            foreach (var task in tasks)
            {
                _fatigueLevel += task.Difficulty * 3;
                _streak++;
            }

            Console.WriteLine($"😴 Fatigue Level: {_fatigueLevel}/100");
            Console.WriteLine($"🔥 Current Streak: {_streak} days\n");
        }
    }
}