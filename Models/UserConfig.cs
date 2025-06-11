using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Study_Planner.Models
{
    public class UserConfig
    {
        public List<string> PrioritySubjects { get; set; } = new List<string>();
        public int MaxHoursPerDay { get; set; } = 4;
        public string PreferredStudyTime { get; set; } = "morning";
        public int PomodoroDuration { get; set; } = 25;
        public int BreakDuration { get; set; } = 5;
    }
}
