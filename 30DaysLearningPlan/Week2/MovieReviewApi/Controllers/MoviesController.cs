using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

using MovieReviewApi.Models;

namespace MovieReviewApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    // In-memory movie list for demo
    private static List<Movie> movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Year = 2010, Rating = 8.8 },
            new Movie { Id = 2, Title = "The Dark Knight", Genre = "Action", Year = 2008, Rating = 9.0 }
        };

    // ✅ GET: api/movies
    [HttpGet("get-all-movies")]
    public ActionResult<IEnumerable<Movie>> GetMovies()
    {
      return Ok(movies);
    }

    // ✅ GET: api/movies/{id}
    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovie(int id)
    {
      // Try to find the movie with the given id
      var movie = movies.FirstOrDefault(m => m.Id == id);

      // If not found, return a 404 Not Found
      if (movie == null)
      {
        return NotFound(new { message = $"Movie with ID {id} not found." });
      }

      // If found, return 200 OK with the movie
      return Ok(movie);
    }

    // ✅ POST: api/movies/add-movie
    [HttpPost("add-movie")]
    public ActionResult<Movie> AddMovie([FromBody] Movie newMovie)
    {
      // ✅ Validate model before proceeding
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // Auto-generate a new ID (safe even if list is empty)
      newMovie.Id = movies.Any() ? movies.Max(m => m.Id) + 1 : 1;

      // Add to list
      movies.Add(newMovie);

      // Return a "201 Created" response with a Location header to the single GET endpoint
      return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, newMovie);
    }

    // ✅ PUT: api/movies/update-movie/{id}
    [HttpPatch("update-movie/{id}")]
    public ActionResult<Movie> UpdateMovie(int id, [FromBody] JsonPatchDocument<Movie> patchDoc)
    {
      var movie = movies.FirstOrDefault(m => m.Id == id);
      if (movie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      if (patchDoc == null)
        return BadRequest();

      patchDoc.ApplyTo(movie, ModelState);

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(new { message = $"{movie.Title} updated successfully.", movie });
    }

    // ✅ DELETE: api/movies/delete-movie/{id}
    [HttpDelete("delete-movie/{id}")]
    public ActionResult DeleteMovie(int id)
    {
      var movie = movies.FirstOrDefault(m => m.Id == id);

      if (movie == null)
        return NotFound(new { message = $"Movie with ID {id} not found." });

      movies.Remove(movie);

      // ✅ Return 200 OK with confirmation message
      return Ok(new { message = $"Movie {movie.Title} has been deleted successfully." });
    }

  }
}