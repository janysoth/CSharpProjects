using MovieReviewApi.Models;
using MovieReviewApi.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieReviewApi.Services
{
  public interface IMovieService
  {
    Task<PagedMoviesResult> GetPagedMoviesAsync(
        string? genre, string? sortBy, string? order, int page, int pageSize);

    Task<IEnumerable<Movie>> GetAllMoviesUnpagedAsync();
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<Movie> AddMovieAsync(Movie movie);
    Task<bool> UpdateMovieAsync(int id, Movie updatedMovie);
    Task<Movie?> PatchMovieAsync(int id, JsonPatchDocument<Movie> patchDoc);
    Task<Movie?> DeleteMovieAsync(int id);
    Task<int> GetTotalMoviesCountAsync();
  }
}