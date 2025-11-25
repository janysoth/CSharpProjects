import React, { useEffect, useState } from "react";
import { deleteMovie, getAllMovies } from "../services/api";
import AddMovieForm from "./AddMovieForm";
import ConfirmDeleteModal from "./ConfirmDeleteModal";
import EditMovieModal from "./EditMovieModal";
import MovieCard from "./MovieCard";

export default function MovieList() {
  /* ---------------------------------------------------
   * STATE MANAGEMENT
   * ---------------------------------------------------
   */

  // Movies and loading/error states
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");

  // Add Movie Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);

  // Edit Movie Modal state (stores the movie being edited)
  const [editMovieModal, setEditMovieModal] = useState(null);

  // Delete Modal state
  const [deleteMovieId, setDeleteMovieId] = useState(null);
  const [deleteLoading, setDeleteLoading] = useState(false);

  /* ---------------------------------------------------
   * FETCH MOVIES FROM API
   * ---------------------------------------------------
   */
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
      setMovies([]); // fallback to empty list
    } finally {
      setLoading(false);
    }
  };

  // Load movies on mount
  useEffect(() => {
    fetchMovies();
  }, []);

  /* ---------------------------------------------------
   * ADD MOVIE HANDLERS
   * ---------------------------------------------------
   */

  const handleMovieAdded = () => {
    fetchMovies(); // refresh list after adding
    closeModal();
  };

  const openModal = () => setIsModalOpen(true);

  // Close modal with fade-out animation
  const closeModal = () => {
    setIsClosing(true);

    setTimeout(() => {
      setIsClosing(false);
      setIsModalOpen(false);
    }, 300);
  };

  /* ---------------------------------------------------
   * DELETE MOVIE HANDLERS
   * ---------------------------------------------------
   */

  // Open confirmation modal
  const handleDeleteClick = (id) => {
    setDeleteMovieId(id);
  };

  const handleDeleteConfirm = async () => {
    if (!deleteMovieId) return;

    try {
      setDeleteLoading(true);

      await deleteMovie(deleteMovieId);

      // Remove movie from UI immediately
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

  /* ---------------------------------------------------
   * UPDATE MOVIE HANDLER
   * ---------------------------------------------------
   */
  const handleUpdateMovie = (updatedMovie) => {
    setMovies((prevMovies) =>
      prevMovies.map((m) =>
        m.id === updatedMovie.id ? { ...m, ...updatedMovie } : m
      )
    );

    setEditMovieModal(null);
    setMessage("Movie updated successfully.");
  };

  /* ---------------------------------------------------
   * RENDER
   * ---------------------------------------------------
   */
  return (
    <section className="movie-section">
      {/* Success / Error Messages */}
      {message && <p className="message success">{message}</p>}
      {error && <p className="message error">{error}</p>}

      {/* Header */}
      <div className="movie-list-header">
        <h2>Movie List</h2>

        <button className="add-movie-btn" onClick={openModal}>
          + Add Movie
        </button>
      </div>

      {/* Loading or Empty State */}
      {loading ? (
        <p className="message">Loading movies…</p>
      ) : movies.length === 0 ? (
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

      {/* ---------------------------------------------------
       * ADD MOVIE MODAL
       * ---------------------------------------------------
       */}
      {(isModalOpen || isClosing) && (
        <div
          className={`modal-overlay ${isModalOpen && !isClosing ? "open" : ""
            }`}
          onClick={closeModal}
        >
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2>Add New Movie</h2>

            <AddMovieForm onMovieAdded={handleMovieAdded} />

            <button className="modal-close" onClick={closeModal}>
              x
            </button>
          </div>
        </div>
      )}

      {/* ---------------------------------------------------
       * EDIT MOVIE MODAL
       * ---------------------------------------------------
       */}
      {editMovieModal && (
        <EditMovieModal
          movie={editMovieModal}
          onClose={() => setEditMovieModal(null)}
          onSuccess={handleUpdateMovie}
        />
      )}

      {/* ---------------------------------------------------
       * DELETE CONFIRMATION MODAL
       * ---------------------------------------------------
       */}
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