import React, { useState } from "react";
import { updateMovie } from "../services/api";
import FormInput from "./FormInput";

/**
 * EditMovieModal Component
 * ------------------------
 * Allows the user to edit an existing movie.
 * Validates form input, shows errors, and updates the movie on save.
 */
export default function EditMovieModal({ movie, onClose, onSuccess }) {

  // --------------------------------------------
  // 1️⃣ Form State
  // --------------------------------------------
  const [form, setForm] = useState({
    title: movie.title || "",
    genre: movie.genre || "",
    year: movie.year || "",
    rating: movie.rating || "",
  });

  // Track saving progress + validation errors
  const [saving, setSaving] = useState(false);
  const [errors, setErrors] = useState({});

  // --------------------------------------------
  // 2️⃣ Handle Input Change
  // --------------------------------------------
  const handleChange = (e) => {
    const { name, value } = e.target;

    // Update field value
    setForm((prev) => ({ ...prev, [name]: value }));

    // Clear error message for that field
    setErrors((prev) => ({ ...prev, [name]: null }));
  };

  // --------------------------------------------
  // 3️⃣ Validation Logic
  // --------------------------------------------
  const validate = () => {
    const newErrors = {};

    if (!form.title.trim()) newErrors.title = "Title is required";
    if (!form.genre.trim()) newErrors.genre = "Genre is required";

    const currentYear = new Date().getFullYear();

    if (form.year && (form.year < 1800 || form.year > currentYear)) {
      newErrors.year = `Enter a valid year (1800-${currentYear})`;
    }

    if (form.rating && (form.rating < 0 || form.rating > 10)) {
      newErrors.rating = "Rating must be between 0 and 10";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0; // True if no errors
  };

  // --------------------------------------------
  // 4️⃣ Save Movie Handler
  // --------------------------------------------
  const handleSave = async (e) => {
    e.preventDefault();

    // Stop if validation fails
    if (!validate()) return;

    setSaving(true);

    try {
      // Build API payload
      const payload = {
        title: form.title,
        genre: form.genre,
        year: parseInt(form.year),
        rating: parseFloat(form.rating),
      };

      // Send update request
      const response = await updateMovie(movie.id, payload);

      // Pass updated movie back to parent
      onSuccess(response.data.data);

      // Temporary: refresh UI (you can remove later)
      window.location.reload();

    } catch (err) {
      console.error(err);
      alert("Failed to save movie. Please try again.");
    } finally {
      setSaving(false);
    }
  };

  // --------------------------------------------
  // 5️⃣ UI Rendering
  // --------------------------------------------
  return (
    <div className="modal-overlay open">
      <div className="modal-content">

        {/* Close button */}
        <button className="modal-close" onClick={onClose}>
          &times;
        </button>

        <h3>Edit Movie</h3>

        <form>

          {/* Title */}
          <FormInput
            label="Title"
            name="title"
            value={form.title}
            onChange={handleChange}
            error={errors.title}
          />

          {/* Genre */}
          <FormInput
            label="Genre"
            name="genre"
            value={form.genre}
            onChange={handleChange}
            error={errors.genre}
          />

          {/* Year */}
          <FormInput
            label="Year"
            name="year"
            type="number"
            value={form.year}
            onChange={handleChange}
            error={errors.year}
          />

          {/* Rating */}
          <FormInput
            label="Rating"
            name="rating"
            type="number"
            value={form.rating}
            onChange={handleChange}
            error={errors.rating}
          />

          {/* Buttons */}
          <div
            style={{
              marginTop: 16,
              display: "flex",
              justifyContent: "flex-end",
              gap: 10,
            }}
          >
            <button
              type="button"
              className="submit-btn"
              onClick={onClose}
              disabled={saving}
            >
              Cancel
            </button>

            <button
              type="button"
              className="submit-btn"
              onClick={handleSave}
              disabled={saving}
            >
              {saving ? "Saving…" : "Save"}
            </button>
          </div>

        </form>

      </div>
    </div>
  );
}