// 📅 Week-1 Day-7: Recap & Mini Project

using System.Collections.Generic;

namespace Day7
{
  // Contact class
  public class Contact
  {
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    // Constructor
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

  // ContactManager class
  public class ContactManager
  {
    private List<Contact> contacts = new List<Contact>();

    // Helper function: read non-null, non-empty user input
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

    // Expose ReadNonEmptyInput publicly for Program usage
    public static string ReadInput(string prompt) => ReadNonEmptyInput(prompt);

    // Add a new contact
    public void AddContact(string name, string phoneNumber)
    {
      contacts.Add(new Contact(name, phoneNumber));
      Console.WriteLine($"✅ Contact '{name}' added successfully!\n");
    }

    // Remove a contact
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

      // More than one contact → ask user for name
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