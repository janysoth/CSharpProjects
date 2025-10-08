using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MovieReviewApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    // Simple GET endpoint returning a list of movie titles
    [HttpGet]
    public IActionResult GetAllMovies()
    {
      var movies = new List<string> { "Inception", "The Dark Knight", "Interstellar" };
      return Ok(movies);
    }
  }
}