using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MovieReviewApi.Models
{
  // Custom validation for Title
  public class TitleValidationAttribute : ValidationAttribute
  {
    private readonly int _maxLength;

    public TitleValidationAttribute(int maxLength = 100)
    {
      _maxLength = maxLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      string title = value?.ToString() ?? string.Empty;

      if (string.IsNullOrWhiteSpace(title))
        return new ValidationResult("Title is required.");

      if (title.Length > _maxLength)
        return new ValidationResult($"Title cannot exceed {_maxLength} characters.");

      return ValidationResult.Success;
    }
  }

  // Custom validation for Genre
  public class GenreValidationAttribute : ValidationAttribute
  {
    private readonly string[] _allowedGenres =
    [
      "Action",
      "Drama",
      "Comedy",
      "Sci-Fi",
      "Horror",
      "Romance",
      "Adventure"
    ];

    protected override ValidationResult? IsValid(Object? value, ValidationContext validationContext)
    {
      string genre = value?.ToString() ?? string.Empty;

      if (string.IsNullOrWhiteSpace(genre))
        return new ValidationResult("Genre is required.");

      // Case-insensitive check
      if (!_allowedGenres.Contains(genre, StringComparer.OrdinalIgnoreCase))
        return new ValidationResult($"Genre must be on of the following: {string.Join(", ", _allowedGenres)}");

      return ValidationResult.Success;
    }
  }

  // Custom validation for release year
  public class ReleaseYearValidationAttribute : ValidationAttribute
  {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      int currentYear = DateTime.Now.Year;

      if (value == null) return new ValidationResult("Year is required.");

      if (int.TryParse(value.ToString(), out int year))
      {
        if (year < 1900 || year > currentYear)
          return new ValidationResult($"Year must be between 1900 and {currentYear}.");
      }
      else
      {
        return new ValidationResult("Year must be a valid number.");
      }

      return ValidationResult.Success;
    }
  }

  // Custom validation for rating
  public class RatingValidationAttribute : ValidationAttribute
  {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      if (value == null) return new ValidationResult("Rating is required.");

      if (double.TryParse(value.ToString(), out double rating))
      {
        if (rating < 0 || rating > 10)
          return new ValidationResult("Rating must be between 0 and 10.");
      }
      else
      {
        return new ValidationResult("Rating must be a valid number.");
      }

      return ValidationResult.Success;
    }
  }

}