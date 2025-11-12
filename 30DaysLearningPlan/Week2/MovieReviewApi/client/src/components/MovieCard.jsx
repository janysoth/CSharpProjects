import React from 'react';

const MovieCard = ({
  title,
  genre,
  rating,
  releaseYear,
}) => {

  return (
    <div className="movie-card">
      <div className="movie-info">
        <h3>Title: {title || "Untitled"}</h3>
        <p>Genre: {genre || "Unknown genre"}</p>
        <p>Rating: ⭐ {rating ?? "N/A"}</p>
        <p>Release Year: 📅 {releaseYear || "—"}</p>
      </div>
    </div>
  );
};

export default MovieCard;