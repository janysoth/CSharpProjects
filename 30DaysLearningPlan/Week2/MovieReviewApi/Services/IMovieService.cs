using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace MovieReviewApi.Services
{
  public interface IMovieService
  {
    IEnumerable<Movie> GetAllMovies();
    Movie? GetMovieById(int id);
    Movie AddMovie(Movie movie);
    bool UpdateMovie(int id, Movie updatedMovie);                  // Full update
    bool PatchMovie(int id, JsonPatchDocument<Movie> patchDoc);    // Partial update
    Movie? DeleteMovie(int id);
  }
}