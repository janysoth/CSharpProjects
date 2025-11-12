// src/App.jsx
import { Route, Routes } from "react-router-dom";
import MovieList from "./components/MovieList";
import "./index.css";
import Layout from "./layouts/Layout";
import About from "./pages/About";
import Home from "./pages/Home";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="movies" element={<MovieList />} />
        <Route path="about" element={<About />} />
      </Route>
    </Routes>
  );
}