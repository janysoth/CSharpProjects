// 📅 Week-1 Day-5: Classes & Objects (OOP Basics)
// ------------------------------------------------
// Topics Covered:
// 1. Define classes and objects
// 2. Fields, properties, constructors
// 3. Encapsulation
// 4. Practice: Create a Person class, method, and objects

using System;

namespace Day5_OOP_Basics
{
    // -----------------------------
    // 1. Define Classes and Objects
    // -----------------------------
    // A class is a blueprint for creating objects.
    // An object is an instance of a class.
    // Example: "Person" is a class, while "Alice" is an object.

    public class Person
    {
        // -----------------------------
        // 2. Fields, Properties, Constructors
        // -----------------------------

        // Private field (internal data storage)
        private int age;

        // Public Property (encapsulation)
        // This protects the private field by controlling how it's accessed.
        public int Age
        {
            get { return age; }
            set
            {
                if (value > 0)
                    age = value;
                else
                    Console.WriteLine("Age must be positive.");
            }
        }

        // Auto-property for Name
        public string Name { get; set; }

        // Constructor: runs when object is created
        public Person(string name, int age)
        {
            Name = name;
            Age = age; // Uses property, so encapsulation rules apply
        }

        // -----------------------------
        // 3. Method to Display Info
        // -----------------------------
        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }

    // -----------------------------
    // 4. Program Execution
    // -----------------------------
    class Program
    {
        static void Main(string[] args)
        {
            // Create objects (instances of Person class)
            Person p1 = new Person("Alice", 25);
            Person p2 = new Person("Bob", 30);
            Person p3 = new Person("Charlie", -5); // Will trigger validation

            // Call method on objects
            p1.DisplayInfo();   // Output: Name: Alice, Age: 25
            p2.DisplayInfo();   // Output: Name: Bob, Age: 30
            p3.DisplayInfo();   // Output: Name: Charlie, Age: 0 (invalid age handled)
        }
    }
}