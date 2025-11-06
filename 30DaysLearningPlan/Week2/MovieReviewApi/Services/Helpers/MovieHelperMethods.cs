using MovieReviewApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewApi.Services.Helpers
{
    public static class MovieHelpers
    {
        // =============================================================
        // APPLY UPDATES
        // =============================================================
        public static void ApplyMovieUpdates(Movie target, Movie source)
        {
            target.Title = source.Title;
            target.Genre = source.Genre;
            target.Year = source.Year;
            target.Rating = source.Rating;
        }

        // =============================================================
        // SEARCH BY TITLE
        // =============================================================
        public static IQueryable<Movie> ApplySearch(this IQueryable<Movie> query, string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(m => m.Title != null && m.Title.ToLower().Contains(lowerSearch));
            }
            return query;
        }

        // =============================================================
        // FILTER BY GENRE
        // =============================================================
        public static IQueryable<Movie> ApplyGenreFilter(this IQueryable<Movie> query, string? genre)
        {
            if (!string.IsNullOrWhiteSpace(genre))
            {
                string lowerGenre = genre.ToLower();
                query = query.Where(m => m.Genre != null && m.Genre.ToLower() == lowerGenre);
            }
            return query;
        }

        // =============================================================
        // APPLY SORTING
        // =============================================================
        public static IQueryable<Movie> ApplySorting(this IQueryable<Movie> query, string? sortBy) =>
            sortBy?.ToLower() switch
            {
                "title" => query.OrderBy(m => m.Title),
                "year" => query.OrderBy(m => m.Year),
                "rating" => query.OrderByDescending(m => m.Rating),
                _ => query.OrderBy(m => m.Id)
            };

        // =============================================================
        // ADJUST PAGE
        // =============================================================
        public static async Task<int> AdjustPageAsync(this IQueryable<Movie> query, int page, int pageSize)
        {
            int totalMovies = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);
            return page > totalPages ? Math.Max(totalPages, 1) : page;
        }
    }
}