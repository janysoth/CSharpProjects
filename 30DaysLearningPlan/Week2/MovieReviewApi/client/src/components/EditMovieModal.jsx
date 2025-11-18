import React, { useState } from "react";
import { updateMovie } from "../services/api";
import FormInput from "./FormInput";

export default function EditMovieModal({ movie, onClose, onSuccess }) {
  const [form, setForm] = useState({
    title: movie.title || "",
    genre: movie.genre || "",
    year: movie.year || "",
    rating: movie.rating || "",
  });

  const [saving, setSaving] = useState(false);
  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm(prev => ({ ...prev, [name]: value }));
    setErrors(prev => ({ ...prev, [name]: null }));
  };

  const validate = () => {
    const newErrors = {};
    if (!form.title.trim()) newErrors.title = "Title is required";
    if (!form.genre.trim()) newErrors.genre = "Genre is required";
    if (form.year && (form.year < 1800 || form.year > new Date().getFullYear()))
      newErrors.year = "Enter a valid year";
    if (form.rating && (form.rating < 0 || form.rating > 10))
      newErrors.rating = "Rating must be 0–10";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSave = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    setSaving(true);
    try {
      const payload = {
        title: form.title,
        genre: form.genre,
        year: parseInt(form.year),
        rating: parseFloat(form.rating),
      };

      const res = await updateMovie(movie.id, payload);
      onSuccess(res.data.data);
    } catch (err) {
      console.error(err);
      alert("Failed to save movie. Try again.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="modal-overlay open">
      <div className="modal-content">
        <button className="modal-close" onClick={onClose}>
          &times;
        </button>

        <h3>Edit Movie</h3>
        <form>
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

          <div style={{ marginTop: 16, display: "flex", justifyContent: "flex-end", gap: 10 }}>
            <button type="button" className="submit-btn" onClick={onClose} disabled={saving}>
              Cancel
            </button>
            <button type="button" className="submit-btn" onClick={handleSave} disabled={saving}>
              {saving ? "Saving…" : "Save"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}