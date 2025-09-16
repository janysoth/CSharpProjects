namespace Day3
{
  public class FlowControlExample
  {
    public static void Run()
    {
      Console.WriteLine("Day3: Control Flow Example:");

      int number = 5;
      if (number > 0)
        Console.WriteLine("Positive Number.");
      else
        Console.WriteLine("Negative Number.");

      for (int i = 1; i <= 3; i++)
      {
        Console.WriteLine($"Loop iteration {i}");
      }
    }
  }
}