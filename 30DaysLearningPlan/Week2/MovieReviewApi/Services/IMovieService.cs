using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviewApi.Services
{
  public interface IMovieService
  {
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<Movie> AddMovieAsync(Movie movie);
    Task<bool> UpdateMovieAsync(int id, Movie updatedMovie);                  // Full update
    Task<bool> PatchMovieAsync(int id, JsonPatchDocument<Movie> patchDoc);    // Partial update
    Task<Movie?> DeleteMovieAsync(int id);
  }
}