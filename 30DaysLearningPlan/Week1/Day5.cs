// 📅 Week-1 Day-5: Classes & Objects (OOP Basics)
// ------------------------------------------------
// Topics Covered:
// 1. Define classes and objects
// 2. Fields, properties, constructors
// 3. Encapsulation
// 4. Practice: Create a Person class, method, and objects

using System;

namespace Day5
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
            Console.WriteLine($"Day 5: Name: {Name}, Age: {Age}");
        }
    }

    // 🏠 Blueprint for a House
    public class House
    {
        // Fields (hidden details - wiring, plumbing, etc.)
        private string paintColor;
        private int numberOfRooms;

        // Property (safe access - like windows/doors)
        public string PaintColor
        {
            get { return paintColor; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    paintColor = value;
            }
        }

        // Read-only property (ont get - like asking how many rooms)
        public int NumberOfRooms
        {
            get { return numberOfRooms; }

        }

        // Constructor (how the house is built)
        public House(string color, int rooms)
        {
            paintColor = color;
            numberOfRooms = rooms;
        }

        // Method (action you can do with the house)
        public void DisplayInfo()
        {
            Console.WriteLine($"🏠 This house has {numberOfRooms} rooms and is painted {paintColor}.");
        }
    }

}