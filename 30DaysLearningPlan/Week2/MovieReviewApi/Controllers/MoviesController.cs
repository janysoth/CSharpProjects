using Microsoft.AspNetCore.Mvc;
using MovieReviewApi.Models;
using System.Collections.Generic;
using System.Linq;

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
    [HttpGet]
    public ActionResult<IEnumerable<Movie>> GetMovies()
    {
      return Ok(movies);
    }

    // ✅ POST: api/movies/add-movie
    [HttpPost("add-movie")]
    public ActionResult<Movie> AddMovie([FromBody] Movie newMovie)
    {
      // Auto-generate a new ID
      newMovie.Id = movies.Max(m => m.Id) + 1;

      // Add to list
      movies.Add(newMovie);

      // Return a "201 Created" response
      return CreatedAtAction(nameof(GetMovies), new { id = newMovie.Id }, newMovie);
    }
  }
}