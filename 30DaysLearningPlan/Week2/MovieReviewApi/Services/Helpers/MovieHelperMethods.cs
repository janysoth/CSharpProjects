using MovieReviewApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewApi.Services.Helpers
{
    /// Provides extension methods for Movie entity operations:
    /// updating, searching, filtering, sorting, and pagination.
    public static class MovieHelpers
    {
        // =============================================================
        // 🛠️ APPLY UPDATES
        // =============================================================
        public static void ApplyUpdates(this Movie original, Movie updated)
        {
            if (original == null || updated == null)
                return;

            original.Title = updated.Title;
            original.Genre = updated.Genre;
            original.Year = updated.Year;
            original.Rating = updated.Rating;
        }

        // =============================================================
        // 🔍 SEARCH BY TITLE
        // =============================================================
        public static IQueryable<Movie> ApplySearch(this IQueryable<Movie> query, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;

            string lowerSearch = search.ToLower();

            return query.Where(m =>
                m.Title != null &&
                m.Title.ToLower().Contains(lowerSearch));
        }

        // =============================================================
        // 🎬 FILTER BY GENRE
        // =============================================================
        public static IQueryable<Movie> ApplyGenreFilter(this IQueryable<Movie> query, string? genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                return query;

            string lowerGenre = genre.ToLower();

            return query.Where(m =>
                m.Genre != null &&
                m.Genre.ToLower() == lowerGenre);
        }

        // =============================================================
        // ↕️ APPLY SORTING (1-parameter legacy version)
        // Keeps compatibility with existing code
        // =============================================================
        public static IQueryable<Movie> ApplySorting(this IQueryable<Movie> query, string? sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query.OrderBy(m => m.Id);

            return sortBy.ToLower() switch
            {
                "title" => query.OrderBy(m => m.Title),
                "year" => query.OrderBy(m => m.Year),
                "rating" => query.OrderByDescending(m => m.Rating),
                _ => query.OrderBy(m => m.Id) // default
            };
        }

        // =============================================================
        // ↕️ APPLY SORTING (2-parameter version: sortBy + order)
        // Used for dynamic paged filtering (ASC/DESC)
        // =============================================================
        public static IQueryable<Movie> ApplySorting(this IQueryable<Movie> query, string? sortBy, string? order)
        {
            bool descending = order?.ToLower() == "desc";

            return sortBy?.ToLower() switch
            {
                "title" =>
                    descending ? query.OrderByDescending(m => m.Title)
                               : query.OrderBy(m => m.Title),

                "year" =>
                    descending ? query.OrderByDescending(m => m.Year)
                               : query.OrderBy(m => m.Year),

                "rating" =>
                    descending ? query.OrderByDescending(m => m.Rating)
                               : query.OrderBy(m => m.Rating),

                // Default fallback: sort by ID
                _ =>
                    descending ? query.OrderByDescending(m => m.Id)
                               : query.OrderBy(m => m.Id)
            };
        }

        // =============================================================
        // 📄 ADJUST PAGE NUMBER
        // Ensures the user never gets an invalid or empty page.
        // =============================================================
        public static async Task<int> AdjustPageAsync(this IQueryable<Movie> query, int page, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 10;

            int totalMovies = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            if (totalPages == 0)
                return 1;

            if (page < 1)
                return 1;

            if (page > totalPages)
                return totalPages;

            return page;
        }
    }
}