import React from "react";

export default function ConfirmDeleteModal({ movieTitle, onCancel, onConfirm, loading }) {
  return (
    <div className="modal-overlay open">
      <div className="modal-content">
        <h3>Confirm Delete</h3>
        <p>Are you sure you want to delete <strong>{movieTitle}</strong>?</p>

        <div style={{ display: "flex", justifyContent: "flex-end", gap: "0.5rem", marginTop: "1rem" }}>
          <button
            className="submit-btn"
            onClick={onCancel}
            disabled={loading}
            style={{ backgroundColor: "#94a3b8" }}
          >
            Cancel
          </button>

          <button
            className="submit-btn"
            onClick={onConfirm}
            disabled={loading}
            style={{ backgroundColor: "#e63946" }}
          >
            {loading ? "Deleting…" : "Delete"}
          </button>
        </div>
      </div>
    </div>
  );
}