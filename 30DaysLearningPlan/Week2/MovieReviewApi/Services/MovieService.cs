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
    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
      return await _context.Movies.ToListAsync();
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
  }
}