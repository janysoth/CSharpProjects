using MovieReviewApi.Models;
using MovieReviewApi.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MovieReviewApi.Data;
using MovieReviewApi.Services.Helpers;

namespace MovieReviewApi.Services
{
  public class MovieService : IMovieService
  {
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context) => _context = context;

    // =============================================================
    // GET PAGED MOVIES WITH FILTERING, SORTING, & PAGE ADJUSTMENT
    // =============================================================
    public async Task<PagedMoviesResult> GetPagedMoviesAsync(
        string? genre, string? sortBy, string? order, int page, int pageSize)
    {
      var query = _context.Movies.AsQueryable()
          .ApplyGenreFilter(genre)
          .ApplySorting(sortBy, order);

      var totalItems = await query.CountAsync();
      var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

      // -------------------------------
      // PAGE ADJUST LOGIC
      // -------------------------------
      if (totalPages == 0)
      {
        page = 1;
      }
      else if (page < 1)
      {
        page = 1;
      }
      else if (page > totalPages)
      {
        page = totalPages;
      }

      var movies = await query
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();

      return new PagedMoviesResult
      {
        Movies = movies,
        TotalItems = totalItems,
        TotalPages = totalPages,
        Page = page,
        PageSize = pageSize
      };
    }

    // =============================================================
    // GET ALL MOVIES UNPAGED
    // =============================================================
    public async Task<IEnumerable<Movie>> GetAllMoviesUnpagedAsync() =>
        await _context.Movies.ToListAsync();

    // =============================================================
    // OTHER CRUD METHODS
    // =============================================================
    public async Task<Movie?> GetMovieByIdAsync(int id) =>
        await _context.Movies.FindAsync(id);

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
      _context.Movies.Add(movie);
      await _context.SaveChangesAsync();
      return movie;
    }

    public async Task<bool> UpdateMovieAsync(int id, Movie updatedMovie)
    {
      var existingMovie = await _context.Movies.FindAsync(id);
      if (existingMovie == null) return false;

      existingMovie.ApplyUpdates(updatedMovie);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Movie?> PatchMovieAsync(int id, JsonPatchDocument<Movie> patchDoc)
    {
      var movie = await _context.Movies.FindAsync(id);
      if (movie == null || patchDoc == null) return null;

      var movieCopy = new Movie
      {
        Title = movie.Title,
        Genre = movie.Genre,
        Year = movie.Year,
        Rating = movie.Rating
      };

      patchDoc.ApplyTo(movieCopy);
      return movieCopy;
    }

    public async Task<Movie?> DeleteMovieAsync(int id)
    {
      var movie = await _context.Movies.FindAsync(id);
      if (movie == null) return null;

      _context.Movies.Remove(movie);
      await _context.SaveChangesAsync();
      return movie;
    }

    public async Task<int> GetTotalMoviesCountAsync() =>
        await _context.Movies.CountAsync();
  }
}