<p align="center">
  <img src="https://img.shields.io/badge/Internship-Software%20Development-blue?style=for-the-badge" alt="Internship Badge"/>
  <img src="https://img.shields.io/badge/Tasks-6%20Completed-success?style=for-the-badge" alt="Tasks Badge"/>
  <img src="https://img.shields.io/github/license/toxicbishop/Software-Dev-Internship?style=for-the-badge" alt="License Badge"/>
</p>

# 💻 Software Development Internship

A collection of **six self-contained programming tasks** completed as part of a software development internship. Each task is implemented in a different language/framework to demonstrate versatility across the development stack.

---

## 📂 Repository Structure

```
Software-Dev-Internship/
├── Task 1/   ─  Number Guessing Game          (Java · Maven)
├── Task 2/   ─  Number Pattern Generator      (Java · Maven)
├── Task 3/   ─  Task Manager (CRUD)           (C++ · Gradle)
├── Task 4/   ─  Temperature Converter          (C++ · Gradle)
├── Task 5/   ─  Persistent Task Manager       (C# · .NET 8)
├── Task 6/   ─  Interactive Web Scraper       (C# · .NET 8)
├── SOFTWARE DEVELOPMENT.pdf
├── LICENSE
└── README.md
```

---

## 🗂️ Task Overview

### Task 1 — Number Guessing Game

| | |
|---|---|
| **Language** | Java 17 |
| **Build** | Maven |
| **Source** | [`Task1_GuessingGame.java`](Task%201/src/Task1_GuessingGame.java) |

A CLI game where the computer picks a random number and the player tries to guess it. Features three difficulty levels (Easy / Medium / Hard), input validation, hint system ("Too high" / "Too low"), and a persistent win/loss scoreboard across rounds.

```bash
cd "Task 1"
mvn compile exec:java
```

---

### Task 2 — Number Pattern Generator

| | |
|---|---|
| **Language** | Java 17 |
| **Build** | Maven |
| **Source** | [`Task2_NumberPatterns.java`](Task%202/src/Task2_NumberPatterns.java) |

Generates four classic number patterns with user-configurable row counts (1–20):

- **Right Triangle** — ascending rows
- **Inverted Right Triangle** — descending rows
- **Pyramid** — centered, mirrored pattern
- **Floyd's Triangle** — consecutive numbering

```bash
cd "Task 2"
mvn compile exec:java
```

---

### Task 3 — Task Manager (CRUD)

| | |
|---|---|
| **Language** | C++17 |
| **Build** | Gradle (cpp-application plugin) |
| **Source** | [`Task3_TaskManager.cpp`](Task%203/src/main/cpp/Task3_TaskManager.cpp) |

An in-memory task manager with full **Create, Read, Update, Delete** operations. Each task has an auto-incrementing ID, title, optional description, and done/pending status. The interactive menu-driven interface includes robust input validation.

```bash
cd "Task 3"
gradle build
./build/exe/main/task3-task-manager      # Linux
.\build\exe\main\task3-task-manager.exe  # Windows
```

---

### Task 4 — Temperature Converter

| | |
|---|---|
| **Language** | C++17 |
| **Build** | Gradle (cpp-application plugin) |
| **Source** | [`Task4_TemperatureConverter.cpp`](Task%204/src/main/cpp/Task4_TemperatureConverter.cpp) |

A bidirectional temperature converter supporting **Fahrenheit ↔ Celsius** conversions. Results are formatted to two decimal places. Includes a looping menu for repeated conversions and full input validation.

```bash
cd "Task 4"
gradle build
./build/exe/main/task4-temperature-converter      # Linux
.\build\exe\main\task4-temperature-converter.exe  # Windows
```

---

### Task 5 — Persistent Task Manager

| | |
|---|---|
| **Language** | C# (.NET 8) |
| **Build** | `dotnet` CLI |
| **Source** | [`Program.cs`](Task%205/Program.cs) · [`TaskItem.cs`](Task%205/TaskItem.cs) · [`TaskManager.cs`](Task%205/TaskManager.cs) |

An enhanced task manager that extends Task 3's CRUD functionality with **file-based persistence**. Tasks are automatically saved to and loaded from `tasks.txt` using a custom line-delimited format. Data survives application restarts. Features structured OOP architecture with separate model and manager classes.

```bash
cd "Task 5"
dotnet run
```

---

### Task 6 — Interactive Web Scraper

| | |
|---|---|
| **Language** | C# (.NET 8) |
| **Build** | `dotnet` CLI |
| **Dependency** | [HtmlAgilityPack](https://html-agility-pack.net/) v1.11.71 |
| **Source** | [`Program.cs`](Task%206/Program.cs) · [`Scraper.cs`](Task%206/Scraper.cs) · [`Presenter.cs`](Task%206/Presenter.cs) |

A fully interactive web scraper with four extraction modes:

1. **Title & Headings** — extracts the page title and all `<h1>`, `<h2>`, `<h3>` elements
2. **All Links** — enumerates anchor tags with resolved absolute URLs (capped at 50)
3. **Meta Info** — pulls title, description, keywords, and Open Graph metadata
4. **Custom XPath** — run arbitrary XPath queries against any web page

Includes proper HTTP headers, 15-second timeout, and graceful error handling.

```bash
cd "Task 6"
dotnet restore
dotnet run
```

---

## 🛠️ Tech Stack

| Language | Version | Build Tool | Tasks |
|:--------:|:-------:|:----------:|:-----:|
| ☕ Java  | 17      | Maven      | 1, 2  |
| ⚙️ C++   | 17      | Gradle     | 3, 4  |
| 🟣 C#    | .NET 8  | dotnet CLI | 5, 6  |

---

## 🚀 Prerequisites

| Tool | Required For | Install |
|------|-------------|---------|
| **JDK 17+** | Tasks 1 & 2 | [adoptium.net](https://adoptium.net/) |
| **Maven 3.8+** | Tasks 1 & 2 | [maven.apache.org](https://maven.apache.org/) |
| **g++ / MSVC** | Tasks 3 & 4 | System C++ compiler |
| **Gradle 7+** | Tasks 3 & 4 | [gradle.org](https://gradle.org/) |
| **.NET 8 SDK** | Tasks 5 & 6 | [dotnet.microsoft.com](https://dotnet.microsoft.com/) |

---

## 📄 License

This project is licensed under the **MIT License** — see the [`LICENSE`](LICENSE) file for details.

---

<p align="center">
  <sub>Built with ❤️ as part of a Software Development Internship</sub>
</p>
