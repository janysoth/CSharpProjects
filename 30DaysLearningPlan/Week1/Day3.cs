namespace Day3
{
  public class FlowControlExample
  {
    public static void Run()
    {
      bool keepRunning = true;

      while (keepRunning)
      {
        Console.WriteLine("\n🎢 Welcome to the Amusement Park Control Flow Menu!");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Flip a ride coin (Even or Odd)");
        Console.WriteLine("2. Ride the Ferris Wheel (1 to 5)");
        Console.WriteLine("3. Pick your park day (1–7)");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
          case 1:
            EvenOrOddRide();
            break;
          case 2:
            FerrisWheelRides();
            break;
          case 3:
            ParkDaySwitch();
            break;
          case 4:
            Console.WriteLine("🎟️ Thanks for visiting the park. Goodbye!");
            keepRunning = false;
            break;
          default:
            Console.WriteLine("❌ Invalid choice. Please pick 1–4.");
            break;
        }

      }
    } // End of Run() Method

    // Practice 1: Even or Odd
    static void EvenOrOddRide()
    {
      Console.Write("Enter a number: ");
      int number = Convert.ToInt32(Console.ReadLine());

      if (number % 2 == 0)
      {
        Console.WriteLine($"The number {number} is EVEN → 🎢 Roller Coaster Ride!");
      }
      else
      {
        Console.WriteLine($"The number {number} is ODD → 🚗 Bumper Cars Ride!");
      }
    } // End of EvenOrOddRide() Method

    // Practice 2: For loop
    static void FerrisWheelRides()
    {
      Console.WriteLine("🎡 Ferris Wheel Ride Count:");
      for (int i = 1; i <= 5; i++)
      {
        Console.WriteLine($"Ride #{i} - Wheee!");
      }
    } // End of FerrisWheelRides() Method

    // Practice 3: Switch
    static void ParkDaySwitch()
    {
      Console.Write("Enter a number (1–7) to choose your park day: ");
      int day = Convert.ToInt32(Console.ReadLine());

      switch (day)
      {
        case 1:
          Console.WriteLine("Monday → The park is quiet, great for rides!");
          break;
        case 2:
          Console.WriteLine("Tuesday → Half-price food day at the park!");
          break;
        case 3:
          Console.WriteLine("Wednesday → Midweek fun with fewer crowds!");
          break;
        case 4:
          Console.WriteLine("Thursday → Special parade in the park!");
          break;
        case 5:
          Console.WriteLine("Friday → Fireworks show at night!");
          break;
        case 6:
          Console.WriteLine("Saturday → The busiest but most exciting day!");
          break;
        case 7:
          Console.WriteLine("Sunday → Relax with shows and family fun!");
          break;
        default:
          Console.WriteLine("Invalid ticket! Please choose 1–7.");
          break;
      }
    } // End of ParkDaySwitch() Method

  } // End of FlowControlExample Class
}

