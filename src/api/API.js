import axios from "axios";

export const API = axios.create({
  baseURL: "https://localhost:7171/api/produtos",
});
//https://localhost:7076/api