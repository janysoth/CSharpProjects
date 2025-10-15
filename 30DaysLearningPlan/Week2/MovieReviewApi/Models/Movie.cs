using System.ComponentModel.DataAnnotations;

namespace MovieReviewApi.Models
{
  public class Movie
  {

    public int Id { get; set; }        // Unique identifier

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; }  // Movie title

    [Required(ErrorMessage = "Genre is required.")]
    public string Genre { get; set; }  // Genre (Action, Drama, etc.)

    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
    public int Year { get; set; }      // Release year

    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
    public double Rating { get; set; } // Movie rating (1-10)
  }
}