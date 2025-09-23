namespace Day2
{
  public class Basics
  {
    // VariableDemo Method
    public static void VariableDemo()
    {
      int age = 30;
      string name = "Jonny";
      double pi = 3.14;

      Console.WriteLine($"Day2: Name = {name}, Age = {age}, Pi = {pi}");
    }

    // PizzaOrderDemo Method
    public static void PizzaOrderDemo()
    {
      // Customer info
      string customerName = "Sarah";
      int pizzaOrdered = 3;
      double pricePerPizza = 12.99;
      double deliveryFee = 5.00;

      // Constant tax rate
      const double TAX_RATE = 0.08;

      // Calculation
      double subTotal = pizzaOrdered * pricePerPizza;
      double tax = subTotal * TAX_RATE;
      double total = subTotal + tax + deliveryFee;

      // Print receipt
      Console.WriteLine("----- PIZZA SHOP RECEIPT -----");
      Console.WriteLine($"Customer: {customerName}");
      Console.WriteLine($"Pizza Ordered: {pizzaOrdered}");
      Console.WriteLine($"Subtotal: ${subTotal}");
      Console.WriteLine($"Tax: ${tax}");
      Console.WriteLine($"Delivery Fee: ${deliveryFee}");
      Console.WriteLine($"Total Amount Due: ${total}");
    }

  }
}