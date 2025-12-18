// src/components/AddMovieForm.jsx
import React, { useEffect, useState } from "react";
import api from "../services/api";
import FormInput from "./FormInput";
import Button from "./buttons/Button";

const AddMovieForm = ({ onMovieAdded, onClose }) => {
  const [formData, setFormData] = useState({
    title: "",
    genre: "",
    releaseYear: "",
    rating: ""
  });
  const [errors, setErrors] = useState({});
  const [success, setSuccess] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  // ESC key closes form
  useEffect(() => {
    const handleEsc = (e) => {
      if (e.key === "Escape") onClose?.();
    };
    window.addEventListener("keydown", handleEsc);
    return () => window.removeEventListener("keydown", handleEsc);
  }, [onClose]);

  const validateForm = () => {
    const errors = {};
    if (!formData.title) errors.title = "Title is required.";
    if (!formData.genre) errors.genre = "Genre is required.";
    if (!formData.releaseYear) errors.releaseYear = "Release year is required.";
    if (formData.rating < 1 || formData.rating > 10)
      errors.rating = "Rating must be between 1 and 10.";
    return errors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const validationErrors = validateForm();

    if (Object.keys(validationErrors).length) {
      setErrors(validationErrors);
      return;
    }

    setErrors({});
    try {
      await api.post("/add-movie", {
        title: formData.title,
        genre: formData.genre,
        year: Number(formData.releaseYear),
        rating: Number(formData.rating)
      });

      setSuccess("Movie added successfully!");
      setFormData({ title: "", genre: "", releaseYear: "", rating: "" });
      onMovieAdded?.();
    } catch {
      setSuccess("");
      setErrors({ form: "Failed to add movie. Please try again." });
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="
        w-full max-w-lg mx-auto
        bg-white
        rounded-xl
        shadow-lg
        border border-gray-100
        p-6 sm:p-8
        space-y-6
      "
    >
      {/* Header */}
      <div className="space-y-1">
        <h2 className="text-xl font-semibold text-gray-800">
          Add New Movie
        </h2>
        <p className="text-sm text-gray-500">
          Enter movie details below
        </p>
      </div>

      <div className="h-px bg-gray-200" />

      {/* Form-level Error */}
      {errors.form && (
        <div className="rounded-md bg-red-50 border border-red-200 px-4 py-3 text-sm text-red-700">
          {errors.form}
        </div>
      )}

      {/* Success Message */}
      {success && (
        <div className="rounded-md bg-green-50 border border-green-200 px-4 py-3 text-sm text-green-700">
          {success}
        </div>
      )}

      {/* Inputs */}
      <div className="grid grid-cols-1 gap-5">
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

        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
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
        </div>
      </div>

      {/* Buttons */}
      <div className="flex gap-3 pt-4">
        <Button
          type="button"
          variant="danger"
          onClick={onClose}
          className="flex-1"
        >
          Cancel
        </Button>

        <Button
          type="submit"
          className="flex-1"
        >
          Add Movie
        </Button>
      </div>
    </form>
  );
};

export default AddMovieForm;
