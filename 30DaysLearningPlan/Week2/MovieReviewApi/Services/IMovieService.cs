using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviewApi.Services
{
  public interface IMovieService
  {
    Task<IEnumerable<Movie>> GetAllMoviesAsync(
      string? genre = null,
      string? sortBy = null,
      string? search = null,
      int page = 1,
      int pageSize = 5
    );
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<Movie> AddMovieAsync(Movie movie);
    Task<bool> UpdateMovieAsync(int id, Movie updatedMovie);
    Task<Movie?> PatchMovieAsync(int id, JsonPatchDocument<Movie> patchDoc);
    Task<Movie?> DeleteMovieAsync(int id);
    Task<int> GetTotalMoviesCountAsync();
  }
}