namespace MovieReviewApi.Models
{
  public class Movie
  {
    public int Id { get; set; }        // Unique identifier
    public string Title { get; set; }  // Movie title
    public string Genre { get; set; }  // Genre (Action, Drama, etc.)
    public int Year { get; set; }      // Release year
    public double Rating { get; set; } // Movie rating (1-10)
  }
}