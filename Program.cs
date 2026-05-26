using ShuntingYard.Core;

namespace ShuntingYard.Entry;

internal static class Program
{
    private static void Main(string[]? args)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Welcome to the Shunting-Yard train station!");
        Console.ResetColor();

        if (args != null && args.Length > 0)
        {
            string commandLineInput = string.Join("", args);
            Console.WriteLine($"\nProcessing arguments: {commandLineInput}");
            HandleInput(commandLineInput);
            Console.WriteLine("\nExiting program..");
            return;
        }

        while (true)
        {
            Console.Write("\nEnter an expression ('e' to exit): ");
            string input = Console.ReadLine() ?? string.Empty;

            int exitCode = HandleInput(input);
            if (exitCode == 0 && input.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
        }

        Console.WriteLine("Exiting program..");
    }

    private static int HandleInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No expression entered!");
            return 1;
        }

        string cleanInput = input.Trim();
        if (cleanInput.Equals("e", StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        // Intercept: Define Variable [dv name value] or [dv name]
        if (cleanInput.StartsWith("dv ", StringComparison.OrdinalIgnoreCase))
        {
            string[] parts = cleanInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                string varName = parts[1];
                double varValue = 0;

                if (parts.Length >= 3 && double.TryParse(parts[2], out double parsedVal))
                {
                    varValue = parsedVal;
                }

                VariableRegistry.Set(varName, varValue);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Variable saved: {varName} = {varValue}");
                Console.ResetColor();
                return 0;
            }
        }

        // Intercept: Delete [del name]
        if (cleanInput.StartsWith("del ", StringComparison.OrdinalIgnoreCase))
        {
            string[] parts = cleanInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                string varName = parts[1];
                
                if (VariableRegistry.Contains(varName))
                {
                    VariableRegistry.Delete(varName);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Variable '{varName}' has been deleted.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"ℹVariable '{varName}' does not exist.");
                }
                Console.ResetColor();
                return 0;
            }
        }

        // Standard Math Flow
        Equation equation = new(cleanInput);
        var (finalAnswer, feedback) = equation.Evaluate();

        if (double.IsNaN(finalAnswer))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The program has caught errors: {feedback}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Result = {finalAnswer}");
        }
        Console.ResetColor();

        return 0;
    }
}