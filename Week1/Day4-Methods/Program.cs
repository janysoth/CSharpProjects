// 📅 Week-1 Day-4: Methods & Functions
// ------------------------------------
//
// In C#, methods are blocks of code that perform specific tasks.
// They are similar to functions in JavaScript, but in C# they live inside classes.
//
// Benefits:
// - Reusable (write once, call many times)
// - Organized & readable code
// - Allow passing input (arguments) and returning output (values)

using System;

class Program
{
    // ==========================================================
    // 1️⃣ Static Method Example
    // ----------------------------------------------------------
    // - Belongs to the class itself (not an object).
    // - Call directly on the class (no "new" keyword needed).
    // - Use when the method does not depend on instance data.
    //
    // Example: utility/helper functions like Math.Sqrt()
    // ==========================================================
    static void SayHelloStatic()
    {
        Console.WriteLine("Hello from a static method!");
    }

    // ==========================================================
    // 2️⃣ Instance Method Example
    // ----------------------------------------------------------
    // - Belongs to an object (instance) of a class.
    // - Requires "new" to create the object before calling.
    // - Use when the method needs object-specific data (state).
    //
    // Example: deposit/withdraw depends on an account's balance.
    // ==========================================================
    class Greeter
    {
        public void SayHelloInstance()
        {
            Console.WriteLine("Hello from an instance method!");
        }
    }

    // ==========================================================
    // 3️⃣ Method with Argument
    // ----------------------------------------------------------
    // Methods can take input values (parameters).
    // Example: passing a user's name into the greeting.
    // ==========================================================
    static void GreetUser(string name)
    {
        Console.WriteLine($"Hello, {name}!");
    }

    // ==========================================================
    // 4️⃣ Method with Return Value
    // ----------------------------------------------------------
    // Methods can send back (return) values using "return".
    // Example: add two numbers and return the sum.
    // ==========================================================
    static int AddNumbers(int a, int b)
    {
        return a + b;
    }

    // ==========================================================
    // 5️⃣ Practice: Add Two Numbers
    // ==========================================================
    static int Add(int num1, int num2)
    {
        return num1 + num2;
    }

    // ==========================================================
    // 6️⃣ Practice: Greet a User
    // ==========================================================
    static void Greet(string name)
    {
        Console.WriteLine($"Welcome, {name}!");
    }

    // ==========================================================
    // 7️⃣ When to use Static vs Instance (Guidelines)
    // ----------------------------------------------------------
    // 🔹 Use STATIC when:
    //   - Method doesn’t need object data
    //   - General helper/utility method (e.g., Math, formatting)
    //
    // 🔹 Use INSTANCE when:
    //   - Method depends on object’s data (fields/properties)
    //   - Each object may have different results
    //
    // Rule of Thumb:
    //   If method needs "this" (access to fields/properties),
    //   it should be an INSTANCE method.
    // ==========================================================

    // Example: Static method = utility function
    static int Square(int number)
    {
        return number * number;
    }

    // Example: Instance method = depends on object state
    class BankAccount
    {
        private decimal balance;

        public BankAccount(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, new balance = {balance}");
        }
    }

    // Main method (program starts here)
    static void Main()
    {
        Console.WriteLine("=== Static Method ===");
        SayHelloStatic();

        Console.WriteLine("\n=== Instance Method ===");
        Greeter g = new Greeter(); // create an object
        g.SayHelloInstance();

        Console.WriteLine("\n=== Passing Arguments ===");
        GreetUser("Jonny");
        GreetUser("Sarah");

        Console.WriteLine("\n=== Returning Values ===");
        int result = AddNumbers(5, 7);
        Console.WriteLine("The sum is: " + result);

        Console.WriteLine("\n=== Practice 1: Add Two Numbers ===");
        int sum = Add(10, 20);
        Console.WriteLine($"The sum is: {sum}");

        Console.WriteLine("\n=== Practice 2: Greet a User ===");
        Greet("Jonny");
        Greet("Anna");

        Console.WriteLine("\n=== Static vs Instance: Example ===");
        Console.WriteLine("Square of 6 is " + Square(6)); // static

        BankAccount account = new BankAccount(100); // instance
        account.Deposit(50);
    }
}