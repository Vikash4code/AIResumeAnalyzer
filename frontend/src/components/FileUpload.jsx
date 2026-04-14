import React, { useState } from "react";

const FileUpload = ({ setFile }) => {
    const [dragActive, setDragActive] = useState(false);
    const [fileName, setFileName] = useState("");

    const handleFile = (file) => {
        if (!file) return;

        const validTypes = [
            "application/pdf",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        ];

        if (!validTypes.includes(file.type)) {
            alert("Only PDF and DOCX files are allowed");
            return;
        }

        setFile(file);
        setFileName(`${file.name} (${(file.size / 1024).toFixed(1)} KB)`);
    };

    return (
        <div className="mb-6">
            <div
                className={`border-2 border-dashed rounded-xl p-6 text-center cursor-pointer transition ${dragActive
                        ? "border-blue-500 bg-blue-50"
                        : "border-gray-300"
                    }`}
                onDragOver={(e) => {
                    e.preventDefault();
                    setDragActive(true);
                }}
                onDragLeave={() => setDragActive(false)}
                onDrop={(e) => {
                    e.preventDefault();
                    setDragActive(false);
                    handleFile(e.dataTransfer.files[0]);
                }}
                onClick={() => document.getElementById("fileInput").click()}
            >
                <input
                    id="fileInput"
                    type="file"
                    accept=".pdf,.docx"
                    className="hidden"
                    onChange={(e) => handleFile(e.target.files[0])}
                />

                <p className="text-gray-600">
                    Drag & Drop your resume here or{" "}
                    <span className="text-blue-500 font-medium">Browse</span>
                </p>

                {fileName && (
                    <p className="mt-3 text-sm text-green-600 font-medium">
                        ✅ {fileName}
                    </p>
                )}
            </div>
        </div>
    );
};

export default FileUpload;