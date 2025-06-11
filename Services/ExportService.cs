using AI_Study_Planner.Models;
using AI_Study_Planner.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AI_Study_Planner.Services
{
    public static class ExportService
    {
        public static void ExportMarkdown(List<StudyTask> tasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# 📚 Study Plan\n");

            // Group by deadline
            var grouped = tasks.GroupBy(t => t.Deadline.Date)
                              .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                sb.AppendLine($"## {group.Key:yyyy-MM-dd} ({group.Key:dddd})");
                sb.AppendLine("| Topic | Duration | Difficulty |");
                sb.AppendLine("|-------|----------|------------|");

                foreach (var task in group)
                {
                    sb.AppendLine($"| {task.Topic} | {task.DurationHours}h | " +
                                 $"{new string('★', task.Difficulty)} |");
                }
                sb.AppendLine();
            }

            File.WriteAllText("study_plan.md", sb.ToString());
            ConsoleHelper.ShowSuccess("✓ Exported to study_plan.md");
        }

        public static void ExportCalendar(List<StudyTask> tasks)
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
            ConsoleHelper.ShowSuccess("✓ Exported to study_calendar.ics");
        }

        public static void ExportWeeklyReport(List<StudyTask> tasks, UserConfig config)
        {
            var startDate = tasks.Min(t => t.Deadline);
            var endDate = tasks.Max(t => t.Deadline);
            var sb = new StringBuilder();

            sb.AppendLine("# 📅 Weekly Study Report\n");
            sb.AppendLine($"## Period: {startDate:MMM dd} - {endDate:MMM dd}\n");

            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                var dailyTasks = tasks.Where(t => t.Deadline.Date == date).ToList();
                if (dailyTasks.Count == 0) continue;

                sb.AppendLine($"### {date:dddd, MMM dd}");
                sb.AppendLine("| Time | Topic | Duration |");
                sb.AppendLine("|------|-------|----------|");

                int hour = config.PreferredStudyTime == "morning" ? 9 :
                          config.PreferredStudyTime == "afternoon" ? 13 : 18;

                foreach (var task in dailyTasks)
                {
                    sb.AppendLine($"| {hour}:00 | {task.Topic} | {task.DurationHours}h |");
                    hour += task.DurationHours;
                }
                sb.AppendLine();
            }

            File.WriteAllText("weekly_report.md", sb.ToString());
            ConsoleHelper.ShowSuccess("✓ Exported to weekly_report.md");
        }

        public static void ExportDailyReport(List<StudyTask> tasks, UserConfig config)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# 📝 Daily Study Plan\n");

            // Get today's tasks
            var dailyTasks = tasks.Where(t => t.Deadline.Date == DateTime.Today).ToList();

            if (dailyTasks.Count == 0)
            {
                sb.AppendLine("No tasks scheduled for today!");
            }
            else
            {
                sb.AppendLine($"## {DateTime.Today:dddd, MMM dd}");
                sb.AppendLine("| Time | Topic | Duration | Priority |");
                sb.AppendLine("|------|-------|----------|----------|");

                int hour = config.PreferredStudyTime == "morning" ? 9 :
                          config.PreferredStudyTime == "afternoon" ? 13 : 18;

                foreach (var task in dailyTasks)
                {
                    string priority = config.PrioritySubjects.Contains(task.Topic) ? "⭐" : "";
                    sb.AppendLine($"| {hour}:00 | {task.Topic} | {task.DurationHours}h | {priority} |");
                    hour += task.DurationHours;
                }

                sb.AppendLine($"\n**Total hours**: {dailyTasks.Sum(t => t.DurationHours)}h");
                sb.AppendLine($"**Priority subjects**: {string.Join(", ", config.PrioritySubjects)}");
            }

            File.WriteAllText("daily_plan.md", sb.ToString());
            ConsoleHelper.ShowSuccess("✓ Exported to daily_plan.md");
        }
    }
}