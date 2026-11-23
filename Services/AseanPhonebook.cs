using System;
using System.Collections.Generic;
using System.Linq;

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
