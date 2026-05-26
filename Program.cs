using System;
using ShuntingYard.Core;

namespace ShuntingYard.Entry;

internal static class Program
{
    private static void Main(string[]? args)
    {
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
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("No expression entered!");
            return 1;
        }
        else if (input.Equals("e", StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        Equation equation = new(input);

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