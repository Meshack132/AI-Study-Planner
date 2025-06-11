using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Study_Planner.Models
{
    public class StudyTask
    {
        public string Topic { get; set; }
        public int DurationHours { get; set; }
        public int Difficulty { get; set; }  
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
    }
}