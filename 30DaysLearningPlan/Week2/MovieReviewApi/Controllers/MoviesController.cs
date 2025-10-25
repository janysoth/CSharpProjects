using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieReviewApi.Models;
using MovieReviewApi.Services;

namespace MovieReviewApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
      _movieService = movieService;
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
    // PATCH: Skipped for now (Day 17)
    // =============================================================
  }
}