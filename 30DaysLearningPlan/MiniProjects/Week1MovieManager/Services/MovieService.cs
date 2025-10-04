using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Week1MovieManager.Models;

namespace Week1MovieManager.Services
{
  public class MovieService
  {
    private const string DataFile = "movies.json";
    private List<Movie> movies;

    // Constructor for the MovieService class
    public MovieService()
    {
      movies = LoadMovies();
    }

    // Add a new movie with Title, Year, and Rating
    public void AddMovie()
    {
      Console.Write("Enter movie title: ");
      string title = Console.ReadLine()?.Trim() ?? "";

      if (string.IsNullOrWhiteSpace(title))
      {
        Console.WriteLine("❌ Title cannot be empty.\n");
        return;
      }

      if (movies.Any(m => string.Equals(m.Title, title, StringComparison.OrdinalIgnoreCase)))
      {
        Console.WriteLine($"⚠️ \"{title}\" is already in your list.\n");
        return;
      }

      Console.Write("Enter release year: ");
      if (!int.TryParse(Console.ReadLine(), out int year) || year < 1888 || year > DateTime.Now.Year + 1)
      {
        Console.WriteLine("❌ Invalid year.\n");
        return;
      }

      Console.Write("Enter rating (0.0 - 10.0): ");
      if (!double.TryParse(Console.ReadLine(), out double rating) || rating < 0 || rating > 10)
      {
        Console.WriteLine("❌ Rating must be between 0 and 10.\n");
        return;
      }

      var movie = new Movie { Title = title, Year = year, Rating = rating };
      movies.Add(movie);
      SaveMovies();
      Console.WriteLine($"✅ \"{movie.Title}\" ({movie.Year}) with rating {movie.Rating}/10 added!\n");
    }

    // Edit the Movie Details
    public void EditMovie()
    {
      if (movies.Count == 0)
      {
        Console.WriteLine("📭 No movies to edit.\n");
        return;
      }

      ListMovies();

      Console.Write("Enter the number of the movie to edit: ");
      string input = Console.ReadLine()?.Trim() ?? "";

      if (int.TryParse(input, out int index))
      {
        if (index >= 1 && index <= movies.Count)
        {
          var movie = movies[index - 1];
          Console.WriteLine($"\nEditing \"{movie.Title}\" ({movie.Year}) - ⭐ {movie.Rating}/10");

          // Ask for a new title
          Console.Write("Enter the new title (leave blank to keep current): ");
          string newTitle = Console.ReadLine()?.Trim() ?? "";

          if (!string.IsNullOrWhiteSpace(newTitle))
          {
            movie.Title = newTitle;
          }

          // Ask for the new year
          Console.Write("Enter the new release year (leave blank to keep current): ");
          string yearInput = Console.ReadLine()?.Trim() ?? "";

          if (!string.IsNullOrWhiteSpace(yearInput) && int.TryParse(yearInput, out int newYear))
          {
            if (newYear >= 1888 && newYear <= DateTime.Now.Year + 1)
            {
              movie.Year = newYear;
            }
            else
            {
              Console.WriteLine("⚠️ Invalid year, keeping old value.");
            }
          }

          // Ask for new rating
          Console.Write("Enter new rating (0.0 - 10.0, leave blank to keep current): ");
          string ratingInput = Console.ReadLine()?.Trim() ?? "";
          if (!string.IsNullOrWhiteSpace(ratingInput) && double.TryParse(ratingInput, out double newRating))
          {
            if (newRating >= 0 && newRating <= 10)
            {
              movie.Rating = newRating;
            }
            else
            {
              Console.WriteLine("⚠️ Invalid rating, keeping old value.");
            }
          }

          SaveMovies();
          Console.WriteLine($"✅ \"{movie.Title}\" was updated!\n");

        }

        else
        {
          Console.WriteLine("❌ Invalid number.\n");
        }
      }

      else
      {
        Console.WriteLine("❌ Please enter a valid number.\n");
      }

    }

    // Show all movies
    public void ListMovies()
    {
      if (movies.Count == 0)
      {
        Console.WriteLine("📭 No movies added yet.\n");
        return;
      }

      Console.WriteLine("\n🎬 Your Movies:");
      for (int i = 0; i < movies.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {movies[i].Title} ({movies[i].Year}) - ⭐ {movies[i].Rating}/10");
      }
      Console.WriteLine();
    }

    // Search movies by title
    public void SearchMovies()
    {
      Console.Write("Search for: ");
      string term = Console.ReadLine()?.Trim() ?? "";

      if (string.IsNullOrWhiteSpace(term))
      {
        Console.WriteLine("❌ Search term cannot be empty.\n");
        return;
      }

      var matches = movies
          .Where(m => m.Title.Contains(term, StringComparison.OrdinalIgnoreCase))
          .ToList();

      if (matches.Count == 0)
      {
        Console.WriteLine("🔎 No matches.\n");
      }
      else
      {
        Console.WriteLine("\n🔎 Found:");
        foreach (var m in matches)
        {
          Console.WriteLine($"- {m.Title} ({m.Year}) - ⭐ {m.Rating}/10");
        }
        Console.WriteLine();
      }
    }

    // Delete a movie by number
    public void DeleteMovie()
    {
      if (movies.Count == 0)
      {
        Console.WriteLine("📭 No movies to delete.\n");
        return;
      }

      ListMovies();
      Console.Write("Enter the number of the movie to delete: ");
      string input = Console.ReadLine()?.Trim() ?? "";

      if (int.TryParse(input, out int index))
      {
        if (index >= 1 && index <= movies.Count)
        {
          var removed = movies[index - 1];
          movies.RemoveAt(index - 1);
          SaveMovies();
          Console.WriteLine($"🗑️ \"{removed.Title}\" was deleted.\n");
        }
        else
        {
          Console.WriteLine("❌ Invalid number.\n");
        }
      }
      else
      {
        Console.WriteLine("❌ Please enter a valid number.\n");
      }
    }

    // Exit and save
    public void ExitApp()
    {
      Console.WriteLine("💾 Saving and exiting...");
      SaveMovies();
      Console.WriteLine("👋 Goodbye!\n");
    }

    // Load movies from JSON file
    private List<Movie> LoadMovies()
    {
      try
      {
        if (File.Exists(DataFile))
        {
          string json = File.ReadAllText(DataFile);
          var list = JsonSerializer.Deserialize<List<Movie>>(json);
          if (list != null) return list;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"⚠️ Couldn't load saved movies: {ex.Message}");
      }

      return new List<Movie>();
    }

    // Save movies to JSON file
    private void SaveMovies()
    {
      try
      {
        string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(DataFile, json);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Error saving movies: {ex.Message}");
      }
    }
  }
}