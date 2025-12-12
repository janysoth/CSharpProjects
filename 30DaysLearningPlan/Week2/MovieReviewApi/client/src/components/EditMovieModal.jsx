import React, { useEffect, useState } from "react";
import { updateMovie } from "../services/api";
import FormInput from "./FormInput";
import Button from "./buttons/Button";

export default function EditMovieModal({ movie, onClose, onSuccess }) {
  const [form, setForm] = useState({
    title: movie.title || "",
    genre: movie.genre || "",
    year: movie.year || "",
    rating: movie.rating || "",
  });

  const [saving, setSaving] = useState(false);
  const [errors, setErrors] = useState({});

  // -----------------------------
  // 1️⃣ ESC key to close
  // -----------------------------
  useEffect(() => {
    const handleEsc = (e) => {
      if (e.key === "Escape") onClose();
    };

    window.addEventListener("keydown", handleEsc);
    return () => window.removeEventListener("keydown", handleEsc);
  }, [onClose]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
    setErrors((prev) => ({ ...prev, [name]: null }));
  };

  const validate = () => {
    const newErrors = {};
    const currentYear = new Date().getFullYear();

    if (!form.title.trim()) newErrors.title = "Title is required";
    if (!form.genre.trim()) newErrors.genre = "Genre is required";
    if (form.year && (form.year < 1800 || form.year > currentYear))
      newErrors.year = `Enter a valid year (1800–${currentYear})`;
    if (form.rating && (form.rating < 0 || form.rating > 10))
      newErrors.rating = "Rating must be between 0 and 10";

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSave = async () => {
    if (!validate()) return;

    setSaving(true);
    try {
      const payload = {
        title: form.title,
        genre: form.genre,
        year: parseInt(form.year),
        rating: parseFloat(form.rating),
      };
      const response = await updateMovie(movie.id, payload);
      onSuccess(response.data.data);
      window.location.reload();
    } catch (err) {
      console.error(err);
      alert("Failed to save movie. Please try again.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <div
      className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-[999] animate-fadeIn"
      onClick={(e) => {
        if (e.target === e.currentTarget) onClose();
      }}
    >
      <div className="bg-white w-full max-w-md rounded-lg shadow-lg p-6 relative animate-scaleIn">
        {/* Close button */}
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-gray-500 hover:text-gray-700 text-2xl leading-none"
        >
          &times;
        </button>

        <h3 className="text-xl font-semibold mb-4">Edit Movie</h3>

        <form className="space-y-4">
          <FormInput
            label="Title"
            name="title"
            value={form.title}
            onChange={handleChange}
            error={errors.title}
          />

          <FormInput
            label="Genre"
            name="genre"
            value={form.genre}
            onChange={handleChange}
            error={errors.genre}
          />

          <FormInput
            label="Year"
            name="year"
            type="number"
            value={form.year}
            onChange={handleChange}
            error={errors.year}
          />

          <FormInput
            label="Rating"
            name="rating"
            type="number"
            value={form.rating}
            onChange={handleChange}
            error={errors.rating}
          />

          {/* Buttons (matches AddMovieForm) */}
          <div className="flex justify-between items-center gap-4 pt-4">
            <Button
              type="button"
              variant="danger"
              onClick={onClose}
              disabled={saving}
              className="w-full"
            >
              Cancel
            </Button>

            <Button
              type="button"
              onClick={handleSave}
              disabled={saving}
              className="w-full"
            >
              {saving ? "Saving…" : "Save"}
            </Button>
          </div>
        </form>
      </div>
    </div>
  );
}