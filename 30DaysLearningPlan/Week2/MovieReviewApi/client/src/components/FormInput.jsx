// src/components/FormInput.jsx
import React from "react";

const FormInput = ({ label, name, value, onChange, error, type = "text" }) => {
  return (
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <input
        type={type}
        id={name}
        name={name}          // ✅ Must match formData key
        value={value}        // ✅ Controlled value from parent
        onChange={onChange}  // ✅ Updates parent state
        className={`input ${error ? "input-error" : ""}`}
      />
      {error && <p className="error-text">{error}</p>}
    </div>
  );
};

export default FormInput;