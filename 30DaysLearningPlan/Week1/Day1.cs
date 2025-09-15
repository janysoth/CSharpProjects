// 📅 Week-1 Day-1: Introduction to .NET & Setup
// -----------------------------------------------------------
// This file contains explanations (in comments) about .NET,
// and also includes a sample C# console program at the bottom.
// -----------------------------------------------------------

/*
========================================================
🟢 What is .NET?
========================================================
- .NET is a free, cross-platform, open-source development platform
  created by Microsoft.

It includes:
  1. Runtime:
     - The environment that runs your applications.
     - For example, the "Common Language Runtime" (CLR) executes C# code.

  2. SDK (Software Development Kit):
     - A collection of tools needed for building apps.
     - Includes compiler (csc), libraries, templates, and CLI (dotnet tool).

  3. CLR (Common Language Runtime):
     - Part of the .NET runtime.
     - It manages memory, executes code, and handles garbage collection.
*/

/*
========================================================
🟢 Types of .NET Applications
========================================================
1. Console Applications
   - Run in a terminal/command line.
   - Great for learning and small utilities.

2. Web API
   - Used to build RESTful APIs.
   - Allows different apps (like React, Angular, mobile apps) to communicate
     with your backend.

3. Blazor
   - A framework for building interactive web apps with C# instead of JavaScript.
   - Can run client-side (WebAssembly) or server-side.
*/

/*
========================================================
🟢 Install .NET SDK & VS Code
========================================================
1. Download and Install .NET SDK:
   - Go to: https://dotnet.microsoft.com/download
   - Install the latest LTS version.

2. Install Visual Studio Code:
   - Go to: https://code.visualstudio.com/
   - Add the C# extension (by Microsoft).

3. Verify installation:
   - Open terminal and run:
        dotnet --version
   - You should see the installed version number.
*/

/*
========================================================
🟢 Run Your First Console Program
========================================================
Steps:
1. Create a new console project:
   dotnet new console -n MyFirstApp

2. Navigate into folder:
   cd MyFirstApp

3. Run the program:
   dotnet run
*/

// -----------------------------------------------------------
// ✅ Example: First C# Console Program
// -----------------------------------------------------------

namespace Day1
{
   public class Intro
   {
      public static void Hello()
      {
         Console.WriteLine("Day1: Introduction to .NET & Setup.");
      }
   }
}