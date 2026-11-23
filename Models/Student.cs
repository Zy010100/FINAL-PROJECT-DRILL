using System;

namespace AseanPhonebook.Models;

public sealed class Student
{
    private string _studentNumber;
    private string _surname;
    private string _firstName;
    private string _occupation;

    public Student(
        string studentNumber,
        string surname,
        string firstName,
        string occupation,
        Gender gender,
        PhoneNumber phone)
    {
        _studentNumber = RequireText(studentNumber, nameof(studentNumber));
        _surname = RequireText(surname, nameof(surname));
        _firstName = RequireText(firstName, nameof(firstName));
        _occupation = RequireText(occupation, nameof(occupation));
        Gender = gender;
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
    }

    public string StudentNumber
    {
        get => _studentNumber;
        set => _studentNumber = RequireText(value, nameof(value));
    }

    public string Surname
    {
        get => _surname;
        set => _surname = RequireText(value, nameof(value));
    }

    public string FirstName
    {
        get => _firstName;
        set => _firstName = RequireText(value, nameof(value));
    }

    public string Occupation
    {
        get => _occupation;
        set => _occupation = RequireText(value, nameof(value));
    }

    public Gender Gender { get; set; }

    public PhoneNumber Phone { get; }

    private string Possessive => Gender == Gender.Male ? "His" : "Her";

    public string ToEditSummary() =>
        $"{FirstName} {Surname} is a {Occupation}. {Possessive} number is {Phone}";

    public string ToSearchLine() =>
        $"{Surname}, {FirstName}, with student number {StudentNumber}, is a {Occupation}. " +
        $"{Possessive} phone number is {Phone}";

    public override string ToString() => ToSearchLine();

    private static string RequireText(string? text, string paramName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Value must not be blank.", paramName);
        }

        return text.Trim();
    }
}
