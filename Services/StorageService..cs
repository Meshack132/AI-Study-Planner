using AI_Study_Planner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AI_Study_Planner.Services
{
    public static class StorageService
    {
        private const string TasksFile = "tasks.json";
        private const string ConfigFile = "userConfig.json";

        public static List<StudyTask> LoadTasks()
        {
            try
            {
                if (File.Exists(TasksFile))
                {
                    var json = File.ReadAllText(TasksFile);
                    return JsonSerializer.Deserialize<List<StudyTask>>(json) ?? new List<StudyTask>();
                }
            }
            catch { /* Suppress errors for new users */ }
            return new List<StudyTask>();
        }

        public static void SaveTasks(List<StudyTask> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(TasksFile, JsonSerializer.Serialize(tasks, options));
        }

        public static UserConfig LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigFile))
                {
                    var json = File.ReadAllText(ConfigFile);
                    return JsonSerializer.Deserialize<UserConfig>(json) ?? new UserConfig();
                }
            }
            catch { /* Use default config on error */ }
            return new UserConfig();
        }

        public static void SaveConfig(UserConfig config)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(ConfigFile, JsonSerializer.Serialize(config, options));
        }
    }
}