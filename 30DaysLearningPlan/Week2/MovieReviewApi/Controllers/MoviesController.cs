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
      // ------------------------------------------------------------
      // Step 1️⃣: Build base query
      // ------------------------------------------------------------
      // Get all movies from the database as a queryable object
      // AsQueryable() lets us build filters and sorting dynamically
      var query = _context.Movies.AsQueryable();

      // ------------------------------------------------------------
      // Step 2️⃣: Filtering
      // ------------------------------------------------------------
      // If the user specified a genre, filter by it (case-insensitive)
      if (!string.IsNullOrEmpty(genre))
      {
        query = query.Where(m =>
          m.Genre != null &&
          string.Equals(m.Genre, genre, StringComparison.OrdinalIgnoreCase));
      }

      // ------------------------------------------------------------
      // Step 3️⃣: Sorting
      // ------------------------------------------------------------
      // Sort the movies based on the "sortBy" and "order" values.
      // Using a switch expression for cleaner conditional logic.
      query = sortBy?.ToLower() switch
      {
        // If user wants to sort by "year"
        "year" => order == "desc"
          ? query.OrderByDescending(m => m.Year) // descending order
          : query.OrderBy(m => m.Year),          // ascending order

        // If user wants to sort by "rating"
        "rating" => order == "desc"
          ? query.OrderByDescending(m => m.Rating)
          : query.OrderBy(m => m.Rating),

        // Default case: sort alphabetically by movie title
        _ => query.OrderBy(m => m.Title)
      };

      // ------------------------------------------------------------
      // Step 4️⃣: Pagination setup
      // ------------------------------------------------------------
      // Get total number of filtered movies (database COUNT query)
      var totalItems = await query.CountAsync();

      // Compute how many total pages exist
      // Example: 12 movies, 5 per page → 3 pages
      var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

      // Optional message to include in the response (used later)
      string? message = null;

      // 🧩 Smart page adjustment
      // Handle cases where the user requests invalid page numbers
      if (totalPages == 0)
      {
        // If no movies at all, set page = 1
        page = 1;
      }
      else if (page > totalPages)
      {
        // If user requested a page beyond available pages,
        // automatically switch to the last page and show a message
        message = $"Page {page} doesn't exist. Showing last available page ({totalPages}).";
        page = totalPages;
      }
      else if (page < 1)
      {
        // If user provided a negative or zero page number,
        // switch to first page and show a message
        message = $"Invalid page number. Showing first page instead.";
        page = 1;
      }

      // ------------------------------------------------------------
      // Step 5️⃣: Fetch paginated data
      // ------------------------------------------------------------
      // Skip the previous pages and take only the requested page
      var movies = await query
        .Skip((page - 1) * pageSize) // skip (page-1)*pageSize movies
        .Take(pageSize)              // take only "pageSize" movies
        .ToListAsync();              // execute SQL and return a list

      // ------------------------------------------------------------
      // Step 6️⃣: Build response
      // ------------------------------------------------------------
      // Return a JSON response with useful metadata
      return Ok(new
      {
        status = "success",
        totalItems,
        totalPages,
        currentPage = page,
        pageSize,
        count = movies.Count,
        message,
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