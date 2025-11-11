using MovieReviewApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewApi.Services.Helpers
{
    /// Provides extension methods to help with Movie entity operations 
    /// such as updating fields, filtering, searching, sorting, and pagination.
    public static class MovieHelpers
    {
        // =============================================================
        // 🛠️ APPLY UPDATES
        // =============================================================
        public static void ApplyUpdates(this Movie original, Movie updated)
        {
            // Guard clause: ensure both objects are valid
            if (original == null || updated == null)
                return;

            // Only update mutable properties
            original.Title = updated.Title;
            original.Genre = updated.Genre;
            original.Year = updated.Year;
            original.Rating = updated.Rating;
        }

        // =============================================================
        // 🔍 SEARCH BY TITLE
        // =============================================================

        /// Filters movies whose title contains the given search term (case-insensitive).
        public static IQueryable<Movie> ApplySearch(this IQueryable<Movie> query, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;

            string lowerSearch = search.ToLower();

            return query.Where(m => m.Title != null && m.Title.ToLower().Contains(lowerSearch));
        }

        // =============================================================
        // 🎬 FILTER BY GENRE
        // =============================================================

        /// Filters movies by the specified genre (case-insensitive).
        public static IQueryable<Movie> ApplyGenreFilter(this IQueryable<Movie> query, string? genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                return query;

            string lowerGenre = genre.ToLower();

            return query.Where(m => m.Genre != null && m.Genre.ToLower() == lowerGenre);
        }

        // =============================================================
        // ↕️ APPLY SORTING
        // =============================================================

        /// Sorts movies based on the specified field (title, year, rating, etc.).
        /// Defaults to sorting by ID.
        public static IQueryable<Movie> ApplySorting(this IQueryable<Movie> query, string? sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query.OrderBy(m => m.Id);

            return sortBy.ToLower() switch
            {
                "title" => query.OrderBy(m => m.Title),
                "year" => query.OrderBy(m => m.Year),
                "rating" => query.OrderByDescending(m => m.Rating),
                _ => query.OrderBy(m => m.Id)
            };
        }

        // =============================================================
        // 📄 ADJUST PAGE NUMBER
        // =============================================================

        /// Ensures the requested page number is within the valid range of pages.
        /// If the requested page exceeds total pages, the last valid page is returned.
        public static async Task<int> AdjustPageAsync(this IQueryable<Movie> query, int page, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 10; // default fallback

            int totalMovies = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            // If requested page exceeds available pages, return last valid page
            return page > totalPages ? Math.Max(totalPages, 1) : Math.Max(page, 1);
        }
    }
}