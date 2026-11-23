using System;
using System.Text;
using AseanPhonebook.Services;
using AseanPhonebook.UI;

namespace AseanPhonebook;

internal static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== ASEAN Phonebook ===");

        try
        {
            var app = new PhonebookApp(
                new Phonebook(),
                new ConsoleInput());
            app.Run();
        }
        catch (EndOfInputException)
        {
            Console.WriteLine();
        }

        Console.WriteLine("Goodbye!");
    }
}
