import axios from "axios";

// Browser-side URL vs. Server-side URL configuration
const isBrowser = typeof window !== "undefined";

const apiUrl = isBrowser
  ? // In browser, use localhost with the exposed port
    "http://localhost:5000/todo"
  : // In server-side (Next.js SSR), use the Docker service name
    "http://backend:8080/todo";

export const db = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
  },
});
