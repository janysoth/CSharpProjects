import React, { useEffect, useState } from "react";
import { deleteMovie, getAllMovies } from "../services/api";
import AddMovieForm from "./AddMovieForm";
import ConfirmDeleteModal from "./ConfirmDeleteModal";
import EditMovieModal from "./EditMovieModal";
import MovieCard from "./MovieCard";

export default function MovieList() {
  const [movies, setMovies] = useState([]); // Always start as an array
  const [loading, setLoading] = useState(true);
  const [addMovieModal, setAddMovieModal] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);
  const [editMovieModal, setEditMovieModal] = useState(null);
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

  const handleUpdateMovie = (updatedMovie) => {
    setMovies(prevMovies =>
      prevMovies.map(m =>
        m.id === updatedMovie.id
          ? { ...m, ...updatedMovie } // merge changes instead of replacing
          : m
      )
    );
    setEditMovieModal(null);
    setMessage("Movie updated successfully.");
  };

  // JSX Rendering
  return (
    <section className="movie-section">
      {message && <p className="message success">{message}</p>}
      {error && <p className="message error">{error}</p>}

      <div className="movie-list-header">
        <h2>Movie List</h2>
        <button className="add-movie-btn" onClick={openModal}>+ Add Movie</button>
      </div>

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
              onEdit={() => setEditMovieModal(movie)}
              onDelete={() => handleDeleteClick(movie.id)}
            />
          ))}
        </div>
      )}

      {/* Add Movie Modal */}
      {(isModalOpen || isClosing) && (
        <div
          className={`modal-overlay ${isModalOpen && !isClosing ? "open" : ""}`}
          onClick={closeModal}
        >
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2>Add New Movie</h2>
            <AddMovieForm onMovieAdded={handleMovieAdded} />
            <button
              className="modal-close"
              onClick={closeModal}
            >
              x
            </button>
          </div>
        </div>
      )}

      {/* Edit Modal */}
      {editMovieModal && (
        <EditMovieModal
          movie={editMovieModal}
          onClose={() => setEditMovieModal(null)}
          onSuccess={handleUpdateMovie}
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