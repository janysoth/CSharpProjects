using System;
using System.Collections.Generic;
using Day2;
using Day3;
using Day4;

class Program
{
  static void Main()
  {
    // 👉 Uncomment the day you want to run:

    // Day1();
    // Day2();
    // Day3();
    Day4();
    // Day5();
    // Day6();
    // Day7();
  }

  // =======================
  // Day 1 – Introduction
  // =======================
  static void Day1()
  {
    Console.WriteLine("Hello, World!");
  }

  // =============================
  // Day 2 – Variables & Operators
  // =============================
  static void Day2()
  {
    string name = "Jonny";
    int age = 25;
    double balance = 150.75;

    Console.WriteLine($"My name is {name}, I am {age} years old, and my balance is ${balance}.");

    int a = 10, b = 3;
    Console.WriteLine($"a + b = {a + b}, a * b = {a * b}, a % b = {a % b}");

    Basics.PizzaDynamicOrder();
  }

  // ====================
  // Day 3 – Control Flow
  // ====================
  static void Day3()
  {
    // Console.Write("Enter a number: ");
    // int number = int.Parse(Console.ReadLine()!);

    // if (number % 2 == 0)
    //   Console.WriteLine("Even number");
    // else
    //   Console.WriteLine("Odd number");

    // Console.WriteLine("Numbers from 1 to 5:");
    // for (int i = 1; i <= 5; i++)
    //   Console.WriteLine(i);

    // Console.Write("Enter day number (1-7): ");
    // int day = int.Parse(Console.ReadLine()!);

    // switch (day)
    // {
    //   case 1: Console.WriteLine("Monday"); break;
    //   case 2: Console.WriteLine("Tuesday"); break;
    //   case 3: Console.WriteLine("Wednesday"); break;
    //   case 4: Console.WriteLine("Thursday"); break;
    //   case 5: Console.WriteLine("Friday"); break;
    //   case 6: Console.WriteLine("Saturday"); break;
    //   case 7: Console.WriteLine("Sunday"); break;
    //   default: Console.WriteLine("Invalid day"); break;
    // }

    FlowControlExample.Run();

  }

  // ===========================
  // Day 4 – Methods & Functions
  // ===========================
  static void Day4()
  {
    // Console.WriteLine(AddNumbers(5, 10));
    // Greet("Jonny");

    // Use static methods
    KitchenTools.PreheatOven();
    KitchenTools.WashVegetables("Carrots");

    // Create chefs (instances)
    Chef Manikka = new Chef("Manikka");
    Chef Samantta = new Chef("Samantta");
    Chef Jaccika = new Chef("Jaccika");

    // Call instance methods with arguments
    Manikka.CookDish("Pasta", "Tomato Sauce");
    Samantta.CookDish("Fried Rice", "Eggs");
    Jaccika.CookDish("Pizza", "Pepperoni");

    // Call instance method with return value
    int pastaCalories = Manikka.CookAndCountCalories("Pasta");
    Console.WriteLine($"Pasta has {pastaCalories} calories.");

  }

  // static int AddNumbers(int x, int y)
  // {
  //   return x + y;
  // }

  // static void Greet(string name)
  // {
  //   Console.WriteLine($"Hello, {name}!");
  // }

  // =========================
  // Day 5 – Classes & Objects
  // =========================
  static void Day5()
  {
    Person person1 = new Person("Jonny", 25);
    person1.DisplayInfo();

    Person person2 = new Person("Alice", 30);
    person2.DisplayInfo();
  }

  class Person
  {
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }

    public void DisplayInfo()
    {
      Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
  }

  // ==============================
  // Day 6 – More OOP (BankAccount)
  // ==============================
  static void Day6()
  {
    BankAccount account = new BankAccount(100);
    account.Deposit(50);
    Console.WriteLine($"Balance: {account.Balance}");
    account.Withdraw(200); // Should show error
    Console.WriteLine($"Balance: {account.Balance}");
  }

  class BankAccount
  {
    private double balance;

    public double Balance
    {
      get { return balance; }
      private set { balance = value; }
    }

    public BankAccount(double initialBalance)
    {
      balance = initialBalance;
    }

    public void Deposit(double amount)
    {
      balance += amount;
    }

    public void Withdraw(double amount)
    {
      if (amount <= balance)
        balance -= amount;
      else
        Console.WriteLine("Insufficient funds!");
    }
  }

  // =====================================
  // Day 7 – Mini Project: Contact Manager
  // =====================================
  static void Day7()
  {
    var manager = new ContactManager();

    while (true)
    {
      Console.WriteLine("📱 Contact Manager");
      Console.WriteLine("1. Add Contact");
      Console.WriteLine("2. Remove Contact");
      Console.WriteLine("3. View All Contacts");
      Console.WriteLine("4. Exit");
      Console.Write("Choose an option: ");

      string choice = Console.ReadLine()!;
      Console.WriteLine();

      switch (choice)
      {
        case "1":
          string name = ContactManager.ReadInput("Enter name: ");
          string phone = ContactManager.ReadInput("Enter phone: ");
          manager.AddContact(name, phone);
          break;

        case "2":
          manager.RemoveContact();
          break;

        case "3":
          manager.DisplayAllContacts();
          break;

        case "4":
          Console.WriteLine("👋 Goodbye!");
          return;

        default:
          Console.WriteLine("⚠️ Invalid choice, please try again.\n");
          break;
      }
    }
  }

  // Contact class (for Day7 only)
  class Contact
  {
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public Contact(string name, string phoneNumber)
    {
      Name = name;
      PhoneNumber = phoneNumber;
    }

    public void DisplayInfo()
    {
      Console.WriteLine($"Name: {Name}, Phone: {PhoneNumber}");
    }
  }

  // ContactManager class (for Day7 only)
  class ContactManager
  {
    private List<Contact> contacts = new List<Contact>();

    private static string ReadNonEmptyInput(string prompt)
    {
      string? input;
      do
      {
        Console.Write(prompt);
        input = Console.ReadLine();
      } while (string.IsNullOrWhiteSpace(input));

      return input;
    }

    public static string ReadInput(string prompt) => ReadNonEmptyInput(prompt);

    public void AddContact(string name, string phoneNumber)
    {
      contacts.Add(new Contact(name, phoneNumber));
      Console.WriteLine($"✅ Contact '{name}' added successfully!\n");
    }

    public void RemoveContact()
    {
      if (contacts.Count == 0)
      {
        Console.WriteLine("⚠️ No contacts to remove.\n");
        return;
      }

      if (contacts.Count == 1)
      {
        Console.WriteLine($"❌ Removing the only contact: '{contacts[0].Name}'\n");
        contacts.RemoveAt(0);
        return;
      }

      string nameToRemove = ReadNonEmptyInput("Enter the name of the contact to remove: ");
      Contact? contactToRemove = contacts.Find(
          c => c.Name.Equals(nameToRemove, StringComparison.OrdinalIgnoreCase)
      );

      if (contactToRemove != null)
      {
        contacts.Remove(contactToRemove);
        Console.WriteLine($"❌ Contact '{nameToRemove}' removed successfully!\n");
      }
      else
      {
        Console.WriteLine($"⚠️ Contact '{nameToRemove}' not found.\n");
      }
    }

    public void DisplayAllContacts()
    {
      if (contacts.Count == 0)
      {
        Console.WriteLine("No contacts found.\n");
        return;
      }

      Console.WriteLine("📒 Contact List:");
      foreach (var contact in contacts)
      {
        contact.DisplayInfo();
      }
      Console.WriteLine();
    }
  }
}