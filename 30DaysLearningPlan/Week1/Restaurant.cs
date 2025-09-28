using System;

namespace Week1Restaurant
{
  // ---------------- Day 6: Encapsulation ----------------
  public class SecretRecipe
  {
    // Private field (customers can't touch directly)
    private string sauceRecipe = "Tomatoes + Garlic + Secret Spices";

    // Public property (controlled access)
    public string SauceRecipe
    {
      get { return "Sorry, the recipe is secret!"; } // only safe info
    }
  }

  // ---------------- Day 5: Classes & Objects ----------------
  public class Restaurant
  {
    // Properties (features of the restaurant)
    public string Name { get; set; }
    public int NumberOfTables { get; private set; }
    public bool IsOpen { get; private set; }

    // Has-a relationship (composition) with SecretRecipe
    private SecretRecipe recipe = new SecretRecipe();

    // Constructor
    public Restaurant(string name, int tables)
    {
      Name = name;
      NumberOfTables = tables;
      IsOpen = false;
    }

    // ---------------- Day 4: Methods ----------------
    // Instance method
    public string MakeBurger(string bunType, string topping)
    {
      return $"Burger with {bunType} bun and {topping}";
    }

    // Static method (general recipe)
    public static void BoilWater()
    {
      Console.WriteLine("Boiling water...");
    }

    // Method to open restaurant
    public void Open()
    {
      IsOpen = true;
      Console.WriteLine($"{Name} is now OPEN with {NumberOfTables} tables!");
    }

    // Method to access encapsulated recipe
    public void ShowSauceRecipe()
    {
      Console.WriteLine(recipe.SauceRecipe);
    }
  }
}