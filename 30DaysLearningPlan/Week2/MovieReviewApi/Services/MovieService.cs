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
      var existing = await _context.Movies.FindAsync(id);
      if (existing == null) return false;

      MovieHelpers.ApplyMovieUpdates(existing, updatedMovie);
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

      // Create a copy of the movie to apply the patch to (so we can normalize changes)
      var movieCopy = new Movie
      {
        Title = movie.Title,
        Genre = movie.Genre,
        Year = movie.Year,
        Rating = movie.Rating
      };

      patchDoc.ApplyTo(movieCopy);

      // Apply normalized updates to the original movie
      MovieHelpers.ApplyMovieUpdates(movie, movieCopy);

      await _context.SaveChangesAsync();
      return movie;
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