import React from "react";

export default function EditIcon({ onClick }) {
  return (
    <button className="icon-btn edit-icon" onClick={onClick}>
      ✏️
    </button>
  );
}