
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AI_Study_Planner.Models;

namespace AIStudyPlanner.Services
{
    public static class ExportService
    {
        public static void ExportPlan(List<StudyTask> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("! No tasks to export");
                return;
            }

            Console.Write("\nExport as (1=Markdown 2=Calendar): ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                ExportMarkdown(tasks);
            }
            else
            {
                ExportCalendar(tasks);
            }
        }

        private static void ExportMarkdown(List<StudyTask> tasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# Study Plan\n");
            sb.AppendLine("| Topic | Duration | Difficulty | Deadline |");
            sb.AppendLine("|-------|----------|------------|----------|");

            foreach (var task in tasks)
            {
                sb.AppendLine($"| {task.Topic} | {task.DurationHours}h | " +
                             $"{new string('★', task.Difficulty)} | {task.Deadline:yyyy-MM-dd} |");
            }

            File.WriteAllText("study_plan.md", sb.ToString());
            Console.WriteLine("✓ Exported to study_plan.md");
        }

        private static void ExportCalendar(List<StudyTask> tasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//StudyPlanner//EN");

            foreach (var task in tasks)
            {
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine($"SUMMARY:{task.Topic}");
                sb.AppendLine($"DTSTART:{task.Deadline:yyyyMMdd}T090000");
                sb.AppendLine($"DTEND:{task.Deadline:yyyyMMdd}T{90000 + task.DurationHours * 10000}");
                sb.AppendLine($"DESCRIPTION:Duration: {task.DurationHours}h | Difficulty: {task.Difficulty}/5");
                sb.AppendLine("END:VEVENT");
            }

            sb.AppendLine("END:VCALENDAR");
            File.WriteAllText("study_calendar.ics", sb.ToString());
            Console.WriteLine("✓ Exported to study_calendar.ics");
        }
    }
}