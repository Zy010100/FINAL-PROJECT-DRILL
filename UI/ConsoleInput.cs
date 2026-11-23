using System;
using System.Linq;
using AseanPhonebook.Models;

namespace AseanPhonebook.UI;

public sealed class ConsoleInput
{
    public string ReadLine(string prompt)
    {
        Console.Write(prompt);
        string? line = Console.ReadLine();
        if (line is null)
        {
            throw new EndOfInputException();
        }

        return line.Trim();
    }

    public string ReadRequired(string prompt)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (text.Length > 0)
            {
                return text;
            }

            Console.WriteLine("Error: this field cannot be empty.");
        }
    }

    public string ReadStudentNumber(string prompt, Func<string, bool> isTaken)
    {
        while (true)
        {
            string number = ReadRequired(prompt);
            if (!isTaken(number))
            {
                return number;
            }

            Console.WriteLine($"Error: student number {number} already exists.");
        }
    }

    public Gender ReadGender(string prompt)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (text.Equals("M", StringComparison.OrdinalIgnoreCase))
            {
                return Gender.Male;
            }

            if (text.Equals("F", StringComparison.OrdinalIgnoreCase))
            {
                return Gender.Female;
            }

            Console.WriteLine("Error: enter M for male or F for female.");
        }
    }

    public AseanCountry ReadCountry(string prompt)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (AseanCountry.TryParseCode(text, out AseanCountry country))
            {
                return country;
            }

            Console.WriteLine(
                "Error: unsupported country code. Valid codes: " +
                string.Join(", ", AseanCountry.All.Select(c => $"{c.Code} ({c.Name})")) + ".");
        }
    }

    public string ReadDigits(string prompt, string fieldName)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (PhoneNumber.IsDigits(text))
            {
                return text;
            }

            Console.WriteLine($"Error: {fieldName} must contain digits only.");
        }
    }

    public bool ReadYesNo(string prompt)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (text.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (text.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            Console.WriteLine("Error: enter Y or N.");
        }
    }

    /// <summary>Reads a whole number between min and max (inclusive).</summary>
    public int ReadChoice(string prompt, int min, int max)
    {
        while (true)
        {
            string text = ReadLine(prompt);
            if (int.TryParse(text, out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }

            Console.WriteLine($"Error: enter a number from {min} to {max}.");
        }
    }
}
