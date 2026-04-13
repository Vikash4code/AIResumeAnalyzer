import React, { useState } from "react";
import FileUpload from "./components/FileUpload";
import JobRoleInput from "./components/JobRoleInput";
import ResultCard from "./components/ResultCard";
import { analyzeResume } from "./services/api";

function App() {
    const [file, setFile] = useState(null);
    const [jobRole, setJobRole] = useState("");
    const [result, setResult] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const handleAnalyze = async () => {
        if (!file || !jobRole) {
            setError("Please upload file and enter job role");
            return;
        }

        try {
            setError("");
            setLoading(true);
            const data = await analyzeResume(file, jobRole);
            setResult(data);
        } catch (err) {
            setError("Error analyzing resume");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 to-gray-100 flex items-center justify-center p-4">
            <div className="w-full max-w-2xl bg-white rounded-2xl shadow-xl p-6">
                <h1 className="text-3xl font-bold text-center mb-6">
                    AI Resume Analyzer
                </h1>

                <FileUpload setFile={setFile} />
                <JobRoleInput jobRole={jobRole} setJobRole={setJobRole} />

                {error && (
                    <p className="text-red-500 text-sm mb-4">{error}</p>
                )}

                <button
                    onClick={handleAnalyze}
                    disabled={loading}
                    className={`w-full py-3 rounded-xl text-white font-medium transition ${loading
                            ? "bg-gray-400 cursor-not-allowed"
                            : "bg-blue-600 hover:bg-blue-700"
                        }`}
                >
                    {loading ? "Analyzing..." : "Analyze Resume"}
                </button>

                <ResultCard result={result} />
            </div>
        </div>
    );
}

export default App;