namespace MovieReviewApi.Models.DTOs
{
  public class PagedMoviesResult
  {
    public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
  }
}