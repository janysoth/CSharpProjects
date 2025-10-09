using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MovieReviewApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    // ✅ Task 1 & 4: Return all movies
    [HttpGet]
    public IActionResult GetAllMovies()
    {
      var movies = new[]
      {
        new { Id = 1, Title = "Inception", Year = 2010, Rating = 8.8 },
        new { Id = 2, Title = "The Dark Knight", Year = 2008, Rating = 9.0 },
        new { Id = 3, Title = "Interstellar", Year = 2014, Rating = 8.6 }
      };

      return Ok(movies);
    }

    // ✅ Task 3: Return a single movie by ID
    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
      var movies = new List<string> { "Inception", "The Dark Knight", "Interstellar" };

      if (id < 0 || id >= movies.Count)
        return NotFound("Movie not found.");

      return Ok(movies[id]);
    }
  }
}