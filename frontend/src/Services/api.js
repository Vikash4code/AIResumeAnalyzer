import axios from "axios";

const API = axios.create({
    baseURL: "http://localhost:5070",
});

export const analyzeResume = async (file, jobRole) => {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("jobRole", jobRole);

    const response = await API.post("/api/resume/analyze", formData);
    return response.data;
};
const BASE_URL = import.meta.env.VITE_API_BASE_URL;

if (!BASE_URL) {
    throw new Error("VITE_API_BASE_URL is not defined");
}

export default BASE_URL;