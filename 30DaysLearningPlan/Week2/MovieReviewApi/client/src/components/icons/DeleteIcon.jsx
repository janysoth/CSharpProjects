import React from "react";

export default function DeleteIcon({ onClick }) {
  return (
    <button className="icon-btn delete-icon" onClick={onClick}>
      🗑️
    </button>
  );
}