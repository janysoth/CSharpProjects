using MovieReviewApi.Models;
using MovieReviewApi.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MovieReviewApi.Data;
using MovieReviewApi.Services.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewApi.Services
{
  public class MovieService : IMovieService
  {
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context) => _context = context;

    // =============================================================
    // GET ALL MOVIES WITHOUT PAGINATION
    // =============================================================
    public async Task<IEnumerable<Movie>> GetAllMoviesUnpagedAsync()
    {
      return await _context.Movies
          .OrderBy(m => m.Id)
          .ToListAsync();
    }

    // =============================================================
    // GET ALL MOVIES WITH PAGINATION / FILTER / SORT
    // =============================================================
    public async Task<PagedMoviesResult> GetAllMoviesAsync(
        string? genre = null,
        string? sortBy = null,
        string? search = null,
        int page = 1,
        int pageSize = 5)
    {
      var query = _context.Movies.AsQueryable()
          .ApplySearch(search)
          .ApplyGenreFilter(genre)
          .ApplySorting(sortBy);

      var totalItems = await query.CountAsync();
      var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

      if (page < 1) page = 1;
      if (page > totalPages && totalPages > 0) page = totalPages;

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
    // GET MOVIE BY ID
    // =============================================================
    public async Task<Movie?> GetMovieByIdAsync(int id) =>
        await _context.Movies.FindAsync(id);

    // =============================================================
    // ADD MOVIE
    // =============================================================
    public async Task<Movie> AddMovieAsync(Movie movie)
    {
      _context.Movies.Add(movie);
      await _context.SaveChangesAsync();
      return movie;
    }

    // =============================================================
    // UPDATE MOVIE
    // =============================================================
    public async Task<bool> UpdateMovieAsync(int id, Movie updatedMovie)
    {
      var existing = await _context.Movies.FindAsync(id);
      if (existing == null) return false;

      existing.ApplyUpdates(updatedMovie);
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

      var copy = new Movie
      {
        Title = movie.Title,
        Genre = movie.Genre,
        Year = movie.Year,
        Rating = movie.Rating
      };

      patchDoc.ApplyTo(copy);
      return copy;
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
    // TOTAL MOVIES COUNT
    // =============================================================
    public async Task<int> GetTotalMoviesCountAsync() =>
        await _context.Movies.CountAsync();
  }
}