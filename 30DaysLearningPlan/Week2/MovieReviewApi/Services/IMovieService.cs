using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviewApi.Services
{
  public interface IMovieService
  {
    Task<IEnumerable<Movie>> GetAllMoviesAsync(string? genre, string? sortBy, string? search, int page, int pageSize);
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<Movie> AddMovieAsync(Movie movie);
    Task<bool> UpdateMovieAsync(int id, Movie updatedMovie);
    Task<Movie?> PatchMovieAsync(int id, JsonPatchDocument<Movie> patchDoc);
    Task<Movie?> DeleteMovieAsync(int id);
    Task<int> GetTotalMoviesCountAsync();
  }
}