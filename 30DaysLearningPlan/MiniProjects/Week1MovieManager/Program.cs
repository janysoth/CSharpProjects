using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

const string DataFile = "movies.json";

// --- Load saved movies (if any) ---
List<Movie> movies = LoadMovies();

while (true)
{
  ShowMenu();
  string choice = Console.ReadLine()?.Trim() ?? "";

  switch (choice)
  {
    case "1":
      AddMovie();
      break;
    case "2":
      ListMovies();
      break;
    case "3":
      SearchMovies();
      break;
    case "4":
      DeleteMovie();
      break;
    case "5":
      ExitApp();
      return;
    default:
      Console.WriteLine("Invalid option — choose 1, 2, 3, 4 or 5.\n");
      break;
  }
}

// ----------------- Functions -----------------

void ShowMenu()
{
  Console.WriteLine();
  Console.WriteLine("Movie Manager 🎬");
  Console.WriteLine("---------------");
  Console.WriteLine("1. Add Movie");
  Console.WriteLine("2. List Movies");
  Console.WriteLine("3. Search Movies");
  Console.WriteLine("4. Delete Movie");
  Console.WriteLine("5. Exit");
  Console.Write("Choose an option: ");
}

// Add Movie logic
void AddMovie()
{
  Console.Write("Enter movie title: ");
  string title = Console.ReadLine()?.Trim() ?? "";

  if (string.IsNullOrWhiteSpace(title))
  {
    Console.WriteLine("Title cannot be empty.\n");
    return;
  }

  if (movies.Any(m => string.Equals(m.Title, title, StringComparison.OrdinalIgnoreCase)))
  {
    Console.WriteLine($"\"{title}\" is already in your list.\n");
    return;
  }

  var movie = new Movie { Title = title };
  movies.Add(movie);
  SaveMovies();
  Console.WriteLine($"✅ \"{movie.Title}\" added to your list!\n");
}

// List Movies logic
void ListMovies()
{
  if (movies.Count == 0)
  {
    Console.WriteLine("No movies added yet.\n");
    return;
  }

  Console.WriteLine("\nYour Movies:");
  for (int i = 0; i < movies.Count; i++)
  {
    Console.WriteLine($"{i + 1}. {movies[i].Title}");
  }
  Console.WriteLine();
}

// Search Movies logic
void SearchMovies()
{
  Console.Write("Search for: ");
  string term = Console.ReadLine()?.Trim() ?? "";

  if (string.IsNullOrWhiteSpace(term))
  {
    Console.WriteLine("Search term cannot be empty.\n");
    return;
  }

  var matches = movies
      .Where(m => m.Title.Contains(term, StringComparison.OrdinalIgnoreCase))
      .ToList();

  if (matches.Count == 0)
  {
    Console.WriteLine("No matches.\n");
  }
  else
  {
    Console.WriteLine("\nFound:");
    foreach (var m in matches)
    {
      Console.WriteLine($"- {m.Title}");
    }
    Console.WriteLine();
  }
}

// 🗑️ Delete Movie logic
void DeleteMovie()
{
  if (movies.Count == 0)
  {
    Console.WriteLine("No movies to delete.\n");
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
      Console.WriteLine("Invalid number.\n");
    }
  }
  else
  {
    Console.WriteLine("Please enter a valid number.\n");
  }
}

// Save and exit
void ExitApp()
{
  Console.WriteLine("Saving and exiting...");
  SaveMovies();
  Console.WriteLine("Goodbye!");
}

// Persistence helpers
List<Movie> LoadMovies()
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
    Console.WriteLine($"Warning: couldn't load saved movies: {ex.Message}");
  }

  return new List<Movie>();
}

void SaveMovies()
{
  try
  {
    string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(DataFile, json);
  }
  catch (Exception ex)
  {
    Console.WriteLine($"Error saving movies: {ex.Message}");
  }
}

// Simple Movie model
class Movie
{
  public string Title { get; set; } = "";
}