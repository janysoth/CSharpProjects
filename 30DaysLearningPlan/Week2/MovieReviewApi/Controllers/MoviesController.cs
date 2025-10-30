using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieReviewApi.Models;
using MovieReviewApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using MovieReviewApi.Data;
using System.Linq;

namespace MovieReviewApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly IMovieService _movieService;
    private readonly AppDbContext _context;

    public MoviesController(IMovieService movieService, AppDbContext context)
    {
      _movieService = movieService;
      _context = context;
    }

    // =============================================================
    // GET: api/movies/get-all-movies
    // =============================================================
    [HttpGet("get-all-movies")]
    public async Task<IActionResult> GetAllMovies()
    {
      var movies = await _movieService.GetAllMoviesAsync();
      return Ok(new
      {
        status = "success",
        count = movies.Count(),
        data = movies
      });
    }

    // =============================================================
    // GET: api/movies/{id}
    // =============================================================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
      var movie = await _movieService.GetMovieByIdAsync(id);
      if (movie == null)
        return NotFound(new
        {
          status = "error",
          message = $"Movie with ID {id} not found."
        });

      return Ok(new
      {
        status = "success",
        data = movie
      });
    }

    // =============================================================
    // POST: api/movies/add-movie
    // =============================================================
    [HttpPost("add-movie")]
    public async Task<IActionResult> AddMovie([FromBody] Movie movie)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return BadRequest(new
        {
          status = "error",
          message = "Validation failed.",
          errors
        });
      }

      var createdMovie = await _movieService.AddMovieAsync(movie);
      return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, new
      {
        status = "success",
        message = $"Movie '{createdMovie.Title}' added successfully.",
        data = createdMovie
      });
    }

    // =============================================================
    // PUT: api/movies/update-movie/{id}
    // =============================================================
    [HttpPut("update-movie/{id}")]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie updatedMovie)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return BadRequest(new
        {
          status = "error",
          message = "Validation failed.",
          errors
        });
      }

      var success = await _movieService.UpdateMovieAsync(id, updatedMovie);

      if (!success)
        return NotFound(new
        {
          status = "error",
          message = $"Movie with ID {id} not found."
        });

      return Ok(new
      {
        status = "success",
        message = $"Movie with ID {id} updated successfully.",
        data = updatedMovie
      });
    }

    // =============================================================
    // DELETE: api/movies/delete-movie/{id}
    // =============================================================
    [HttpDelete("delete-movie/{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
      var deletedMovie = await _movieService.DeleteMovieAsync(id);
      if (deletedMovie == null)
        return NotFound(new
        {
          status = "error",
          message = $"Movie with ID {id} not found."
        });

      return Ok(new
      {
        status = "success",
        message = $"Movie '{deletedMovie.Title}' has been deleted successfully.",
        data = deletedMovie
      });
    }

    // =============================================================
    // PATCH: api/movies/patch-movie/{id}
    // =============================================================
    [HttpPatch("patch-movie/{id}")]
    public async Task<IActionResult> PatchMovie(int id, JsonPatchDocument<Movie> patchDoc)
    {
      if (patchDoc == null)
        return BadRequest(new { status = "error", message = "Invalid patch request." });

      var movie = await _movieService.PatchMovieAsync(id, patchDoc);

      if (movie == null)
        return NotFound(new
        {
          status = "error",
          message = $"Movie with ID {id} not found."
        });

      if (!TryValidateModel(movie))
      {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return BadRequest(new
        {
          status = "error",
          message = "Validation failed after patching.",
          errors
        });
      }

      await _context.SaveChangesAsync();

      var updatedFields = string.Join(", ", patchDoc.Operations.Select(op => op.path.TrimStart('/')));

      return Ok(new
      {
        status = "success",
        message = $"Movie '{movie.Title}' updated successfully.",
        details = $"Fields changed: {updatedFields}.",
        data = movie
      });
    }
  }
}