import { useEffect, useState } from "react";
import api from "../services/api";

export default function Home() {
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    api.get("/get-all-movies")
      .then(res => {
        // Extract the array from the 'data' property
        setMovies(res.data.data || []);
      })
      .catch(err => {
        console.error("API Error:", err);
        setError("Failed to load movies.");
      })
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading movies...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h1>Movies List</h1>
      <ul>
        {movies.map(movie => (
          <li key={movie.id}>
            {movie.title} ({movie.year}) - Rating: {movie.rating}
          </li>
        ))}
      </ul>
    </div>
  );
}