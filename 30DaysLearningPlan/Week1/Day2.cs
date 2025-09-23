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

    // Pizza Order Shop Dynamic
    public static void PizzaDynamicOrder()
    {
      Console.WriteLine("----- PIZZA SHOP ORDER -----");

      // Ask for customer name
      Console.Write("Enter your name: ");
      string customerName = Console.ReadLine()!;

      // Quantity of Pizzas Order
      Console.Write("How many pizzas would you like to order? ");
      int pizzasOrdered = int.Parse(Console.ReadLine()!);

      // Price per Pizza
      Console.Write("Enter the price per pizza: ");
      double pricePerPizza = double.Parse(Console.ReadLine()!);

      // Ask if delivery is needed
      Console.Write("Do you want delivery? (Yes/No): ");
      string deliveryAnswer = Console.ReadLine()!.ToLower();

      double deliveryFee = (deliveryAnswer == "yes") ? 5.00 : 0.00;

      // Constant tax rate
      const double TAX_RATE = 0.08;

      // Calculation
      double subTotal = pizzasOrdered * pricePerPizza;
      double tax = subTotal * TAX_RATE;
      double total = subTotal + tax + deliveryFee;

      // Print Receipt: 
      Console.WriteLine("\n----- PIZZA SHOP RECEIPT -----");
      Console.WriteLine($"Customer: {customerName}");
      Console.WriteLine($"Pizzas Ordered: {pizzasOrdered}");
      Console.WriteLine($"Subtotal: {subTotal}");
      Console.WriteLine($"Tax: ${tax:F2}");
      Console.WriteLine((deliveryAnswer == "yes") ? ($"Delivery Fee: ${deliveryFee:F2}") : "");
      Console.WriteLine($"Total Amount Due: ${total:F2}");
    }
  }
}