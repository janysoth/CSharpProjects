using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieReviewApi.Models;
using MovieReviewApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using MovieReviewApi.Data;

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
    public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
    {
      var movies = await _movieService.GetAllMoviesAsync();
      return Ok(movies);
    }

    // =============================================================
    // GET: api/movies/{id}
    // =============================================================
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(int id)
    {
      var movie = await _movieService.GetMovieByIdAsync(id);
      if (movie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      return Ok(movie);
    }

    // =============================================================
    // POST: api/movies/add-movie
    // =============================================================
    [HttpPost("add-movie")]
    public async Task<ActionResult<Movie>> AddMovie([FromBody] Movie movie)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var createdMovie = await _movieService.AddMovieAsync(movie);
      return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
    }

    // =============================================================
    // PUT: api/movies/update-movie/{id}
    // =============================================================
    [HttpPut("update-movie/{id}")]
    public async Task<ActionResult<Movie>> UpdateMovie(int id, [FromBody] Movie updatedMovie)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var success = await _movieService.UpdateMovieAsync(id, updatedMovie);
      if (!success)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      return Ok(new
      {
        message = $"Movie with ID {id} updated successfully.",
        movie = updatedMovie
      });
    }

    // =============================================================
    // DELETE: api/movies/delete-movie/{id}
    // =============================================================
    [HttpDelete("delete-movie/{id}")]
    public async Task<ActionResult<Movie>> DeleteMovie(int id)
    {
      var deletedMovie = await _movieService.DeleteMovieAsync(id);
      if (deletedMovie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      return Ok(new
      {
        message = $"{deletedMovie.Title} has been deleted successfully.",
        movie = deletedMovie
      });
    }

    // =============================================================
    // PATCH: api/movies/patch-movie/{id}
    // =============================================================
    [HttpPatch("patch-movie/{id}")]
    public async Task<IActionResult> PatchMovie(int id, JsonPatchDocument<Movie> patchDoc)
    {
      if (patchDoc == null)
        return BadRequest(new { message = "Invalid patch request." });

      // Apply the patch using the service
      var movie = await _movieService.PatchMovieAsync(id, patchDoc);

      if (movie == null)
        return NotFound(new { message = "Movie not found." });

      // ✅ Validate after patching
      if (!TryValidateModel(movie))
        return BadRequest(ModelState);

      // Save only if valid
      await _context.SaveChangesAsync();

      // 🧩 Get the list of fields changed (for display)
      var updatedFields = string.Join(", ", patchDoc.Operations.Select(op => op.path.TrimStart('/')));

      // ✅ Clean response with a newline between sentences
      var message = $"The movie '{movie.Title}' has been updated successfully.<br><br>Fields changed: {updatedFields}.";

      // Return separate summary and details
      return Ok(new
      {
        summary = $"The movie '{movie.Title}' has been updated successfully.",
        details = $"Fields changed: {updatedFields}.",
        data = movie
      });
    }
  }
}