using System;
using Day1;
using Day2;
using Day3;
using Day4;
using Day5;
using Day6;
using Day7;

class Program
{
  static void Main(string[] args)
  {
    // // Day 1
    // Intro.Hello();

    // // Day 2
    // Basics.VariableDemo();

    // // Day 3
    // FlowControlExample.Run();

    // // Day 4
    // int a = 10;
    // int b = 20;
    // int sum = Calculator.Add(a, b);
    // int product = Calculator.Multiply(a, b);
    // Console.WriteLine($"Day 4: The Sum of {a} and {b} is {sum}.");
    // Console.WriteLine($"Day 4: The Product of {a} and {b} is {product}.");

    // // Day 5
    // Person person = new Person("Alice", 25);
    // person.DisplayInfo();

    // // Day 6
    // BankAccount account = new BankAccount("Jonny Vorn Soth", 1000m);
    // account.DisplayInfo();

    // account.Deposit(500m);
    // account.Withdraw(4000m);

    // account.DisplayInfo();

    // Day 7 
    ContactManager manager = new ContactManager();
    bool exit = false;

    while (!exit)
    {
      Console.WriteLine("==== Contact Manager ====");
      Console.WriteLine("1. Display Contacts");
      Console.WriteLine("2. Add Contact");
      Console.WriteLine("3. Remove Contact");
      Console.WriteLine("4. Exit");
      Console.Write("Choose an option (1-4): ");

      string? choice = Console.ReadLine();
      Console.WriteLine();

      switch (choice)
      {
        case "1":
          manager.DisplayAllContacts();
          break;

        case "2":
          string name = ContactManager.ReadInput("Enter contact name: ");
          string phone = ContactManager.ReadInput("Enter phone number: ");
          manager.AddContact(name, phone);
          break;

        case "3":
          manager.RemoveContact();
          break;

        case "4":
          exit = true;
          Console.WriteLine("Exiting Contact Manager. Goodbye!");
          break;

        default:
          Console.WriteLine("⚠️ Invalid option. Try again.\n");
          break;
      }
    }

  }
}

