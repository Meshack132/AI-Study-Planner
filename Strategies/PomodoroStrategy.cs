using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Study_Planner.Models;

namespace AI_Study_Planner.Strategies
{ 

    public class PomodoroStrategy : IScheduleStrategy
    {
        public void Schedule(List<StudyTask> tasks)
        {
            Console.WriteLine("\n⏱️ Pomodoro Schedule Generated");
            Console.WriteLine("---------------------------");
            int energy = 100;
            int streak = 0;

            foreach (var task in tasks)
            {
                int pomodoros = (int)Math.Ceiling(task.DurationHours * 2.0);
                energy -= task.Difficulty * 8;
                streak++;

                Console.WriteLine($"» [{task.Deadline:ddd}] {task.Topic} " +
                                $"({pomodoros} pomodoros) - Energy: {energy}%");

                if (pomodoros > 4)
                {
                    Console.WriteLine("   [LONG SESSION] - Added extended break");
                    energy += 30;
                }

                if (energy < 40)
                {
                    Console.WriteLine("   [SHORT BREAK] - 5min recharge");
                    energy += 40;
                    streak = 0;
                }
            }

            Console.WriteLine($"\n🔥 Current Streak: {streak} days");
        }
    }
}