// src/components/FormInput.jsx
export default function FormInput({
  label,
  name,
  value,
  onChange,
  error,
  type = "text",
}) {
  return (
    <div className="flex flex-col gap-1">
      <label
        htmlFor={name}
        className="text-sm font-medium text-gray-700"
      >
        {label}
      </label>

      <input
        type={type}
        id={name}
        name={name}
        value={value}
        onChange={onChange}
        className={`
          w-full rounded-md px-3 py-2 border text-sm transition
          focus:outline-none focus:ring-2 
          ${error
            ? "border-red-500 focus:ring-red-400 bg-red-50"
            : "border-gray-300 focus:ring-blue-400 bg-white"
          }
        `}
      />

      {error && (
        <p className="text-red-600 text-xs">
          {error}
        </p>
      )}
    </div>
  );
}