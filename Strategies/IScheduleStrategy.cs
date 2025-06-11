using AI_Study_Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Study_Planner.Strategies
{
    public interface IScheduleStrategy
    {
       
            void Schedule(List<StudyTask> tasks);
            bool IsPriority(string topic);  // Add this method
        }
    }