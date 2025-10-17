using MovieReviewApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace MovieReviewApi.Services
{
  public interface IMovieService
  {
    List<Movie> GetAllMovies();
    Movie? GetMovieById(int id);
    void AddMovie(Movie movie);
    bool UpdateMovie(int id, Movie updatedMovie);                  // Full update
    bool PatchMovie(int id, JsonPatchDocument<Movie> patchDoc);    // Partial update
    bool DeleteMovie(int id);
  }
}