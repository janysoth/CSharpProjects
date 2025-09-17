namespace Day5
{
  public class Person
  {
    public string Name { get; set; }
    private int age;

    public int Age
    {
      get => age;
      set
      {
        if (value > 0) age = value;
        else Console.WriteLine("Age must be positive.");
      }
    }

    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }

    public void DisplayInfo()
    {
      Console.WriteLine($"Day 5: Name = {Name}, Age = {Age}.");
    }
  }
}