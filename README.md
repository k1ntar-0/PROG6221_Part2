# PROG6221
part 2 chatbot
# CyberSentinel Security Terminal

### Student Details
* **Name:** Ndaedzo Given Tshiovhe
* **Student Number:** 10487456
* **Application Type:** Windows Forms (GUI) Cybersecurity Chatbot

---

## 1. Project Overview
CyberSentinel is an interactive, desktop-based cybersecurity advisory assistant built using C# and .NET Windows Forms. The system is engineered to help users navigate modern threat landscapes—such as password vulnerabilities, local SMS scams, and digital privacy leaks—by parsing natural language, tracking user preferences, and offering an interactive real-time defense checklist.

---

## 2. Technical Implementation Checklist

This application explicitly implements the core learning units and assignment constraints required for the POE:

* **Delegates (Requirement 2):** Uses a custom delegate signature (`public delegate string ResponseProcessor(string input);`) to decouple the user interface from the core business processing logic.
* **Generic Collections (Requirement 3 & 8):** Data storage utilizes a generic `Dictionary<string, List<string>>` matrix to map intent keys directly to arrays of localized security recommendations.
* **State & Memory Management (Requirement 5):** Tracks context state changes during runtime, storing the user's name (`userName`), explicit category preferences (`favoriteTopic`), and structural conversation tracking (`lastDiscussedTopic`).
* **Sentiment Parsing Engine (Requirement 6):** Dynamically extracts emotional states (e.g., worried, frustrated) from the raw input text to prepending empathetic, reassuring structural context to the responses.
* **Custom Exception Classes & Advanced Handling (Requirement 7):** Built specialized error hierarchies inheriting from `System.Exception` to handle operational failures cleanly without breaking runtime execution:
    * `EmptyInputException`: Catches empty message submissions.
    * `MissingContextException`: Triggers if the user asks to "explain more" before defining a base topic domain.
    * `UnknownQueryException`: Gracefully catches and prints unrecognized query packages in a stylized red console log view.
* **Interactive WinForms Mechanics (Part 3):** Combines a dynamic custom `CheckedListBox`, `ProgressBar`, and an item state event handler to visually calculate and display a live "System Defense Index" score based on user threat-mitigation tasks.

---

## 3. Architecture & Code Structure

The file `Program.cs` is cleanly split into three distinct modular segments:
1.  **Custom Exceptions:** Houses the robust error classes to isolate type parameters from main threads.
2.  **Application Entry:** Initializes the modern configuration frameworks and boots up the main window canvas loop.
3.  **ChatbotForm (GUI & Processing):** Controls all visual item placement (RichTextBox, TextBoxes, Buttons, ProgressBars) and executes the string processing logic through the registered delegate mapping.

---

## 4. How to Run the Application

### Prerequisites
* .NET 8.0 SDK (or newer) installed on a Windows operating system.
* VS Code with the C# Dev Kit extension enabled.

### Compilation Steps
1. Ensure your `chatbot.csproj` contains the required platform targets:
   ```xml
   <TargetFramework>net8.0-windows</TargetFramework>
   <UseWindowsForms>true</UseWindowsForms>