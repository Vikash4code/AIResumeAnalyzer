import axios from "axios";

const BASE_URL =
    import.meta.env.VITE_API_BASE_URL ||
    "https://airesumeanalyzer-backend-nn30.onrender.com";

const API = axios.create({
    baseURL: BASE_URL,
});

export const analyzeResume = async (file, jobRole) => {
    const formData = new FormData();
    formData.append("file", file);
    formData.append("jobRole", jobRole);

    const response = await API.post("/api/resume/analyze", formData);
    return response.data;
};