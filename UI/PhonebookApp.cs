using System;
using System.Collections.Generic;
using System.Linq;
using AseanPhonebook.Models;
using AseanPhonebook.Services;

namespace AseanPhonebook.UI;

public sealed class PhonebookApp
{
    private const int AllCountriesChoice = 6;

    private readonly Phonebook _phonebook;
    private readonly ConsoleInput _input;

    public PhonebookApp(Phonebook phonebook, ConsoleInput input)
    {
        _phonebook = phonebook;
        _input = input;
    }

    //main menu

    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");

            string choice = _input.ReadLine("Enter choice: ");
            switch (choice)
            {
                case "1":
                    StoreEntries();
                    break;
                case "2":
                    EditEntry();
                    break;
                case "3":
                    SearchByCountry();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose 1, 2, 3 or 4.");
                    break;
            }
        }
    }

    //  store

    private void StoreEntries()
    {
        bool another;
        do
        {
            Console.WriteLine();
            Student student = ReadNewStudent();
            _phonebook.Add(student);
            Console.WriteLine($"Saved {student.FirstName} {student.Surname} ({student.StudentNumber}).");

            another = _input.ReadYesNo("Do you want to enter another entry [Y/N]? ");
        }
        while (another);
    }

    private Student ReadNewStudent()
    {
        string studentNumber =
            _input.ReadStudentNumber("Enter student number: ", _phonebook.Contains);
        string surname = _input.ReadRequired("Enter surname: ");
        string firstName = _input.ReadRequired("Enter first name: ");
        string occupation = _input.ReadRequired("Enter occupation: ");
        Gender gender = _input.ReadGender("Enter gender (M for male, F for female): ");
        AseanCountry country = _input.ReadCountry("Enter country code: ");
        string areaCode = _input.ReadDigits("Enter area code: ", "area code");
        string number = _input.ReadDigits("Enter number: ", "phone number");

        return new Student(
            studentNumber, surname, firstName, occupation, gender,
            new PhoneNumber(country, areaCode, number));
    }

    //  edit

    private void EditEntry()
    {
        Console.WriteLine();
        string studentNumber = _input.ReadRequired("Enter student number: ");
        Student? student = _phonebook.Find(studentNumber);
        if (student is null)
        {
            Console.WriteLine($"Error: student number {studentNumber} does not exist.");
            return;
        }

        bool editing = true;
        while (editing)
        {
            Console.WriteLine();
            Console.WriteLine($"Here is the existing information about {student.StudentNumber}:");
            Console.WriteLine(student.ToEditSummary());
            Console.WriteLine();
            Console.WriteLine("Which of the following information do you wish to change?");
            Console.WriteLine("[1] Student number [2] Surname [3] Gender [4] Occupation");
            Console.WriteLine("[5] Country code [6] Area code [7] Phone number");
            Console.WriteLine("[8] None – Go back to main menu");

            int choice = _input.ReadChoice("Enter choice: ", 1, 8);
            switch (choice)
            {
                case 1:
                    string newNumber = _input.ReadStudentNumber(
                        "Enter new student number: ",
                        n => _phonebook.Find(n) is { } other && !ReferenceEquals(other, student));
                    _phonebook.TryChangeStudentNumber(student, newNumber);
                    break;
                case 2:
                    student.Surname = _input.ReadRequired("Enter new surname: ");
                    break;
                case 3:
                    student.Gender = _input.ReadGender("Enter new gender (M for male, F for female): ");
                    break;
                case 4:
                    student.Occupation = _input.ReadRequired("Enter new occupation: ");
                    break;
                case 5:
                    student.Phone.Country = _input.ReadCountry("Enter new country code: ");
                    break;
                case 6:
                    student.Phone.AreaCode = _input.ReadDigits("Enter new area code: ", "area code");
                    break;
                case 7:
                    student.Phone.Number = _input.ReadDigits("Enter new phone number: ", "phone number");
                    break;
                case 8:
                    editing = false;
                    break;
            }
        }
    }

    //  search

    private void SearchByCountry()
    {
        Console.WriteLine();
        Console.WriteLine("From which country:");
        Console.WriteLine("[1] Philippines [2] Thailand [3] Singapore [4] Indonesia");
        Console.WriteLine("[5] Malaysia [6] ALL [0] No More");

        List<AseanCountry> selected = ReadCountryChoices(out bool all);

        IReadOnlyList<Student> results;
        string heading;
        if (all)
        {
            results = _phonebook.GetAll();
            heading = "Here are the students from all countries:";
        }
        else if (selected.Count == 0)
        {
            Console.WriteLine("No country selected.");
            return;
        }
        else
        {
            results = _phonebook.SearchByCountries(selected);
            heading = $"Here are the students from {JoinNames(selected)}:";
        }

        Console.WriteLine(heading);
        if (results.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        foreach (Student student in results)
        {
            Console.WriteLine(student.ToSearchLine());
        }
    }
    private List<AseanCountry> ReadCountryChoices(out bool all)
    {
        // 1-5 selects a country, 6 selects ALL, and 0/N finishes selection.
        // This also allows the user to enter 1, then 6 to search ALL countries.
        List<AseanCountry> selected = new();
        all = false;

        while (true)
        {
            string prompt = selected.Count == 0
                ? "Enter choice: "
                : "Enter another choice (1-5, 6=ALL, 0/N=finish, Y=continue): ";

            string input = _input.ReadLine(prompt);

            if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                return selected;
            }

            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Select another country.");
                continue;
            }

            if (!int.TryParse(input, out int choice) || choice < 0 || choice > AllCountriesChoice)
            {
                Console.WriteLine("Invalid choice. Enter 1-6, 0, Y, or N.");
                continue;
            }

            if (choice == 0)
            {
                return selected;
            }

            if (choice == AllCountriesChoice)
            {
                all = true;
                return selected;
            }

            AseanCountry country = AseanCountry.All[choice - 1];
            if (selected.Contains(country))
            {
                Console.WriteLine($"{country.Name} is already selected.");
                continue;
            }

            selected.Add(country);
            Console.WriteLine($"{country.Name} selected.");
        }
    }

    private static string JoinNames(IReadOnlyList<AseanCountry> countries)
    {
        List<string> names = countries.Select(c => c.Name).ToList();
        if (names.Count == 1)
        {
            return names[0];
        }

        return string.Join(", ", names.Take(names.Count - 1)) + " and " + names[^1];
    }
}
