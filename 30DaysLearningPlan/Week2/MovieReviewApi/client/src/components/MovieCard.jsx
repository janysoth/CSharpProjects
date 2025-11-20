import React from "react";
import DeleteIcon from "./icons/DeleteIcon";
import EditIcon from "./icons/EditIcon";

const MovieCard = ({
  title,
  genre,
  rating,
  releaseYear,
  onEdit,
  onDelete,
}) => {
  return (
    <div className="movie-card">
      <div className="movie-info">
        <h3>Title: {title || "Untitled"}</h3>
        <p>Genre: {genre || "Unknown genre"}</p>
        <p>Rating: ⭐ {rating ?? "N/A"}</p>
        <p>Release Year: 📅 {releaseYear || "—"}</p>
      </div>

      {/* ICON CONTAINER */}
      <div className="movie-card-actions">
        <EditIcon onClick={onEdit} />
        <DeleteIcon onClick={onDelete} />
      </div>
    </div>
  );
};

export default MovieCard;