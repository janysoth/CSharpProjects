// 📅 Week-1 Day-3: C# Basics: Control Flow (if, switch, loops)
// ============================================================
//
// 🔹 Study Plan
// -------------
// 1. if, else if, else
// 2. switch statement
// 3. Loops: for, while, do-while
//
// 📝 Practice:
// ------------
// 1. Ask user for a number and print if it’s even or odd.
// 2. Print numbers 1 to 5 using for, while, and do-while loops.
// 3. Use a switch to print the day of the week based on a number (1–7).
//
// ============================================================

bool exitProgram = false;

while (!exitProgram)
{
    // ==========================
    // Menu
    // ==========================
    Console.WriteLine("=== Week 1 Day 3: Control Flow Exercises ===");
    Console.WriteLine("Select an exercise to run:");
    Console.WriteLine("1. Check if a number is even or odd");
    Console.WriteLine("2. Print numbers 1 to 5 using loops");
    Console.WriteLine("3. Print day of the week using switch");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            RunEvenOddExercise();
            break;
        case "2":
            RunLoopsExercise();
            break;
        case "3":
            RunSwitchExercise();
            break;
        case "0":
            exitProgram = true;
            Console.WriteLine("Exiting program. Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid choice! Please enter 0-3.");
            break;
    }

    Console.WriteLine();
}

// ============================================================
// Exercise 1: Check Even or Odd
// ============================================================
void RunEvenOddExercise()
{
    Console.WriteLine("=== Exercise 1: Even or Odd ===");
    Console.Write("Enter a number: ");
    int number = int.Parse(Console.ReadLine());

    if (number % 2 == 0)
        Console.WriteLine($"{number} is even.");
    else
        Console.WriteLine($"{number} is odd.");
}

// ============================================================
// Exercise 2: Loops (for, while, do-while)
// ============================================================
void RunLoopsExercise()
{
    Console.WriteLine("=== Exercise 2a: Numbers 1 to 5 using for loop ===");
    for (int i = 1; i <= 5; i++)
        Console.WriteLine(i);

    Console.WriteLine();

    Console.WriteLine("=== Exercise 2b: Numbers 1 to 5 using while loop ===");
    int j = 1;
    while (j <= 5)
    {
        Console.WriteLine(j);
        j++;
    }

    Console.WriteLine();

    Console.WriteLine("=== Exercise 2c: Numbers 1 to 5 using do-while loop ===");
    int k = 1;
    do
    {
        Console.WriteLine(k);
        k++;
    } while (k <= 5);
}

// ============================================================
// Exercise 3: Switch Statement (Day of the Week)
// ============================================================
void RunSwitchExercise()
{
    Console.WriteLine("=== Exercise 3: Day of the Week ===");
    Console.Write("Enter a number (1-7): ");
    int day = int.Parse(Console.ReadLine());

    switch (day)
    {
        case 1: Console.WriteLine("Monday"); break;
        case 2: Console.WriteLine("Tuesday"); break;
        case 3: Console.WriteLine("Wednesday"); break;
        case 4: Console.WriteLine("Thursday"); break;
        case 5: Console.WriteLine("Friday"); break;
        case 6: Console.WriteLine("Saturday"); break;
        case 7: Console.WriteLine("Sunday"); break;
        default: Console.WriteLine("Invalid number! Please enter 1-7."); break;
    }
}