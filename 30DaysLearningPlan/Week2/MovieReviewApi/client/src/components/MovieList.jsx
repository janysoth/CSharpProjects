// src/components/MovieList.jsx
import { useEffect, useState } from "react";
import api from "../services/api";
import MovieCard from "./MovieCard";

const MovieList = () => {
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchMovies = async () => {
      setLoading(true);
      setError("");
      try {
        const res = await api.get("/get-all-movies");
        setMovies(res.data.data || []);
      } catch (err) {
        setError("Failed to fetch movies. Please try again later.");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchMovies();
  }, []);

  if (loading) return <p className="message">Loading movies...</p>;
  if (error) return <p className="message error">{error}</p>;
  if (!movies || movies.length === 0) return <p className="message">No movies found.</p>;

  return (
    <div className="movie-list">
      {movies.map((movie) => (
        <MovieCard
          key={movie.id ?? movie._id ?? movie.title}
          title={movie.title}
          genre={movie.genre}
          rating={movie.rating}
          releaseYear={movie.releaseYear || movie.year}
        />
      ))}
    </div>
  );
};

export default MovieList;