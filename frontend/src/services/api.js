import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7175",
});

export default api;
