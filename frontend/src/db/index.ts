import axios from "axios";

// Browser'da çalışırken localhost'a erişebilmesi için URL'i düzenliyoruz
const isDevelopment = process.env.NODE_ENV === "development";
const apiUrl =
  process.env.NEXT_PUBLIC_API_URL ||
  (isDevelopment ? "http://localhost:5000/todo" : "http://backend:5000/todo");

export const db = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
  },
});
