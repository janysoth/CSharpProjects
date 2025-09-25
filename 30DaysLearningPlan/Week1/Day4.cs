namespace Day4
{
  public class Calculator
  {
    public static int Add(int a, int b) => a + b;
    public static int Multiply(int a, int b) => a * b;
  }

  // ===================
  // Real-World Analogy
  // ===================

  // Static class for kitchen tools (available to everyone)
  public static class KitchenTools
  {
    public static void PreheatOven()
    {
      Console.WriteLine("Oven is preheated!");
    }

    public static void WashVegetables(string vegetable)
    {
      Console.WriteLine($"{vegetable} is washed and ready!");
    }
  }

  // Chef class (instance methods belong to a specific chef)
  public class Chef
  {
    public string Name;

    public Chef(string name)
    {
      Name = name;
    }

    // Instance method: cooks a dish using provided ingredients
    public void CookDish(string dish, string ingredient)
    {
      Console.WriteLine($"{Name} is cooking {dish} with {ingredient}");
    }

    // Instance method with return value: calculates calories for a dish
    public int CookAndCountCalories(string dish)
    {
      int calories = dish.Length * 50;
      Console.WriteLine($"{Name} finished cooking {dish}");
      return calories;
    }

  }

}