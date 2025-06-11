using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AI_Study_Planner.Utilities
{
    public static class ConsoleHelper
    {
        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowHeader(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{message}");
            Console.WriteLine(new string('=', message.Length));
            Console.ResetColor();
        }

        public static string ReadWithDefault(string prompt, string defaultValue)
        {
            Console.Write($"{prompt} [{defaultValue}]: ");
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }
    }
}