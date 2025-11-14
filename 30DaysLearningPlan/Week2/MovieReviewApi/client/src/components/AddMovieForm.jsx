import React, { useState } from "react";
import api from "../services/api";
import FormInput from "./FormInput";

const AddMovieForm = ({ onMovieAdded }) => {
  const [formData, setFormData] = useState({
    title: "",
    genre: "",
    releaseYear: "",
    rating: "",
  });

  const [errors, setErrors] = useState({});
  const [success, setSuccess] = useState("");
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const validateForm = () => {
    const errors = {};
    if (!formData.title.trim()) errors.title = "Title is required.";
    if (!formData.genre.trim()) errors.genre = "Genre is required.";
    if (!formData.releaseYear) errors.releaseYear = "Release year is required.";
    if (!formData.rating || formData.rating < 1 || formData.rating > 10)
      errors.rating = "Rating must be between 1 and 10.";
    return errors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors({});
    setSuccess("");

    const validationErrors = validateForm();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    setLoading(true);

    // ✅ Convert numbers and handle empty strings
    const payload = {
      title: formData.title.trim() || null,
      genre: formData.genre.trim() || null,
      year: formData.releaseYear ? Number(formData.releaseYear) : null,
      rating: formData.rating ? Number(formData.rating) : null,
    };

    try {
      const res = await api.post("/add-movie", payload);
      setSuccess("Movie added successfully!");
      setFormData({ title: "", genre: "", releaseYear: "", rating: "" });

      if (onMovieAdded) onMovieAdded(res.data);
    } catch (err) {
      console.error("AxiosError:", err);
      const message =
        err.response?.data?.message || "Failed to add movie. Please try again.";
      setErrors({ general: message });
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="add-movie-form">
      {errors.general && <p className="error-text">{errors.general}</p>}
      {success && <p className="success-text">{success}</p>}

      <FormInput
        label="Title"
        name="title"
        value={formData.title}
        onChange={handleChange}
        error={errors.title}
      />
      <FormInput
        label="Genre"
        name="genre"
        value={formData.genre}
        onChange={handleChange}
        error={errors.genre}
      />
      <FormInput
        label="Release Year"
        name="releaseYear"
        type="number"
        value={formData.releaseYear}
        onChange={handleChange}
        error={errors.releaseYear}
      />
      <FormInput
        label="Rating (1–10)"
        name="rating"
        type="number"
        value={formData.rating}
        onChange={handleChange}
        error={errors.rating}
      />

      <button type="submit" className="submit-btn" disabled={loading}>
        {loading ? "Adding..." : "Add Movie"}
      </button>
    </form>
  );
};

export default AddMovieForm;