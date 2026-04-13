import React from "react";

const ResultCard = ({ result }) => {
    if (!result) return null;

    const scoreColor =
        result.score > 75
            ? "bg-green-500"
            : result.score > 50
                ? "bg-yellow-500"
                : "bg-red-500";

    return (
        <div className="mt-8 p-6 bg-white rounded-2xl shadow-lg space-y-6">
            {/* ATS Score */}
            <div>
                <h2 className="text-xl font-semibold mb-2">ATS Score</h2>
                <div className="w-full bg-gray-200 rounded-full h-4">
                    <div
                        className={`${scoreColor} h-4 rounded-full transition-all duration-500`}
                        style={{ width: `${result.score}%` }}
                    ></div>
                </div>
                <p className="mt-2 font-medium">{result.score}%</p>
            </div>

            {/* Matched Skills */}
            <div>
                <h3 className="font-semibold mb-2 text-green-600">
                    Matched Skills
                </h3>
                <div className="flex flex-wrap gap-2">
                    {result.matchedSkills.map((skill, i) => (
                        <span
                            key={i}
                            className="bg-green-100 text-green-700 px-3 py-1 rounded-full text-sm"
                        >
                            {skill}
                        </span>
                    ))}
                </div>
            </div>

            {/* Missing Skills */}
            <div>
                <h3 className="font-semibold mb-2 text-red-600">
                    Missing Skills
                </h3>
                <div className="flex flex-wrap gap-2">
                    {result.missingSkills.map((skill, i) => (
                        <span
                            key={i}
                            className="bg-red-100 text-red-700 px-3 py-1 rounded-full text-sm"
                        >
                            {skill}
                        </span>
                    ))}
                </div>
            </div>

            {/* Suggestions */}
            <div>
                <h3 className="font-semibold mb-2 text-blue-600">
                    Suggestions
                </h3>
                <ul className="space-y-2">
                    {result.suggestions.map((s, i) => (
                        <li
                            key={i}
                            className="bg-blue-50 p-3 rounded-lg text-sm"
                        >
                            {s}
                        </li>
                    ))}
                </ul>
            </div>

            {/* Summary */}
            <div className="p-4 bg-gray-50 rounded-lg text-gray-700">
                {result.summaryFeedback}
            </div>
        </div>
    );
};

export default ResultCard;