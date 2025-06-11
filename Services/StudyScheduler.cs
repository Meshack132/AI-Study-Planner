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

        public StudyScheduler(IScheduleStrategy strategy)
        {
            _strategy = strategy;
        }

        public void GenerateSchedule(List<StudyTask> tasks)
        {
            tasks.Sort((a, b) =>
            {
                int dateComp = a.Deadline.CompareTo(b.Deadline);
                if (dateComp != 0) return dateComp;

                // Use the strategy's IsPriority method
                bool aPriority = _strategy.IsPriority(a.Topic);
                bool bPriority = _strategy.IsPriority(b.Topic);

                // Higher priority comes first
                return bPriority.CompareTo(aPriority);
            });

            _strategy.Schedule(tasks);
        }
    }
}