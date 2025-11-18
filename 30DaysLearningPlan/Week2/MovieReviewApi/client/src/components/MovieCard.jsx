import React from "react";

const MovieCard = ({ title, genre, rating, releaseYear, onEdit, onDelete, deleting }) => {
  return (
    <div className="movie-card">
      <div className="movie-info">
        <h3>Title: {title || "Untitled"}</h3>
        <p>Genre: {genre || "Unknown genre"}</p>
        <p>Rating: ⭐ {rating ?? "N/A"}</p>
        <p>Release Year: 📅 {releaseYear || "—"}</p>
      </div>

      <div
        style={{
          display: "flex",
          justifyContent: "flex-end",
          gap: "0.5rem",
          padding: "0 0.75rem 0.75rem",
        }}
      >
        <button
          className="submit-btn"
          onClick={onEdit}
          style={{ padding: "0.25rem 0.5rem", fontSize: "0.85rem" }}
        >
          Edit
        </button>

        <button
          className="submit-btn"
          onClick={onDelete}
          disabled={deleting}
          style={{
            padding: "0.25rem 0.5rem",
            fontSize: "0.85rem",
            backgroundColor: deleting ? "#94a3b8" : "#e63946",
          }}
        >
          {deleting ? "Deleting…" : "Delete"}
        </button>
      </div>
    </div>
  );
};

export default MovieCard;