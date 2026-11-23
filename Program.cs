
using System;
using System.Collections.Generic;
using System.Linq;

// CLASS 1: ASEAN COUNTRY
public class AseanCountry
{
    public string Name { get; set; }
    public string CountryCode { get; set; }

    public AseanCountry(string name, string countryCode)
    {
        Name = name;
        CountryCode = countryCode;
    }
}

// CLASS 2: COUNTRY CATALOG
public static class CountryCatalog
{
    public static readonly List<AseanCountry> Countries =
        new List<AseanCountry>
        {
            new AseanCountry("Malaysia", "60"),
            new AseanCountry("Indonesia", "62"),
            new AseanCountry("Philippines", "63"),
            new AseanCountry("Singapore", "65"),
            new AseanCountry("Thailand", "66")
        };

    // Order used in the country search menu
    public static readonly List<AseanCountry> SearchOrder =
        new List<AseanCountry>
        {
            Countries[2], // Philippines
            Countries[4], // Thailand
            Countries[3], // Singapore
            Countries[1], // Indonesia
            Countries[0]  // Malaysia
        };

    public static AseanCountry FindByCode(string code)
    {
        return Countries.FirstOrDefault(
            country => country.CountryCode == code);
    }
}

// CLASS 3: STUDENT
public class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public char Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(
        string studentNumber,
        string surname,
        string firstName,
        string occupation,
        char gender,
        string countryCode,
        string areaCode,
        string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    public string GetFullPhoneNumber()
    {
        return CountryCode + "-" + AreaCode + "-" + PhoneNumber;
    }

    public string GetGender()
    {
        return Gender == 'M' ? "Male" : "Female";
    }

    public void DisplayInformation()
    {
        Console.WriteLine(
            FirstName + " " + Surname +
            " is a " + Occupation + ".");

        Console.WriteLine("Student Number: " + StudentNumber);
        Console.WriteLine("Gender: " + GetGender());

        AseanCountry country = CountryCatalog.FindByCode(CountryCode);

        Console.WriteLine(
            "Country: " + (country != null ? country.Name : "Unknown"));

        Console.WriteLine(
            "Phone Number: " + GetFullPhoneNumber());
    }

    public void DisplaySearchResult()
    {
        AseanCountry country = CountryCatalog.FindByCode(CountryCode);

        Console.WriteLine(
            Surname + ", " + FirstName +
            ", with student number " + StudentNumber +
            ". Occupation: " + Occupation +
            ". Gender: " + GetGender() +
            ". Phone number: " + GetFullPhoneNumber() +
            " (" + (country != null ? country.Name : "Unknown") + ").");
    }
}

// CLASS 4: ASEAN PHONEBOOK
public class AseanPhonebook
{
    private List<Student> students = new List<Student>();

    // Add a student
    public bool AddStudent(Student student)
    {
        if (FindStudent(student.StudentNumber) != null)
        {
            return false;
        }

        students.Add(student);
        return true;
    }

    // Find student using student number
    public Student FindStudent(string studentNumber)
    {
        return students.FirstOrDefault(
            student => string.Equals(
                student.StudentNumber,
                studentNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    // Check whether a student number is already used
    public bool IsStudentNumberTaken(
        string studentNumber,
        Student excludedStudent = null)
    {
        return students.Any(student =>
            !ReferenceEquals(student, excludedStudent) &&
            string.Equals(
                student.StudentNumber,
                studentNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    // Search students by selected countries
    public List<Student> SearchByCountries(List<string> countryCodes)
    {
        HashSet<string> selectedCodes =
            new HashSet<string>(countryCodes);

        return students
            .Where(student => selectedCodes.Contains(student.CountryCode))
            .OrderBy(student => student.Surname, StringComparer.OrdinalIgnoreCase)
            .ThenBy(student => student.FirstName, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    // Get total number of entries
    public int GetStudentCount()
    {
        return students.Count;
    }
}

// MAIN PROGRAM
public class Program
{
    static AseanPhonebook phonebook = new AseanPhonebook();

    public static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("       ASEAN PHONEBOOK SYSTEM");
            Console.WriteLine("==================================");
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");
            Console.WriteLine("==================================");

            int choice = ReadInteger("Enter choice: ", 1, 4);

            switch (choice)
            {
                case 1:
                    StoreStudent();
                    break;

                case 2:
                    EditStudent();
                    break;

                case 3:
                    SearchPhonebook();
                    break;

                case 4:
                    running = false;
                    Console.WriteLine("\nThank you for using ASEAN Phonebook!");
                    Console.WriteLine("Goodbye!");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress ENTER to return to the main menu...");
                Console.ReadLine();
            }
        }
    }

    // STORE TO ASEAN PHONEBOOK
    static void StoreStudent()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("     STORE TO ASEAN PHONEBOOK");
        Console.WriteLine("==================================");

        bool addAnother = true;

        while (addAnother)
        {
            Console.WriteLine("\nEnter Student Information");
            Console.WriteLine("----------------------------------");

            string studentNumber;

            // Ensure student number is unique
            while (true)
            {
                studentNumber = ReadRequired("Enter student number: ");

                if (!phonebook.IsStudentNumberTaken(studentNumber))
                {
                    break;
                }

                Console.WriteLine(
                    "ERROR: Student number already exists. Try again.");
            }

            string surname = ReadRequired("Enter surname: ");

            string firstName = ReadRequired("Enter first name: ");

            string occupation = ReadRequired("Enter occupation: ");

            char gender = ReadGender();

            string countryCode = ReadCountryCode("Enter country code: ");

            string areaCode = ReadDigits("Enter area code: ");

            string phoneNumber = ReadDigits("Enter number: ");

            Student newStudent = new Student(
                studentNumber,
                surname,
                firstName,
                occupation,
                gender,
                countryCode,
                areaCode,
                phoneNumber
            );

            phonebook.AddStudent(newStudent);

            Console.WriteLine("\nStudent successfully added!");

            Console.WriteLine(
                "Total students in phonebook: " +
                phonebook.GetStudentCount());

            addAnother = ReadYesNo(
                "\nDo you want to enter another entry [Y/N]? ");
        }
    }

    // EDIT ENTRY IN ASEAN PHONEBOOK
    static void EditStudent()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("      EDIT ASEAN PHONEBOOK");
        Console.WriteLine("==================================");

        string studentNumber =
            ReadRequired("Enter student number: ");

        Student student = phonebook.FindStudent(studentNumber);

        if (student == null)
        {
            Console.WriteLine(
                "\nERROR: Student number does not exist.");
            return;
        }

        bool editing = true;

        while (editing)
        {
            Console.WriteLine("\nHere is the existing information about " +
                              student.StudentNumber + ":");

            Console.WriteLine("----------------------------------");

            student.DisplayInformation();

            Console.WriteLine("\nWhich of the following information do you wish to change?");
            Console.WriteLine("[1] Student number");
            Console.WriteLine("[2] Surname");
            Console.WriteLine("[3] Gender");
            Console.WriteLine("[4] Occupation");
            Console.WriteLine("[5] Country code");
            Console.WriteLine("[6] Area code");
            Console.WriteLine("[7] Phone number");
            Console.WriteLine("[8] None - Go back to main menu");

            Console.WriteLine("----------------------------------");

            int choice = ReadInteger("Enter choice: ", 1, 8);

            switch (choice)
            {
                case 1:
                    string newStudentNumber =
                        ReadRequired("Enter new student number: ");

                    if (phonebook.IsStudentNumberTaken(
                        newStudentNumber, student))
                    {
                        Console.WriteLine(
                            "ERROR: Student number already exists.");
                    }
                    else
                    {
                        student.StudentNumber = newStudentNumber;
                        Console.WriteLine(
                            "Student number successfully updated!");
                    }
                    break;

                case 2:
                    student.Surname =
                        ReadRequired("Enter new surname: ");

                    Console.WriteLine("Surname successfully updated!");
                    break;

                case 3:
                    student.Gender = ReadGender();
                    Console.WriteLine("Gender successfully updated!");
                    break;

                case 4:
                    student.Occupation =
                        ReadRequired("Enter new occupation: ");

                    Console.WriteLine("Occupation successfully updated!");
                    break;

                case 5:
                    student.CountryCode =
                        ReadCountryCode("Enter new country code: ");

                    Console.WriteLine("Country code successfully updated!");
                    break;

                case 6:
                    student.AreaCode =
                        ReadDigits("Enter new area code: ");

                    Console.WriteLine("Area code successfully updated!");
                    break;

                case 7:
                    student.PhoneNumber =
                        ReadDigits("Enter new phone number: ");

                    Console.WriteLine("Phone number successfully updated!");
                    break;

                case 8:
                    editing = false;
                    break;
            }
        }
    }

    // SEARCH ASEAN PHONEBOOK BY COUNTRY
    
static void SearchPhonebook()
{
    Console.Clear();

    Console.WriteLine("==================================");
    Console.WriteLine(" SEARCH ASEAN PHONEBOOK BY COUNTRY");
    Console.WriteLine("==================================");

    Console.WriteLine("From which country:");
    Console.WriteLine("[1] Philippines");
    Console.WriteLine("[2] Thailand");
    Console.WriteLine("[3] Singapore");
    Console.WriteLine("[4] Indonesia");
    Console.WriteLine("[5] Malaysia");
    Console.WriteLine("[6] ALL");
    Console.WriteLine("[0] No More");

    List<AseanCountry> selectedCountries =
        new List<AseanCountry>();

    bool allCountries = false;

    while (true)
    {
        int choice = ReadInteger("\nEnter choice: ", 0, 6);

        if (choice == 0)
        {
            break;
        }

        if (choice == 6)
        {
            allCountries = true;
            selectedCountries = new List<AseanCountry>(
                CountryCatalog.Countries);
            break;
        }

        AseanCountry selected =
            CountryCatalog.SearchOrder[choice - 1];

        bool alreadySelected = selectedCountries.Any(
            country => country.CountryCode == selected.CountryCode);

        if (alreadySelected)
        {
            Console.WriteLine(
                "This country has already been selected.");
            continue;
        }

        selectedCountries.Add(selected);

        Console.WriteLine(
            "\n" + selected.Name + " successfully selected!");

        // Ask whether the user wants another country
        bool addAnother = ReadYesNo(
            "Do you want to select another country [Y/N]? ");

        if (!addAnother)
        {
            break;
        }
    }

    if (selectedCountries.Count == 0)
    {
        Console.WriteLine("\nNo countries selected.");
        return;
    }

    Console.WriteLine("\n==================================");

    if (allCountries)
    {
        Console.WriteLine(
            "Here are all students from all countries:");
    }
    else
    {
        string countryNames =
            FormatCountryNames(selectedCountries);

        Console.WriteLine(
            "Here are the students from " + countryNames + ":");
    }

    Console.WriteLine("==================================");

    List<string> countryCodes = selectedCountries
        .Select(country => country.CountryCode)
        .ToList();

    List<Student> results =
        phonebook.SearchByCountries(countryCodes);

    if (results.Count == 0)
    {
        Console.WriteLine(
            "\nNo student records found for the selected countries.");
    }
    else
    {
        Console.WriteLine();

        foreach (Student student in results)
        {
            student.DisplaySearchResult();
            Console.WriteLine();
        }

        Console.WriteLine("----------------------------------");
        Console.WriteLine("Total students found: " + results.Count);
    }
}


    // FORMAT SELECTED COUNTRY NAMES
    static string FormatCountryNames(
        List<AseanCountry> countries)
    {
        List<string> names = countries
            .Select(country => country.Name)
            .ToList();

        if (names.Count == 1)
        {
            return names[0];
        }

        if (names.Count == 2)
        {
            return names[0] + " and " + names[1];
        }

        return string.Join(", ", names.Take(names.Count - 1))
               + " and " + names[names.Count - 1];
    }

    // INPUT VALIDATION METHODS

    // Read a required text input
    static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = (Console.ReadLine() ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }

    // Read an integer within a specified range
    static int ReadInteger(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = Console.ReadLine() ?? "";

            int number;

            if (int.TryParse(input, out number) &&
                number >= minimum &&
                number <= maximum)
            {
                return number;
            }

            Console.WriteLine(
                "Invalid choice. Enter a number from " +
                minimum + " to " + maximum + ".");
        }
    }

    // Read gender (M/F)
    static char ReadGender()
    {
        while (true)
        {
            Console.Write("Enter gender (M for male, F for female): ");

            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "M" || input == "F")
            {
                return input[0];
            }

            Console.WriteLine("Invalid gender. Please enter M or F.");
        }
    }

    // Read and validate country code
    static string ReadCountryCode(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string code = (Console.ReadLine() ?? "").Trim();

            if (CountryCatalog.FindByCode(code) != null)
            {
                return code;
            }

            Console.WriteLine("Invalid country code.");

            Console.WriteLine("Available country codes:");

            foreach (AseanCountry country in CountryCatalog.Countries)
            {
                Console.WriteLine(
                    country.Name + " - " + country.CountryCode);
            }
        }
    }

    // Read only numeric input (preserves leading zeros)
    static string ReadDigits(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = (Console.ReadLine() ?? "").Trim();

            if (input.Length > 0 &&
                input.All(character =>
                    character >= '0' && character <= '9'))
            {
                return input;
            }

            Console.WriteLine(
                "Invalid input. Please enter digits only.");
        }
    }

    // Read Y/N response
    static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string input =
                (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "Y")
            {
                return true;
            }

            if (input == "N")
            {
                return false;
            }

            Console.WriteLine("Invalid input. Please enter Y or N.");
        }
    }
}
