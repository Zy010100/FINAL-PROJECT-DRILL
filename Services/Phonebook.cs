using System;
using System.Collections.Generic;
using System.Linq;
using AseanPhonebook.Models;

namespace AseanPhonebook.Services;

public sealed class Phonebook
{
    private readonly List<Student> _students = new();

    public int Count => _students.Count;

    private static bool SameNumber(string a, string b) =>
        string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

    public bool Contains(string studentNumber) => Find(studentNumber) is not null;

    public Student? Find(string studentNumber) =>
        _students.FirstOrDefault(s => SameNumber(s.StudentNumber, studentNumber));

    public bool Add(Student student)
    {
        if (Contains(student.StudentNumber))
        {
            return false;
        }

        _students.Add(student);
        return true;
    }

    public bool TryChangeStudentNumber(Student student, string newNumber)
    {
        Student? existing = Find(newNumber);
        if (existing is not null && !ReferenceEquals(existing, student))
        {
            return false;
        }

        student.StudentNumber = newNumber;
        return true;
    }

    public IReadOnlyList<Student> SearchByCountries(IEnumerable<AseanCountry> countries)
    {
        HashSet<AseanCountry> wanted = new(countries);
        return SortBySurname(_students.Where(s => wanted.Contains(s.Phone.Country)));
    }

    public IReadOnlyList<Student> GetAll() => SortBySurname(_students);

    private static IReadOnlyList<Student> SortBySurname(IEnumerable<Student> students) =>
        students
            .OrderBy(s => s.Surname, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(s => s.FirstName, StringComparer.CurrentCultureIgnoreCase)
            .ToList();
}
