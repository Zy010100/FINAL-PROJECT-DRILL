using System;

public class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public Gender Gender { get; set; }
    public AseanCountry Country { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(
        string studentNumber,
        string surname,
        string firstName,
        string occupation,
        Gender gender,
        AseanCountry country,
        string areaCode,
        string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        Country = country;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    public string GetFullPhoneNumber()
    {
        return Country.Code + "-" + AreaCode + "-" + PhoneNumber;
    }

    public void DisplayInformation()
    {
        Console.WriteLine(
            FirstName + " " + Surname +
            " is a " + Occupation + ".");

        Console.WriteLine("Student Number: " + StudentNumber);
        Console.WriteLine("Gender: " + Gender);
        Console.WriteLine("Country: " + Country.Name);
        Console.WriteLine("Phone Number: " + GetFullPhoneNumber());
    }

    public void DisplaySearchResult()
    {
        Console.WriteLine(
            Surname + ", " + FirstName +
            ", with student number " + StudentNumber +
            ". Occupation: " + Occupation +
            ". Gender: " + Gender +
            ". Phone number: " + GetFullPhoneNumber() +
            " (" + Country.Name + ").");
    }
}
