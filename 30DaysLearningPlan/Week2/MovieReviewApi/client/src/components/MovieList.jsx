import React, { useEffect, useState } from "react";
import { deleteMovie, getAllMovies } from "../services/api";
import AddMovieForm from "./AddMovieForm";
import ConfirmDeleteModal from "./ConfirmDeleteModal";
import EditMovieModal from "./EditMovieModal";
import MovieCard from "./MovieCard";

export default function MovieList() {
  /* ---------------------------------------------------
   * STATE MANAGEMENT
   * --------------------------------------------------- */
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);

  const [editMovieModal, setEditMovieModal] = useState(null);
  const [deleteMovieId, setDeleteMovieId] = useState(null);
  const [deleteLoading, setDeleteLoading] = useState(false);

  /* ---------------------------------------------------
   * FETCH MOVIES
   * --------------------------------------------------- */
  const fetchMovies = async () => {
    try {
      setLoading(true);
      setError("");

      const res = await getAllMovies();
      setMovies(res?.data?.data ?? []);
    } catch (err) {
      console.error(err);
      setError("Failed to load movies.");
      setMovies([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchMovies();
  }, []);

  /* ---------------------------------------------------
   * ADD MOVIE
   * --------------------------------------------------- */
  const openModal = () => setIsModalOpen(true);

  const closeModal = () => {
    setIsClosing(true);
    setTimeout(() => {
      setIsClosing(false);
      setIsModalOpen(false);
    }, 300);
  };

  const handleMovieAdded = () => {
    fetchMovies();
    closeModal();
  };

  /* ---------------------------------------------------
   * DELETE MOVIE
   * --------------------------------------------------- */
  const handleDeleteClick = (id) => {
    setDeleteMovieId(id);
  };

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

  /* ---------------------------------------------------
   * UPDATE MOVIE
   * --------------------------------------------------- */
  const handleUpdateMovie = (updatedMovie) => {
    setMovies((prev) =>
      prev.map((m) =>
        m.id === updatedMovie.id ? { ...m, ...updatedMovie } : m
      )
    );

    setEditMovieModal(null);
    setMessage("Movie updated successfully.");
  };

  /* ---------------------------------------------------
   * RENDER
   * --------------------------------------------------- */
  return (
    <section className="mx-auto max-w-6xl p-4">
      {/* Messages */}
      {message && (
        <p className="mb-4 rounded-md bg-green-100 px-4 py-2 text-green-700">
          {message}
        </p>
      )}
      {error && (
        <p className="mb-4 rounded-md bg-red-100 px-4 py-2 text-red-700">
          {error}
        </p>
      )}

      {/* Header */}
      <div className="mb-6 flex items-center justify-between">
        <h2 className="text-2xl font-bold text-gray-800">Movie List</h2>

        <button
          onClick={openModal}
          className="
            rounded-md bg-blue-600 px-4 py-2 text-white
            transition hover:bg-blue-700
            focus:outline-none focus:ring-2 focus:ring-blue-500
          "
        >
          + Add Movie
        </button>
      </div>

      {/* Content */}
      {loading ? (
        <p className="text-gray-600">Loading movies…</p>
      ) : movies.length === 0 ? (
        <p className="text-gray-600">No movies found.</p>
      ) : (
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 auto-rows-fr">
          {movies.map((movie) => (
            <MovieCard
              key={movie.id}
              title={movie.title}
              genre={movie.genre}
              rating={movie.rating}
              releaseYear={movie.year}
              onEdit={() => setEditMovieModal(movie)}
              onDelete={() => handleDeleteClick(movie.id)}
            />
          ))}
        </div>
      )}

      {/* ADD MOVIE MODAL */}
      {(isModalOpen || isClosing) && (
        <div
          className={`
            fixed inset-0 z-50 flex items-center justify-center
            bg-black/50 transition-opacity
            ${isModalOpen && !isClosing ? "opacity-100" : "opacity-0"}
          `}
          onClick={closeModal}
        >
          <div
            className="relative w-full max-w-lg"
            onClick={(e) => e.stopPropagation()}
          >
            <AddMovieForm
              onMovieAdded={handleMovieAdded}
              onClose={closeModal}
            />

            <button
              onClick={closeModal}
              className="
                absolute right-4 top-4
                text-gray-400 hover:text-gray-600
                focus:outline-none
              "
              aria-label="Close modal"
            >
              ✕
            </button>
          </div>
        </div>
      )}

      {/* EDIT MOVIE MODAL */}
      {editMovieModal && (
        <EditMovieModal
          movie={editMovieModal}
          onClose={() => setEditMovieModal(null)}
          onSuccess={handleUpdateMovie}
        />
      )}

      {/* DELETE CONFIRM MODAL */}
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
