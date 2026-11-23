using System;

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
