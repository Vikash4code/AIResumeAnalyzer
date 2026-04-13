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