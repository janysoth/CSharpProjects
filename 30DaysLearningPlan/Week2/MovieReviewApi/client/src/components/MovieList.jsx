import React, { useEffect, useState } from "react";
import { deleteMovie, getAllMovies } from "../services/api";
import ConfirmDeleteModal from "./ConfirmDeleteModal";
import EditMovieModal from "./EditMovieModal";
import MovieCard from "./MovieCard";

export default function MovieList() {
  const [movies, setMovies] = useState([]); // Always start as an array
  const [loading, setLoading] = useState(true);
  const [modalMovie, setModalMovie] = useState(null);
  const [deleteMovieId, setDeleteMovieId] = useState(null);
  const [deleteLoading, setDeleteLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  // Fetch movies from API
  const fetchMovies = async () => {
    try {
      setLoading(true);
      setError("");
      const res = await getAllMovies();
      console.log(res.data.data);
      const moviesData = res?.data?.data ?? [];
      setMovies(moviesData);
    } catch (err) {
      console.error(err);
      setError("Failed to load movies.");
      setMovies([]); // fallback to empty array
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchMovies();
  }, []);

  // Delete logic
  const handleDeleteClick = (id) => setDeleteMovieId(id);

  const handleDeleteConfirm = async () => {
    if (!deleteMovieId) return;
    try {
      setDeleteLoading(true);
      await deleteMovie(deleteMovieId);
      setMovies((prev) => prev.filter((m) => m.id !== deleteMovieId));
      setMessage("Movie deleted successfully.");
    } catch (err) {
      console.error(err);
      setError("Failed to delete movie.");
    } finally {
      setDeleteLoading(false);
      setDeleteMovieId(null);
    }
  };

  // JSX Rendering
  return (
    <section className="movie-section">
      {message && <p className="message success">{message}</p>}
      {error && <p className="message error">{error}</p>}

      {loading ? (
        <p className="message">Loading movies…</p>
      ) : !movies || movies.length === 0 ? (
        <p className="message">No movies found.</p>
      ) : (
        <div className="movie-list">
          {movies.map((movie) => (
            <MovieCard
              key={movie.id}
              title={movie.title}
              genre={movie.genre}
              rating={movie.rating}
              releaseYear={movie.year}
              deleting={deleteMovieId === movie.id && deleteLoading}
              onEdit={() => setModalMovie(movie)}
              onDelete={() => handleDeleteClick(movie.id)}
            />
          ))}
        </div>
      )}

      {/* Edit Modal */}
      {modalMovie && (
        <EditMovieModal
          movie={modalMovie}
          onClose={() => setModalMovie(null)}
          onSuccess={(updatedMovie) => {
            setMovies((prev) =>
              prev.map((m) => (m.id === updatedMovie.id ? updatedMovie : m))
            );
            setModalMovie(null);
            setMessage("Movie updated successfully.");
          }}
        />
      )}

      {/* Delete Confirmation Modal */}
      {deleteMovieId && (
        <ConfirmDeleteModal
          movieTitle={
            movies.find((m) => m.id === deleteMovieId)?.title || "this movie"
          }
          onCancel={() => setDeleteMovieId(null)}
          onConfirm={handleDeleteConfirm}
          loading={deleteLoading}
        />
      )}
    </section>
  );
}