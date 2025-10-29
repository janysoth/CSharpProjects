using System.ComponentModel.DataAnnotations;

namespace MovieReviewApi.Models
{
  public class Movie
  {
    public int Id { get; set; } // Unique identifier

    [TitleValidation(100)]
    public string? Title { get; set; } = string.Empty; // Movie title

    [GenreValidation]
    public string? Genre { get; set; } = string.Empty; // Genre (Action, Drama, etc.)

    [ReleaseYearValidation]
    public int? Year { get; set; } // Release year

    [RatingValidation]
    public double? Rating { get; set; } // Movie rating (0–10)
  }
}