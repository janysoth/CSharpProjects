using MovieReviewApi.Models;

namespace MovieReviewApi.Data
{
  public static class DbSeeder
  {
    public static void Seed(AppDbContext context)
    {
      if (context.Movies.Any()) return;

      var movies = new List<Movie>
            {
                new Movie { Title = "Inception", Genre = "Sci-Fi", Year = 2010, Rating = 9.0 },
                new Movie { Title = "The Dark Knight", Genre = "Action", Year = 2008, Rating = 9.1 },
                new Movie { Title = "Interstellar", Genre = "Sci-Fi", Year = 2014, Rating = 8.6 },
                new Movie { Title = "Pulp Fiction", Genre = "Crime", Year = 1994, Rating = 8.9 },
                new Movie { Title = "The Shawshank Redemption", Genre = "Drama", Year = 1994, Rating = 9.3 },
                new Movie { Title = "The Matrix", Genre = "Sci-Fi", Year = 1999, Rating = 8.7 },
                new Movie { Title = "Forrest Gump", Genre = "Drama", Year = 1994, Rating = 8.8 },
                new Movie { Title = "Gladiator", Genre = "Action", Year = 2000, Rating = 8.5 },
                new Movie { Title = "Titanic", Genre = "Romance", Year = 1997, Rating = 7.9 },
                new Movie { Title = "Avatar", Genre = "Sci-Fi", Year = 2009, Rating = 7.8 }
            };

      context.Movies.AddRange(movies);
      context.SaveChanges();
    }
  }
}