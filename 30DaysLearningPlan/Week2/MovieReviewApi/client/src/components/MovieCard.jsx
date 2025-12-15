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
    <div className="group flex items-start justify-between gap-4 rounded-lg border border-gray-200 bg-white p-4 shadow-sm transition hover:shadow-md focus-within:shadow-md">
      {/* MOVIE INFO */}
      <div className="space-y-1">
        <h3 className="text-lg font-semibold text-gray-900">
          Title: {title || "Untitled"}
        </h3>

        <p className="text-sm text-gray-600">
          Genre: {genre || "Unknown genre"}
        </p>

        <p className="text-sm text-gray-600">
          Rating: ⭐ {rating ?? "N/A"}
        </p>

        <p className="text-sm text-gray-600">
          Release Year: 📅 {releaseYear || "—"}
        </p>
      </div>

      {/* ICON CONTAINER */}
      <div
        className="
          flex items-center gap-3
          opacity-100 translate-x-0
          sm:opacity-0 sm:translate-x-2
          sm:group-hover:opacity-100 sm:group-hover:translate-x-0
          sm:group-focus-within:opacity-100 sm:group-focus-within:translate-x-0
          transition-all duration-200
        "
      >
        <EditIcon
          onClick={onEdit}
          className="cursor-pointer text-blue-600 hover:text-blue-800 focus:outline-none"
        />
        <DeleteIcon
          onClick={onDelete}
          className="cursor-pointer text-red-600 hover:text-red-800 focus:outline-none"
        />
      </div>
    </div>
  );
};

export default MovieCard;