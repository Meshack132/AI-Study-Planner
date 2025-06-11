using AI_Study_Planner.Models;
using AI_Study_Planner.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AI_Study_Planner.Services
{
    public static class ValidationService
    {
        public static bool ValidateTaskInput(string topic, string duration, string difficulty, string deadline, out StudyTask task)
        {
            task = new StudyTask();
            var errors = new List<string>();

            
            if (string.IsNullOrWhiteSpace(topic))
                errors.Add("Topic cannot be empty");

            
            if (!int.TryParse(duration, out int hours) || hours <= 0 || hours > 24)
                errors.Add("Duration must be a number between 1-24 hours");

            
            if (!int.TryParse(difficulty, out int diff) || diff < 1 || diff > 5)
                errors.Add("Difficulty must be a number between 1-5");

            
            if (!DateTime.TryParseExact(deadline, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                errors.Add("Deadline must be in YYYY-MM-DD format");
            else if (date < DateTime.Today)
                errors.Add("Deadline cannot be in the past");

            if (errors.Count > 0)
            {
                ConsoleHelper.ShowError("Validation errors:");
                foreach (var error in errors) Console.WriteLine($"- {error}");
                return false;
            }

            task.Topic = topic;
            task.DurationHours = hours;
            task.Difficulty = diff;
            task.Deadline = date;
            return true;
        }
    }
}