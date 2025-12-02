import React from "react";

const Button = ({ children, variant = "primary", className = "", ...props }) => {
  return (
    <button
      {...props}
      className={`btn btn-${variant} ${className}`}
    >
      {children}
    </button>
  );
};

export default Button;