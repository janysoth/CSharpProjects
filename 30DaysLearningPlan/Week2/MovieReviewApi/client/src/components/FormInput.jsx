import React from 'react';

const FormInput = ({
  label,
  inputName,
  value,
  onChange,
  type = "text",
  error
}) => (
  <div className="form-group">
    <label>{label}</label>
    <input
      type={type}
      name={inputName}
      value={value}
      onChange={onChange}
      className={`input ${error ? "input-error" : ""}`}
    />
    {error && <p className='error-text'>{error}</p>}
  </div>
);

export default FormInput;