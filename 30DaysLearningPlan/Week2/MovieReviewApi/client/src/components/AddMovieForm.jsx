import React, { useState } from "react";
import FormInput from "./FormInput";

const AddMovieForm = () => {
  const [formData, setFormData] = useState({
    title: "",
    genre: "",
    releaseYear: "",
    rating: ""
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const validateForm = () => {
    const errors = {};
    if (!formData.title) errors.title = "Title is required.";
    if (!formData.genre) errors.genre = "Genre is required.";
    if (!formData.releaseYear) errors.releaseYear = "Release year is required.";
    if (formData.rating < 1 || formData.rating > 10)
      errors.rating = "Rating must be between 1 and 10.";
    return errors;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const validationErrors = validateForm();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }
    console.log("Form Submitted:", formData);
  };

  return (
    <form onSubmit={handleSubmit} className="add-movie-form">
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

      <button type="submit" className="submit-btn">
        Add Movie
      </button>
    </form>
  );
};

export default AddMovieForm;