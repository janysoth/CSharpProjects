using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewApi.Services
{
  /// <summary>
  /// MovieService implements IMovieService and provides
  /// CRUD operations for managing movies using an in-memory list.
  /// </summary>
  public class MovieService : IMovieService
  {
    // =============================================================
    // 🗂️ In-memory data source (temporary for demo purposes)
    // In a real-world app, this would come from a database.
    // =============================================================
    private readonly List<Movie> _movies = new()
    {
      new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Rating = 9, Year = 2015 },
      new Movie { Id = 2, Title = "The Dark Knight", Genre = "Action", Rating = 9, Year = 2024 }
    };

    // =============================================================
    // ✅ GET ALL MOVIES
    // Returns the complete movie list
    // =============================================================
    public IEnumerable<Movie> GetAllMovies() => _movies;

    // =============================================================
    // ✅ GET MOVIE BY ID
    // Searches for a movie by ID; returns null if not found
    // =============================================================
    public Movie? GetMovieById(int id) =>
      _movies.FirstOrDefault(m => m.Id == id);

    // =============================================================
    // ✅ ADD NEW MOVIE
    // Assigns a unique ID and adds the movie to the list
    // =============================================================
    public Movie AddMovie(Movie movie)
    {
      // Generate a new ID safely even if the list is empty
      int nextId = _movies.Any() ? _movies.Max(m => m.Id) + 1 : 1;
      movie.Id = nextId;

      _movies.Add(movie);

      return movie;
    }

    // =============================================================
    // ✅ UPDATE MOVIE (Full Update)
    // Replaces all editable fields of an existing movie
    // =============================================================
    public bool UpdateMovie(int id, Movie updatedMovie)
    {
      var existingMovie = GetMovieById(id);

      if (existingMovie == null)
        return false; // Movie not found

      // Update all fields (for a full replacement)
      existingMovie.Title = updatedMovie.Title;
      existingMovie.Genre = updatedMovie.Genre;
      existingMovie.Rating = updatedMovie.Rating;
      existingMovie.Year = updatedMovie.Year;

      return true;
    }

    // =============================================================
    // ✅ PATCH MOVIE (Partial Update)
    // Applies a JSON Patch document to modify only certain fields
    // =============================================================
    public bool PatchMovie(int id, JsonPatchDocument<Movie> patchDoc)
    {
      var existingMovie = GetMovieById(id);

      if (existingMovie == null)
        return false;

      // Apply JSON patch operations (replace, add, remove)
      patchDoc.ApplyTo(existingMovie);

      return true;
    }

    // =============================================================
    // ✅ DELETE MOVIE
    // Removes the movie from the list and returns it
    // =============================================================
    public Movie? DeleteMovie(int id)
    {
      var existingMovie = GetMovieById(id);

      if (existingMovie == null)
        return null;

      _movies.Remove(existingMovie);
      return existingMovie;
    }
  }
}