// src/layouts/Layout.jsx
import { Link, Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <>
      <nav>
        <h2>🎬 Movie Review</h2>
        <div>
          <Link to="/">Home</Link>
          <Link to="/movies">Movies</Link>
          <Link to="/about">About</Link>
        </div>
      </nav>

      <main>
        <Outlet />
      </main>
    </>
  );
}