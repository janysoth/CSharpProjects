import { useEffect, useState } from "react";
import api from "../services/api";
import AddMovieForm from "./AddMovieForm";
import MovieCard from "./MovieCard";

const MovieList = () => {
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);

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

  useEffect(() => {
    fetchMovies();
  }, []);

  const handleMovieAdded = () => {
    fetchMovies();
    closeModal();
  };

  const openModal = () => setIsModalOpen(true);

  const closeModal = () => {
    setIsClosing(true);
    setTimeout(() => {
      setIsClosing(false);
      setIsModalOpen(false);
    }, 300);
  };

  if (loading) return <p className="message">Loading movies...</p>;
  if (error) return <p className="message error">{error}</p>;

  return (
    <div className="movie-section">
      <div className="movie-list-header">
        <h2>Movie List</h2>
        <button className="add-movie-btn" onClick={openModal}>+ Add Movie</button>
      </div>

      {movies.length === 0 ? (
        <p className="message">No movies found.</p>
      ) : (
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
      )}

      {(isModalOpen || isClosing) && (
        <div
          className={`modal-overlay ${isModalOpen && !isClosing ? "open" : ""}`}
          onClick={closeModal}
        >
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2>Add New Movie</h2>
            <AddMovieForm onMovieAdded={handleMovieAdded} />
            <button className="modal-close" onClick={closeModal}>✖</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default MovieList;