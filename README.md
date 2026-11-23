# ASEAN Phonebook System

A C# console-based application that allows users to store, edit, and search student contact information from five selected ASEAN countries. The system uses Object-Oriented Programming (OOP) principles and stores records temporarily in memory.

---

## Project Information

| Detail | Information |
|---|---|
| **Project Name** | ASEAN Phonebook System |
| **Programming Language** | C# |
| **IDE** | Visual Studio |
| **Developer** | Zyril Delliro |
| **Date** | N/A |

---

## Overview

The ASEAN Phonebook System is designed to manage student contact information from five selected Southeast Asian countries.

Users can add student records, modify existing information, and search for students based on their country. Each contact record contains personal information and an international phone number consisting of a country code, area code, and phone number.

The application does not use a database or external text files. All records are stored temporarily in memory and are cleared when the program exits.

---

## Features

- **Store Student Records** – Add student information to the phonebook.
- **Edit Student Records** – Modify existing student information using their student number.
- **Search by Country** – Search for students from one or multiple ASEAN countries.
- **Search All Countries** – Display all registered students regardless of country.
- **Alphabetical Sorting** – Arrange search results alphabetically by surname.
- **Input Validation** – Validate user inputs, gender, country codes, and duplicate student numbers.
- **Unlimited Entries** – Store as many student records as available memory allows.

---

## Supported ASEAN Countries

| No. | Country | Country Code |
|---|---|---|
| 1 | Philippines | 63 |
| 2 | Thailand | 66 |
| 3 | Singapore | 65 |
| 4 | Indonesia | 62 |
| 5 | Malaysia | 60 |

---

## Project Structure

```text
AseanPhonebook/
│
├── Models/
│   ├── AseanCountry.cs
│   ├── Student.cs
│   └── CountryCatalog.cs
│
├── Services/
│   └── AseanPhonebookService.cs
│
├── UI/
│   ├── ConsoleInput.cs
│   └── EndOfInputException.cs
│   └── PhonebookApp.cs
│
├── AseanPhonebook.csproj
├── Program.cs
└── README.md
```

*Note: Adjust the filenames above to match your actual project files.*

### Folder Responsibilities

| Folder / File | Description |
|---|---|
| `Models/` | Contains the classes representing students and ASEAN countries. |
| `Services/` | Handles phonebook operations and country information. |
| `UI/` | Handles menus, user interaction, and input validation. |
| `Program.cs` | Entry point of the application. |
| `.gitignore` | Specifies files and folders Git should ignore. |
| `README.md` | Contains project documentation. |

---

## Object-Oriented Programming (OOP)

The project applies the following OOP concepts:

### 1. Encapsulation
Organizes data and operations within classes and controls how the phonebook manages student records.

### 2. Abstraction
Separates application responsibilities into models, services, and user interface components.

### 3. Classes and Objects
Uses classes such as `Student`, `AseanCountry`, and `AseanPhonebook` to represent the system's components.

### 4. Composition
The phonebook manages a collection of student objects using `List<Student>`.

---

## Main Menu

When the application starts, the following menu is displayed:

```text
[1] Store to ASEAN phonebook
[2] Edit entry in ASEAN phonebook
[3] Search ASEAN phonebook by country
[4] Exit
```

### Store Student Information

Users can enter the following information:

- Student number
- Surname
- First name
- Occupation
- Gender
- Country code
- Area code
- Phone number

### Edit Student Information

Users can search for an existing student using their student number and modify any of the following:

- Student number
- Surname
- Gender
- Occupation
- Country code
- Area code
- Phone number

### Search by Country

Users can select one or multiple countries to filter student records.

The application also provides an `ALL` option to display students from all five supported countries.

Search results are arranged alphabetically according to surname.

---

## Requirements

Before running the application, make sure you have installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/) (optional, for version control)

---

## Data Storage

This project uses in-memory storage.

- No database is required.
- No external text files are used to store student records.
- Student information is available only while the application is running.
- All stored records are lost when the application exits.

---

## Author

**Zyril Delliro**

BSIT 2A

---

## License

This project was developed for educational purposes as part of an academic programming assignment.
