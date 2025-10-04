using System;
using Week1MovieManager.Services;

namespace Week1MovieManager
{
  class Program
  {
    static void Main(string[] args)
    {
      var movieService = new MovieService();

      while (true)
      {
        ShowMenu();
        string choice = Console.ReadLine()?.Trim() ?? "";

        switch (choice)
        {
          case "1":
            movieService.AddMovie();
            break;
          case "2":
            movieService.EditMovie();
            break;
          case "3":
            movieService.ListMovies();
            break;
          case "4":
            movieService.SearchMovies();
            break;
          case "5":
            movieService.DeleteMovie();
            break;
          case "6":
            movieService.ExitApp();
            return;
          default:
            Console.WriteLine("Invalid option — choose 1, 2, 3, 4 or 5.\n");
            break;
        }
      }
    }

    private static void ShowMenu()
    {
      Console.WriteLine();
      Console.WriteLine("Movie Manager 🎬");
      Console.WriteLine("---------------");
      Console.WriteLine("1. Add Movie");
      Console.WriteLine("2. Edit Movie");
      Console.WriteLine("3. List Movies");
      Console.WriteLine("4. Search Movies");
      Console.WriteLine("5. Delete Movie");
      Console.WriteLine("6. Exit");
      Console.Write("Choose an option: ");
    }
  }
}