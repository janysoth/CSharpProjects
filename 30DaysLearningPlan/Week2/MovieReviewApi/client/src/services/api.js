import axios from "axios";

const API = axios.create({
  baseURL: "http://localhost:5262/api/movies",
  headers: { "Content-Type": "application/json" },
});

// =======================
// Get All Moves
// =======================
export const getAllMovies = () => API.get("/get-all-movies");

// =======================
// PUT (Full Update)
// =======================
export const updateMovie = (id, movieData) =>
  API.put(`/update-movie/${id}`, movieData);

// =======================
// PATCH (Partial Update)
// =======================
export const patchMovie = (id, patchDoc) =>
  API.patch(`/patch-movie/${id}`, patchDoc);

// =======================
// DELETE
// =======================
export const deleteMovie = (id) =>
  API.delete(`/delete-movie/${id}`);

export default API;