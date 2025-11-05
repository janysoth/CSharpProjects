using MovieReviewApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieReviewApi.Data;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewApi.Services
{
  public class MovieService : IMovieService
  {
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
      _context = context;
    }

    // =============================================================
    // GET ALL MOVIES
    // =============================================================
    public async Task<IEnumerable<Movie>> GetAllMoviesAsync(string? genre, string? sortBy, string? search, int page, int pageSize)
    {
      var query = _context.Movies.AsQueryable();

      // 🔍 Search by title
      if (!string.IsNullOrWhiteSpace(search))
        query = query.Where(m => m.Title != null && m.Title.ToLower().Contains(search.ToLower()));

      // 🎭 Filter by genre
      if (!string.IsNullOrWhiteSpace(genre))
        query = query.Where(m => m.Genre != null && m.Genre.ToLower() == genre.ToLower());

      // 🔃 Sorting
      query = sortBy switch
      {
        "title" => query.OrderBy(m => m.Title),
        "year" => query.OrderBy(m => m.Year),
        "rating" => query.OrderByDescending(m => m.Rating),
        _ => query.OrderBy(m => m.Id)
      };

      // 📄 Pagination
      int totalMovies = await query.CountAsync();
      int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);
      if (page > totalPages)
        page = totalPages > 0 ? totalPages : 1;

      return await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }
    // =============================================================
    // GET MOVIE BY ID
    // =============================================================
    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
      return await _context.Movies.FindAsync(id);
    }

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

      existing.Title = updatedMovie.Title;
      existing.Genre = updatedMovie.Genre;
      existing.Year = updatedMovie.Year;
      existing.Rating = updatedMovie.Rating;

      await _context.SaveChangesAsync();
      return true;
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
    // PATCH MOVIE
    // =============================================================
    public async Task<Movie?> PatchMovieAsync(int id, JsonPatchDocument<Movie> patchDoc)
    {
      var movie = await _context.Movies.FindAsync(id);

      if (movie == null) return null;

      // Just apply patch; let controller handle validation
      patchDoc.ApplyTo(movie);

      return movie; // Don't save yet. 
    }

    // =============================================================
    // GET TOTAL MOVIES COUNT
    // =============================================================
    public async Task<int> GetTotalMoviesCountAsync() =>
      await _context.Movies.CountAsync();

  }
}