using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieReviewApi.Models;
using MovieReviewApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using MovieReviewApi.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieReviewApi.Services.Helpers;

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
      var movies = await _movieService.GetAllMoviesUnpagedAsync();
      return Ok(new
      {
        status = "success",
        count = movies.Count(),
        data = movies
      });
    }

    // =============================================================
    // ✅ NEW ENDPOINT — FILTERING, SORTING & PAGINATION
    // GET: api/movies/filter?genre=Action&sortBy=rating&order=desc&page=1&pageSize=5
    // =============================================================
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredMovies(
    string? genre,
    string? sortBy,
    string? order,
    int page = 1,
    int pageSize = 5
)
    {
      var pagedResult = await _movieService.GetPagedMoviesAsync(
          genre, sortBy, order, page, pageSize
      );

      string? message = null;
      if (page != pagedResult.Page)
        message = $"Page {page} doesn't exist. Showing last available page ({pagedResult.Page}).";

      return Ok(new
      {
        status = "success",
        totalItems = pagedResult.TotalItems,
        totalPages = pagedResult.TotalPages,
        currentPage = pagedResult.Page,
        pageSize = pagedResult.PageSize,
        count = pagedResult.Movies.Count(),
        message,
        data = pagedResult.Movies
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
        return BadRequest(new
        {
          status = "error",
          message = "Invalid patch request."
        });

      var movie = await _context.Movies.FindAsync(id);

      if (movie == null)
        return NotFound(new
        {
          status = "error",
          message = $"Movie with ID {id} not found."
        });

      // Get the patched copy (not applied to database yet)
      var patchedCopy = await _movieService.PatchMovieAsync(id, patchDoc);

      if (patchedCopy == null)
        return BadRequest(new { status = "error", message = "Failed to apply patch." });

      // Validate patched copy
      if (!TryValidateModel(patchedCopy))
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

      // ✅ Validation passed — now safely apply changes to original entity
      movie.ApplyUpdates(patchedCopy);
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