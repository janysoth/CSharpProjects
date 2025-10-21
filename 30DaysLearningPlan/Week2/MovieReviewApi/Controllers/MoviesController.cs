using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using MovieReviewApi.Models;
using MovieReviewApi.Services;

namespace MovieReviewApi.Controllers
{
  // The [ApiController] attribute enables automatic model validation
  // and better error responses (e.g., 400 BadRequest for invalid model states)
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    // Dependency Injection (DI)
    // IMovieService abstracts the movie operations (Get, Add, Update, Delete)
    private readonly IMovieService _movieService;

    // Constructor-based injection of the movie service
    public MoviesController(IMovieService movieService)
    {
      _movieService = movieService;
    }

    // =============================================================
    // ✅ GET: api/movies/get-all-movies
    // Returns a list of all movies
    // =============================================================
    [HttpGet("get-all-movies")]
    public ActionResult<IEnumerable<Movie>> GetAllMovies()
    {
      var movies = _movieService.GetAllMovies();
      return Ok(movies); // 200 OK
    }

    // =============================================================
    // ✅ GET: api/movies/{id}
    // Retrieves a single movie by its ID
    // =============================================================
    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovieById(int id)
    {
      var movie = _movieService.GetMovieById(id);

      if (movie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      return Ok(movie); // 200 OK
    }

    // =============================================================
    // ✅ POST: api/movies/add-movie
    // Adds a new movie
    // =============================================================
    [HttpPost("add-movie")]
    public ActionResult AddMovie([FromBody] Movie movie)
    {
      // Server-side validation check
      if (!ModelState.IsValid)
        return BadRequest(ModelState); // 400 Bad Request

      _movieService.AddMovie(movie);

      // 201 Created + returns the location of the new movie resource
      return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    // =============================================================
    // ✅ PUT: api/movies/update-movie/{id}
    // Performs a full update (replace all fields)
    // =============================================================
    [HttpPut("update-movie/{id}")]
    public ActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      bool success = _movieService.UpdateMovie(id, updatedMovie);

      if (!success)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      return Ok(new
      {
        message = $"Movie with ID {id} updated successfully.",
        movie = updatedMovie
      });
    }

    // =============================================================
    // ✅ PATCH: api/movies/patch-movie/{id}
    // Performs a partial update (only modifies provided fields)
    // =============================================================
    [HttpPatch("patch-movie/{id}")]
    public ActionResult<Movie> PatchMovie(int id, [FromBody] JsonPatchDocument<Movie> patchDoc)
    {
      if (patchDoc == null)
        return BadRequest(new { message = "Invalid update info. Please try again." });

      var movie = _movieService.GetMovieById(id);

      if (movie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      // Apply JSON patch (like replace, add, remove operations)
      patchDoc.ApplyTo(movie, ModelState);

      // Re-run validation after patching
      TryValidateModel(movie);

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(new
      {
        message = $"{movie.Title} updated successfully.",
        movie
      });
    }

    // =============================================================
    // ✅ DELETE: api/movies/delete-movie/{id}
    // Deletes a movie by ID
    // =============================================================
    [HttpDelete("delete-movie/{id}")]
    public ActionResult DeleteMovie(int id)
    {
      var deletedMovie = _movieService.DeleteMovie(id);

      if (deletedMovie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      return Ok(new { message = $"{deletedMovie.Title} has been deleted successfully." });
    }
  }
}