// 📅 Week-1 Day-2: C# Basics: Variables, Data Types, Operators
// ============================================================
//
// 🔹 Study Plan
// -------------
// 1. Variables (int, string, bool, double)
// 2. Constants (const)
// 3. Operators (+, -, *, /, %, ==, !=)
//
// 📝 Practice:
// ------------
// 1. Create variables for name, age, and balance.
// 2. Print a sentence using them.
// 3. Do a simple arithmetic calculation.
//
// ============================================================

using System;

class Program
{
    static void Main(string[] args)
    {
        // ============================================================
        // 1. VARIABLES
        // ============================================================
        // A variable is like a "box" in memory that stores data.
        // In C#, variables must have a data type.
        //
        // Common data types:
        // - int    → whole numbers (e.g., 25)
        // - string → text values (e.g., "Hello")
        // - double → decimal numbers (e.g., 19.99)
        // - bool   → true or false
        //
        // We use '=' to assign values.

        string name = "Jonny";     // A string stores text
        int age = 25;              // An int stores whole numbers
        double balance = 1500.75;  // A double stores decimals
        bool isActive = true;      // A bool stores true/false values

        Console.WriteLine("✅ VARIABLES EXAMPLE:");
        Console.WriteLine($"Name: {name}, Age: {age}, Balance: {balance}, Active: {isActive}");
        Console.WriteLine();

        // ============================================================
        // 2. CONSTANTS
        // ============================================================
        // A constant is a value that CANNOT change once assigned.
        // Use 'const' before the type.
        //
        // Example: Pi is always the same value.

        const double Pi = 3.14159;

        Console.WriteLine("✅ CONSTANT EXAMPLE:");
        Console.WriteLine($"The value of Pi is {Pi}");
        Console.WriteLine();

        // ============================================================
        // 3. OPERATORS
        // ============================================================
        // Operators perform actions on variables.
        //
        // Arithmetic Operators:
        // + (addition), - (subtraction), * (multiplication),
        // / (division), % (modulus = remainder)
        //
        // Comparison Operators:
        // == (equals), != (not equals)

        int a = 10;
        int b = 3;

        Console.WriteLine("✅ OPERATOR EXAMPLES:");
        Console.WriteLine($"Addition: {a} + {b} = {a + b}");
        Console.WriteLine($"Subtraction: {a} - {b} = {a - b}");
        Console.WriteLine($"Multiplication: {a} * {b} = {a * b}");
        Console.WriteLine($"Division (integer): {a} / {b} = {a / b}");
        Console.WriteLine($"Remainder: {a} % {b} = {a % b}");
        Console.WriteLine($"Equals? {a} == {b} → {a == b}");
        Console.WriteLine($"Not Equals? {a} != {b} → {a != b}");
        Console.WriteLine();

        // ============================================================
        // 4. PRACTICE TASKS
        // ============================================================

        // Task 1: Create variables
        string practiceName = "Alice";
        int practiceAge = 30;
        double practiceBalance = 2000.50;

        // Task 2: Print a sentence
        Console.WriteLine("✅ PRACTICE OUTPUT:");
        Console.WriteLine($"Hello, my name is {practiceName}. I am {practiceAge} years old and my balance is ${practiceBalance}.");

        // Task 3: Arithmetic calculation
        int x = 10;
        int y = 4;
        int sum = x + y;
        int product = x * y;
        int remainder = x % y;

        Console.WriteLine($"Sum: {x} + {y} = {sum}");
        Console.WriteLine($"Product: {x} * {y} = {product}");
        Console.WriteLine($"Remainder: {x} % {y} = {remainder}");
    }
}