using MovieReviewApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieReviewApi.Data;
using MovieReviewApi.Services.Helpers;

namespace MovieReviewApi.Services
{
  public class MovieService : IMovieService
  {
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context) => _context = context;

    // =============================================================
    // GET ALL MOVIES
    // =============================================================
    public async Task<IEnumerable<Movie>> GetAllMoviesAsync(
        string? genre, string? sortBy, string? search, int page, int pageSize)
    {
      var query = _context.Movies.AsQueryable()
          .ApplySearch(search)
          .ApplyGenreFilter(genre)
          .ApplySorting(sortBy);

      page = await query.AdjustPageAsync(page, pageSize);

      return await query
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();
    }

    // =============================================================
    // GET MOVIE BY ID
    // =============================================================
    public async Task<Movie?> GetMovieByIdAsync(int id) =>
        await _context.Movies.FindAsync(id);

    // =============================================================
    // ADD NEW MOVIE
    // =============================================================
    public async Task<Movie> AddMovieAsync(Movie movie)
    {
      _context.Movies.Add(movie);
      await _context.SaveChangesAsync();
      return movie;
    }

    // =============================================================
    // UPDATE MOVIE (FULL)
    // =============================================================
    public async Task<bool> UpdateMovieAsync(int id, Movie updatedMovie)
    {
      var existingMovie = await _context.Movies.FindAsync(id);
      if (existingMovie == null) return false;

      existingMovie.ApplyUpdates(updatedMovie);
      await _context.SaveChangesAsync();
      return true;
    }

    // =============================================================
    // PATCH MOVIE
    // =============================================================
    public async Task<Movie?> PatchMovieAsync(int id, JsonPatchDocument<Movie>? patchDoc)
    {
      var movie = await _context.Movies.FindAsync(id);

      if (movie == null || patchDoc == null) return null;

      // Create a temporary copy to test the patch
      var movieCopy = new Movie
      {
        Title = movie.Title,
        Genre = movie.Genre,
        Year = movie.Year,
        Rating = movie.Rating
      };

      // Apply patch only to the copy
      patchDoc.ApplyTo(movieCopy);

      // Return the patched copy for control-level validation
      return movieCopy;
    }

    // =============================================================
    // DELETE MOVIE
    // =============================================================
    public async Task<Movie?> DeleteMovieAsync(int id)
    {
      var movie = await _context.Movies.FindAsync(id);
      if (movie == null) return null;

      _context.Movies.Remove(movie);
      await _context.SaveChangesAsync();
      return movie;
    }

    // =============================================================
    // GET TOTAL MOVIES COUNT
    // =============================================================
    public async Task<int> GetTotalMoviesCountAsync() =>
        await _context.Movies.CountAsync();
  }
}