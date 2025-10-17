using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewApi.Services
{
  public class MovieService : IMovieService
  {
    private readonly List<Movie> _movies = new()
        {
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Rating = 9, Year = 2015 },
            new Movie { Id = 2, Title = "The Dark Knight", Genre = "Action", Rating = 9, Year = 2024 }
        };

    public List<Movie> GetAllMovies() => _movies;

    public Movie? GetMovieById(int id) => _movies.FirstOrDefault(m => m.Id == id);

    public void AddMovie(Movie movie)
    {
      int nextId = _movies.Any() ? _movies.Max(m => m.Id) + 1 : 1;
      movie.Id = nextId;
      _movies.Add(movie);
    }

    public bool UpdateMovie(int id, Movie updatedMovie)
    {
      var movie = _movies.FirstOrDefault(m => m.Id == id);
      if (movie == null) return false;

      movie.Title = updatedMovie.Title;
      movie.Genre = updatedMovie.Genre;
      movie.Rating = updatedMovie.Rating;
      return true;
    }

    public bool PatchMovie(int id, JsonPatchDocument<Movie> patchDoc)
    {
      var movie = _movies.FirstOrDefault(m => m.Id == id);
      if (movie == null) return false;

      patchDoc.ApplyTo(movie);
      return true;
    }

    public bool DeleteMovie(int id)
    {
      var movie = _movies.FirstOrDefault(m => m.Id == id);
      if (movie == null) return false;

      _movies.Remove(movie);
      return true;
    }
  }
}