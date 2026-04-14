import React from "react";

const suggestions = [
    "Java Developer",
    "Frontend Developer",
    "Full Stack Developer",
    "Data Analyst",
    "Software Engineer",
];

const JobRoleInput = ({ jobRole, setJobRole }) => {
    return (
        <div className="mb-6">
            <input
                type="text"
                placeholder="Enter Job Role (e.g. Java Developer)"
                value={jobRole}
                onChange={(e) => setJobRole(e.target.value)}
                className="w-full p-3 border rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-400"
            />

            <div className="flex flex-wrap gap-2 mt-2">
                {suggestions.map((role, index) => (
                    <button
                        key={index}
                        onClick={() => setJobRole(role)}
                        className="px-3 py-1 text-sm bg-gray-100 hover:bg-blue-100 rounded-full transition"
                    >
                        {role}
                    </button>
                ))}
            </div>
        </div>
    );
};

export default JobRoleInput;