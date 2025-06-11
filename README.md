

```markdown
# 🧠 AI Study Planner (CLI)

A command-line AI-powered study schedule planner built in C#.  
Designed to help students or self-learners efficiently plan study sessions using clean architecture and design patterns.

> 🚀 Built as a portfolio project to demonstrate software design principles and practical utility.

---

## 📌 Features

- ⏰ **Smart Scheduling** based on task deadline and priority
- 🧩 Uses the **Strategy Pattern** to allow flexible scheduling logic
- 🧹 Clean, modular architecture (SRP/OCP)
- 📦 CLI-based interface with extensible codebase
- 🔄 Easily expandable to integrate AI models, time-blocking, or calendar sync

---

## 🧠 Architecture

### 📁 Key Components

| Folder | Purpose |
|--------|---------|
| `Models/` | Contains the core `StudyTask` data class |
| `Services/` | Main logic to sort & assign schedule using strategies |
| `Strategies/` | Implements the `IScheduleStrategy` interface (Strategy Pattern) |
| `Utils/` | Utility functions such as time calculation and input parsing |

### 🔧 Design Patterns

- **Strategy Pattern**: Swap scheduling algorithms (e.g., standard, priority-based).
- **SOLID Principles**: SRP for separation of responsibilities, OCP for extensibility.

---

## 📷 Preview

```

## 📅 AI Study Planner

Enter your tasks:

1. Math Revision - Due: 2025-06-10 - Priority: High
2. AI Research - Due: 2025-06-08 - Priority: Medium

✔️ Recommended Schedule:
\[2025-06-05] AI Research - 2 hrs
\[2025-06-06] Math Revision - 3 hrs
...

````

---

## 🔍 How It Works

1. Define study tasks in the `Program.cs` or future CSV/JSON imports
2. Pass tasks to the `StudyScheduler` class
3. The scheduler applies a strategy (e.g., `StandardStrategy`)
4. Outputs optimized study plan in console

---

## 🚀 Getting Started

### 🛠 Requirements
- .NET 8 SDK
- Windows CLI / PowerShell

### ▶️ Run

```bash
git clone https://github.com/Meshack132/AI-Study-Planner.git
cd AI-Study-Planner
dotnet run
````

---

## 🔬 Sample Code

```csharp
var tasks = new List<StudyTask>
{
    new StudyTask("AI Fundamentals", DateTime.Parse("2025-06-06"), 3),
    new StudyTask("Maths Revision", DateTime.Parse("2025-06-08"), 2)
};

IScheduleStrategy strategy = new StandardStrategy();
StudyScheduler scheduler = new StudyScheduler(strategy);
scheduler.GenerateSchedule(tasks);
```

---

## 📈 Future Improvements

* ✅ Add file-based task import/export (CSV or JSON)
* ✅ Add test coverage (xUnit)
* 🔄 Support time tracking
* 🧠 Integrate basic AI to suggest optimal study blocks

---

## 👨‍💻 Author

**Meshack Mthimkhulu**
Software Engineering Intern | C# | DevOps | AI Enthusiast
🔗 [LinkedIn](https://www.linkedin.com/in/meshackmthimkhulu/) • 🐙 [GitHub](https://github.com/Meshack132)

---

## 📄 License

MIT License — free to use, modify, and distribute.

```

